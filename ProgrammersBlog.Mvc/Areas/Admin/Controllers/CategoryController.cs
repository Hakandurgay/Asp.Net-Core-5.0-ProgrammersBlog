using Microsoft.AspNetCore.Mvc;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Mvc.Areas.Admin.Models;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Utilities.Extensions;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
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
        public async Task<IActionResult> Index()
        {

            var result = await _categoryService.GetAllByNonDeleted();
            return View(result.Data);  //success mi error mu olduğunu viewda result ile kontrol edilebilr
            //if(result.ResultStatus==ResultStatus.Success )
            //{
            //    return View(result.Data);
            //}

            //return View();
        }
        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_CategoryAddPartial");
        }
        [HttpPost]
        public async Task<IActionResult> Add(CategoryAddDto categoryAddDto)
        {
            //var categoryAjaxModel = new CategoryAddAjaxViewModel
            //{
            //    CategoryAddPartial = await this.RenderViewToStringAsync("_CategoryAddPartial",categoryAddDto),
            //};
            if (ModelState.IsValid)
            {
                var result = await _categoryService.Add(categoryAddDto, "Hakan Durgay");
                if (result.ResultStatus == ResultStatus.Success)
                {
                    var categoryAddAjaxModel = JsonSerializer.Serialize(new CategoryAddAjaxViewModel
                    {
                        CategoryDto = result.Data,
                        CategoryAddPartial = await this.RenderViewToStringAsync("_CategoryAddPartial", categoryAddDto)
                    });
                    return Json(categoryAddAjaxModel);
                }
            }
            var categoryAddAjaxErrorModel = JsonSerializer.Serialize(new CategoryAddAjaxViewModel
            {
                CategoryAddPartial = await this.RenderViewToStringAsync("_CategoryAddPartial", categoryAddDto)
            });
            return Json(categoryAddAjaxErrorModel);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int categoryId)
        {
            var result = await _categoryService.GetCategoryUpdateDto(categoryId);
            if(result.ResultStatus==ResultStatus.Success)
            {
                return PartialView("_CategoryUpdatePartial", result.Data);
            }
            return NotFound();
        }
        public async Task<JsonResult> GetAllCategories()
        {
            var result = await _categoryService.GetAllByNonDeleted();
            var categories = JsonSerializer.Serialize(result.Data, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(categories);
        }
        [HttpPost]
        public async  Task<JsonResult> Delete(int categoryId)
        {
            var result = await _categoryService.Delete(categoryId, "Hakan Durgay");
            var deletedCategory = JsonSerializer.Serialize(result.Data);
            return Json(deletedCategory);
        }
    }
}
