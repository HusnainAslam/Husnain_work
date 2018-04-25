using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StitchFab.Models
{
    public class Cart
    {
        public int Cart_Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public int Quantity { get; set; }
        public int C_Id { get; set; }
        public int T_Id { get; set; }
        public string Img { get; set; }
    }
}