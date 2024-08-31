using Mapster;

namespace ProductAdmin.model
{
    public class Mappingconfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Product, ProductDTOcs>()
                  .Map(dest => dest.CategoryIds, src => src.Categories.Select(c => c.Id));

            config.NewConfig<ProductDTOcs, Product>()
                  .Map(dest => dest.Categories, src => src.CategoryIds.Select(id => new Category { Id = id }).ToList());
        }
    }
}
