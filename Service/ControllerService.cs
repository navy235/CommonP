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
    public class ControllerService : IControllerService
    {

        private readonly IUnitOfWork db;

        public ControllerService(IUnitOfWork db)
        {
            this.db = db;
        }

        public IQueryable<Controller> GetALL()
        {
            return db.Set<Controller>();
        }

        public IQueryable<Controller> GetKendoALL()
        {
            db.SetProxyCreationEnabledFlase();
            return db.Set<Controller>();
        }

        public void Create(Controller model)
        {
            db.Add<Controller>(model);
            db.Commit();
        }

        public Controller Create(ControllerViewModel model)
        {
            var entity = new Controller();
            entity.Name = model.Name;
            entity.Description = model.Description;
            db.Add<Controller>(entity);
            db.Commit();
            return entity;
        }

        public void Update(Controller model)
        {
            var target = Find(model.ID);
            db.Attach<Controller>(target);
            target.Name = model.Name;
            target.Description = model.Description;
            db.Commit();
        }

        public Controller Update(ControllerViewModel model)
        {

            var entity = Find(model.ID);
            db.Attach<Controller>(entity);
            entity.Name = model.Name;
            entity.Description = model.Description;
            db.Commit();
            return entity;
        }

        public void Delete(Controller model)
        {
            var target = Find(model.ID);
            db.Remove<Controller>(target);
            db.Commit();
        }

        public Controller Find(int ID)
        {
            return db.Set<Controller>().Single(x => x.ID == ID);
        }
    }
}