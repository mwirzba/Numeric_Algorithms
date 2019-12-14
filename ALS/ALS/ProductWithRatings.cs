using System;
using System.Collections.Generic;
using System.Text;

namespace ALS
{
    public class ProductWithRatings
    {
        public long ProductId { get; set; }
        public List<CustomerRating> customersRatings { get; set; }
    }
}
