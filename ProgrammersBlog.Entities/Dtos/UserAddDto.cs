﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProgrammersBlog.Entities.Dtos
{
    public class UserAddDto
    {
        [DisplayName("Kullanıcı Adı")]
        [Required(ErrorMessage = "{0} Boş Geçilmemelidir.")]  //kategori adı yazar sıfır yerine
        [MaxLength(50, ErrorMessage = "{0} {1} Karakterden Büyük Olmamalıdır.")] //fluent api ile veritabanına eklerken kontrol ediliyordu burda kullanıcıdan alınırken kontrol ediliyor
        [MinLength(3, ErrorMessage = "{0} {1} Karakterden Küçük Olmamalıdır.")]
        public string UserName { get; set; }
        [DisplayName("E-Posta Adresi")]
        [Required(ErrorMessage = "{0} Boş Geçilmemelidir.")]  //kategori adı yazar sıfır yerine
        [MaxLength(100, ErrorMessage = "{0} {1} Karakterden Büyük Olmamalıdır.")] //fluent api ile veritabanına eklerken kontrol ediliyordu burda kullanıcıdan alınırken kontrol ediliyor
        [MinLength(10, ErrorMessage = "{0} {1} Karakterden Küçük Olmamalıdır.")]
        [DataType(DataType.EmailAddress)]
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
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [DisplayName("Resim")]
        [Required(ErrorMessage = "{0} Lütfen Bir {0} seçiniz")]  //kategori adı yazar sıfır yerine    
        [DataType(DataType.Upload)]
        public IFormFile PictureFile { get; set; }
        public string Picture { get; set; }
    }
}
