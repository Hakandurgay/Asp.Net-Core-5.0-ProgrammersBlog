using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgrammersBlog.Shared.Entities.Abstract
{
    public abstract class DtoGetBase //dtolarda tekrar eden kısım
    {
        public virtual ResultStatus ResultStatus { get; set; }
        public virtual string Message { get; set; }

    }
}
