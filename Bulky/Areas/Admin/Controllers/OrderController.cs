using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Bulky.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class OrderController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public OrderController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public IActionResult Index()
		{
			return View();
		}

		#region API CALLS
		[HttpGet]
		public string GetAll()
		{
			List<OrderHeader> objOrderHeader = _unitOfWork.orderHeader.GetAll(includeProperties: "ApplicationUser").ToList();
			return JsonConvert.SerializeObject(new { data = objOrderHeader });
			// return Json(new { data = objProductList });
		}

		
		#endregion
	}
}
