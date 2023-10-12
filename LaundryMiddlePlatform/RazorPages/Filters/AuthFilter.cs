using Microsoft.AspNetCore.Mvc.Filters;
using RazorPages.Constants;
using RazorPages.Models;
using RazorPages.Utils;

namespace RazorPages.Filters;

public class AuthFilter : IPageFilter
{
    public void OnPageHandlerSelected(PageHandlerSelectedContext context)
    {

        var url = context.HttpContext.Request.Path.ToString();
        if (url == "/" || url == "/Register")
            return;

        var currentUser = context.HttpContext.Session.GetObjectFromJson<CurrentUser>(SessionKey.CurrentUserKey);

        if (currentUser == null)
            context.HttpContext.Response.Redirect("/");
        else if (currentUser.Role != Role.Admin && (url.ToLower().StartsWith("/admin") || url.ToLower().StartsWith("/admin/")))
            context.HttpContext.Response.Redirect("/ForbidenError");
    }

    public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
    {

    }

    public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
    {

    }
}
