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
    public class GroupController : Controller
    {
        //
        // GET: /Action/

        [Inject]
        public IControllerService ControllerService { get; set; }

        [Inject]
        public IActionService ActionService { get; set; }

        [Inject]
        public IGroupService GroupService { get; set; }

        public ActionResult Index()
        {
            var model = new GroupSearchViewModel();
            ViewBag.Data_ControllerID = Utilities.GetSelectListData(
             ControllerService.GetALL(), x => x.ID, x => x.Name, true);
            return View(model);
        }

        public ActionResult getall(GroupSearchViewModel model, int page = 1, int rows = 10)
        {
            var query = GroupService.GetALL();


            if (!string.IsNullOrEmpty(model.SearchName))
            {
                query = query.Where(x => x.Name.Contains(model.SearchName));
            }

            var count = query.Count();

            var data = query.Select(x => new GroupListViewModel()
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
            var model = new GroupViewModel();
            ViewBag.RoleID_LoadUrl = Url.Action("GetRoleCombox", "AjaxService");
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GroupViewModel model)
        {
            ServiceResult result = new ServiceResult();
            if (ModelState.IsValid)
            {
                try
                {
                    GroupService.Create(model);
                    result.Message = "添加群组成功！";
                    LogHelper.WriteLog("添加群组成功");
                }
                catch (Exception ex)
                {
                    result.Message = Utilities.GetInnerMostException(ex);
                    result.AddServiceError(result.Message);
                    LogHelper.WriteLog("添加群组错误", ex);
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
            var entity = GroupService.GetALL()
                .Include(x => x.Role).Single(x => x.ID == ID);
            string RoleID = string.Join(",", entity.Role.Select(x => x.ID.ToString()));
            var model = new GroupViewModel()
            {
                ID = entity.ID,
                Description = entity.Description,
                Name = entity.Name,
                RoleID = RoleID
            };

            ViewBag.RoleID_LoadUrl = Url.Action("GetRoleCombox", "AjaxService", new { RoleID = RoleID });
            return PartialView(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GroupViewModel model)
        {
            ServiceResult result = new ServiceResult();
            if (ModelState.IsValid)
            {
                try
                {
                    GroupService.Update(model);
                    result.Message = "编辑群组成功！";
                    LogHelper.WriteLog("编辑群组成功");
                }
                catch (Exception ex)
                {
                    result.Message = Utilities.GetInnerMostException(ex);
                    result.AddServiceError(result.Message);
                    LogHelper.WriteLog("编辑群组错误", ex);
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
            var entity = GroupService.Find(ID);
            try
            {
                GroupService.Delete(entity);
                result.Message = "删除群组成功！";
                LogHelper.WriteLog("删除群组成功");
            }
            catch (Exception ex)
            {
                result.Message = Utilities.GetInnerMostException(ex);
                result.AddServiceError(result.Message);
                LogHelper.WriteLog("删除群组错误", ex);
            }
            return Json(result);
        }

    }

}
