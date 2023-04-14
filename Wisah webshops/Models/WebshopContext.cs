using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Wisah_webshops.Models
{
    public class WebshopContext : DbContext
    {
        public WebshopContext(DbContextOptions<WebshopContext> options)
            : base(options)
        {
        }

        public DbSet<ProductViewModel> Products { get; set; }
        public DbSet<orderViewModel> Orders { get; set; }
    }

}
