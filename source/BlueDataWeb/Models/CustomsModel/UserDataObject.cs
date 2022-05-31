using BlueDataWeb.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlueDataWeb.Models.Entites
{
    public class UserDataObject
    {
    }
    
   
 
    public partial class Contact
    {
        List<ItemStatus> lstTrangThaiLienHe = (new DataModel()).getTrangThaiLienHe();

        private string _udTrangThaiLienHe = string.Empty;
        public string udTrangThaiLienHe
        {
            get
            {
                if (_udTrangThaiLienHe == string.Empty)
                {
                    ItemStatus item = lstTrangThaiLienHe.Where(p => p.ID == this.Status).FirstOrDefault();
                    _udTrangThaiLienHe = item != null ? item.Value : string.Empty;
                }

                return _udTrangThaiLienHe;
            }
        }
    }

}