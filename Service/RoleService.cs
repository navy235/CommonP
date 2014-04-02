using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonP.Models;
using CommonP.ViewModels;
using CommonP.Service.Interface;
using Maitonn.Core;
using CommonP.Utils;
using Kendo.Mvc.Extensions;

namespace CommonP.Service
{
    public class RoleService : IRoleService
    {

        private readonly IUnitOfWork db;

        private readonly IActionService ActionService;

        public RoleService(IUnitOfWork db, IActionService ActionService)
        {
            this.db = db;
            this.ActionService = ActionService;
        }
        public IQueryable<Role> GetALL()
        {
            return db.Set<Role>();
        }

        public IQueryable<Role> GetKendoALL()
        {
            db.SetProxyCreationEnabledFlase();
            return db.Set<Role>();
        }

        public void Create(Role model)
        {
            db.Add<Role>(model);
            db.Commit();
        }

        public Role Create(RoleViewModel model)
        {
            var entity = new Role();
            entity.Name = model.Name;
            entity.Description = model.Description;
            if (!string.IsNullOrEmpty(model.ActionID))
            {
                var ActionArray = Utilities.GetIdList(model.ActionID);
                var ActionList = ActionService.GetALL().Where(x => ActionArray.Contains(x.ID));
                entity.Action.AddRange(ActionList);
            }
            db.Add<Role>(entity);
            db.Commit();
            return entity;
        }

        public void Update(Role model)
        {
            var target = Find(model.ID);
            db.Attach<Role>(target);
            target.Name = model.Name;
            target.Description = model.Description;
            db.Commit();
        }

        public Role Update(RoleViewModel model)
        {

            var entity = Find(model.ID);
            db.Attach<Role>(entity);
            entity.Name = model.Name;
            entity.Description = model.Description;
            var ActionArray = new List<int>();
            if (string.IsNullOrEmpty(model.ActionID))
            {
                entity.Action = new List<CommonP.Models.Action>();
            }
            else
            {
                ActionArray = Utilities.GetIdList(model.ActionID);
                var ActiontList = ActionService.GetALL().Where(x => ActionArray.Contains(x.ID));
                var currentActionArray = entity.Action.Select(x => x.ID).ToList();
                foreach (CommonP.Models.Action ac in ActionService.GetALL())
                {
                    if (ActionArray.Contains(ac.ID))
                    {
                        if (!currentActionArray.Contains(ac.ID))
                        {
                            entity.Action.Add(ac);
                        }
                    }
                    else
                    {
                        if (currentActionArray.Contains(ac.ID))
                        {
                            entity.Action.Remove(ac);
                        }
                    }
                }
            }
            db.Commit();
            return entity;
        }

        public void Delete(Role model)
        {
            var target = Find(model.ID);
            db.Remove<Role>(target);
            db.Commit();
        }

        public Role Find(int ID)
        {
            return db.Set<Role>().Single(x => x.ID == ID);
        }
    }
}