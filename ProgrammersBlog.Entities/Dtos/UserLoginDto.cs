using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProgrammersBlog.Entities.Dtos
{
    public class UserLoginDto
    {
        public string Email { get; set; }
        [DisplayName("Şifre")]
        [Required(ErrorMessage = "{0} Boş Geçilmemelidir.")]  //kategori adı yazar sıfır yerine
        [MaxLength(30, ErrorMessage = "{0} {1} Karakterden Büyük Olmamalıdır.")] //fluent api ile veritabanına eklerken kontrol ediliyordu burda kullanıcıdan alınırken kontrol ediliyor
        [MinLength(5, ErrorMessage = "{0} {1} Karakterden Küçük Olmamalıdır.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName("Telefon Numarası")]
        [Required(ErrorMessage = "{0} Boş Geçilmemelidir.")]  //kategori adı yazar sıfır yerine
        [MaxLength(13, ErrorMessage = "{0} {1} Karakterden Büyük Olmamalıdır.")] //fluent api ile veritabanına eklerken kontrol ediliyordu burda kullanıcıdan alınırken kontrol ediliyor
        [MinLength(13, ErrorMessage = "{0} {1} Karakterden Küçük Olmamalıdır.")]

        public bool RememberMe { get; set; }
    }
}
