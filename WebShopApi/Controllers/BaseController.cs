using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace WebShop.Controllers;

public class BaseController  : ControllerBase
{
    //
    // Returns the token's user-id value as int
    //
    protected int? GetUserId()
    {
        var userIdString = User.FindFirstValue("user-id");
        var isValidInt = int.TryParse(userIdString, out var userId);
        if (!isValidInt) return null;
        return userId;
    }
}