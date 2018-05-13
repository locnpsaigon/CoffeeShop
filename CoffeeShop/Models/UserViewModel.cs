using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using CoffeeShop.DAL;

namespace CoffeeShop.Models
{
    public class UserViewModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage="Tên đăng nhập không được rỗng")]
        [RegularExpression(@"^[A-Za-z0-9._-]{2,25}$", ErrorMessage = "Tên đăng nhập tối thiểu 2 ký")]
        [StringLength(15)]
        public string Username { get; set; }

        [Required(ErrorMessage="Họ không được rỗng")]
        [StringLength(32)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Tên không được rỗng")]
        [StringLength(32)]
        public string LastName { get; set; }

        [Required(ErrorMessage="Mật khẩu không được rỗng!")]
        [RegularExpression(@"(?=.*\d)(?=.*[A-Za-z]).{5,}$", ErrorMessage = "Mật khẩu tối thiểu 5 ký tự bao gồm chữ và số")]
        [DataType(DataType.Password)]
        [StringLength(32)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu không trùng khớp")]
        [DataType(DataType.Password)]
        [StringLength(32)]
        public string ConfirmPassword { get; set; }

        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public bool IsActive { get; set; }

        public IList<RoleSelectViewModel> RoleSelectViewModels { get; set; }
    }
}