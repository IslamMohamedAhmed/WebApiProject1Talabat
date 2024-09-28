using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductWithBrandAndTypeSpecifications : BaseSpecification<Product>
    {
        public ProductWithBrandAndTypeSpecifications(ProductSpecParams Params) : base(P => (!Params.BrandId.HasValue || P.ProductBrandId == Params.BrandId) && (!Params.TypeId.HasValue || P.ProductTypeId == Params.TypeId))
        {
            Includes.Add(i => i.ProductBrand);
            Includes.Add(i => i.ProductType);
            if (!string.IsNullOrEmpty(Params.Sort))
            {
                switch (Params.Sort)
                {
                    case "PriceAsc":
                        AddOrderBy(i => i.Price);
                        break;

                    case "PriceDsc":
                        AddOrderByDescending(i => i.Price);
                        break;
                    default:
                        AddOrderBy(i => i.Name);
                        break;

                }
            }
            else
            {
                AddOrderBy(i => i.Name);
            }

            ApplyPagination(Params.PageSize, Params.PageSize * (Params.PageIndex - 1));

        }

        public ProductWithBrandAndTypeSpecifications(int id) : base(p => p.Id == id)
        {
            Includes.Add(i => i.ProductBrand);
            Includes.Add(i => i.ProductType);
        }
    }
}
