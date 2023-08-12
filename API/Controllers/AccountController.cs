using API.Dtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseApiController
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ITokenProvider _tokenProvider;
        public AccountController( IMapper mapper ,ITokenProvider tokenProvider , UserManager<AppUser> userManager , SignInManager<AppUser> signInManager)
        {
            _mapper = mapper;
            _tokenProvider = tokenProvider;
            _userManager = userManager;
            _signInManager = signInManager;

        }
        [HttpPost("login")]

        public async Task<ActionResult<UserDto>> Login(LoginDto login)
        {
            var FoundUser = await _userManager.FindByEmailAsync(login.Email);
            if (FoundUser is null)
            {
                return Unauthorized(new ApiResponse(401 , "You Are Not Authorized"));
            }

            var passwordCheck = await _signInManager.CheckPasswordSignInAsync(FoundUser, login.Password,false);
            if (!passwordCheck.Succeeded) { return Unauthorized(new ApiResponse(401, "You Are Not Authorized")); }

            return Ok(new UserDto
            {
                DisplayName = FoundUser.DisplayName,
                Email = FoundUser.Email,
                Token =  _tokenProvider.CreateToken(FoundUser)
            });
        }

        [HttpPost("register")]

        public async Task<ActionResult<UserDto>> register(RegisterDto register)
        {
            var newUser = new AppUser
            {
                DisplayName = register.DisplayName,
                UserName= register.Email,
                Email = register.Email,
            };

             await _userManager.AddPasswordAsync(newUser, register.Password);

            var createdUser = await _userManager.CreateAsync(newUser);
            if (!createdUser.Succeeded) { return BadRequest(new ApiResponse(400, "You Have made a bad request")); }

            return Ok(new UserDto
            {
                DisplayName = newUser.DisplayName,
                Email = newUser.Email,
                Token = _tokenProvider.CreateToken(newUser)
            });
        }

        [HttpGet("testJWT")]
        [Authorize]
        public ActionResult<string> getUser()
        {
            return Ok("Super sevceret kegt");
        }

        [HttpGet("GetUser")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var FoundUser = await _userManager.GetUserByClaims(User);
            return Ok(new UserDto
            {
                DisplayName=FoundUser.DisplayName,
                Email =FoundUser.Email,
                Token = _tokenProvider.CreateToken(FoundUser)
            });
        }

        [HttpGet("Address")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> GetAddress()
        {
            var FoundUser = await _userManager.GetUserWithAddressByClaims(User);
            return _mapper.Map<AddressDto>(FoundUser.Address) ;
        }

        [HttpPut("updateuser")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> UpdateUser(AddressDto address)
        {
            var FoundUser = await _userManager.GetUserWithAddressByClaims(User);
            var newaddress = _mapper.Map<Address>(FoundUser.Address);
            FoundUser.Address = newaddress;

            var updatedUser = await _userManager.UpdateAsync(FoundUser);

            if (!updatedUser.Succeeded) { return BadRequest(new ApiResponse(400, "Bad request was recieved while updating the user")); }

            
            return Ok(address);
        }

        [HttpGet("emailexists")]
        [Authorize]
        public async Task<ActionResult<bool>> CheckEmail([FromQuery]string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }
    }
}
