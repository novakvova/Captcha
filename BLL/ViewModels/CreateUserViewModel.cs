using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BLL.ViewModels
{
    public class CreateUserViewModel
    {
        [Required,Display(Name = "E-mail address"),EmailAddress]
        public string Email { get; set; }

        [Display(Name = "LastName")]
        [Required, StringLength(maximumLength: 100)]
        public string LastName { get; set; }

        [Display(Name = "Name")]
        [Required, StringLength(maximumLength: 100)]
        public string Name { get; set; }

        [Display(Name = "SecondName")]
        [Required, StringLength(maximumLength: 100)]
        public string SecondName { get; set; }

        [Required,Display(Name = "Password"),StringLength(maximumLength:20,ErrorMessage="{0} maximum length, {2} minimum lendth",MinimumLength=6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password),Display(Name="Confirm password"),Compare("Password",ErrorMessage="Пароль має співпадати")]
        public string ConfirmPassword { get; set; }

        [Required, Display(Name="Select role")]
        public int RoleId { get; set; }
        [Display(Name = "Image user")]
        public HttpPostedFileBase Image { get; set; }

    }
}