using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShoppingApplication
{
    interface IProduct
    {
        int ProdID { get; set; }
        string ItemName { get; set; }
        double ItemPrice { get; set; }
    }
}
