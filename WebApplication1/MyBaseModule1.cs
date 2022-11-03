using Loaf.Core.Modularity;

public class MyBaseModule1 : LoafModule
{
    public override void ConfigureService(ServiceConfigurationContext context)
    {
        Console.WriteLine("ִ�е��˱�������MyBaseModule1 ConfigureService");
    }
    public override void PostConfigureService(ServiceConfigurationContext context)
    {
        Console.WriteLine("ִ�е��˱�������MyBaseModule1 PostConfigureService");
    }

    public override void PreConfigureService(ServiceConfigurationContext context)
    {
        Console.WriteLine("ִ�е��˱�������MyBaseModule1 PreConfigureService");
    }
}