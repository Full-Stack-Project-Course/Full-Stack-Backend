using API.Helpers;
using API.Middlewares;
using Core.Identity;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Microsoft.AspNetCore.Authentication;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



#region Configuring DBs
builder.Services.AddDbContext<StoreContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});



builder.Services.AddDbContext<AppIdentityDbContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("IdentityConnection"));
});



builder.Services.AddIdentityCore<AppUser>(opt =>
{

})
.AddEntityFrameworkStores<AppIdentityDbContext>()
.AddSignInManager<SignInManager<AppUser>>();

var jwtSection = builder.Configuration.GetSection("Token");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["key"])),
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidIssuer = jwtSection["issuer"],
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddSingleton<IConnectionMultiplexer>(opt =>
{
    var redisConnection = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("redis"));
    return ConnectionMultiplexer.Connect(redisConnection);
});

#endregion

#region Registering services
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ITokenProvider, TokenProvider>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork , UnitOfWork>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();


builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);


#endregion


builder.Services.AddCors(opt =>
{
    opt.AddPolicy("corsPolicy", policy => policy.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
});
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseStaticFiles();


using var scope = app.Services.CreateScope();

var context = scope.ServiceProvider.GetRequiredService<StoreContext>();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
var IdentityContext = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();
var UserManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

try
{
    await context.Database.MigrateAsync();
    await IdentityContext.Database.MigrateAsync();
    await StoreContextSeed.SeedDataAsync(context);
    await AppIdentityDbContextSeed.SeedDataAsync(UserManager);
}
catch (Exception ex)
{

    logger.LogError(ex , "error while migrating");
}

app.UseExceptionHandler("/error/500");


app.UseMiddleware<ExceptionMiddleware>();
app.UseCors("corsPolicy");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseStatusCodePagesWithReExecute("/error/{0}");
app.UseAuthorization();

app.MapControllers();

app.Run();
