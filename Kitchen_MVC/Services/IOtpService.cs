namespace Kitchen_MVC.Services
{
    public interface IOtpService
    {
        public string GenerateOTP(int digitNumber = 6);
    }
}
