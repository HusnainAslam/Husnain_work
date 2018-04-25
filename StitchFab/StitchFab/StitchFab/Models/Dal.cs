using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace StitchFab.Models
{
    public class Dal
    {
        string connString = @"Data Source=DESKTOP-E0ABGRE;Initial Catalog=StitchFab;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public int SignupCustomer(Customer c)
        {
            using (SqlConnection conn = new SqlConnection(connString))               
            {
                conn.Open();
                int id = 0;
                string query;
                if(c.Cid>0)
                {
                    query = string.Format("update dbo.Customer set C_Name='{0}',C_Email='{1}',C_Password='{2}',C_Address='{3}'C_Contact='{4}',C_CNIC='{5}' where C_Id={6}", c.Name, c.Email, c.Password, c.Address, c.Contact,c.CNIC,c.Cid);
                    SqlCommand cmd = new SqlCommand(query, conn);
                    id = cmd.ExecuteNonQuery();
                    if (id > 0)
                        return c.Cid;
                }
                else
                {
                    query = string.Format("select * from dbo.Customer where C_Email='{0}'",c.Email);
                    SqlCommand cmd2 = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd2.ExecuteReader();
                    if (reader.HasRows)
                    {
                        id = -99;

                    }
                    else if (c.Name != "" && c.Password != "" && c.Email != "" && c.Contact != "" && c.Address != "" && c.CNIC!="")
                    {
                        reader.Close();
                        query = string.Format("INSERT INTO dbo.Customer  Values('{0}','{1}','{2}','{3}','{4}','{5}')", c.Name, c.Email, c.Password, c.Address, c.Contact, c.CNIC);
                        SqlCommand cmd = new SqlCommand(query + "select scope_identity()", conn);
                         id = Convert.ToInt32(cmd.ExecuteScalar());
                         
                    }
                }
                conn.Close();
                return id;
                 
            }          
            
        }

        public int SignupTailor(Tailor t)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                int id = 0;
                string query;
                if (t.Tid > 0)
                {
                    query = string.Format("update dbo.Tailor set T_Name='{0}',T_Email='{1}',T_Password='{2}',T_Address='{3}',T_Contact='{4}',T_CNIC='{5}' where T_Id={6}", t.Name, t.Email, t.Password, t.Address, t.Contact, t.CNIC, t.Tid);
                    SqlCommand cmd = new SqlCommand(query, conn);
                    id = cmd.ExecuteNonQuery();
                    if (id > 0)
                        return t.Tid;
                }
                else
                {
                    query = string.Format("select * from dbo.Tailor where T_Email='{0}'", t.Email);
                    SqlCommand cmd2 = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd2.ExecuteReader();
                    if (reader.HasRows)
                    {
                        id = -99;

                    }
                    else if (t.Name != "" && t.Password != "" && t.Email != "" && t.Contact != "" && t.Address != "" && t.CNIC != "")
                    {
                        reader.Close();
                        query = string.Format("INSERT INTO dbo.Tailor  Values('{0}','{1}','{2}','{3}','{4}','{5}',1)", t.Name, t.Email, t.Password, t.Address, t.Contact, t.CNIC);
                        SqlCommand cmd = new SqlCommand(query + "select scope_identity()", conn);
                        id = Convert.ToInt32(cmd.ExecuteScalar());

                    }
                }
                conn.Close();
                return id;

            }          
        }

        public int LoginCustomer(Customer c)
        {
            int id = 0;
            using (SqlConnection conn=new SqlConnection(connString))
            {
              
                    conn.Open();
                    String query = string.Format("Select * from Customer where C_Email='{0}' and C_Password='{1}'", c.Email, c.Password);
                    SqlCommand cmd = new SqlCommand(query , conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if(reader.Read()==true)
                    {
                        string sid = reader["C_Id"].ToString();
                        id = Convert.ToInt32(sid);
                    }
                    conn.Close();
               
            }
            return id;
        }
        public int LoginTailor(Tailor t)
        {
            int id = 0;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                String query = string.Format("Select * from Tailor where T_Email='{0}' and T_Password='{1}'", t.Email, t.Password);            
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader= cmd.ExecuteReader();
                if (reader.Read() == true)
                {                    
                    string sid = reader["T_Id"].ToString();
                    id = Convert.ToInt32(sid);
                }
                conn.Close();

            }
            return id;
        }
        //tailor Profile pic 
        public int UploadPhoto(Tailor t)
        {

            int id = 0;
            using (SqlConnection conn = new SqlConnection(connString))
            {

                conn.Open();
                String query = string.Format("update tailor set t_ProfilePic='{0}' where t_id={1}", t.ProfilePicName, t.Tid);                                                                                            
                SqlCommand cmd = new SqlCommand(query, conn);
                id = cmd.ExecuteNonQuery();
                if(id>0)
                {
                    return t.Tid;
                }
                conn.Close();                
            }
            return id;
        }

        //get customer who is login
        public Customer GetCustomer(int id)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                Customer c = new Customer();
                string query = string.Format("select * from dbo.Customer where C_ID={0}", id);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                   c.Cid = id;
                    c.Name = reader["C_Name"].ToString();
                    c.Password = reader["C_Password"].ToString();
                    c.Email = reader["C_Email"].ToString();
                    c.Contact = reader["C_Contact"].ToString();
                    c.Address = reader["C_Address"].ToString();
                    c.CNIC = reader["C_CNIC"].ToString();
                }
                return c;
            }
            
        }
        //get Tailor who is login
        public Tailor GetTailor(int id)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                Tailor t= new Tailor();
                string query = string.Format("select * from dbo.Tailor where T_ID={0}", id);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    t.Tid = id;
                    t.Name = reader["T_Name"].ToString();
                    t.Password = reader["T_Password"].ToString();
                    t.Email = reader["T_Email"].ToString();
                    t.Contact = reader["T_Contact"].ToString();
                    t.Address = reader["T_Address"].ToString();
                    t.CNIC = reader["T_CNIC"].ToString();
                    t.ProfilePicName = reader["T_ProfilePic"].ToString();
                    
                }
                conn.Close();
                return t;
            }
            
        }
        // get list of tailors in db
        public List<Tailor> Tailors()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                var model = new List<Tailor>();
                conn.Open();
                String sql = "SELECT * FROM dbo.Tailor";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var t = new Tailor();
                    t.Tid = Convert.ToInt32(reader["T_Id"].ToString());
                    t.Name = reader["T_Name"].ToString();
                    t.ProfilePicName = reader["T_ProfilePic"].ToString();
                    model.Add(t);
                }
                conn.Close();

                return model;
            }
        }
        //get categries from db
        public List<Category> GetAllCatagory()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                var model = new List<Category>();
                conn.Open();
                String sql = "SELECT * FROM dbo.Category";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var ct = new Category();
                    ct.Ct_Name = reader["Ct_Name"].ToString();
                    ct.Ct_Id = Convert.ToInt32(reader["Ct_Id"].ToString());
                    model.Add(ct);
                }

                conn.Close();
                return model;
            }
        }
        //get list of colors from db
        public List<Colour> GetAllColour()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                var model = new List<Colour>();
                conn.Open();
                String sql = "SELECT * FROM dbo.Colour";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var clr = new Colour();
                    clr.Clr_Name = reader["Clr_Name"].ToString();
                    clr.Clr_Id = Convert.ToInt32(reader["Clr_Id"].ToString());
                    model.Add(clr);
                }

                conn.Close();
                return model;
            }
        }
        //get list of colors from db 
        public List<Size> GetAllSize()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                var model = new List<Size>();
                conn.Open();
                String sql = "SELECT * FROM dbo.Size";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var size = new Size();
                    size.Size_Name = reader["Size_Name"].ToString();
                    size.Size_Id = Convert.ToInt32(reader["Size_Id"].ToString());
                    model.Add(size);
                }
                conn.Close();

                return model;
            }
        }
        //insert product images in db 
        public int SaveImages(Image i)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {                
                conn.Open();
                String query = string.Format("INSERT INTO dbo.Image Values('{0}','{1}','{2}','{3}')",i.Img1,i.Img2,i.Img3,i.Img4);
                SqlCommand cmd = new SqlCommand(query + "select scope_identity()", conn);
                int id = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
                return id;
            }            
        }
        //get category id 
        public int GetCategoryId(Category c)
        {
            int id = 0;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = string.Format("Select * from dbo.Category where Ct_Name='{0}'", c.Ct_Name);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read() == true)
                {
                    string sid = reader["Ct_Id"].ToString();
                    id = Convert.ToInt32(sid);
                }
                conn.Close();
            }
            return id ;
        }
        public List<int> GetColorId(List<Colour> clr)
        {
            int id = 0;
            List<int> color = new List<int>();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                foreach (var v in clr)
                {
                    string query = string.Format("Select * from dbo.Colour where Clr_Name='{0}'", v.Clr_Name);
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read() == true)
                    {
                        string sid = reader["Clr_Id"].ToString();
                        id = Convert.ToInt32(sid);
                        color.Add(id);
                    }

                }
                conn.Close();
            }
            return color;

        }
        public List<int> GetSizeId(string[] s)
        {
            int id = 0;
            List<int> sz = new List<int>();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                foreach (var v in s)
                {
                    string query = string.Format("Select * from dbo.Size where Size_Name='{0}'", v);
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read() == true)
                    {
                        string sid = reader["Size_Id"].ToString();
                        id = Convert.ToInt32(sid);
                        sz.Add(id);
                    }

                }
                conn.Close();
            }
            return sz;
        }
        //upload the products in db 
        public int UploadProduct(List<Product> p)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                int id = 0;
                foreach (var u in p)
                {
                    String query = string.Format("INSERT into dbo.Product Values({0},{1},{2},{3},{4},{5},'{6}')", u.T_Id, u.Size_Id, u.Ct_Id, u.Clr_Id, u.Img_Id, u.Price, u.P_Name);
                    SqlCommand cmd = new SqlCommand(query + "select scope_identity()", conn);
                    id = Convert.ToInt32(cmd.ExecuteScalar());
                }
                conn.Close();
                return id;
            }
        }

        //get the images of product of specific tailor
        public List<int> ProductImageIdList(int tid, int ct_id = 0)
        {

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                List<int> lst = new List<int>();
                string query;
                if (ct_id == 0)
                {
                    query = string.Format("Select DISTINCT(Img_Id) from product where T_Id=" + tid);
                }
                else
                {
                    query = string.Format("Select DISTINCT(Img_Id) from product where T_Id={0} AND Ct_Id={1}", tid, ct_id);
                }
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int imgid = Convert.ToInt32(reader["Img_Id"].ToString());
                    lst.Add(imgid);
                }
                conn.Close();
                return lst;
            }


        }
        //get the names of product images
        public List<Image> ProductImageName(List<int> i)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                List<Image>m=new List<Image>();
                
                conn.Open();
                foreach(var s in i)
                {
                    string query = string.Format("Select * from dbo.Image where Image.Img_Id="+s );
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Image m1 = new Image();
                        m1.Img_Id = s;
                        m1.Img1 =reader["Img1"].ToString();
                        m1.Img2 = reader["Img2"].ToString();
                        m1.Img3 = reader["Img3"].ToString();
                        m1.Img4 = reader["img4"].ToString();
                        m.Add(m1);
                    }

                    reader.Close();
                }
                conn.Close();
                return m;
            }

        }
        public List<Product> ProductList(List<int> i)
        {
            
            using (SqlConnection conn = new SqlConnection(connString))
            {
                List<Product>p=new List<Product>();
                conn.Open();
                foreach(var s in i)
                {
                    string query = string.Format("Select TOP 1 * from dbo.Product where Product.Img_Id="+s );
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Product p1 = new Product();
                        p1.P_Id=Convert.ToInt32(reader["P_Id"].ToString());
                        p1.P_Name=reader["P_Name"].ToString();
                        p1.Price=Convert.ToInt32(reader["Price"].ToString());
                        p.Add(p1);
                    }
                    reader.Close();
                }
                conn.Close();
                return p;
            }
            
        }
        public int GetImageId(int pid) //gets a image id from product table on bases of product id this function will be used to get colors and sizes of a specif product
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                int imgId = 0;
                string query = String.Format("select * from Product where Product.P_Id=" + pid);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    imgId = Convert.ToInt32(reader["Img_Id"].ToString());
                }
                conn.Close();
                return imgId;
            }
        }
        public List<Size> GetSizes(int id) //will get sizes of a specific product
        {

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                List<Size> Size = new List<Size>();
                int imgId = GetImageId(id);
                string query = string.Format("select * from SIZE where SIZE.Size_Id IN (select DISTINCT(Size_Id) from Product p where P.Img_Id={0})", imgId);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var v = new Size();
                    v.Size_Id = Convert.ToInt32(reader["Size_Id"].ToString());
                    v.Size_Name = reader["Size_Name"].ToString();
                    Size.Add(v);
                }
                conn.Close();
                return Size;
            }
        }

        public List<Colour> GetColours(int id) //will get colors of a specific product
        {

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                List<Colour> Colour = new List<Colour>();
                int imgId = GetImageId(id);
                string query = string.Format("select * from COLOUR where COLOUR.Clr_Id IN (select DISTINCT(Clr_Id) from Product p where P.Img_Id={0})", imgId);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var v = new Colour();
                    v.Clr_Id = Convert.ToInt32(reader["Clr_Id"].ToString());
                    v.Clr_Name = reader["Clr_Name"].ToString();
                    Colour.Add(v);
                }
                conn.Close();
                return Colour;
            }
        }
        public Product GetDetailProduct(int id) //gets a specific product from product table on product id to use in detailed product view
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                String sql;
                sql = string.Format("SELECT * FROM Product where Product.P_Id=" + id);
                var P = new Product();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    P.P_Id = Convert.ToInt32(reader["P_Id"].ToString());
                    P.Price = Convert.ToInt32(reader["Price"].ToString());
                    P.P_Name = reader["P_Name"].ToString();
                    P.Ct_Id = Convert.ToInt32(reader["Ct_Id"].ToString());
                    P.T_Id = Convert.ToInt32(reader["T_Id"].ToString());
                    P.Size_Id = Convert.ToInt32(reader["Size_Id"].ToString());
                    P.Clr_Id = Convert.ToInt32(reader["Clr_Id"].ToString());
                    P.Img_Id = Convert.ToInt32(reader["Img_Id"].ToString());
                }
                conn.Close();
                return P;
            }
        }
        public Image GetImageObject(int imgId)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                String sql;
                sql = string.Format("SELECT * FROM Image where Image.Img_Id=" + imgId);
                var img = new Image();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    img.Img_Id = imgId;
                    img.Img1 = reader["Img1"].ToString();
                    img.Img2 = reader["Img2"].ToString();
                    img.Img3 = reader["Img3"].ToString();
                    img.Img4 = reader["Img4"].ToString();
                }
                conn.Close();
                return img;
            }
            
        }
        public Category GetCatagory(int ctid)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                String sql;
                sql = string.Format("SELECT * FROM Category where Category.Ct_Id=" + ctid);
                var ctg = new Category();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ctg.Ct_Id = ctid;
                    ctg.Ct_Name = reader["Ct_Name"].ToString();
                }
                conn.Close();
                return ctg;
            }
        }
       
       
        public void delete(int ImgId)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                
                string query = String.Format("delete from Product where P_Id IN (select P_Id from Product where Product.Img_Id={0})",ImgId);
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public List<Product> GetAllProduct(int Tid, int Ct_Id = 0) //will get product from product table on bases of category id and tailor id
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                var model = new List<Product>();
                List<int> ImgListId = ProductImageIdList(Tid, Ct_Id);
                conn.Open();
                String sql;

                foreach (var v in ImgListId)
                {
                    sql = string.Format("SELECT TOP 1 * FROM dbo.Product p where p.Img_Id={0}", v);
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var P = new Product();
                        P.P_Id = Convert.ToInt32(reader["P_Id"].ToString());
                        P.Price = Convert.ToInt32(reader["Price"].ToString());
                        P.P_Name = reader["P_Name"].ToString();
                        P.Ct_Id = Convert.ToInt32(reader["Ct_Id"].ToString());
                        P.T_Id = Convert.ToInt32(reader["T_Id"].ToString());
                        P.Size_Id = Convert.ToInt32(reader["Size_Id"].ToString());
                        P.Clr_Id = Convert.ToInt32(reader["Clr_Id"].ToString());
                        P.Img_Id = Convert.ToInt32(reader["Img_Id"].ToString());
                        model.Add(P);
                    }
                    reader.Close();
                }



                conn.Close();

                return model;
            }
        }
        public int PostMeasurement(Measurements m, int C_id) //Will post measurements in measurement table
        {
            int id = 0;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                int Fab_id = 1;
                String query = string.Format("Insert INTO [Measurements]  Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}')", C_id, Fab_id, m.Neck, m.Chest, m.Shoulder, m.H_Shoulder_Right, m.H_Shoulder_Left, m.Back_Width, m.Front_Chest, m.Stomach, m.Waist, m.Sleeves, m.Wrist, m.Bicep, m.H_Back_Length, m.F_Back_Length, m.Trouser_Outseam, m.Trouser_Inseam, m.Crotch, m.Thigh, m.Knee);
                SqlCommand cmd = new SqlCommand(query + "select scope_identity()", conn);
                id = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
            }

            return id;
        }
        public List<Category> GetCategoryByTailor(int tid) //will get categories from table 
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                var model = new List<Category>();
                conn.Open();
                String sql = string.Format("SELECT * FROM dbo.Category where Ct_Id IN (select DISTINCT(Ct_Id) from PRODUCT where T_Id={0})", tid);
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var ct = new Category();
                    ct.Ct_Name = reader["Ct_Name"].ToString();
                    ct.Ct_Id = Convert.ToInt32(reader["Ct_Id"].ToString());
                    model.Add(ct);
                }


                return model;
            }
        }

        //Add new Item in CArt Table When customer Hit Add to Cart Button
        public int AddToCart(string Img,Customer C,Tailor T,string name, int price, Size size, Colour color, int quantity)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                String sql = string.Format("INSERT INTO Cart VALUES('{0}',{1},'{2}','{3}',{4},{5},{6},'{7}')", name, price, size.Size_Name, color.Clr_Name, quantity,C.Cid,T.Tid,Img);
                SqlCommand cmd = new SqlCommand(sql + "select scope_identity()", conn);
                int id = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
                return id;
            }
        }

        //Move All Values From Item to Cart Table and return a string of ids , seprated
        public string CartToItems(Customer C)
        {
            var obj = ViewCart(C);
            using (SqlConnection conn = new SqlConnection(connString))
            {
                int id = 0;
                string ids = "";
                conn.Open();
                foreach (var v in obj)
                {
                    String sql = string.Format("INSERT INTO Items VALUES('{0}',{1},'{2}','{3}',{4},{5},{6},'{7}')", v.Name, v.Price, v.Size, v.Color, v.Quantity, v.C_Id, v.T_Id, v.Img);
                    SqlCommand cmd = new SqlCommand(sql + "select scope_identity()", conn);
                    id = Convert.ToInt32(cmd.ExecuteScalar());
                    if(ids=="")
                    {
                        ids = Convert.ToString(id);
                    }
                    else
                    {
                        ids =ids+","+ Convert.ToString(id);
                    }
                }
                
                conn.Close();
                return ids;
            }
        }

        //To clean Cart On Placing Order
        public void CleanCart(int Cid)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string query = string.Format("Delete from Items where C_Id=" + Cid);
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        //Funtion placed the Order on Finalizing order By Customer 
        public int PlaceOrder(Customer C)
        {
            string ItemIds = CartToItems(C);
            CleanCart(C.Cid);
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                String sql = string.Format("INSERT INTO Order VALUES('{0}',{1})", ItemIds, C.Cid);
                SqlCommand cmd = new SqlCommand(sql + "select scope_identity()", conn);
                int id = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
                return id;
            }
        }

        //Get and return List  of All items Added by customer in  Cart
        public List<Cart> ViewCart(Customer C)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                    List<Cart> list = new List<Cart>();
                    conn.Open();

                        String sql = string.Format("select * from Cart where C_Id=" + 1);
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Cart ct = new Cart();
                            ct.Cart_Id = Convert.ToInt32(reader["Cart_Id"].ToString());
                            ct.Name = reader["Name"].ToString();
                            ct.Price = Convert.ToInt32(reader["Price"].ToString());
                            ct.Size = reader["Size"].ToString();
                            ct.Color = reader["Color"].ToString();
                            ct.Quantity = Convert.ToInt32(reader["Quantity"].ToString());
                            ct.Img = reader["Img"].ToString();
                            list.Add(ct);
                        }
                   
                    conn.Close();
                    return list;
                }
               
            }

       
       }
}