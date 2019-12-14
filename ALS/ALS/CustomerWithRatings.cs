using System;
using System.Collections.Generic;
using System.Text;

namespace ALS
{
    class CustomerWithRatings
    {
        public string  Customer { get; set; }
        public Dictionary<long,double> ProductsRatings { get; set; }
    }
}
