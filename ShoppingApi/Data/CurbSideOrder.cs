using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApi.Data
{
    public enum CurbSideOrderStatus { Processing,Approved}
    public class CurbSideOrder
    {
        public int Id { get; set; }
        public string For { get; set; }
        public string Items { get; set; }
        public DateTime? PickupDate { get; set; }
        public CurbSideOrderStatus Status { get; set; }
    }
}
