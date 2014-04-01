using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonP.Models;
using CommonP.ViewModels;

namespace CommonP.Service.Interface
{
    public interface IRoleService
    {
        IQueryable<Role> GetALL();

        IQueryable<Role> GetKendoALL();

        void Create(Role model);

        Role Create(RoleViewModel model);

        void Update(Role model);

        Role Update(RoleViewModel model);

        void Delete(Role model);

        Role Find(int ID);

    }
}