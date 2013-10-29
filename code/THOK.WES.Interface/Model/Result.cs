using System;
using System.Collections.Generic;
using System.Text;

namespace THOK.WES.Interface.Model
{
    public class Result
    {
        public bool IsSuccess = false;
        public string Message = string.Empty;
        public string ProductCode = string.Empty;
        public decimal Quantity;
        public BillDetail[] BillDetails = new BillDetail[] { };
        public ShelfInfo[] ShelfInfo = new ShelfInfo[] { };
        public OutAbnormityBill[] OutAbnormityBill = new OutAbnormityBill[] { };
    }
}
