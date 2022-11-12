using Loaf.Core.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Loaf.Core.Encryptors
{
    public class Md5EncryptModule : LoafModule
    {
        public override void ConfigureService(LoafModuleContext context)
        {
            context.Services.AddTransient<IEncryptor, MD5Encryptor>();
        }
    }
}