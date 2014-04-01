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
    public class ActionController : Controller
    {
        //
        // GET: /Action/

        [Inject]
        public IControllerService ControllerService { get; set; }

        [Inject]
        public IActionService ActionService { get; set; }

        public ActionResult Index()
        {
            var model = new ActionSearchViewModel();
            ViewBag.Data_ControllerID = Utilities.GetSelectListData(
             ControllerService.GetALL(), x => x.ID, x => x.Name, true);
            return View(model);
        }

        public ActionResult getall(ActionSearchViewModel model, int page = 1, int rows = 10)
        {
            var query = ActionService.GetALL()
                .Include(x => x.Controller);

            if (!string.IsNullOrEmpty(model.ActionName))
            {
                query = query.Where(x => x.Name.Contains(model.ActionName));
            }
            if (model.ControllerID.HasValue)
            {
                query = query.Where(x => x.ControllerID == model.ControllerID.Value);
            }
            var count = query.Count();

            var data = query.Select(x => new ActionListViewModel()
                {
                    ID = x.ID,
                    ControllerID = x.ControllerID,
                    ControllerName = x.Controller.Name,
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
            var model = new ActionViewModel();
            ViewBag.Data_ControllerID = Utilities.GetSelectListData(
                ControllerService.GetALL(), x => x.ID, x => x.Name);
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ActionViewModel model)
        {
            ServiceResult result = new ServiceResult();
            if (ModelState.IsValid)
            {
                try
                {
                    ActionService.Create(model);
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
            var entity = ActionService.Find(ID);
            var model = new ActionViewModel()
            {
                ID = entity.ID,
                Description = entity.Description,
                Name = entity.Name,
                ControllerID = entity.ControllerID
            };
            ViewBag.Data_ControllerID = Utilities.GetSelectListData(
               ControllerService.GetALL(), x => x.ID, x => x.Name, model.ControllerID);
            return PartialView(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ActionViewModel model)
        {
            ServiceResult result = new ServiceResult();
            if (ModelState.IsValid)
            {
                try
                {
                    ActionService.Update(model);
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
            var entity = ActionService.Find(ID);
            try
            {
                ActionService.Delete(entity);
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
