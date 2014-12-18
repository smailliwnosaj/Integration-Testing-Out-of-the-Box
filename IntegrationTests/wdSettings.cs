public class wdSettings
{
    public string SettingsFileTempPath
    {
        get
        {
            return System.IO.Path.GetTempPath() + @"\IntegrationTests.Settings.xml";
        }
    }

    public string ApplicationName
    {
        get;
        set;
    }
    public string ApplicationPath
    {
        get;
        set;
    }
    public string AdminUserName
    {
        get;
        set;
    }
    public string AdminPassword
    {
        get;
        set;
    }



}
