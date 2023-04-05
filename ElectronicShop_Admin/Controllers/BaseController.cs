using ElectronicShop_Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ElectronicShop_Admin.Controllers
{
	public class BaseController : Controller
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			var token = Request.Cookies[Constants.TOKEN];
			if (token == null)
			{
				context.Result = new RedirectResult(Constants.PAGE_LOGIN);
			}
			base.OnActionExecuting(context);
		}
	}
}
