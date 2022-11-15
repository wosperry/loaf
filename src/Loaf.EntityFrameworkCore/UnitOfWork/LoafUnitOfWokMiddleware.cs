using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Loaf.EntityFrameworkCore.UnitOfWork
{
    public class LoafUnitOfWokMiddleware
    {
        public RequestDelegate Next { get; }
        public IUnitOfWork Uow { get; }

        public LoafUnitOfWokMiddleware(RequestDelegate requestDelegate, IUnitOfWork uow)
        {
            Next = requestDelegate;
            Uow = uow;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await Uow?.BeginAsync();
                await Next.Invoke(httpContext);
                await Uow?.CommitAsync();
            }
            catch (Exception)
            {
                await Uow.RollBackAsync();
                throw;
            }
        }
    }
}