using AutoMapper;
using GAM33.Dtos;
using GAM33.Exceptions;
using GAM33.Helpers;
using Gma33.Core.Entites.IdentityEntites;
using Gma33.Core.Interfaces.IdentityServicesInterfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace GAM33.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        #region constructor 
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IToken _token;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AccountsController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IToken token,
            IConfiguration configuration,
            IMapper mapper
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _token = token;
            _configuration = configuration;
            _mapper = mapper;
        }
        #endregion

        #region Login EndPoint

        [HttpPost("Login")]

        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null) return BadRequest("Invalid Login");

            var PasswordCheck = await _signInManager.CheckPasswordSignInAsync(user, model.Password, true);

            if (!PasswordCheck.Succeeded) return BadRequest("Invalid Login");

            var returnedValue = new UserDto()
            {
                Email = user.Email!,
                DisplayName = user.DisplayName,
                Token = await _token.GetTokenAsync(user),
            };
            return Ok(returnedValue);

        }
        #endregion

        #region Register EndPoint

        [HttpPost("Register")]

        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            try
            {
                if (CheckEmailIsExistAsync(model.Email).Result.Value)
                {
                    return BadRequest("Invalid User");
                }
                var user = new ApplicationUser()
                {
                    Email = model.Email,
                    DisplayName = model.DisplayName,
                    UserName = model.Email.Split('@')[0],
                    PhoneNumber = model.PhoneNumber,

                };

                var CreateUser = await _userManager.CreateAsync(user, model.Password);

                if (!CreateUser.Succeeded) throw new ValidationException(CreateUser.Errors.Select(E => E.Description));

                var returnedValue = new UserDto()
                {

                    DisplayName = model.DisplayName,
                    Email = model.Email,
                    Token = await _token.GetTokenAsync(user)
                };

                return Ok(returnedValue);

            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = "An error occurred during registration", Details = ex.Message });
            }


        }
        #endregion

        #region Forgot Password EndPoint
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);

            if (user == null) return BadRequest("Invalid Data");

            var PasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (string.IsNullOrEmpty(PasswordToken)) return BadRequest("Invalid Data");

            var callbackUrl = $"{_configuration["BaseApiUrl"]}/Accounts/ResetPassword?code={PasswordToken}&email={user.Email}";

            return Ok(new
            {

                email = user.Email,

                token = PasswordToken


            });


        }
        #endregion

        #region Reset Password EndPoint

        [HttpPost("ResetPassword")]

        public async Task<ActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null) return BadRequest("Invalid Data");

            var Result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);

            if (Result.Succeeded) return Ok("Password Reset Successful");
            return BadRequest("Invalid Data");
        }
        #endregion

        #region Get Current User EndPoint

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetCurrentUser")]

        public async Task<ActionResult> GetCurrentUser()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);

            if (Email is null) return BadRequest("Invalid user");

            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null) return BadRequest("Invalid user");

            var ReturnedValue = new
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
            };

            return Ok(ReturnedValue);

        }
        #endregion

        #region Get Current User Address EndPoint

        [HttpGet("GetCurrentUserAddress")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]


        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
        {
            var user = await _userManager.FindEamilWithAddressAsync(User);
            var MappAddress = _mapper.Map<Address, AddressDto>(user.Address);
            return Ok(MappAddress);

        }
        #endregion

        #region Update Current User Address EndPoint
        [HttpPost("UpdateAddress")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto addressDto)
        {
            var user = await _userManager.FindEamilWithAddressAsync(User);

            if (user == null)
                return Unauthorized();

            var newAddress = _mapper.Map<Address>(addressDto);

            // Check first before accessing user.Address.Id
            if (user.Address != null)
            {
                newAddress.Id = user.Address.Id;
            }

            newAddress.UserId = user.Id;
            user.Address = newAddress;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return BadRequest("Invalid Data");

            return Ok(addressDto);
        }

        #endregion

        #region Check Email is Exist EndPoint

        [HttpGet("IsEmailExist")]

        public async Task<ActionResult<bool>> CheckEmailIsExistAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                return false;
            }
            return true;
        } 
        #endregion



    }
}
