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
    public class ActionViewModel
    {


        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        [Required(ErrorMessage = "请选择控制器")]
        [Display(Name = "控制器")]
        [UIHint("DropDownList")]
        public int ControllerID { get; set; }


        [Required(ErrorMessage = "请输入操作名称")]
        [Display(Name = "操作名称")]
        public string Name { get; set; }

        [Required(ErrorMessage = "请输入操作描述")]
        [Display(Name = "描述")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }

    public class ActionSearchViewModel
    {
        [Display(Name = "操作名称")]
        public string ActionName { get; set; }

        [Display(Name = "控制器")]
        [UIHint("DropDownList")]
        public int? ControllerID { get; set; }

    }

    public class ActionListViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string ControllerName { get; set; }

        public string Description { get; set; }

        public int ControllerID { get; set; }
    }
}