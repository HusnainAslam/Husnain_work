using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StitchFab.Models;


namespace StitchFab.Controllers
{
    public class CustomerController : Controller
    {
        Bal b = new Bal();

        //
        // GET: /Customer/
        public ActionResult Tailor()
        {
            var obj = b.Tailors();

            return View(obj);
        }
        //public ActionResult Product()
        //{
        //    if(Session["login"]==null)
        //    {
        //        RedirectToAction("~/Home/Login");
        //    }
        //    ViewBag.Message = "Your Product page.";

        //    return View();
        //}
        public ActionResult DetailProduct(int id = 0)
        {
            Product product = b.GetDetailProduct(id);
            List<Colour> Colours = b.GetColours(id);
            List<Size> Sizes = b.GetSizes(id);
            int imgId = b.GetImageId(product.P_Id);
            Image img = b.GetImageObject(imgId);
            List<Product> allproduct = b.GetAllProduct(product.T_Id, 0);
            var obj3 = b.ProductImageIdList(product.T_Id, 0);
            var obj4 = b.ProductImageName(obj3);
            ViewBag.product = product;
            ViewBag.colours = Colours;
            ViewBag.size = Sizes;
            ViewBag.Image = img;
            ViewBag.allproduct = allproduct;
            ViewBag.allimages = obj4;
            return View();
        }
        [HttpGet]
        public ActionResult DetailFabric(int id = 0)
        {
            Product product = b.GetDetailProduct(id);
            List<Colour> Colours = b.GetColours(id);
            int imgId = b.GetImageId(product.P_Id);
            Image img = b.GetImageObject(imgId);
            List<Product> allproduct = b.GetAllProduct(product.T_Id, 0);
            var obj3 = b.ProductImageIdList(product.T_Id, 0);
            var obj4 = b.ProductImageName(obj3);
            ViewBag.product = product;
            ViewBag.colours = Colours;
            ViewBag.Image = img;
            ViewBag.allproduct = allproduct;
            ViewBag.allimages = obj4;
            return View();
        }
        [HttpPost]
        public ActionResult DetailFabric2(Customer c, Measurements m)
        {
            // if (Session["Customer"] != null)
            //{
            Measurements measure = new Measurements()
            {
                Neck = m.Neck,
                Chest = m.Chest,
                Shoulder = m.Shoulder,
                H_Shoulder_Right = m.H_Shoulder_Right,
                H_Shoulder_Left = m.H_Shoulder_Left,
                Back_Width = m.Back_Width,
                Front_Chest = m.Front_Chest,
                Stomach = m.Stomach,
                Waist = m.Waist,
                Sleeves = m.Sleeves,
                Wrist = m.Wrist,
                Bicep = m.Bicep,
                H_Back_Length = m.H_Back_Length,
                F_Back_Length = m.F_Back_Length,
                Trouser_Outseam = m.Trouser_Outseam,
                Trouser_Inseam = m.Trouser_Inseam,
                Crotch = m.Crotch,
                Thigh = m.Thigh,
                Knee = m.Knee

            };
           // int C_id = b.GetCustomerId(c);
           // C_id = 1;
           // int id2 = b.PostMeasurement(measure, C_id);
            //  }
            return RedirectToAction("DetailFabric", "Customer");
        }
        public ActionResult Product(int Tid = 0, int Ct_Id = 0)
        {
            var obj1 = b.GetCategoryByTailor(Tid);
            var obj2 = b.GetAllProduct(Tid, Ct_Id);
            var obj3 = b.ProductImageIdList(Tid, Ct_Id);
            var obj4 = b.ProductImageName(obj3);
            ViewBag.Categories = obj1;
            ViewBag.Product = obj2;
            ViewBag.Image = obj4;
            return View();

        }
        public ActionResult AddToCart(Product P,string Img,Tailor T,Size size,Colour color,int quantity)
        {
            if ((Customer)Session["Customer"] != null)
            {
                var obj = (Customer)Session["Customer"];
                 
                var id = b.AddToCart(Img,obj,T,P.P_Name,P.Price,size,color,quantity);
                
                return RedirectToAction("DetailProduct","Customer",new { id=P.P_Id});
            }
            else
            {
                return RedirectToAction("Login", "Home",new {Pid=P.P_Id});
            }
        }
        public ActionResult ViewCart()
        {
            var obj = (Customer)Session["Customer"];
            var list = b.ViewCart(obj);
            ViewBag.ItemList = list;
            return View();
        }
        public ActionResult PlaceOrder()
        {
            var obj = (Customer)Session["Customer"];
            b.PlaceOrder(obj);
            return View();
        }
    }
}