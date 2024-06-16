using EcommerceMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System.ComponentModel;

namespace EcommerceMVC.Controllers.Admin
{
    public class AdminController : Controller
    {
        private readonly ProductDbContext _dbContext;
        private readonly IWebHostEnvironment env;
        public AdminController(ProductDbContext dbContext, IWebHostEnvironment env)
        {
            _dbContext = dbContext;
            this.env = env; 

        }
        [HttpGet]   
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductDTO prm)
        {
            if (prm.Image == null)
            {
                ModelState.AddModelError("Image", "The image file is required");
            }
            if (!ModelState.IsValid)
            {
                return View(prm);
            }
            string fileName = "";
            if (prm.Image != null)
            {
                string folder = Path.Combine(env.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "_" + prm.Image.FileName;
                string filePath = Path.Combine(folder, fileName);
                prm.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                var data = new Product()
                {
                    Name = prm.Name,
                    Description = prm.Description,
                    Category = prm.Category,
                    Brand = prm.Brand,
                    Price = prm.Price,
                    CreatedAt = DateTime.Now,
                    Image = fileName
                };
                _dbContext.Products.Add(data);
                _dbContext.SaveChanges();
                return RedirectToAction("List");

            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var data=_dbContext.Products.Find(id);  
            if (data == null)
            {
                return View("List");
            }
            var data2 = new ProductDTO()
            {
                Name= data.Name,
                Description= data.Description,
                Category= data.Category,
                Brand = data.Brand,
                Price = data.Price                
            };
            ViewData["ProductId"] = data.Id;
            ViewData["Image"] = data.Image;
            ViewData["CreatedAt"] = data.CreatedAt.ToString();         
            return View(data2);
        }
        [HttpPost]
        public IActionResult Edit(int id, ProductDTO prm)
        {
            var data = _dbContext.Products.Find(id);
            if (data == null)
            {
                return RedirectToAction("List");
            }
            if (!ModelState.IsValid)
            {
                ViewData["ProductId"] = data.Id;
                ViewData["Image"] = data.Image;
                ViewData["CreatedAt"] = data.CreatedAt.ToString();
                return View(prm);
            }
            string fileName = data.Image;
            if (prm.Image != null)
            {
                string folder = Path.Combine(env.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "_" + prm.Image.FileName;
                string filePath = Path.Combine(folder, fileName);
                prm.Image.CopyTo(new FileStream(filePath, FileMode.Create));

            }
            data.Name = prm.Name;
            data.Description = prm.Description;
            data.Price = prm.Price;
            data.Category = prm.Category;
            data.Image = fileName;
            data.Brand = prm.Brand;
            _dbContext.SaveChanges();
           return RedirectToAction("List");
        }
        [HttpGet]
        public IActionResult List()
        {
            var data=_dbContext.Products.ToList();
            return View(data);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var data = _dbContext.Products.Find(id);
            if (data == null) {
                return RedirectToAction("List");
            }
            //string imagePath=env.WebRootPath+"/Images/"+data.Image;
            //System.IO.File.Delete(imagePath);   
            _dbContext.Products.Remove(data);
            _dbContext.SaveChanges();
            return RedirectToAction("List");
        }

    }
}
