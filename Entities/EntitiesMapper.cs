using AutoMapper;
using Bookstore.Products;
using System;

namespace Bookstore.Entities
{
    class EntitiesMapper
    {
        private MapperConfiguration config;
        private IMapper mapper;
        public EntitiesMapper()
        {
            config = new MapperConfiguration(
                            cfg =>
                            {
                                cfg.CreateMap<BookEntity, Book>().ReverseMap();
                                cfg.CreateMap<MagazineEntity, Magazine>().ReverseMap();
                            }
                        );
            mapper = config.CreateMapper();
        }
        public ShopProduct GetProductMappedFromEntity(ShopProductEntity entity)
        {
            ShopProduct product;

            string entityClassName = entity.GetType().Name;

            switch (entityClassName)
            {
                case "BookEntity":
                    product = mapper.Map<Book>(entity);
                    break;
                case "MagazineEntity":
                    product = mapper.Map<Magazine>(entity);
                    break;
                default:
                    throw new Exception($"Could not map entity: {entityClassName}");
            }

            return product;
        }
    }
}
