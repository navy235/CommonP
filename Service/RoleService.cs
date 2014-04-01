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
    public class RoleService : IRoleService
    {

        private readonly IUnitOfWork db;

        public RoleService(IUnitOfWork db)
        {
            this.db = db;
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