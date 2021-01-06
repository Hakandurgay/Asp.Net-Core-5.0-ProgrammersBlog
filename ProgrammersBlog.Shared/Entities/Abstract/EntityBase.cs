using System;
using System.Collections.Generic;
using System.Text;

namespace ProgrammersBlog.Shared.Entities.Abstract
{
  public abstract class  EntityBase
    {
        public virtual int Id { get; set; }
        public virtual DateTime CreatedDate { get; set; } = DateTime.Now; //override edilebilmesi için virtual kullanıldı. virtual ve değer atanabilmesi için de abstract sınıf.
        public virtual DateTime ModifiedDate { get; set; } = DateTime.Now;
        public virtual bool IsDeleted { get; set; } = false;
        public virtual bool IsActive { get; set; } = true;
        public virtual string CreatedByName { get; set; } = "Admin";
        public virtual string ModifiedByName { get; set; } = "Admin";
        public virtual string Note { get; set; }
    }
}
