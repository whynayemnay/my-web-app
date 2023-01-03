using my_web_app.Data.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Threading;

namespace my_web_app.Data
{
    public class AppDbInitialiser
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope()) 
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                if(!context.Books.Any())
                {
                    context.Books.AddRange(new Book()
                    {
                        Title = "1st book Title",
                        Description = "1st book desctiption",
                        isRead = true,
                        DateRead = DateTime.Now.AddDays(-10),
                        Rate = 4,
                        Genre = "Biopic",
                        CoverUrl = "https//...",
                        DateAdded = DateTime.Now
                    }, new Book()
                    {
                        Title = "2st book Title",
                        Description = "2st book desctiption",
                        isRead = false,
                        Genre = "Drama",
                        CoverUrl = "https//...",
                        DateAdded = DateTime.Now
                    }
                    );

                    context.SaveChanges();
                }
            }
        }
    }
}
