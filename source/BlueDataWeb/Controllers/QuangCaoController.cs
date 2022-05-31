using BlueDataWeb.Models.Entites;
using System.Linq;
using System.Web.Mvc;

namespace BlueDataWeb.Controllers
{
    public class QuangCaoController : Controller
    {
        private BlueDataWebEntities db = new BlueDataWebEntities();
        public ActionResult QuangCaoTop()
        {
            QuangCao qc = db.QuangCaos.Where(x => x.PositionID.Value == 0 && x.IsShow == true).FirstOrDefault();
            return PartialView("_QuangCaoTop", qc);
        }
        public ActionResult QuangCaoCenter()
        {
            QuangCao qc = db.QuangCaos.Where(x => x.PositionID.Value == 1 && x.IsShow == true).FirstOrDefault();
            return PartialView("_QuangCaoCenter", qc);
        }
    }
}