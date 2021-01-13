using Microsoft.AspNetCore.Mvc;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammersBlog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async  Task<IActionResult> Index()
        {

            var result = await _categoryService.GetAll();
            return View(result.Data);  //success mi error mu olduğunu viewda result ile kontrol edilebilr
            //if(result.ResultStatus==ResultStatus.Success )
            //{
            //    return View(result.Data);
            //}

            //return View();
        }
    }
}
