using System.Collections.Generic;
namespace ProductAdmin.model
{
    public class ProductDTOcs
    {
        public int Id { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<int> SelectedCategoryIds { get; set; } = new List<int>();
        public List<int> CategoryIds { get; set; } = new List<int>();
        public List<Category> AvailableCategories { get; set; } = new List<Category>();
            }
}

