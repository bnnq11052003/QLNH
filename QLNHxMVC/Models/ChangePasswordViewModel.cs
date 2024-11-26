namespace QLNHxMVC.Models
{
    public class ChangePasswordViewModel
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; } // Trường xác nhận mật khẩu mới
    }
}
