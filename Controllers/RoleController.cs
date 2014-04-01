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
    public class RoleController : Controller
    {
        //
        // GET: /Action/

        [Inject]
        public IControllerService ControllerService { get; set; }

        [Inject]
        public IActionService ActionService { get; set; }

        [Inject]
        public IRoleService RoleService { get; set; }

        public ActionResult Index()
        {
            var model = new RoleSearchViewModel();
            ViewBag.Data_ControllerID = Utilities.GetSelectListData(
             ControllerService.GetALL(), x => x.ID, x => x.Name, true);
            return View(model);
        }

        public ActionResult getall(RoleSearchViewModel model, int page = 1, int rows = 10)
        {
            var query = RoleService.GetALL();


            if (!string.IsNullOrEmpty(model.SearchName))
            {
                query = query.Where(x => x.Name.Contains(model.SearchName));
            }

            var count = query.Count();

            var data = query.Select(x => new RoleListViewModel()
            {
                ID = x.ID,
                Description = x.Description,
                Name = x.Name
            })
                .OrderBy(x => x.ID)
                .Skip((page - 1) * rows)
                .Take(rows).ToList();

            var obj = new
            {
                rows = data,
                total = count
            };
            return Json(obj);
        }

        public ActionResult Create()
        {
            var model = new RoleViewModel();
            ViewBag.ActionID_LoadUrl = Url.Action("GetActionComboTree", "AjaxService");
            ViewBag.ActionID_Prefix = "c_";
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RoleViewModel model)
        {
            ServiceResult result = new ServiceResult();
            if (ModelState.IsValid)
            {
                try
                {
                    RoleService.Create(model);
                    result.Message = "添加操作成功！";
                    LogHelper.WriteLog("添加操作成功");
                }
                catch (Exception ex)
                {
                    result.Message = Utilities.GetInnerMostException(ex);
                    result.AddServiceError(result.Message);
                    LogHelper.WriteLog("添加操作错误", ex);
                }
            }
            else
            {
                result.Message = "请检查表单是否填写完整！";
                result.AddServiceError("请检查表单是否填写完整！");

            }

            return Json(result);
        }

        public ActionResult Edit(int ID)
        {
            var entity = RoleService.GetALL().Include(x => x.Action).Single(x => x.ID == ID);
            var model = new RoleViewModel()
            {
                ID = entity.ID,
                Description = entity.Description,
                Name = entity.Name
            };
            string ActionID = string.Join(",", entity.Action.Select(x => x.ID.ToString()));
            //ViewBag.ActionID_LoadUrl = Url.Action("GetActionGroupCombox", "AjaxService", new { ActionID = ActionID });
            ViewBag.ActionID_Prefix = "c_";
            ViewBag.ActionID_LoadUrl = Url.Action("GetActionComboTree", "AjaxService", new { ActionID = ActionID });
            return PartialView(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RoleViewModel model)
        {
            ServiceResult result = new ServiceResult();
            if (ModelState.IsValid)
            {
                try
                {
                    RoleService.Update(model);
                    result.Message = "编辑操作成功！";
                    LogHelper.WriteLog("编辑操作成功");
                }
                catch (Exception ex)
                {
                    result.Message = Utilities.GetInnerMostException(ex);
                    result.AddServiceError(result.Message);
                    LogHelper.WriteLog("编辑操作错误", ex);
                }
            }
            else
            {
                result.Message = "请检查表单是否填写完整！";
                result.AddServiceError("请检查表单是否填写完整！");

            }
            return Json(result);
        }

        public ActionResult Delete(int ID)
        {
            ServiceResult result = new ServiceResult();
            var entity = RoleService.Find(ID);
            try
            {
                RoleService.Delete(entity);
                result.Message = "删除操作成功！";
                LogHelper.WriteLog("删除操作成功");
            }
            catch (Exception ex)
            {
                result.Message = Utilities.GetInnerMostException(ex);
                result.AddServiceError(result.Message);
                LogHelper.WriteLog("删除操作错误", ex);
            }
            return Json(result);
        }

    }

}
