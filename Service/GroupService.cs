using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonP.Models;
using CommonP.ViewModels;
using CommonP.Service.Interface;
using CommonP.Utils;
using Kendo.Mvc.Extensions;
using Maitonn.Core;

namespace CommonP.Service
{
    public class GroupService : IGroupService
    {

        private readonly IUnitOfWork db;

        private readonly IRoleService RoleService;
        public GroupService(IUnitOfWork db, IRoleService RoleService)
        {
            this.db = db;
            this.RoleService = RoleService;
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

            if (!string.IsNullOrEmpty(model.RoleID))
            {
                var RoleArray = Utilities.GetIdList(model.RoleID);
                var RoleList = RoleService.GetALL().Where(x => RoleArray.Contains(x.ID));
                entity.Role.AddRange(RoleList);
            }

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
            var RoleArray = new List<int>();
            if (string.IsNullOrEmpty(model.RoleID))
            {
                entity.Role = new List<CommonP.Models.Role>();
            }
            else
            {
                RoleArray = Utilities.GetIdList(model.RoleID);
                var RoletList = RoleService.GetALL().Where(x => RoleArray.Contains(x.ID));
                var currentRoleArray = entity.Role.Select(x => x.ID).ToList();
                foreach (CommonP.Models.Role ac in RoleService.GetALL())
                {
                    if (RoleArray.Contains(ac.ID))
                    {
                        if (!currentRoleArray.Contains(ac.ID))
                        {
                            entity.Role.Add(ac);
                        }
                    }
                    else
                    {
                        if (currentRoleArray.Contains(ac.ID))
                        {
                            entity.Role.Remove(ac);
                        }
                    }
                }
            }
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