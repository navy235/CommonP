using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonP.Models;
using CommonP.ViewModels;

namespace CommonP.Service.Interface
{
    public interface IActionService
    {
        IQueryable<Models.Action> GetALL();

        IQueryable<Models.Action> GetKendoALL();

        void Create(Models.Action model);

        Models.Action Create(ActionViewModel model);

        void Update(Models.Action model);

        Models.Action Update(ActionViewModel model);

        void Delete(Models.Action model);

        Models.Action Find(int ID);
    }
}