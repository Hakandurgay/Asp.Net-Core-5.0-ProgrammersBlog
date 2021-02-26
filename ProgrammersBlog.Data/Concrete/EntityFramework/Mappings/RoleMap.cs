using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgrammersBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgrammersBlog.Data.Concrete.EntityFramework.Mappings
{
    public class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> b)
        {

            // Primary key
            b.HasKey(r => r.Id);

            // Index for "normalized" role name to allow efficient lookups
            b.HasIndex(r => r.NormalizedName).HasDatabaseName("RoleNameIndex").IsUnique();

            // Maps to the AspNetRoles table
            b.ToTable("AspNetRoles");

            // A concurrency token for use with the optimistic concurrency checking
            b.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            b.Property(u => u.Name).HasMaxLength(100);
            b.Property(u => u.NormalizedName).HasMaxLength(100);

            // The relationships between Role and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each Role can have many entries in the UserRole join table
            b.HasMany<UserRole>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();

            // Each Role can have many associated RoleClaims
            b.HasMany<RoleClaim>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();

            b.HasData(
                new Role
                {
                    Id = 1,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp =  Guid.NewGuid().ToString()
                },
                                new Role
                                {
                                    Id = 2,
                                    Name = "Editor",
                                    NormalizedName = "EDITOR",
                                    ConcurrencyStamp =  Guid.NewGuid().ToString()
                                }
            );
            //builder.HasKey(r => r.Id);
            //builder.Property(r => r.Id).ValueGeneratedOnAdd();
            //builder.Property(r => r.Name).IsRequired();
            //builder.Property(r => r.Name).HasMaxLength(30);
            //builder.Property(r => r.Description).IsRequired();
            //builder.Property(r => r.Description).HasMaxLength(250);
            //builder.Property(r => r.CreatedByName).IsRequired();
            //builder.Property(r => r.CreatedByName).HasMaxLength(50);
            //builder.Property(r => r.ModifiedByName).IsRequired();
            //builder.Property(r => r.ModifiedByName).HasMaxLength(50);
            //builder.Property(r => r.CreatedByName).IsRequired();
            //builder.Property(r => r.ModifiedDate).IsRequired();
            //builder.Property(r => r.IsActive).IsRequired();
            //builder.Property(r => r.IsDeleted).IsRequired();
            //builder.Property(r => r.Note).HasMaxLength(500);
            //builder.ToTable("Roles");

            //builder.HasData(new Role  //veritabanı oluşturulurken tanımlandığı için isrequired olmayanların da verilmesi gerekiyor
            //{
            //    Id = 1,
            //    Name="Admin",
            //    Description="Admin Rolü, Tüm Haklara Sahiptir",
            //    IsActive=true,
            //    IsDeleted=false,
            //    CreatedByName="InitialCreate",
            //    CreatedDate=DateTime.Now,
            //    ModifiedByName="InitialCreate",
            //    ModifiedDate=DateTime.Now,
            //    Note="Admin Rolüdür."
            //}) ;
        }
    }
}
