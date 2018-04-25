using StitchFab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StitchFab.Controllers
{
    public class HomeController : Controller
    {
        Bal b = new Bal();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        
        
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Login(int Pid = 0)
        {
            ViewBag.pid = Pid;
            return View();
        }
        public ActionResult LoginPost(Customer c,Tailor t,string login,int pid=0)
        {

            if (login == "Customer")
            {
                Customer c1 = new Customer()
                {
                    Email = c.Email,
                    Password = c.Password
                };
                int id = b.LoginCustomer(c1);
                if(id>0)
                {
                    var obj = b.GetCustomer(id);
                    Session["Customer"] = obj;
                 
                    return RedirectToAction("Product", "Customer", new {Pid = pid });
                  
                }
                ViewBag.Msg = "Username or Password is incorrect";
                return RedirectToAction("Login", "Home", new { Pid = pid });
            }
            else if(login=="Tailor")
            {
                Tailor t1 = new Tailor()
                {
                    Email = t.Email,
                    Password = t.Password
                };
                int id = b.LoginTailor(t1);
                if (id > 0)
                {
                    var obj = b.GetTailor(id);
                    Session["Tailor"] = obj;
                    return RedirectToAction("Index", "Tailor");
                }
                ViewBag.Msg = "Username or Password is incorrect";
                return RedirectToAction("Login", "Home",new {Pid=pid });
            }
            else
            {
                ViewBag.Msg = "Username or Password is incorrect";
                return RedirectToAction("Login", "Home", new { Pid = pid });
            }           
        }
        public ActionResult SignUpPost(Customer c,Tailor t, string signup)
        {           
            if(signup=="Customer")
            {
                if (c != null || c.Cid != 0)
                {
                    int id = b.SignupCustomer(c);
                    var obj = b.GetCustomer(id);
                    if (id == -99)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        Session["Customer"] = obj;
                        if (Session["Customer"] != null)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return RedirectToAction("Product", "Customer");
                        }
                    }
                }                            
            }
            else
            {
                if (t != null || t.Tid != 0)
                {

                    int id = b.SignupTailor(t);
                    var obj = b.GetTailor(id);
                    if (id == -99)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        Session["Tailor"] = obj;
                        if (Session["Tailor"] != null)
                        {
                            return RedirectToAction("ProfilePic", "Tailor");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Tailor");
                        }
                    }
                }
                
            }
            return RedirectToAction("SignUp", "Home");
        }
        
        public ActionResult SignUp()
        {
            return View();
        }
       public ActionResult Logout()
        {
           if (Session["Tailor"] != null)
                Session.Abandon();
           else
                Session["Customer"] = null;
           return RedirectToAction("Index", "Home");

        }
    }
}