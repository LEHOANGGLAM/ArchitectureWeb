using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using BlueDataWeb.Models.Entites;

namespace BlueDataWeb.Models.Service
{
    public static class ProductService
    {
        //public void UpdateQuantity(BlueDataWebEntities db, Product product, int Soluong)
        //{
        //    product.Quantity = product.Quantity - Soluong;
        //    db.Entry(product).State = EntityState.Modified;
        //    db.SaveChanges();
        //}

        //public void UpdateQuantityColor(BlueDataWebEntities db, int? colorid , int Soluong)
        //{
        //    Color color = db.Colors.Where(m => m.Id == colorid).FirstOrDefault();
        //    Product product = db.Products.Where(m => m.ProductID == color.ProductId).FirstOrDefault();
        //    color.QuantityColor = color.QuantityColor - Soluong;
        //    product.Quantity = product.Quantity - Soluong;
        //    db.Entry(product).State = EntityState.Modified;
        //    db.Entry(color).State = EntityState.Modified;
        //    db.SaveChanges();
        //}

        public static List<Product> GetLstProductFlowBoPhan(int categoryid)
        {
            using (var context = new BlueDataWebEntities())
            {
                IQueryable<Product> query = context.Products.Include("Category");
                query = query.Where(x => (x.IsSelected ==null || x.IsSelected==false) && x.CategoryID == categoryid && x.Published == true);
                return query.ToList();
            }
        }

        public static void ListProductFolowCateFull(int categoryid, ref List<Product> listProduct)
        {
            using (var context = new BlueDataWebEntities())
            {
                var lstCategory = context.Categories.Where(m=>(m.IsDeleted ==null || m.IsDeleted==false) && m.ParentCategoryID !=null).ToList();
                if (lstCategory.Where(m => m.ParentCategoryID == categoryid).Count() > 0)
                {
                    foreach (var item in lstCategory.Where(m => m.ParentCategoryID == categoryid))
                    {
                        listProduct.AddRange(GetLstProductFlowBoPhan(item.CategoryID));
                        ListProductFolowCateFull(item.CategoryID, ref listProduct);
                    }
                }
            }
        }

       

    }
}