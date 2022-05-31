using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using BlueDataWeb.Models.Entites;

namespace BlueDataWeb.Models.CustomsModel
{
    public class UpdateDate
    {
        public void Updateday(List<Product> lstproduct, BlueDataWebEntities db)
        {
            foreach (var product in lstproduct)
            {
                if (product.EndDate != null && product.DateCreated != null)
                {
                    double time = (product.EndDate.Value - product.DateCreated.Value).TotalDays;
                    if (time > 0)
                    {
                        product.EndDate = product.EndDate.Value.AddDays(time);
                        db.Entry(product).State = EntityState.Modified;
                    }
                }
            }

            db.SaveChanges();
        }
    }
}