using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProgrammersBlog.Entities.Dtos
{
    public class CategoryAddDto
    {
        [DisplayName("Kategori Adı")]
        [Required(ErrorMessage ="{0} Boş Geçilmemelidir.")]  //kategori adı yazar sıfır yerine
        [MaxLength(70,ErrorMessage ="{0} {1} Karakterden Büyük Olmamalıdır.")] //fluent api ile veritabanına eklerken kontrol ediliyordu burda kullanıcıdan alınırken kontrol ediliyor
        [MinLength(3, ErrorMessage ="{0} {1} Karakterden Az Olmamalıdır.") ]
        public string Name { get; set; }

        [DisplayName("Kategori Açıklaması")]      
        [MaxLength(500, ErrorMessage = "{0} {1} Karakterden Büyük Olmamalıdır.")]
        [MinLength(3, ErrorMessage = "{0} {1} Karakterden Az Olmamalıdır.")]
        public string Description { get; set; }

        [DisplayName("Kategori Özel Not Alanı")]
        [MaxLength(500, ErrorMessage = "{0} {1} Karakterden Büyük Olmamalıdır.")]
        [MinLength(3, ErrorMessage = "{0} {1} Karakterden Az Olmamalıdır.")]
        public string Note { get; set; }

        [DisplayName("Aktif Mi?")]
        [Required(ErrorMessage = "{0} Boş Geçilmemelidir.")]
        public bool IsActive { get; set; }
    }
}
