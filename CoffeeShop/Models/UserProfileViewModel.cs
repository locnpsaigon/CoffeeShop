using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using CoffeeShop.DAL;

namespace CoffeeShop.Models
{
    public class UserProfileViewModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }

        [Required(ErrorMessage = "Mật khẩu hiện tại không được rỗng")]
        [RegularExpression(@"(?=.*\d)(?=.*[A-Za-z]).{5,}$", ErrorMessage = "Mật khẩu tối thiểu 5 ký tự bao gồm chữ và số")]
        [DataType(DataType.Password)]
        [StringLength(32)]
        public string CurrentPassword { get; set; }

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

    }
}