using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonP.Models;
using CommonP.ViewModels;

namespace CommonP.Service.Interface
{
    public interface IControllerService
    {
        IQueryable<Controller> GetALL();

        IQueryable<Controller> GetKendoALL();

        void Create(Controller model);

        Controller Create(ControllerViewModel model);

        void Update(Controller model);

        Controller Update(ControllerViewModel model);

        void Delete(Controller model);

        Controller Find(int ID);
    }
}