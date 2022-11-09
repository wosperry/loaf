using Loaf.Admin.Entities;
using Loaf.Core.Data;
using Loaf.EntityFrameworkCore.Repository.Attributes;
using Loaf.EntityFrameworkCore.Repository.Extensions;
using Loaf.EntityFrameworkCore.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Loaf.Admin.Controllers.Test
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public ValuesController(IRepository<Tttttttttttt> tttRepository)
        {
            TttRepository = tttRepository;
        }

        public IRepository<Tttttttttttt> TttRepository { get; }

        [HttpPost]
        public Task<PagedResult<Tttttttttttt>> Test(TestParameter param )
        {
            var query = TttRepository.GetQueryable(param);
            return query.GetPagedResultAsync(param);
        }
    }

    public class TestParameter: PageQueryParameter
    {
        [LoafGreaterThan]
        public int? Count { get; set; }
    }
}
