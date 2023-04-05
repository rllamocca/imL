namespace imL.Package.Hosting
{
    public interface IHostPeriodSetting
    {
        double? Period { set; get; }
        double? Delay { set; get; }
        double? TimeOut { set; get; }
    }
}
