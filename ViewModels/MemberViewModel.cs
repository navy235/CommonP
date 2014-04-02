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
    public class MemberViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        [Required(ErrorMessage = "请输入姓名")]
        [Display(Name = "姓名")]
        [RegularExpression(@"^[\u4e00-\u9fa5|A-Za-z|0-9|_]+$", ErrorMessage = "昵称含有非法字符.")]
        [Remote("NickNameExists", "AjaxService", ErrorMessage = "姓名含有非法字符或已注册")]
        public string Name { get; set; }

        [Required(ErrorMessage = "请输入电子邮箱")]
        [Display(Name = "电子邮箱")]
        [RegularExpression(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "电子邮箱格式不正确.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "请输入密码")]
        [StringLength(15, ErrorMessage = "请输入{2}-{1}位密码", MinimumLength = 6)]
        [Display(Name = "设定密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "请确认密码")]
        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "两次密码不一致")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "请选择性别")]
        [Display(Name = "性别")]
        [UIHint("RadioList")]
        [AdditionalMetadata("RadioList", "男,女")]
        public bool Sex { get; set; }

        [Required(ErrorMessage = "请选择用户群组")]
        [Display(Name = "用户群组")]
        [UIHint("DropDownList")]
        public int GroupID { get; set; }

        [Display(Name = "上传头像")]
        [UIHint("UploadImg")]
        public string AvtarUrl { get; set; }
    }

    public class MemberSearchViewModel
    {
        [Display(Name = "姓名")]
        public string SearchName { get; set; }

        [Display(Name = "用户群组")]
        [UIHint("DropDownList")]
        public int? GroupID { get; set; }
    }

    public class MemberListViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string GroupName { get; set; }

        public int GroupID { get; set; }
    }
}