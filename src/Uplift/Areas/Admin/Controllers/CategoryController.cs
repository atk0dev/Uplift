using System.Threading.Tasks;

namespace Uplift.Areas.Admin.Controllers
{
    using System;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Uplift.DataAccess.Data.Repository.IRepository;
    using Uplift.Models;

    [Authorize]
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Upsert(int? id)
        {
            Category category = new Category();
            
            if (id == null)
            {
                return View(category);
            }

            category = _unitOfWork.Category.Get(id.GetValueOrDefault());
            
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.Id == 0)
                {
                    category.CreateAt = DateTime.UtcNow;
                    category.CreateBy = HttpContext?.User?.Identity?.Name;
                    _unitOfWork.Category.Add(category);
                }
                else
                {
                    category.ModifiedAt = DateTime.UtcNow;
                    category.ModifiedBy = HttpContext?.User?.Identity?.Name;
                    _unitOfWork.Category.Update(category);
                }

                await _unitOfWork.SaveAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.Category.GetAll() });
            //return Json(new { data = _unitOfWork.SP_Call.ReturnList<Category>(SD.usp_GetAllCategory,null)  });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Category.Get(id);
            if(objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }

            _unitOfWork.Category.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete successful." });

        }


        #endregion
    }
}