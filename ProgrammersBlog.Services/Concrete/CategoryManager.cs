﻿using AutoMapper;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using ProgrammersBlog.Shared.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Services.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        //mapper olmadan yazılmış hal
        //public async Task<IResult> Add(CategoryAddDto categoryAddDto, string createdByName)
        //{
        //    await _unitOfWork.Categories.AddAsync(new Category
        //    {
        //        Name = categoryAddDto.Name,
        //        Description = categoryAddDto.Description,
        //        Note = categoryAddDto.Note,
        //        IsActive = categoryAddDto.IsActive,
        //        CreatedByName = createdByName,
        //        CreatedDate = DateTime.Now,
        //        ModifiedByName = createdByName,
        //        ModifiedDate = DateTime.Now,  //bu değerler diğer tarafta da atandığı için girilmeyebilir
        //        IsDeleted = false
        //    }).ContinueWith(t => _unitOfWork.SaveAsync());   //biraz bekleyip aşağıdakini çalıştırmak yerine çok hızlı bir şekilde bu task ile devam ediyor
        //                                                     //   await _unitOfWork.SaveAsync();
        //    return new Result(ResultStatus.Success, $"{categoryAddDto.Name} adlı kategori başarıyla eklenmiştir");
        //}
        public async Task<IDataResult<CategoryDto>> Add(CategoryAddDto categoryAddDto, string createdByName)
        {
            var category = _mapper.Map<Category>(categoryAddDto);

            category.CreatedByName = createdByName;
            category.ModifiedByName = createdByName;
        var addedCategory=    await _unitOfWork.Categories.AddAsync(category);//.ContinueWith(t => _unitOfWork.SaveAsync());   //biraz bekleyip aşağıdakini çalıştırmak yerine çok hızlı bir şekilde bu task ile devam ediyor
        await _unitOfWork.SaveAsync(); //ef core 5 trade safe olmadığı için böyle olmalı
            return new DataResult<CategoryDto>(ResultStatus.Success, $"{categoryAddDto.Name} adlı kategori başarıyla eklenmiştir", new CategoryDto
            {
                Category = addedCategory,
                ResultStatus = ResultStatus.Success,
                Message=$"{categoryAddDto.Name} adlı kategori başarıyla eklenmiştir"
            }) ;
            
            
        }


        public async Task<IDataResult<CategoryDto>> Get(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId, c => c.Articles);
            if (category != null)
            {
                return new DataResult<CategoryDto>(ResultStatus.Success, new CategoryDto
                {
                    Category = category,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<CategoryDto>(ResultStatus.Error, "Böyle bir kategori bulunamadı.", new CategoryDto{

                Category=null,
                ResultStatus=ResultStatus.Error,
                Message= "Böyle bir kategori bulunamadı."
            });
        }

        public async Task<IDataResult<CategoryListDto>> GetAll()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(null, c => c.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                });

            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Hiçbir kategori bulunamadı.", new CategoryListDto
            {
                Categories=null,
                ResultStatus=ResultStatus.Error,
                Message= "Hiçbir kategori bulunamadı."
            });

        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeleted()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(c => !c.IsDeleted, c => c.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                });

            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Hiçbir kategori bulunamadı.", new CategoryListDto
            {
                Categories = null,
                ResultStatus = ResultStatus.Error,
                Message = "Hiçbir kategori bulunamadı."
            });

        }
        public async Task<IDataResult<CategoryDto>> Delete(int categoryId, string modifiedByName)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                category.IsDeleted = true;
                category.ModifiedByName = modifiedByName;
                category.ModifiedDate = DateTime.Now;
                var deletedCategory = await _unitOfWork.Categories.UpdateAsync(category);//.ContinueWith(t => _unitOfWork.SaveAsync());
                await _unitOfWork.SaveAsync();
                return new DataResult<CategoryDto>(ResultStatus.Success, $"{deletedCategory.Name } adlı kategori başarıyla silinmiştir ", new CategoryDto
                {
                    Category = deletedCategory,
                    ResultStatus = ResultStatus.Success,
                    Message = $"{deletedCategory.Name} adlı kategori başarıyla silinmiştir"
                });
            }
            return new DataResult<CategoryDto>(ResultStatus.Error, $"böyle bir kategori bulunamadı ", new CategoryDto
            {
                Category = null,
                ResultStatus = ResultStatus.Error,
                Message = $"böyle bir kategori bulunamadı "
            });

        }
        public async Task<IResult> HardDelete(int categoryId)
        {

            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                await _unitOfWork.Categories.DeleteAsync(category);//.ContinueWith(t => _unitOfWork.SaveAsync());
                await _unitOfWork.SaveAsync();

                return new Result(ResultStatus.Success, $"{category.Name } adlı kategori başarıyla veritabanından silinmiştir ");
            }
            return new Result(ResultStatus.Error, "Böyle bir kategori bulunamadı.");
        }
        //mapping olmadan
        //public async Task<IResult> Update(CategoryUpdateDto categoryUpdateDto, string modifiedByName)
        //{
        //    var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryUpdateDto.Id);
        //    if (category != null)
        //    {
        //        category.Name = categoryUpdateDto.Name;
        //        category.Description = categoryUpdateDto.Description;
        //        category.Note = categoryUpdateDto.Note;
        //        category.IsActive = categoryUpdateDto.IsActive;
        //        category.IsDeleted = categoryUpdateDto.IsDeleted;
        //        category.ModifiedByName = modifiedByName;
        //        category.ModifiedDate = DateTime.Now;
        //        await _unitOfWork.Categories.UpdateAsync(category).ContinueWith(t => _unitOfWork.SaveAsync());
        //        return new Result(ResultStatus.Success, $"{categoryUpdateDto.Name } adlı kategori başarıyla güncellenmiştir ");
        //    }
        //    return new Result(ResultStatus.Error, "Böyle bir kategori bulunamadı.");
        //}
        public async Task<IDataResult<CategoryDto>> Update(CategoryUpdateDto categoryUpdateDto, string modifiedByName)
        {
            var category = _mapper.Map<Category>(categoryUpdateDto);
            category.ModifiedByName = modifiedByName;

        var updatedCategory=    await _unitOfWork.Categories.UpdateAsync(category);//.ContinueWith(t => _unitOfWork.SaveAsync());
            await _unitOfWork.SaveAsync();
            return new DataResult<CategoryDto>(ResultStatus.Success, $"{categoryUpdateDto.Name } adlı kategori başarıyla güncellenmiştir ", new CategoryDto
            {
                 Category=updatedCategory,
                ResultStatus = ResultStatus.Success,
                Message = $"{categoryUpdateDto.Name} adlı kategori başarıyla eklenmiştir"
            });
         
          

        }
        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAndActive()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(c => !c.IsDeleted && c.IsActive, c => c.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Hiçbir kateogori bulunamadı.", null);
        }

        public async Task<IDataResult<CategoryUpdateDto>> GetCategoryUpdateDto(int categoryId)
        {
            var result = await _unitOfWork.Categories.AnyAsync(c => c.Id == categoryId);
            if(result)
            {
                var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
                var categoryUpdateDto = _mapper.Map<CategoryUpdateDto>(category);
                return new DataResult<CategoryUpdateDto>(ResultStatus.Success, categoryUpdateDto);
            }
            return new DataResult<CategoryUpdateDto>(ResultStatus.Error, "Böyle bir kategori bulunamadı", null);
        }
    }
}
