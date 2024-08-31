using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductAdmin.data;
using ProductAdmin.model;
using System.Threading.Tasks;

namespace ProductAdmin.Pages.NewFolder
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public ProductDTOcs Product { get; set; }

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.Categories)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            Product = new ProductDTOcs
            {
                Id = product.Id,
                Sku = product.Sku,
                Name = product.Name,
                SelectedCategoryIds = product.Categories.Select(c => c.Id).ToList(),
                AvailableCategories = await _context.Categories.ToListAsync()
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Product.AvailableCategories = await _context.Categories.ToListAsync();
                return Page();
            }

            var productToUpdate = await _context.Products
                .Include(p => p.Categories)
                .FirstOrDefaultAsync(p => p.Id == Product.Id);

            if (productToUpdate == null)
            {
                return NotFound();
            }

            productToUpdate.Sku = Product.Sku;
            productToUpdate.Name = Product.Name;

            // Update the categories
            productToUpdate.Categories.Clear();
            foreach (var categoryId in Product.SelectedCategoryIds)
            {
                var category = await _context.Categories.FindAsync(categoryId);
                if (category != null)
                {
                    productToUpdate.Categories.Add(category);
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
