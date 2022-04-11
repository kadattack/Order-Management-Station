using Metronik.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Metronik.TokenAuthentication;

public class TokenAuthorizationService : Attribute, IAuthorizationFilter
{

    public void OnAuthorization(AuthorizationFilterContext context)
    {

        var dbcontext = context.HttpContext.RequestServices.GetRequiredService<DataContext>();

        var vertified = false;
        if (context.HttpContext.Request.Headers.ContainsKey("clientToken"))
        {
            var token = context.HttpContext.Request.Headers["clientToken"];
            var res = dbcontext!.Oms.FirstOrDefault(x => x.omsId == token);
            if (res != null)
            {
                vertified = true;
            }
        }

        if (vertified) return;
        context.ModelState.AddModelError("Unautharized", "Not valid authorization");
        context.Result = new UnauthorizedObjectResult(context.ModelState);


    }
}