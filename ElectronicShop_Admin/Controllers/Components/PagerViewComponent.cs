using ElectronicShop_Model.Common;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicShop_Admin.Controllers.Components
{
	public class PagerViewComponent : ViewComponent
	{
		public Task<IViewComponentResult> InvokeAsync(PagedResultBase result)
		{
			return Task.FromResult((IViewComponentResult)View("Default", result));
		}
	}
}