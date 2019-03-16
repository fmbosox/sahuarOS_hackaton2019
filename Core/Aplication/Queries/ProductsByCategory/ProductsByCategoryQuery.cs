using System.Collections.Generic;
using System.Linq;
using Core.Model;

namespace Core.Aplication.Queries.ProductsByCategory
{
    public class ProductsByCategoryQuery
    {
        private readonly SahuarOSContext _context;

        public ProductsByCategoryQuery(SahuarOSContext context)
        {
            _context = context;
        }

        public IList<ProductsByCategoryResult> Execute(int categoryId)
        {
            return _context.Products.Where(product => product.Category.Id == categoryId)
                .Select(p => new ProductsByCategoryResult
                {
                    descripciton = p.Description,
                    id = p.Id,
                    name = p.Name,
                    sku = p.SKU
                }).ToList();
        }
    }
}