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
        public ProductWithBrandAndTypeSpecifications() : base() {
            Includes.Add(i => i.ProductBrand);
            Includes.Add(i => i.ProductType);


        }

        public ProductWithBrandAndTypeSpecifications(int id) : base(p=>p.Id == id)
        {
            Includes.Add(i => i.ProductBrand);
            Includes.Add(i => i.ProductType);
        }
    }
}
