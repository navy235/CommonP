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
    public class ControllerViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        [Required(ErrorMessage = "请输入控制器名称")]
        [Display(Name = "控制器")]
        public string Name { get; set; }

        [Required(ErrorMessage = "请输入控制器描述")]
        [Display(Name = "描述")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}