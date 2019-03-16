﻿using Core.Aplication.Queries.ProductsByCategory;
using Infrastructure.Model;
using Microsoft.AspNetCore.Mvc;

namespace SahuarOS.Controllers
{
    public class ProductController : Controller
    {
        private readonly SahuarOSEFContext _context;

        public ProductController(SahuarOSEFContext context)
        {
            _context = context;
        }

        public IActionResult Catergory(int id)
        {
            var query = new ProductsByCategoryQuery(_context);
            return Json(query.Execute(id));
        }

        public IActionResult Image(int id)
        {
            return File(_context.Products.Find(id)?.Image, "image/jpeg");
        }
    }
}