using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StitchFab.Models
{
    public class Measurements
    {
        public int Neck { get; set; }
        public int Chest { get; set; }
        public int Shoulder { get; set; }
        public int H_Shoulder_Right { get; set; }
        public int H_Shoulder_Left { get; set; }
        public int Back_Width { get; set; }
        public int Front_Chest { get; set; }
        public int Stomach { get; set; }
        public int Waist { get; set; }
        public int Sleeves { get; set; }
        public int Wrist { get; set; }
        public int Bicep { get; set; }

        public int H_Back_Length { get; set; }
        public int F_Back_Length { get; set; }
        public int Trouser_Outseam { get; set; }
        public int Trouser_Inseam { get; set; }
        public int Crotch { get; set; }
        public int Thigh { get; set; }
        public int Knee { get; set; }
    }
}