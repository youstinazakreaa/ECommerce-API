using onine.core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onine.core.Specifications
{
    public class ProductWithFilterationForCount:BaseSpecifications<Product>

    {
        public ProductWithFilterationForCount(ProductSpecParams parameters): base(p=>
        (!parameters.brandId.HasValue || p.ProductBrandId == parameters.brandId.Value)
           &&
        (!parameters.typeId.HasValue || p.ProductTypeId == parameters.typeId.Value) )
         

        {
            
        }
    }
}
