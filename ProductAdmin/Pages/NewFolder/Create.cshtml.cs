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

namespace ProductAdmin.Pages.NewFolder
{
    public class CreateModel : PageModel
    {
        private readonly ProductAdmin.data.ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; }

        [BindProperty]
        public List<int> SelectedCategoryIds { get; set; }

        public List<Category> Categories { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Load all categories to populate the selection list
            Categories = await _context.Categories.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // If the model state is not valid, reload the categories for the form
                Categories = await _context.Categories.ToListAsync();
                return Page();
            }

            // Load the selected categories based on the selected IDs
            var selectedCategories = await _context.Categories
                                                   .Where(c => SelectedCategoryIds.Contains(c.Id))
                                                   .ToListAsync();

            // Add the selected categories to the product
            Product.Categories = selectedCategories;

            // Save the product to the database
            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            // Redirect to the index page after successful creation
            return RedirectToPage("./Index");
        }
    }
}
