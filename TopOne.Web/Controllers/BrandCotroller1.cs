using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Reflection.Metadata.Ecma335;
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

                
                if (!Directory.Exists(upload))
                {
                    Directory.CreateDirectory(upload);
                }

                using (var fileStream = new FileStream(Path.Combine(upload, newFileName + extention), FileMode.Create))
                {
                    file[0].CopyTo(fileStream);
                }

                brand.BrandLogo = @"\images\brand\" + newFileName + extention;



            }
            if (ModelState.IsValid)
            {
                _dbcontext.Brand.Add(brand);
                _dbcontext.SaveChanges();

                TempData["Success"] = "Record Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public IActionResult Details(Guid id) 
        {
            Brand brand = _dbcontext.Brand.FirstOrDefault(x=>x.Id == id);

            return View(brand);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            Brand brand = _dbcontext.Brand.FirstOrDefault(x => x.Id == id);

            return View(brand);
        }

        [HttpPost]
        public IActionResult Edit(Brand brand)
        {
            string webRootPath = _IWebHostEnvironment.WebRootPath;

            var file = HttpContext.Request.Form.Files;

            if (file.Count > 0)
            {
                string newFileName = Guid.NewGuid().ToString();

                var upload = Path.Combine(webRootPath, @"images\brand");

                var extention = Path.GetExtension(file[0].FileName);

                // delete existing image 
                var objFromDb = _dbcontext.Brand.AsNoTracking().FirstOrDefault(x => x.Id == brand.Id);

                if (objFromDb.BrandLogo != null) {

                    var oldImagePath = Path.Combine(webRootPath, objFromDb.BrandLogo.Trim('\\'));

                    if (System.IO.File.Exists(oldImagePath)) 
                    {
                        System.IO.File.Delete(oldImagePath);
                    
                    }


                }



                if (!Directory.Exists(upload))
                {
                    Directory.CreateDirectory(upload);
                }

                using (var fileStream = new FileStream(Path.Combine(upload, newFileName + extention), FileMode.Create))
                {
                    file[0].CopyTo(fileStream);
                }

                brand.BrandLogo = @"\images\brand\" + newFileName + extention;



            }


            if (ModelState.IsValid)
            {
                var objFromDb = _dbcontext.Brand.AsNoTracking().FirstOrDefault(x => x.Id == brand.Id);

                objFromDb.Name = brand.Name;
                objFromDb.EstablishedYear = brand.EstablishedYear;

                if (brand.BrandLogo != null)
                {
                    objFromDb.BrandLogo = brand.BrandLogo;
                    
                }

                _dbcontext.Brand.Update(objFromDb);
                _dbcontext.SaveChanges();
                TempData["warning"] = "Record Updated Successfully";


                return RedirectToAction(nameof(Index));
            }
            return View();
        }


        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            Brand brand = _dbcontext.Brand.FirstOrDefault(x => x.Id == id);

            return View(brand);
        }

        [HttpPost]
        public IActionResult Delete(Brand brand) 
        {
            string webRootPath = _IWebHostEnvironment.WebRootPath;
            if (string.IsNullOrEmpty(brand.BrandLogo)) 
            {
                var objFromDb = _dbcontext.Brand.AsNoTracking().FirstOrDefault(x => x.Id == brand.Id);

                if (objFromDb.BrandLogo != null)
                {

                    var oldImagePath = Path.Combine(webRootPath, objFromDb.BrandLogo.Trim('\\'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);

                    }


                }

            }
            _dbcontext.Brand.Remove(brand);
            _dbcontext.SaveChanges();
            TempData["error"] = "Record Deleted Successfully";

            return RedirectToAction(nameof(Index));

        }

      

    }
}
