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
    public class MemberService : IMemberService
    {

        private readonly IUnitOfWork db;

        public MemberService(IUnitOfWork db)
        {
            this.db = db;
        }
        public IQueryable<Member> GetALL()
        {
            return db.Set<Member>();
        }

        public IQueryable<Member> GetKendoALL()
        {
            db.SetProxyCreationEnabledFlase();
            return db.Set<Member>();
        }

        public void Create(Member model)
        {
            db.Add<Member>(model);
            db.Commit();
        }

        public Member Create(MemberViewModel model)
        {
            var entity = new Member();
            entity.Name = model.Name;
            entity.Password = model.Password;
            entity.GroupID = model.GroupID;
            db.Add<Member>(entity);
            db.Commit();
            return entity;
        }

        public void Update(Member model)
        {
            var target = Find(model.ID);
            db.Attach<Member>(target);
            target.Name = model.Name;
            target.Password = model.Password;
            target.GroupID = model.GroupID;
            db.Commit();
        }

        public Member Update(MemberViewModel model)
        {

            var entity = Find(model.ID);
            db.Attach<Member>(entity);
            entity.Name = model.Name;
            entity.Password = model.Password;
            entity.GroupID = model.GroupID;
            db.Commit();
            return entity;
        }

        public void Delete(Member model)
        {
            var target = Find(model.ID);
            db.Remove<Member>(target);
            db.Commit();
        }

        public Member Find(int ID)
        {
            return db.Set<Member>().Single(x => x.ID == ID);
        }
    }
}