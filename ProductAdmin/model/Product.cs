
using System.Collections.Generic;
namespace ProductAdmin.model
{
    public class Product
    {
        public int Id { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
    }

   
}
