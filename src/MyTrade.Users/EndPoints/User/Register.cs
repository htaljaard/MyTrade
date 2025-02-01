using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using MyTrade.Users.Domain;

namespace MyTrade.Users.EndPoints.User;

public record RegisterRequest(string Email, string Password);
internal class Register : Endpoint<RegisterRequest>
{
    private readonly UserManager<ApplicationUser> _userManager;
    public Register(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public override void Configure()
    {
        Post("/users/register");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterRequest req, CancellationToken ct)
    {
        var newUser = new ApplicationUser
        {
            Email = req.Email,
            UserName = req.Email

        };
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
}
