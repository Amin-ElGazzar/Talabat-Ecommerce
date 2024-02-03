using AutoMapper;
using Ecommerce.Domain.Entities.Identity;
using Ecommerce.Domain.IServices;
using Ecommerce.Dtos;
using Ecommerce.Errors;
using Ecommerce.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Security.Claims;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ITokenServices tokenServices;
        private readonly IMapper mapper;

        public AccountsController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,ITokenServices tokenServices,IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenServices = tokenServices;
            this.mapper = mapper;
        }
        [HttpPost("login")]
        public async Task<ActionResult<UsersDto>> LoginAsync(LoginDto login)
        {
            var user =await userManager.FindByEmailAsync(login.Email);
            if (user == null) return Unauthorized(new ApiResponse(401));
            var result =  await signInManager.CheckPasswordSignInAsync(user, login.Password,false);
            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

            return Ok(new UsersDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await tokenServices.CreateTokenAsync(user, userManager)
            }) ;
            
           
        }
        [HttpPost("register")]
        public async Task<ActionResult<UsersDto>> RegisterAsync(RegisterDto register)
        {
            if (CheckEmailExests(register.Email).Result.Value)
                return BadRequest(new ApiResponseValidationErrors()
                {
                    Errors = new string[] {"Email already exist"}
                });
            var user = new ApplicationUser()
            {
                DisplayName = register.DisplayName,
                Email = register.Email,
                PhoneNumber = register.PhoneNumber,
                UserName =register.Email.Split('@')[0],
            };
            var result= await userManager.CreateAsync(user,register.Password);
            if (!result.Succeeded) return BadRequest(new ApiResponse(400));
                return Ok(new UsersDto()
            {
                DisplayName= user.DisplayName,
                Email = user.Email,
                Token  = await tokenServices.CreateTokenAsync(user, userManager)
                });
        }

        
        [HttpGet("currentUser")]
        [Authorize]
        public async Task<ActionResult<UsersDto>> GetCurrentUserAsync()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
          var  user = await userManager.FindByEmailAsync(Email);
            return Ok(new UsersDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await tokenServices.CreateTokenAsync(user, userManager)
            });
        }

        [HttpGet("GetUserAddress")]
        [Authorize]
        public async Task<ActionResult<UserWithAddressDto>> GetUserAddressAsync()
        {
            var user =await userManager.GetUserWithAddress(User);
            var address = mapper.Map<Address, UserWithAddressDto>(user.Address);
            return Ok(address);
        }
        [HttpPut("UpdateUserAddress")]
        [Authorize]
        public async Task<ActionResult<UserWithAddressDto>> UpdateUserAsync(UserWithAddressDto userAddress)
        {
            var address =  mapper.Map<UserWithAddressDto, Address>(userAddress);
            var user = await userManager.GetUserWithAddress(User);
            address.Id=user.Address.Id;
            user.Address = address;
            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded) return BadRequest(new ApiResponse(400));
            return Ok(userAddress);

        }

        [HttpGet("emailExist")]
        public async Task<ActionResult<bool>> CheckEmailExests(string email)
        {
           return userManager.FindByEmailAsync(email) is not null;
        }
    }
}
