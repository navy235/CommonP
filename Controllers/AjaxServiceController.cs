using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using CommonP.ViewModels;
using CommonP.Service.Interface;
using CommonP.Utils;
using Maitonn.Core;
using System.Data.Entity;

namespace CommonP.Controllers
{
    public class AjaxServiceController : Controller
    {
        [Inject]
        public IControllerService ControllerService { get; set; }

        [Inject]
        public IActionService ActionService { get; set; }

        [Inject]
        public IRoleService RoleService { get; set; }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetActionGroupCombox(string ActionID = null)
        {
            var actions = ActionService.GetALL().Include(x => x.Controller)
                .ToList()
                .Select(x => new GroupSelectListItem()
                {
                    text = x.Name,
                    value = x.ID.ToString(),
                    group = x.Controller.Name

                }).ToList();
            if (!string.IsNullOrEmpty(ActionID))
            {
                var ids = Utilities.GetIdList(ActionID);
                foreach (var id in ids)
                {
                    if (actions.Any(x => x.value == id.ToString()))
                    {
                        actions.Single(x => x.value == id.ToString()).selected = true;
                    }
                }
            }
            return Json(actions, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetActionComboTree(string ActionID = null)
        {
            var ids = Utilities.GetIdList(ActionID);
            var controlls = ControllerService.GetALL()
                .Include(x => x.Action)
                .ToList();

            var list = new List<ComboTreeItem>();
            foreach (var col in controlls)
            {
                var item = new ComboTreeItem()
                {
                    id = "c_" + col.ID.ToString(),
                    text = col.Name,

                    children = col.Action.Select(x => new ComboTreeItem()
                    {
                        id = x.ID.ToString(),
                        text = x.Name,
                        @checked = ids.Contains(x.ID)

                    }).ToList()
                };
                list.Add(item);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}
