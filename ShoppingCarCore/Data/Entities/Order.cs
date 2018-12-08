using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCarCore.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [Display(Name = "Is deliveried?")]
        public bool IsDeliveried { get; set; }

        public IEnumerable<OrderDetail> Details { get; set; }
    }
}
