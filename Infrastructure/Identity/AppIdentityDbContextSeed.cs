using Core.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
     
        public static async Task SeedDataAsync(UserManager<AppUser> _userManager)
        {
            if(! await _userManager.Users.AnyAsync()  )  {
                var user = new AppUser
                {
                    DisplayName = "mahmoudv2020",
                    UserName = "mahmoudv2020",
                    Email="mahmoudv2012@gmail.com",
                    
                    Address = new Address
                    {
                        FirstName = "mahmoud",
                        LastName = "hesham",
                        City = "portsaid",
                        State = "Egypt",
                        Street = "Mohammed Ali St.",
                        ZipCode = 151651
                    }
                };

                await _userManager.CreateAsync(user, "Vcut2020@");
            }
        }
    }
}
