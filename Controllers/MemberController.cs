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


namespace CommonP.Groups
{
    public class MemberController : Controller
    {
        //
        // GET: /Member/

        [Inject]
        public IGroupService GroupService { get; set; }

        [Inject]
        public IMemberService MemberService { get; set; }

        public ActionResult Index()
        {
            var model = new MemberSearchViewModel();
            ViewBag.Data_GroupID = Utilities.GetSelectListData(
             GroupService.GetALL(), x => x.ID, x => x.Name, true);
            return View(model);
        }

        public ActionResult getall(MemberSearchViewModel model, int page = 1, int rows = 10)
        {
            var query = MemberService.GetALL()
                .Include(x => x.Group);

            if (!string.IsNullOrEmpty(model.SearchName))
            {
                query = query.Where(x => x.Name.Contains(model.SearchName));
            }
            if (model.GroupID.HasValue)
            {
                query = query.Where(x => x.GroupID == model.GroupID.Value);
            }
            var count = query.Count();

            var data = query.Select(x => new MemberListViewModel()
            {
                ID = x.ID,
                GroupID = x.GroupID,
                GroupName = x.Group.Name,
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
            var model = new MemberViewModel();
            ViewBag.Data_GroupID = Utilities.GetSelectListData(
                GroupService.GetALL(), x => x.ID, x => x.Name);
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MemberViewModel model)
        {
            ServiceResult result = new ServiceResult();
            if (ModelState.IsValid)
            {
                try
                {
                    MemberService.Create(model);
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
            var entity = MemberService.Find(ID);
            var model = new MemberViewModel()
            {
                ID = entity.ID,
                Name = entity.Name,
                GroupID = entity.GroupID
            };
            ViewBag.Data_GroupID = Utilities.GetSelectListData(
               GroupService.GetALL(), x => x.ID, x => x.Name, model.GroupID);
            return PartialView(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MemberViewModel model)
        {
            ServiceResult result = new ServiceResult();
            if (ModelState.IsValid)
            {
                try
                {
                    MemberService.Update(model);
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
            var entity = MemberService.Find(ID);
            try
            {
                MemberService.Delete(entity);
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
