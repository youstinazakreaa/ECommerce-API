using onine.core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onine.core.Specifications
{
    public class ProductwithBrandandType : BaseSpecifications<Product>
    {
        public ProductwithBrandandType(ProductSpecParams parameters)
            : base(p =>
            (string.IsNullOrEmpty(parameters.Search) || p.Name.ToLower().Contains(parameters.Search.ToLower()))
            &&
            (!parameters.brandId.HasValue || p.ProductBrandId == parameters.brandId.Value)
            && 
            (!parameters.typeId.HasValue || p.ProductTypeId == parameters.typeId.Value)
            )
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);

            switch (parameters.sort)
            {
                case "PriceAsc":
                    SetOrderBy(p => p.Price);
                    break;

                case "PriceDesc":
                    SetOrderBy( p => p.Price);
                    break;
                default:
                    SetOrderBy (p => p.Name);
                    break;
            }

            ApplyPagination(
                parameters.PageSize * (parameters.PageIndex - 1),
                parameters.PageSize
                );



        }
        public ProductwithBrandandType(int id) : base(P => P.Id == id)
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);
        }
    }
       
}
