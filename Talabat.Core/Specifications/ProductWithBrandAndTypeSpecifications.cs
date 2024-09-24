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
        public ProductWithBrandAndTypeSpecifications(string Sort) : base() {
            Includes.Add(i => i.ProductBrand);
            Includes.Add(i => i.ProductType);
            if (!string.IsNullOrEmpty(Sort))
            {
                switch (Sort)
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

        }

        public ProductWithBrandAndTypeSpecifications(int id) : base(p=>p.Id == id)
        {
            Includes.Add(i => i.ProductBrand);
            Includes.Add(i => i.ProductType);
        }
    }
}
