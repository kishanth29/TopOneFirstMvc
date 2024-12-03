using Microsoft.EntityFrameworkCore;
using TopOne.Web.Models;

namespace TopOne.Web.database
{
    public class TopOneDbContext :DbContext
    {
        public TopOneDbContext(DbContextOptions <TopOneDbContext> options) : base(options) {

        
        
        }
        public DbSet<Brand> Brand { get; set; }  // Brand it will create a table in database 
        
    }
}
