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

namespace CommonP.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        [Inject]
        public IControllerService ControllerService { get; set; }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult list()
        {
            return View();
        }

        public ActionResult getall(int page = 1, int rows = 10)
        {
            var query = ControllerService.GetALL()
                .Select(x => new ControllerListViewModel()
                {
                    Description = x.Description,
                    ID = x.ID,
                    Name = x.Name
                })
                .OrderBy(x => x.ID)
                .Skip((page - 1) * rows)
                .Take(rows).ToList();

            var count = ControllerService.GetALL().Count();

            var data = new
            {
                rows = query,
                total = count
            };
            return Json(data);
        }

        public ActionResult Create()
        {
            var model = new ControllerViewModel();
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ControllerViewModel model)
        {
            ServiceResult result = new ServiceResult();
            TempData["Service_Result"] = result;
            if (ModelState.IsValid)
            {
                try
                {
                    ControllerService.Create(model);
                    result.Message = "添加控制器成功！";
                    LogHelper.WriteLog("添加控制器成功");
                }
                catch (Exception ex)
                {
                    result.Message = Utilities.GetInnerMostException(ex);
                    result.AddServiceError(result.Message);
                    LogHelper.WriteLog("添加控制器错误", ex);
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
            var entity = ControllerService.Find(ID);
            var model = new ControllerViewModel()
            {
                ID = entity.ID,
                Description = entity.Description,
                Name = entity.Name
            };
            return PartialView(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ControllerViewModel model)
        {
            ServiceResult result = new ServiceResult();
            if (ModelState.IsValid)
            {
                try
                {
                    ControllerService.Update(model);
                    result.Message = "编辑控制器成功！";
                    LogHelper.WriteLog("编辑控制器成功");
                }
                catch (Exception ex)
                {
                    result.Message = Utilities.GetInnerMostException(ex);
                    result.AddServiceError(result.Message);
                    LogHelper.WriteLog("编辑控制器错误", ex);
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
            var entity = ControllerService.Find(ID);
            try
            {
                ControllerService.Delete(entity);
                result.Message = "删除控制器成功！";
                LogHelper.WriteLog("删除控制器成功");
            }
            catch (Exception ex)
            {
                result.Message = Utilities.GetInnerMostException(ex);
                result.AddServiceError(result.Message);
                LogHelper.WriteLog("删除控制器错误", ex);
            }
            return Json(result);
        }

    }
}
