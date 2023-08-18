using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Bulky.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
                 
            return View(objCompanyList);
        }
        public IActionResult Upsert(int? id)
        {
            if(id == 0 || id == null)
            {
                // Create
                return View(new Company());
            }
            else
            {
                // Update
                Company companyObj = _unitOfWork.Company.Get(u=>u.Id==id);
                return View(companyObj);
            }
            
        }
        [HttpPost]
        public IActionResult Upsert(Company companyObj)
        { 
            if (ModelState.IsValid)
            {
                if(companyObj.Id == 0)
                {
                    _unitOfWork.Company.Add(companyObj);
                }
                else
                {
                    _unitOfWork.Company.Update(companyObj);
                }
                _unitOfWork.Save();
                TempData["success"] = "Company Updated Successfully.";
                return RedirectToAction("Index");
            }
            else
            {
                return View(companyObj);
            }
            
        }

        
        #region API CALLS
        [HttpGet]
        public string GetAll()
            {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            return JsonConvert.SerializeObject(new { data = objCompanyList }); 
            // Json(new { data = objCompanyList });
        }

        [HttpDelete]
        public string Delete(int? id)
        {
            var CompanyToBeDeleted = _unitOfWork.Company.Get(u =>u.Id==id);
            if (CompanyToBeDeleted == null)
            {
                return JsonConvert.SerializeObject(new { success = false, 
                    message = "Error while Deleting"});
            }
            
            _unitOfWork.Company.Remove(CompanyToBeDeleted);
            _unitOfWork.Save();
            return JsonConvert.SerializeObject(new { success = true , message = "Delete Successful!!!"});
            //Json(new { data = objCompanyList });
        }
        #endregion
    }
}
