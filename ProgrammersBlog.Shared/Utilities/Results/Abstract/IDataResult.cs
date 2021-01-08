using System;
using System.Collections.Generic;
using System.Text;

namespace ProgrammersBlog.Shared.Utilities.Results.Abstract
{
    public interface IDataResult<out T> : IResult //out yazılarak hem liste hem tek bir entity taşınabiir
    {
        public T Data { get; }//istersek kategori, istersek kategori listesi atılabilir     //new DataResult<Category>(ResultStatus.Succes,category)
                                                                                             //new DataResult<<IList>Category>(ResultStatus.Succes,categoryList)

    }
}
