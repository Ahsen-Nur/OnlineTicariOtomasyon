using Microsoft.AspNetCore.Mvc;

public class BaseAdminController : Controller
{
    public override void OnActionExecuting(
        Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext context)
    {
        var rol = HttpContext.Session.GetString("Rol");

        if (rol != "Admin")
        {
            context.Result = new RedirectToActionResult(
                "GirisYap", "Login", null);
        }

        base.OnActionExecuting(context);
    }
}
