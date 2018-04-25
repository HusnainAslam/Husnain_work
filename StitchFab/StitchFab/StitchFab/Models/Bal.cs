using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StitchFab.Models
{
    public class Bal
    {
        Dal d = new Dal();
        public int SignupCustomer(Customer c)
        {
            return d.SignupCustomer(c);
        }
        public int SignupTailor(Tailor c)
        {
            return d.SignupTailor(c);
        }
        public int LoginCustomer(Customer c)
        {
            return d.LoginCustomer(c);
        }
        public int LoginTailor(Tailor c)
        {
            return d.LoginTailor(c);
        }
        public Customer GetCustomer(int id)
        {
            return d.GetCustomer(id);
        }
        public Tailor GetTailor(int id)
        {
            return d.GetTailor(id);
        }
        public int UploadPhoto(Tailor t)
        {
            return d.UploadPhoto(t);
        }
        public List<Tailor> Tailors()
        {
            return d.Tailors();
        }
        public List<Category> GetAllCatagory()
        {
            return d.GetAllCatagory();
        }
        public List<Colour> GetAllColour()
        {
            return d.GetAllColour();
        }
        public List<Size> GetAllSize()
        {
            return d.GetAllSize();
        }
        public int SaveImages(Image i)
        {
            return d.SaveImages(i);
        }
        public int GetCategoryId(Category c)
        {
            return d.GetCategoryId(c);
        }
        public List<int> GetColorId(List<Colour> clr)
        {
            return d.GetColorId(clr);
        }
        public List<int> GetSizeId(string[] s)
        {
            return d.GetSizeId(s);
        }
        public int UploadProduct(List<Product> p)
        {
            return d.UploadProduct(p);
        }
        public List<int> ProductImageIdList(int tid, int ct_id = 0)
        {
            return d.ProductImageIdList(tid, ct_id);
        }
        public List<Image> ProductImageName(List<int> i)
        {
            return d.ProductImageName(i);
        }
        public List<Product> ProductList(List<int> i)
        {
            return d.ProductList(i);
        }
        public int GetImageId(int pid)
        {
            return d.GetImageId(pid);
        }
        public List<Size> GetSizes(int id)
        {
            return d.GetSizes(id);
        }
        public List<Colour> GetColours(int id)
        {
            return d.GetColours(id);
        }
        public Product GetDetailProduct(int id)
        {
            return d.GetDetailProduct(id);
        }
        public Image GetIamgeObject(int imgId)
        {
            return d.GetImageObject(imgId);
        }
        public Category GetCatagory(int ctid)
        {
            return d.GetCatagory(ctid);
        }
        public void delete(int ImgId)
        {
            d.delete(ImgId);
        }
        public List<Product> GetAllProduct(int Tid, int Ct_Id = 0)
        {
            return d.GetAllProduct(Tid, Ct_Id);
        }
        public int PostMeasurement(Measurements m, int C_id)
        {
            return d.PostMeasurement(m, C_id);
        }
        public List<Category> GetCategoryByTailor(int Tid)
        {
            return d.GetCategoryByTailor(Tid);
        }
        public int AddToCart(string Img,Customer C,Tailor T,string name, int price, Size size, Colour color, int quantity)
        {
            return d.AddToCart(Img,C,T,name, price, size, color, quantity);
        }
        public int PlaceOrder(Customer C)
        {
            return d.PlaceOrder(C);
        }
        public List<Cart> ViewCart(Customer C)
        {
            return d.ViewCart(C);
        }
        public Image GetImageObject(int imgId)
        {
            return d.GetImageObject(imgId);
        }
    }
}