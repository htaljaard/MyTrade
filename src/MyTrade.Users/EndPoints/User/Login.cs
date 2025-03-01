using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyTrade.Users.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTrade.Users.EndPoints.User
{
    public record LoginRequest(string Email, string Password);
    internal class Login : Endpoint<LoginRequest>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<Login> _logger;

        public Login(UserManager<ApplicationUser> userManager, ILogger<Login> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public override void Configure()
        {
            Post("/users/login");
            AllowAnonymous();
        }

        public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
        {
            try
            {
                var existingUser = await _userManager.FindByEmailAsync(req.Email);
                if (existingUser == null)
                {
                    AddError("User does not exist");
                    await SendUnauthorizedAsync();
                    return;
                }

                var loginResult = _userManager.CheckPasswordAsync(existingUser, req.Password).Result;
                if (!loginResult)
                {
                    AddError("Invalid Email or Password");
                    await SendUnauthorizedAsync();
                    return;
                }

                var jwtSecret = Config.GetValue<string>("Auth:JWTSecret");

                var token = JwtBearer.CreateToken(options =>
                {
                    options.SigningKey = jwtSecret!;
                    options.ExpireAt = DateTime.UtcNow.AddHours(1);
                    options.User["Email"] = existingUser.Email!;
                });

                await SendAsync(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during login");
                AddError("Internal server error occured during login");
                await SendErrorsAsync(500);                
            }

        }
    }
}
