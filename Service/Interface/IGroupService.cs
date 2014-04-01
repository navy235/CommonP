using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonP.Models;
using CommonP.ViewModels;

namespace CommonP.Service.Interface
{
    public interface IGroupService
    {
        IQueryable<Group> GetALL();

        IQueryable<Group> GetKendoALL();

        void Create(Group model);

        Group Create(GroupViewModel model);

        void Update(Group model);

        Group Update(GroupViewModel model);

        void Delete(Group model);

        Group Find(int ID);

    }
}