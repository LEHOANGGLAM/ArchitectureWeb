using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlueDataWeb.Areas.Admin.Models.Service;
using BlueDataWeb.Models.Entites;
using BlueDataWeb.Utility;
 

namespace BlueDataWeb.Areas.Admin.Models
{
    public static class NewCateHelper
    {
       
        public static List<NewCategory> GetListChild(long rootID, bool includeRoot = true)
        {
             BlueDataWebEntities db = new BlueDataWebEntities();
            var lstAll = db.NewCategories.ToList();
            var result = new List<NewCategory>();
            if (includeRoot)
            {
                result.Add(lstAll.Where(x => x.NewCategoriesID == rootID).First());

            }

            var lstAll_child_root = lstAll.Where(p => p.ParentCategoryID == rootID).ToList();

            foreach (var mp in lstAll_child_root.Where(p => p.ParentCategoryID == rootID))
            {
                result.Add(mp);

                result.AddRange(RenderMenuItem(lstAll, mp));
            }
            return result;
        }

        public static List<NewCategory> RenderMenuItem(List<NewCategory> menuList, NewCategory mi)
        {
            var result = new List<NewCategory>();

            foreach (var cp in menuList.Where(p => p.ParentCategoryID == mi.NewCategoriesID))
            {
                result.Add(cp);
                result.AddRange(RenderMenuItem(menuList, cp));
            }

            return result;
        }

    }
}