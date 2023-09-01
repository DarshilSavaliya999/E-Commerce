using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Bulky.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

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

        public IActionResult Details(int orderId)
        {
            OrderVM orderVM = new()
            {
                OrderHeader = _unitOfWork.orderHeader.Get(u=>u.Id == orderId , includeProperties:"ApplicationUser"),
            OrderDetail = _unitOfWork.OrderDetail.GetAll(u=>u.OrderHeaderId == orderId,includeProperties:"Product"),
            };
            return View(orderVM);
        }

        #region API CALLS
        [HttpGet]
		public string GetAll(string status)
		{
			IEnumerable<OrderHeader> objOrderHeader = _unitOfWork.orderHeader.GetAll(includeProperties: "ApplicationUser").ToList();

            switch (status)
            {
                case "pending":
					objOrderHeader = objOrderHeader.Where(u=>u.PaymentStatus == SD.PaymentStatusDelayedPayment);
                    break;
                case "completed":
                    objOrderHeader = objOrderHeader.Where(u => u.OrderStatus == SD.StatusShipped);
                    break;
                case "approved":
                    objOrderHeader = objOrderHeader.Where(u => u.OrderStatus == SD.StatusApproved);
                    break;
                case "inprocess":
                    objOrderHeader = objOrderHeader.Where(u => u.OrderStatus == SD.StatusInProcess);
                    break;
                default:
                    break;

            }
            return JsonConvert.SerializeObject(new { data = objOrderHeader });
			// return Json(new { data = objProductList });
		}

		
		#endregion
	}
}
