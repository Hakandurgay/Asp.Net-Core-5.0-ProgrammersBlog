using ProgrammersBlog.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgrammersBlog.Entities.Concrete
{
    public class Category : EntityBase, IEntity
    {
        //kalıtım sayesinde override yazarak entitybasedekilere ulaşabilirz
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}
