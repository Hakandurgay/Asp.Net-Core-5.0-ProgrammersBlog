using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgrammersBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgrammersBlog.Data.Concrete.EntityFramework.Mappings
{

    public class ArticleMap : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(a => a.Id);  //idyi belirler
            builder.Property(a => a.Id).ValueGeneratedOnAdd(); //id'nin bir bir artmasını sağlar
            builder.Property(a => a.Title).HasMaxLength(100); //max 100 karakter olabilir.
            builder.Property(a => a.Title).IsRequired(true); //true ise zorunlu
            builder.Property(a => a.Content).IsRequired(true);
            builder.Property(a => a.Content).HasColumnType("NVARCHAR(MAX)");
            builder.Property(a => a.Date).IsRequired();
            builder.Property(a => a.SeoAuthor).IsRequired();
            builder.Property(a => a.SeoAuthor).HasMaxLength(50);
            builder.Property(a => a.SeoDescription).HasMaxLength(150);
            builder.Property(a => a.SeoDescription).IsRequired();
            builder.Property(a => a.SeoTags).IsRequired();
            builder.Property(a => a.SeoTags).HasMaxLength(70);
            builder.Property(a => a.ViewsCount).IsRequired();
            builder.Property(a => a.CommentCount).IsRequired();
            builder.Property(a => a.Thumbnail).IsRequired();
            builder.Property(a => a.Thumbnail).HasMaxLength(250);
            builder.Property(a => a.CreatedByName).IsRequired();
            builder.Property(a => a.CreatedByName).HasMaxLength(50);
            builder.Property(a => a.ModifiedByName).IsRequired();
            builder.Property(a => a.ModifiedByName).HasMaxLength(50);
            builder.Property(a => a.CreatedByName).IsRequired();
            builder.Property(a => a.ModifiedDate).IsRequired();
            builder.Property(a => a.IsActive).IsRequired();
            builder.Property(a => a.IsDeleted).IsRequired();
            builder.Property(a => a.Note).HasMaxLength(500); //null geçilebilir ise is required verilmez. verimedğinde geçilebilri oluyor

            builder.HasOne<Category>(a => a.Category).WithMany(c => c.Articles).HasForeignKey(a => a.CategoryId);  // bir kategori birden fazla article içerebilir
            builder.HasOne<User>(a => a.User).WithMany(u => u.Articles).HasForeignKey(a => a.UserId);

            builder.ToTable("Articles"); //db deki tablo adı

            builder.HasData(
                new Article   //veritabanı oluşturulurken tanımlandığı için isrequired olmayanların da verilmesi gerekiyor
                {
                    Id = 1,
                    CategoryId = 1,
                    UserId = 1,
                    Content = "Lorem Ipsum, dizgi ve baskı endüstrisinde kullanılan mıgır metinlerdir. Lorem Ipsum, adı bilinmeyen bir matbaacının bir hurufat numune kitabı oluşturmak üzere bir yazı galerisini alarak karıştırdığı 1500'lerden beri endüstri standardı sahte metinler olarak kullanılmıştır. Beşyüz yıl boyunca varlığını sürdürmekle kalmamış, aynı zamanda pek değişmeden elektronik dizgiye de sıçramıştır. 1960'larda Lorem Ipsum pasajları da içeren Letraset yapraklarının yayınlanması ile ve yakın zamanda Aldus PageMaker gibi Lorem Ipsum sürümleri içeren masaüstü yayıncılık yazılımları ile popüler olmuştur.",
                    Title = "C# 9.0 ve .NET 5 Yenilikleri",
                    Thumbnail = "Default.jpg",
                    SeoDescription = "C# 9.0 ve .NET 5 Yenilikleri",
                    SeoTags = "C#, .NET CORE, .NET5",
                    SeoAuthor = "Hakan Durgay",
                    Date = DateTime.Now,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedDate = DateTime.Now,
                    ModifiedByName = "InitialCreate",
                    ModifiedDate = DateTime.Now,
                    Note = "C# 9.0 ve .NET 5 Yenilikleri",
                    ViewsCount=100,
                    CommentCount=200
                    
                },
               new Article   //veritabanı oluşturulurken tanımlandığı için isrequired olmayanların da verilmesi gerekiyor
               {
                   Id = 2,
                   CategoryId = 2,
                   UserId = 1,
                   Content = "Yinelenen bir sayfa içeriğinin okuyucunun dikkatini dağıttığı bilinen bir gerçektir. Lorem Ipsum kullanmanın amacı, sürekli 'buraya metin gelecek, buraya metin gelecek' yazmaya kıyasla daha dengeli bir harf dağılımı sağlayarak okunurluğu artırmasıdır. Şu anda birçok masaüstü yayıncılık paketi ve web sayfa düzenleyicisi, varsayılan mıgır metinler olarak Lorem Ipsum kullanmaktadır. Ayrıca arama motorlarında 'lorem ipsum' anahtar sözcükleri ile arama yapıldığında henüz tasarım aşamasında olan çok sayıda site listelenir. Yıllar içinde, bazen kazara, bazen bilinçli olarak (örneğin mizah katılarak), çeşitli sürümleri geliştirilmiştir.",
                   Title = "C++ 11 ve 19 Yenilikleri",
                   Thumbnail = "Default.jpg",
                   SeoDescription = "C++ 11 ve 19 Yenilikleri",
                   SeoTags = "C++ 11 ve 19 Yenilikleri",
                   SeoAuthor = "Hakan Durgay",
                   Date = DateTime.Now,
                   IsActive = true,
                   IsDeleted = false,
                   CreatedByName = "InitialCreate",
                   CreatedDate = DateTime.Now,
                   ModifiedByName = "InitialCreate",
                   ModifiedDate = DateTime.Now,
                   Note = "C++ 11 ve 19 Yenilikleri",
                   ViewsCount = 50,
                   CommentCount = 14
               },
                     new Article   //veritabanı oluşturulurken tanımlandığı için isrequired olmayanların da verilmesi gerekiyor
                     {
                         Id = 3,
                         CategoryId = 3,
                         UserId = 1,
                         Content = "Lorem Ipsum pasajlarının birçok çeşitlemesi vardır. Ancak bunların büyük bir çoğunluğu mizah katılarak veya rastgele sözcükler eklenerek değiştirilmişlerdir. Eğer bir Lorem Ipsum pasajı kullanacaksanız, metin aralarına utandırıcı sözcükler gizlenmediğinden emin olmanız gerekir. İnternet'teki tüm Lorem Ipsum üreteçleri önceden belirlenmiş metin bloklarını yineler. Bu da, bu üreteci İnternet üzerindeki gerçek Lorem Ipsum üreteci yapar. Bu üreteç, 200'den fazla Latince sözcük ve onlara ait cümle yapılarını içeren bir sözlük kullanır. Bu nedenle, üretilen Lorem Ipsum metinleri yinelemelerden, mizahtan ve karakteristik olmayan sözcüklerden uzaktır.",
                         Title = "JavaScript ES2019 ve ES2020 Yenilikler",
                         Thumbnail = "Default.jpg",
                         SeoDescription = "JavaScript ES2019 ve ES2020 Yenilikler",
                         SeoTags = "JavaScript ES2019 ve ES2020 Yenilikler",
                         SeoAuthor = "Hakan Durgay",
                         Date = DateTime.Now,
                         IsActive = true,
                         IsDeleted = false,
                         CreatedByName = "InitialCreate",
                         CreatedDate = DateTime.Now,
                         ModifiedByName = "InitialCreate",
                         ModifiedDate = DateTime.Now,
                         Note = "JavaScript ES2019 ve ES2020 Yenilikler",
                         ViewsCount = 56,
                         CommentCount = 125
                     }
            );
        }
    }
}
