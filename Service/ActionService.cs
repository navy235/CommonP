using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonP.Models;
using CommonP.ViewModels;
using CommonP.Service.Interface;
using Maitonn.Core;

namespace CommonP.Service
{
    public class ActionService : IActionService
    {

        private readonly IUnitOfWork db;

        public ActionService(IUnitOfWork db)
        {
            this.db = db;
        }

        public IQueryable<Models.Action> GetALL()
        {
            return db.Set<Models.Action>();
        }

        public IQueryable<Models.Action> GetKendoALL()
        {
            db.SetProxyCreationEnabledFlase();
            return db.Set<Models.Action>();
        }

        public void Create(Models.Action model)
        {
            db.Add<Models.Action>(model);
            db.Commit();
        }

        public Models.Action Create(ActionViewModel model)
        {
            var entity = new Models.Action();
            entity.ControllerID = model.ControllerID;
            entity.Name = model.Name;
            entity.Description = model.Description;
            db.Add<Models.Action>(entity);
            db.Commit();
            return entity;
        }

        public void Update(Models.Action model)
        {
            var target = Find(model.ID);
            db.Attach<Models.Action>(target);
            target.Name = model.Name;
            target.ControllerID = model.ControllerID;
            target.Description = model.Description;
            db.Commit();
        }

        public Models.Action Update(ActionViewModel model)
        {

            var entity = Find(model.ID);
            db.Attach<Models.Action>(entity);
            entity.ControllerID = model.ControllerID;
            entity.Name = model.Name;
            entity.Description = model.Description;
            db.Commit();
            return entity;
        }

        public void Delete(Models.Action model)
        {
            var target = Find(model.ID);
            db.Remove<Models.Action>(target);
            db.Commit();
        }

        public Models.Action Find(int ID)
        {
            return db.Set<Models.Action>().Single(x => x.ID == ID);
        }
    }
}