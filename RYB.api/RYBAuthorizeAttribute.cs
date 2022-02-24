using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RYB.Model.ViewModel;

namespace RYB.api;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RYBAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // skip authorization if action is decorated with [AllowAnonymous] attribute
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;

        // authorization
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        var user = (UserProfile)context.HttpContext.Items["User"];
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        if (user == null)
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
    }
}

