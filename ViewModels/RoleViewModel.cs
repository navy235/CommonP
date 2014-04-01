using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Security;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Web.Mvc;
using Maitonn.Core;

namespace CommonP.ViewModels
{
    public class RoleViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        [Required(ErrorMessage = "请输入角色名称")]
        [Display(Name = "角色名称")]
        public string Name { get; set; }

        [Required(ErrorMessage = "请输入操作描述")]
        [Display(Name = "描述")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

 

        [Required(ErrorMessage = "请选择操作")]
        [Display(Name = "操作列表")]
        //[UIHint("GroupCombox")]
        [UIHint("TreeCombo")]
        public string ActionID { get; set; }
    }

    public class RoleSearchViewModel
    {
        [Display(Name = "操作名称")]
        public string SearchName { get; set; }


    }

    public class RoleListViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}