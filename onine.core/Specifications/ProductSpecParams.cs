using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onine.core.Specifications
{
    public class ProductSpecParams
    {
        public string? sort { get; set; }
        public int? brandId { get; set; }
        public int? typeId { get; set; }

        public string? Search { get; set; }


        private int pagesize = 5;


        public int PageSize {
            get { return pagesize; }
            set { pagesize = value > 10 ? 10 : value; }

        }
        public int PageIndex { get; set; } = 1;
    }
}
