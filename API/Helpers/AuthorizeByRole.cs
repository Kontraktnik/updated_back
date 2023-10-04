using Microsoft.AspNetCore.Authorization;

namespace API.Helpers;

public class AuthorizeByRole : AuthorizeAttribute
{
    public AuthorizeByRole(params string[] roles) : base()
    {
        Roles = String.Join(",", roles);
    }
}