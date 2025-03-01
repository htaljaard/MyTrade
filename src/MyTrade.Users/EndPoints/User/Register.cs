using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MyTrade.Users.Domain;

namespace MyTrade.Users.EndPoints.User;

public record RegisterRequest(string Email, string Password);
internal class Register : Endpoint<RegisterRequest>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<Register> _logger;
    public Register(UserManager<ApplicationUser> userManager, ILogger<Register> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public override void Configure()
    {
        Post("/users/register");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterRequest req, CancellationToken ct)
    {
        try
        {
            var newUser = new ApplicationUser
            {
                Email = req.Email,
                UserName = req.Email
            };

            var existingUser = _userManager.FindByEmailAsync(newUser.Email);

            if (existingUser != null)
            {
                AddError("User already existgs");
                await SendErrorsAsync();
                return;
            }

            try
            {
                await _userManager.CreateAsync(newUser, req.Password);

                await SendOkAsync();
            }
            catch (Exception ex)
            {
                AddError(ex.Message);
                await SendErrorsAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occured when registering user");
            AddError("Internal error occured when registering user");
            await SendErrorsAsync(500);
        }
    }
}
