using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonP.Models;
using CommonP.ViewModels;

namespace CommonP.Service.Interface
{
    public interface IMemberService
    {
        IQueryable<Member> GetALL();

        IQueryable<Member> GetKendoALL();

        void Create(Member model);

        Member Create(MemberViewModel model);

        void Update(Member model);

        Member Update(MemberViewModel model);

        void Delete(Member model);

        Member Find(int ID);

    }
}