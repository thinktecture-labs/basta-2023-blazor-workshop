using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkshopApi.Database;
using WorkshopShared;

namespace WorkshopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConferencesController : ControllerBase
    {
        private readonly ConferencesDbContext context;

        public ConferencesController(ConferencesDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConferenceOverview>>> Index()
        {
            var conferences = await context.Conferences.ToListAsync();
            return Ok(conferences);
        }


    }
}
