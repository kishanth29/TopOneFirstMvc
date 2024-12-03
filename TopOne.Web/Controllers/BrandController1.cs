using Microsoft.AspNetCore.Mvc;
using TopOne.Web.database;
using TopOne.Web.Models;

namespace TopOne.Web.Controllers
{
    public class BrandController1 : Controller     
    {
        private readonly TopOneDbContext _dbcontext;

        public BrandController1(TopOneDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Brand> brands = _dbcontext.Brand .ToList(); 
            return View(brands);
        }
    }
}
