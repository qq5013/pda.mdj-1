using System;
using System.Collections.Generic;
using System.Text;

namespace THOK.WES.Interface.Model
{
    public class Detail
    {
        public int TaskID { get; set; }
        public string OrderID { get; set; }
        public string OrderType { get; set; }
        public string CellCode { get; set; }
        public string ProductName { get; set; }
        public string PositionName { get; set; }
        public decimal PieceQuantity { get; set; }
        public decimal BarQuantity { get; set; }
        public string Status { get; set; }
    }
}
