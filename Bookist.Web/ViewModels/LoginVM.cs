using System.ComponentModel.DataAnnotations;

namespace Bookist.Web.ViewModels;

public class LoginVM
{
    [Required(ErrorMessage = "用户名不能为空")]
    public string Username { get; set; }

    [Required(ErrorMessage = "密码不能为空")]
    public string Password { get; set; }

    public string ReturnUrl { get; set; }

    public bool RememberMe { get; set; }
}