namespace Kitchen_MVC.Services.ServiceImpl
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Current { get => DateTime.Now; set => throw new NotImplementedException(); }
    }
}
