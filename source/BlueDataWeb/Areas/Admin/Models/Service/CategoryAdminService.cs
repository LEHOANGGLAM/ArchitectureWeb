using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlueDataWeb.Models.Entites;

namespace BlueDataWeb.Areas.Admin.Models.Service
{
    public class CategoryAdminService
    {
        public List<Category> getlstcategory(List<Category> lstcategory, int? categoryID)
        {
            Category categorylevel1 = lstcategory.Where(m => m.CategoryID == categoryID && m.ParentCategoryID == 0).FirstOrDefault();
            Category category = lstcategory.Where(m => m.CategoryID == categoryID).FirstOrDefault();
            List<Category> lstcate = new List<Category>();
            if (categorylevel1 != null && categorylevel1.ParentCategoryID == 0)
            {
                lstcate.Add(categorylevel1);
                List<Category> lstcategorylevel2 = lstcategory.Where(m => m.ParentCategoryID == categorylevel1.CategoryID).ToList();
                foreach (Category cateLeve2ID in lstcategorylevel2)
                {
                    lstcate.Add(cateLeve2ID);
                    List<Category> lstcategorylevel3 = lstcategory.Where(m => m.ParentCategoryID == cateLeve2ID.CategoryID).ToList();
                    lstcate.AddRange(lstcategorylevel3);
                }
            }
            else
            {
                lstcate.Add(category);
                var lstCategorylv2 = lstcategory.Where(m => m.ParentCategoryID == category.CategoryID);
                //nêu cate levl2
                if (lstCategorylv2.Count() > 0)
                {
                    lstcate.AddRange(lstCategorylv2);
                }
            }

            return lstcate;
        }

        public List<Product> FindPrductlist(List<Category> categorylist, List<Product> lstproduct)
        {
            List<Product> productlist = new List<Product>();
            foreach (var c in categorylist)
            {
                var pro = lstproduct.Where(p => p.CategoryID == c.CategoryID);
                foreach (var p in pro)
                {
                    productlist.Add(p);
                }
            }
            return productlist;
        }
    }
}