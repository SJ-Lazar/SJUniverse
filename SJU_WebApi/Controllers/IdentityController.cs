using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SJU_WebApi.Services.Email;
using SJU_WebApi.Services.Token;
using SJU_WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace SJU_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IJWTTokenGenerator _jwtToken;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly IEmailSender _emailSender;

        public IdentityController(
            UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager, 
            IJWTTokenGenerator jwtToken,
            RoleManager<IdentityRole> roleManager,
            IConfiguration config,
            IEmailSender emailSender)
        {
            _jwtToken = jwtToken;
            _config = config;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
           
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var userfromDb = await _userManager.FindByNameAsync(model.Username);

            if(userfromDb == null)
            {
                return BadRequest();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(userfromDb, model.Password, false);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            var roles = await _userManager.GetRolesAsync(userfromDb);

            IList<Claim> claims = await _userManager.GetClaimsAsync(userfromDb);
            return Ok(new
            {
                result = result,
                username = userfromDb.UserName,
                email = userfromDb.Email,
                token = _jwtToken.GenerateToken(userfromDb, roles, claims)
            }) ;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if(!(await _roleManager.RoleExistsAsync(model.Role)))
            {
                await _roleManager.CreateAsync(new IdentityRole(model.Role));
            }


            var userToCreate = new IdentityUser
            {
                Email = model.Email,
                UserName = model.Username,
            };

            var result = await _userManager.CreateAsync(userToCreate, model.Password);

            if (result.Succeeded)
            {
                var userFromDb = await _userManager.FindByNameAsync(userToCreate.UserName);

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(userFromDb);

                var uriBuilder = new UriBuilder(_config["ReturnPaths:ConfirmEmail"]);
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                query["token"] = token;
                query["userid"] = userFromDb.Id;
                uriBuilder.Query = query.ToString();
                var urlString = uriBuilder.ToString();

                var senderEmail = _config["ReturnPaths:SenderEmail"];

                await _emailSender.SendEmailAsync(senderEmail, userFromDb.Email, "Confirm your email address", urlString);


                //add role to user 
                await _userManager.AddToRoleAsync(userFromDb, model.Role);
                

                var claim = new Claim("Jobtitle", model.JobTitle);
                await _userManager.AddClaimAsync(userFromDb, claim);

                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("confirmemail")]
        public async Task<IActionResult> confirmEmail(ConfirmEmailModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            var result = await _userManager.ConfirmEmailAsync(user, model.Token);

            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
