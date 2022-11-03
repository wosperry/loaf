using Loaf.Core.Modularity;

[DependsOn(
    typeof(MyBaseModule1),
    typeof(MyBaseModule2)
)]
public class MyModule : LoafModule
{
    public override void ConfigureService(ServiceConfigurationContext context)
    {
        Console.WriteLine("ִ�е���MyModule ConfigureService");
        var services = context.Services;
        services.AddSwaggerDocument();
        services.AddControllers();
    }

    public override void PreConfigureService(ServiceConfigurationContext context)
    {
        Console.WriteLine("ִ�е���MyModule PreConfigureService");
    }
    public override void PostConfigureService(ServiceConfigurationContext context)
    {
        Console.WriteLine("ִ�е���MyModule PostConfigureService");
    }
}