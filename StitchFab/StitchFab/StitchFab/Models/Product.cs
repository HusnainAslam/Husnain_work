using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StitchFab.Models
{
    public class Product
    {
        public int P_Id { get; set; }
        public int T_Id { get; set; }
        public int Size_Id { get; set; }
        public int Ct_Id { get; set; }
        public int Clr_Id { get; set; }

        public int Img_Id { get; set; }
        public int Price { get; set; }
        public  string P_Name { get; set; }

    }
}