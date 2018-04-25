using StitchFab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace StitchFab.Controllers
{
    public class TailorController : Controller
    {
        Bal b = new Bal();
        //
        // GET: /Tailor/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TailorProfile()
        {
            if(Session["Tailor"]!=null)
            return View();
            else
            {
                return RedirectToAction("Login", "Home");
            }
            
        }
        public ActionResult Orders()
        {
            return View();
        }
        public ActionResult ViewOrder()
        {
            return View();
        }
        public ActionResult MyProducts()
        {
            if(Session["Tailor"]!=null)
            {
                var obj = (Tailor)Session["Tailor"];
                int tid = obj.Tid;
                List<int> imgIdList = b.ProductImageIdList(tid);
                List<Image> ImageList = b.ProductImageName(imgIdList);
                List<Product> productList = b.ProductList(imgIdList);
                ViewBag.images = ImageList;
                ViewBag.product = productList;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
           
        }
        public ActionResult EditProfile()
        {
            if (Session["Tailor"] != null)
                return View();
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public ActionResult EditProfile2(Tailor t)
        {
            if(Session["Tailor"]!=null)
            {
                var obj = (Tailor)Session["Tailor"];
                Tailor t1 = new Tailor()
                {
                    Tid=obj.Tid,
                    Name=t.Name,
                    Email=t.Email,
                    Password=t.Password,
                    Address=t.Address,
                    CNIC=t.CNIC,
                    Contact=t.Contact,                 
                };
                int id=b.SignupTailor(t1);
                var obj2 = b.GetTailor(t1.Tid);
                Session["Tailor"] = null;
                Session["Tailor"] = obj2;
                return RedirectToAction("Index", "Tailor");

            }
            else
            {
                return RedirectToAction("SignUp", "Home");
            }

            
        }
        
        public ActionResult ProfilePic()
        {  
            
            return View();
        }
        [HttpPost]
        public ActionResult ProfilePicLoad(HttpPostedFileBase Img)
        {
           
            var uniquename="";
            if(Img!=null)
            {
               // var file = Request.Files["Image"];
                if(Img.ContentLength>0)
                {
                    var ext = System.IO.Path.GetExtension(Img.FileName);

                    //Generate a unique name using Guid
                    uniquename = Guid.NewGuid().ToString() + ext;
                    //Get phiscal path of our folder where we want to save photo
                    var rootPath = Server.MapPath("~/Content/template/img/TailorProfile");
                    var fileSavePath = System.IO.Path.Combine(rootPath, uniquename);                                      
                    var t = (Tailor)Session["Tailor"];
                    t.ProfilePicName = uniquename;                   
                    int d = b.UploadPhoto(t);

                    //save upload file to folder
                    Img.SaveAs(fileSavePath);                                                         

                }

            }
            TempData["msg"] = "Registered Successfully";
            return RedirectToAction("Index", "Tailor");
          
        }

        public ActionResult AddNewProducts()
        {
            if(Session["Tailor"]!=null)
            {
                var obj1 = b.GetAllCatagory();
                var obj2 = b.GetAllColour();
                var obj3 = b.GetAllSize();
                ViewBag.Categories = obj1;
                ViewBag.Colors = obj2;
                ViewBag.Sizes = obj3;

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }

        }
        [HttpPost]
        public ActionResult UploadProduct(Category ct, int[] clr_Id, int[] SizeId, HttpPostedFileBase Img1, HttpPostedFileBase Img2, HttpPostedFileBase Img3, HttpPostedFileBase Img4, int price, string P_Name) 
        {
                     
            Image i=new Image();
            var uniquename="";            
               
                if(Img1.ContentLength>0)
                {
                    var ext = System.IO.Path.GetExtension(Img1.FileName);

                    //Generate a unique name using Guid
                    uniquename = Guid.NewGuid().ToString() + ext;

                    //Get phiscal path of our folder where we want to save photo
                    var rootPath = Server.MapPath("~/Content/template/img/TailorProduct");

                    var fileSavePath = System.IO.Path.Combine(rootPath, uniquename);

                    i.Img1 = uniquename;

                    //save upload file to folder
                    Img1.SaveAs(fileSavePath);                                                     
                }
                if(Img2!=null)
                {
                    if (Img2.ContentLength > 0)
                    {
                        var ext = System.IO.Path.GetExtension(Img2.FileName);

                        //Generate a unique name using Guid
                        uniquename = Guid.NewGuid().ToString() + ext;

                        //Get phiscal path of our folder where we want to save photo
                        var rootPath = Server.MapPath("~/Content/template/img/TailorProduct");

                        var fileSavePath = System.IO.Path.Combine(rootPath, uniquename);

                        i.Img2 = uniquename;

                        //save upload file to folder
                        Img2.SaveAs(fileSavePath);
                    }
                }              
                else
                {
                    i.Img2 = "No Image";
                }
                if(Img3!=null)
                {
                    if (Img3.ContentLength > 0)
                    {
                        var ext = System.IO.Path.GetExtension(Img3.FileName);

                        //Generate a unique name using Guid
                        uniquename = Guid.NewGuid().ToString() + ext;

                        //Get phiscal path of our folder where we want to save photo
                        var rootPath = Server.MapPath("~/Content/template/img/TailorProduct");

                        var fileSavePath = System.IO.Path.Combine(rootPath, uniquename);

                        i.Img3 = uniquename;

                        //save upload file to folder
                        Img3.SaveAs(fileSavePath);
                    }
                }
                
                else
                {
                    i.Img3 = "No Image";
                }
             if(Img4!=null)
                {
                    if (Img4.ContentLength > 0)
                    {
                        var ext = System.IO.Path.GetExtension(Img4.FileName);

                        //Generate a unique name using Guid
                        uniquename = Guid.NewGuid().ToString() + ext;

                        //Get phiscal path of our folder where we want to save photo
                        var rootPath = Server.MapPath("~/Content/template/img/TailorProduct");

                        var fileSavePath = System.IO.Path.Combine(rootPath, uniquename);

                        i.Img4 = uniquename;

                        //save upload file to folder
                        Img4.SaveAs(fileSavePath);
                    }
                }
                
                else
                {
                    i.Img4 = "No Image";
                }
                int Imgid = b.SaveImages(i);
                int ctId = b.GetCategoryId(ct);
             //   var clrId = b.GetColorId(clr);
            //    var SizeId = b.GetSizeId(Size_Name);
                var obj = (Tailor)Session["Tailor"];
                int tid = obj.Tid;
                List<Product> plist = new List<Product>();
                foreach (var v in clr_Id)
                {
                    foreach (var u in SizeId)
                    {
                        Product p = new Product()
                        {
                            T_Id = tid,
                            Ct_Id = ctId,
                            Clr_Id = v,
                            Size_Id = u,
                            Img_Id = Imgid,
                            Price= price,
                            P_Name=P_Name
                        };
                        plist.Add(p);
                    }
                }
                int id = b.UploadProduct(plist);


            return RedirectToAction("MyProducts", "Tailor");
        }
        public ActionResult EditProduct(int id)
        {
            if(Session["Tailor"]!=null)
            {
                Product product = b.GetDetailProduct(id);
                List<Colour> SelectColours = b.GetColours(id);
                List<Size> SelectSizes = b.GetSizes(id);
                Category SelectCatagory = b.GetCatagory(product.Ct_Id);
                List<Colour> Colours = b.GetAllColour();
                List<Size> Sizes = b.GetAllSize();
                List<Category> Catagories = b.GetAllCatagory();
                Image img = b.GetIamgeObject(product.Img_Id);

                ViewBag.products = product;
                ViewBag.Sclrs = SelectColours;
                ViewBag.Ssizes = SelectSizes;
                ViewBag.Scategories = SelectCatagory;
                ViewBag.clr = Colours;
                ViewBag.size = Sizes;
                ViewBag.category = Catagories;
                ViewBag.image = img;

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public ActionResult EditProduct2( Category ct, int[] clr_Id, int[] SizeId, HttpPostedFileBase Img1, HttpPostedFileBase Img2, HttpPostedFileBase Img3, HttpPostedFileBase Img4, int price, string P_Name,int ImgId)
        {
            b.delete(ImgId);

            Image i = new Image();
            var uniquename = "";

            if (Img1.ContentLength > 0)
            {
                var ext = System.IO.Path.GetExtension(Img1.FileName);

                //Generate a unique name using Guid
                uniquename = Guid.NewGuid().ToString() + ext;

                //Get phiscal path of our folder where we want to save photo
                var rootPath = Server.MapPath("~/Content/template/img/TailorProduct");

                var fileSavePath = System.IO.Path.Combine(rootPath, uniquename);

                i.Img1 = uniquename;

                //save upload file to folder
                Img1.SaveAs(fileSavePath);
            }
            if (Img2 != null)
            {
                if (Img2.ContentLength > 0)
                {
                    var ext = System.IO.Path.GetExtension(Img2.FileName);

                    //Generate a unique name using Guid
                    uniquename = Guid.NewGuid().ToString() + ext;

                    //Get phiscal path of our folder where we want to save photo
                    var rootPath = Server.MapPath("~/Content/template/img/TailorProduct");

                    var fileSavePath = System.IO.Path.Combine(rootPath, uniquename);

                    i.Img2 = uniquename;

                    //save upload file to folder
                    Img2.SaveAs(fileSavePath);
                }
            }
            else
            {
                i.Img2 = "No Image";
            }
            if (Img3 != null)
            {
                if (Img3.ContentLength > 0)
                {
                    var ext = System.IO.Path.GetExtension(Img3.FileName);

                    //Generate a unique name using Guid
                    uniquename = Guid.NewGuid().ToString() + ext;

                    //Get phiscal path of our folder where we want to save photo
                    var rootPath = Server.MapPath("~/Content/template/img/TailorProduct");

                    var fileSavePath = System.IO.Path.Combine(rootPath, uniquename);

                    i.Img3 = uniquename;

                    //save upload file to folder
                    Img3.SaveAs(fileSavePath);
                }
            }

            else
            {
                i.Img3 = "No Image";
            }
            if (Img4 != null)
            {
                if (Img4.ContentLength > 0)
                {
                    var ext = System.IO.Path.GetExtension(Img4.FileName);

                    //Generate a unique name using Guid
                    uniquename = Guid.NewGuid().ToString() + ext;

                    //Get phiscal path of our folder where we want to save photo
                    var rootPath = Server.MapPath("~/Content/template/img/TailorProduct");

                    var fileSavePath = System.IO.Path.Combine(rootPath, uniquename);

                    i.Img4 = uniquename;

                    //save upload file to folder
                    Img4.SaveAs(fileSavePath);
                }
            }

            else
            {
                i.Img4 = "No Image";
            }
            int Imgid = b.SaveImages(i);
            int ctId = b.GetCategoryId(ct);
            //   var clrId = b.GetColorId(clr);
            //    var SizeId = b.GetSizeId(Size_Name);
            var obj = (Tailor)Session["Tailor"];
            int tid = obj.Tid;
            List<Product> plist = new List<Product>();
            foreach (var v in clr_Id)
            {
                foreach (var u in SizeId)
                {
                    Product p = new Product()
                    {
                        T_Id = tid,
                        Ct_Id = ctId,
                        Clr_Id = v,
                        Size_Id = u,
                        Img_Id = Imgid,
                        Price = price,
                        P_Name = P_Name
                    };
                    plist.Add(p);
                }
            }
            int id = b.UploadProduct(plist);
            return RedirectToAction("MyProducts", "Tailor");
        }
	}
}