using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Models
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage="Tên đăng nhập không được rỗng")]
        [RegularExpression(@"^[A-Za-z0-9._-]{2,25}$", ErrorMessage = "Tên đăng nhập tối thiểu 2 ký")]
        [StringLength(15)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Mật khẩu đăng nhập không được rỗng")]
        [RegularExpression(@"(?=.*\d)(?=.*[A-Za-z]).{5,}$", ErrorMessage = "Mật khẩu tối thiểu 5 ký tự bao gồm chữ và số")]
        [DataType(DataType.Password)]
        [StringLength(32)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}