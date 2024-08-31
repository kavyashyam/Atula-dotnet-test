using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProductAdmin.data;
using ProductAdmin.model;
using Pomelo.EntityFrameworkCore.MySql;


namespace ProductAdmin.Pages.NewFolder
{
    public class IndexModel : PageModel
    {
        private readonly ProductAdmin.data.ApplicationDbContext _context;

        public IndexModel(ProductAdmin.data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; } = default!;
        public IList<Category> Category { get;set; }= default!;

        public async Task OnGetAsync()
        {
            //Product = await _context.Products.ToListAsync();
            Product = await _context.Products
                                     .Include(p => p.Categories)
                                     .ToListAsync();
        }
    }
}
