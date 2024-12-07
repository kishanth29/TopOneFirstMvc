using Microsoft.AspNetCore.Mvc;
using TopOne.Web.database;
using TopOne.Web.Models;

namespace TopOne.Web.Controllers
{
    public class BrandController1 : Controller
    {
        private readonly TopOneDbContext _dbcontext;
        private readonly IWebHostEnvironment _IWebHostEnvironment;

        public BrandController1(TopOneDbContext dbContext , IWebHostEnvironment WebHostEnvironment)
        {
            _dbcontext = dbContext;
            _IWebHostEnvironment = WebHostEnvironment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Models.Brand> brands = _dbcontext.Brand.ToList();
            return View(brands);
        }

        [HttpGet]
        //[HttpPost]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Brand brand)
        {
            string webRootPath = _IWebHostEnvironment.WebRootPath;

            var file = HttpContext.Request.Form.Files;

            if (file.Count > 0) { 
                string newFileName = Guid.NewGuid().ToString();

                var upload = Path.Combine(webRootPath, @"images\brand");

                var extention = Path.GetExtension(file[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(upload, newFileName + extention), FileMode.Create))
                {
                    file[0].CopyTo(fileStream);
                }



            }



            if (ModelState.IsValid)
            {
                _dbcontext.Brand.Add(brand);
                _dbcontext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
