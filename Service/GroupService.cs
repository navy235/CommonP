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
    public class GroupService : IGroupService
    {

        private readonly IUnitOfWork db;

        public GroupService(IUnitOfWork db)
        {
            this.db = db;
        }
        public IQueryable<Group> GetALL()
        {
            return db.Set<Group>();
        }

        public IQueryable<Group> GetKendoALL()
        {
            db.SetProxyCreationEnabledFlase();
            return db.Set<Group>();
        }

        public void Create(Group model)
        {
            db.Add<Group>(model);
            db.Commit();
        }

        public Group Create(GroupViewModel model)
        {
            var entity = new Group();
            entity.Name = model.Name;
            entity.Description = model.Description;
            db.Add<Group>(entity);
            db.Commit();
            return entity;
        }

        public void Update(Group model)
        {
            var target = Find(model.ID);
            db.Attach<Group>(target);
            target.Name = model.Name;
            target.Description = model.Description;
            db.Commit();
        }

        public Group Update(GroupViewModel model)
        {

            var entity = Find(model.ID);
            db.Attach<Group>(entity);
            entity.Name = model.Name;
            entity.Description = model.Description;
            db.Commit();
            return entity;
        }

        public void Delete(Group model)
        {
            var target = Find(model.ID);
            db.Remove<Group>(target);
            db.Commit();
        }

        public Group Find(int ID)
        {
            return db.Set<Group>().Single(x => x.ID == ID);
        }
    }
}