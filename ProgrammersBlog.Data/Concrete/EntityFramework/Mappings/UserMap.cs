using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgrammersBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgrammersBlog.Data.Concrete.EntityFramework.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> b)
        {
            b.Property(u => u.Picture).IsRequired();
            b.Property(u => u.Picture).HasMaxLength(250);



            // Primary key
            b.HasKey(u => u.Id);

            // Indexes for "normalized" username and email, to allow efficient lookups
            b.HasIndex(u => u.NormalizedUserName).HasDatabaseName("UserNameIndex").IsUnique();
            b.HasIndex(u => u.NormalizedEmail).HasDatabaseName("EmailIndex");

            // Maps to the AspNetUsers table
            b.ToTable("AspNetUsers");

            // A concurrency token for use with the optimistic concurrency checking
            b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            b.Property(u => u.UserName).HasMaxLength(100);
            b.Property(u => u.NormalizedUserName).HasMaxLength(100);
            b.Property(u => u.Email).HasMaxLength(100);
            b.Property(u => u.NormalizedEmail).HasMaxLength(100);

            // The relationships between User and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each User can have many UserClaims
            b.HasMany<UserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

            // Each User can have many UserLogins
            b.HasMany<UserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

            // Each User can have many UserTokens
            b.HasMany<UserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

            // Each User can have many entries in the UserRole join table
            b.HasMany<UserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();




            //İDENTİTY EKLENDİĞİ İÇİN KALDIRILDI
            //builder.HasKey(u => u.Id);
            //builder.Property(u => u.Id).ValueGeneratedOnAdd();
            //builder.Property(u => u.Email).IsRequired();
            //builder.Property(u => u.Email).HasMaxLength(50);
            //builder.HasIndex(u => u.Email).IsUnique();  // her email bir kere kayıt olabilir
            //builder.Property(u => u.UserName).IsRequired();
            //builder.Property(u => u.UserName).HasMaxLength(20);
            //builder.HasIndex(u => u.UserName).IsUnique();
            //builder.Property(u => u.PasswordHash).IsRequired();
            //builder.Property(u => u.PasswordHash).HasColumnType("VARBINARY(500)");
            //builder.Property(u => u.Description).HasMaxLength(500);
            //builder.Property(u => u.FirstName).IsRequired();
            //builder.Property(u => u.FirstName).HasMaxLength(30);
            //builder.Property(u => u.LastName).IsRequired();
            //builder.Property(u => u.LastName).HasMaxLength(30);


            //builder.Property(u => u.CreatedByName).IsRequired();
            //builder.Property(u => u.CreatedByName).HasMaxLength(50);
            //builder.Property(u => u.ModifiedByName).IsRequired();
            //builder.Property(u => u.ModifiedByName).HasMaxLength(50);
            //builder.Property(u => u.CreatedByName).IsRequired();
            //builder.Property(u => u.ModifiedDate).IsRequired();
            //builder.Property(u => u.IsActive).IsRequired();
            //builder.Property(u => u.IsDeleted).IsRequired();
            //builder.Property(u => u.Note).HasMaxLength(500);

            //builder.HasOne<Role>(u => u.Role).WithMany(u => u.Users).HasForeignKey(u => u.RoleId);
            //builder.ToTable("Users");

            //builder.HasData(new User  //veritabanı oluşturulurken tanımlandığı için isrequired olmayanların da verilmesi gerekiyor
            //{
            //    Id = 1,
            //    RoleId = 1,
            //    FirstName = "Hakan",
            //    LastName = "Durgay",
            //    UserName = "hakandurgay",
            //    Email = "hakandurgay@gmail.com",
            //    IsActive = true,
            //    IsDeleted = false,
            //    CreatedByName = "InitialCreate",
            //    CreatedDate = DateTime.Now,
            //    ModifiedByName = "InitialCreate",
            //    ModifiedDate = DateTime.Now,
            //    Description = "İlk Admin Kullanıcı",
            //    Note = "Admin Kullanıcı",
            //    PasswordHash = Encoding.ASCII.GetBytes("0192023a7bbd73250516f069df18b500"),
            //    Picture= "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcSX4wVGjMQ37PaO4PdUVEAliSLi8-c2gJ1zvQ&usqp=CAU"


            //}); 
        }
    }
}
