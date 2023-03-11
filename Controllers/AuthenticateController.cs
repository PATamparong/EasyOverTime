using Microsoft.AspNetCore.Mvc;
using EasyOverTime.Models.Login;
using EasyOverTime.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using EasyOverTime.Models;
using System.Text;
using MySqlX.XDevAPI;

namespace EasyOverTime.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticateController : Controller
    {
        private readonly UserManager<IdentityUser>? _userManager;
        private readonly RoleManager<IdentityRole>? _roleManager;
        private IConfiguration? _configuration;


        public AuthenticateController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManage, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManage;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.Username);

                if (ModelState.IsValid)
                {
                    if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                    {
                        var userRoles = await _userManager.GetRolesAsync(user);

                        var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                        foreach (var role in userRoles)
                        {
                            authClaims.Add(new Claim(ClaimTypes.Role, role));
                        }

                        var token = GetToken(authClaims);
                        Console.WriteLine("nisulodddddd");

                        var result = Json(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo,
                            respCode = 1,
                            status = "success",
                            message = "Login Successfully!"
                        });

                        var resultValue = result.Value;

                        Console.WriteLine(resultValue);
                        //HttpContext.Session.SetString("resultValue", resultValue.ToString());
                        //var hello = HttpContext.Session.GetString("resultValue");
                        //Console.WriteLine(hello);

                        return result;

                    }

                    return Unauthorized();
                }else
                {
                    return View();

                }
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpPost]
        [Route("register")]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            Console.WriteLine("nisulodddddd", result);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }


        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }

    }
}
