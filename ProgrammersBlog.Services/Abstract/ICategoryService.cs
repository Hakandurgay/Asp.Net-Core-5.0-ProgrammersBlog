﻿using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Services.Abstract
{
    public interface ICategoryService
    {
        // Task<IDataResult<Category>> Get(int categoryId); bunun yerine aşağıdaki gibi olmalı

        Task<IDataResult<CategoryDto>> Get(int categoryId);
        //   Task<IDataResult<IList<Category>>> GetAll();
        Task<IDataResult<CategoryListDto>> GetAll();
        //Task<IDataResult<IList<Category>>> GetAllByNonDeleted();
        Task<IDataResult<CategoryListDto>> GetAllByNonDeleted();
        Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAndActive();
        Task<IResult> Add(CategoryAddDto categoryAddDto, string createdByName);
        Task<IResult> Update(CategoryUpdateDto categoryUpdateDto, string modifiedByName);
        Task<IResult> Delete(int categoryId, string modifiedByName);  //aktifliği pasif yapar
        Task<IResult> HardDelete(int categoryId); //veritabınından silinir
    }
}
