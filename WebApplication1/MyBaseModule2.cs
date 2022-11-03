using Loaf.Core.Modularity;

[DependsOn(
    typeof(MyBaseModule3)
)]
public class MyBaseModule2 : LoafModule
{
    public override void ConfigureService(ServiceConfigurationContext context)
    {
        Console.WriteLine("ִ�е��˱�������MyBaseModule2 ConfigureService");
    }
    public override void PostConfigureService(ServiceConfigurationContext context)
    {
        Console.WriteLine("ִ�е��˱�������MyBaseModule2 PostConfigureService");
    }

    public override void PreConfigureService(ServiceConfigurationContext context)
    {
        Console.WriteLine("ִ�е��˱�������MyBaseModule2 PreConfigureService");
    }
}