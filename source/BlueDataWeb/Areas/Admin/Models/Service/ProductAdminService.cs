using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using BlueDataWeb.Models.Entites;

namespace BlueDataWeb.Areas.Admin.Models.Service
{
    public class ProductAdminService
    {
        // update phi ship
        public void UpdateMultiShip(FormCollection c, BlueDataWebEntities db)
        {
            int i = 0;
            var MaKHArray = c.GetValues("item.ID");
            var price = c.GetValues("item.Price");
            for (i = 0; i < MaKHArray.Count(); i++)
            {
                int ma = int.Parse(MaKHArray[i].ToString());
                Ship ship = db.Ships.Where(m => m.ID == ma).FirstOrDefault();

                ship.Price = price[i].ToString() != "" ? Convert.ToInt32(price[i]) : 0;
                //db.Entry(thuoctinhsanpham).State = EntityState.Modified;
            }

            db.SaveChanges();
        }

        // them phi ship
        public void CreateShip(FormCollection collection, int productid, BlueDataWebEntities db)
        {

            for (int i = 1; i <= 6; i++)
            {
                double price = 0;
                string provice = string.Empty;
                if (i == 1)
                {

                    price = (collection["zone1"] == "" ? 0 : Convert.ToDouble(collection["zone1"]));

                    provice = "69,61, 60,68,65, 63,59,58, 72, 53, 52, 45, 41, 35, 38, 31, 23, 21,15,14, 12,56,55";

                }

                if (i == 2)
                {
                    price = (collection["zone2"] == "" ? 0 : Convert.ToDouble(collection["zone2"]));
                    provice = "71,2";

                }

                if (i == 3)
                {
                    price = (collection["zone3"] == "" ? 0 : Convert.ToDouble(collection["zone3"]));
                    provice = "62,51,16,40,42,29,28,24,27,26,55,67,64,57,54,45,49,47,43,44,36,39,37,34,33,32,30,22,19,18,17,13,10,46, 70, 25, 11";

                }
                if (i == 4)
                {
                    price = (collection["zone4"] == "" ? 0 : Convert.ToDouble(collection["zone4"]));
                    provice = "144,145,138";

                }
                if (i == 5)
                {
                    price = (collection["zone4"] == "" ? 0 : Convert.ToDouble(collection["zone4"]));
                    provice = "146, 149, 856, 147";

                }
                if (i == 6)
                {
                    price = (collection["zone1"] == "" ? 0 : Convert.ToDouble(collection["zone1"]));
                    provice = "127,128,129,130,131,132,133,134,135,136,137,141, 142, 139, 143,140";

                }

                Ship ship = new Ship()
                {
                    Price = price,
                    ProductID = productid,
                    ProviceDistrictID = provice,
                    ZoneID = i
                };
                db.Ships.Add(ship);
            }
            db.SaveChanges();
        }

        // search
        public List<Product> GetProductlist(List<Product> lstpro, string txtsearch = null)
        {
            //var lstpro = db.Products.ToList();
            List<Product> LstProduct = new List<Product>();
            var Result = lstpro.Where(m => m.EndDate >= DateTime.Now).ToList();
            if (txtsearch != null)
            {
                string a = txtsearch;
                string s = Utility.HelperSEO.convertToUnSign3(a).ToUpper();
                //    //Tách chuỗi

                string[] arrayStr = s.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < arrayStr.Count(); i++)
                {
                    Result = Result.FindAll(
                delegate(Product math)
                {
                    if (Utility.HelperSEO.convertToUnSign3(math.ProductName).ToUpper().Contains(arrayStr[i]))
                        return true;
                    else
                        return false;
                }
                     );
                }

            }
            return Result;
        }

        public void UpdateSiteMap(string OldUrl, string NewUrl)
        {
            string path = System.Web.HttpContext.Current.Server.MapPath("~/sitemap.xml");
            XDocument xmlFile = XDocument.Load(path);
            XmlDocument xmlFile1 = new XmlDocument();
            xmlFile1.LoadXml(xmlFile.ToString());
            XmlNodeList nodes = xmlFile1.DocumentElement.ChildNodes;
            foreach (XmlNode childnode in nodes)
            {
                foreach (XmlNode node in childnode.ChildNodes)
                {
                    if (node.Name == "loc")
                    {
                        if (node.InnerText == OldUrl)
                        {
                            node.InnerText = NewUrl;
                        }
                    }
                }
            }

            xmlFile1.Save(path);
        }

        public void AddSiteMap(string NewUrl)
        {
            //clone node - loi.nguyen 20131220
            //XmlDocument vidDocument = new XmlDocument();
            //vidDocument.Load(path);
            //XmlNamespaceManager manager = new XmlNamespaceManager(vidDocument.NameTable);
            //manager.AddNamespace("ns", "http://www.sitemaps.org/schemas/sitemap/0.9");
            //XmlNode node = vidDocument.SelectNodes("//ns:url", manager)[1];

            //XmlNode newnode = node.CloneNode(true);
            //// update element values for the new node
            //newnode.SelectSingleNode("loc").InnerText = NewUrl;
            //newnode.SelectSingleNode("changefreq").InnerText = "daily";
            //newnode.SelectSingleNode("priority").InnerText = ""daily"";

            //// append the new node to the document
            //vidDocument.DocumentElement.AppendChild(newnode);
            //add node
            string path = System.Web.HttpContext.Current.Server.MapPath("~/sitemap.xml");
          
            XDocument xDoc = XDocument.Load(path);
            XNamespace ns = xDoc.Root.Name.Namespace;
            XElement urlset = xDoc.Element(ns + "urlset");
            urlset.Add(
                    new XElement(ns + "url",
                        new XElement(ns + "loc",
                            NewUrl),
                             new XElement(ns + "changefreq",
                            "daily"),
                             new XElement(ns + "priority",
                            "0.80")));
            //xDoc.Save(path);
        }

        public void DeleteSiteMap()
        {
            string path = System.Web.HttpContext.Current.Server.MapPath("~/sitemap.xml");
            XDocument xmlFile = XDocument.Load(path);
            XmlDocument xmlFile1 = new XmlDocument();
            xmlFile1.LoadXml(xmlFile.ToString());
            XmlNodeList nodes = xmlFile1.DocumentElement.ChildNodes;
            foreach (XmlNode childnode in nodes)
            {
                foreach (XmlNode node in childnode.ChildNodes)
                {
                    if (node.Name == "loc")
                    {
                        if (node.InnerText == "loinguyen")
                        {
                            xmlFile1.DocumentElement.RemoveChild(childnode);
                           // xmlFile1.Save(path);
                            break;
                        }
                    }
                }
            }
        }

    }
}