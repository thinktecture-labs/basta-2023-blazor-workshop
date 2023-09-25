using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkshopApi.Database;
using WorkshopShared;
using WorkshopApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using WorkshopApi.SignalR;

namespace WorkshopApi.Controllers
{
    [Authorize("api")]
    [Route("api/[controller]")]
    [ApiController]
    public class ConferencesController : ControllerBase
    {
        private readonly ConferencesDbContext context;
        private readonly IHubContext<ConferencesHub> _hub;

        public ConferencesController(ConferencesDbContext context, IHubContext<ConferencesHub> hub)
        {
            this.context = context;
            _hub = hub;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConferenceOverview>>> Index()
        {
            var conferences = (await context.Conferences.ToListAsync())
                .Select(c=> c.ToOverview());
            return Ok(conferences);
        }

        [HttpGet("{id}")]
        public ActionResult<ConferenceDetails> GetConferenceById(Guid id)
        {
            var data = context.Conferences
                .Single(c => c.Id == id)
                .ToDetails();

            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<ConferenceDetails>> AddConference([FromBody] ConferenceDetails conference)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var conf = conference.ToConference();

            context.Conferences.Add(conf);
            await context.SaveChangesAsync();

            await _hub.Clients.All.SendAsync(SignalRNames.ConferenceAdded, conf.ToOverview());

            return CreatedAtAction(nameof(GetConferenceById), new { id = conf.Id }, conf);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConference(Guid id, [FromBody] ConferenceDetails conference)
        {
            if (id != conference.Id)
            {
                return BadRequest();
            }

            var conf = conference.ToConference();
            context.Entry(conf).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
                await _hub.Clients.All.SendAsync(SignalRNames.ConferenceUpdated, conf.ToOverview());
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await context.Conferences.AnyAsync(c => c.Id == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConference(Guid id)
        {
            var conf = context.Conferences.Find(id);
            if (conf == null) return NotFound();

            context.Conferences.Remove(conf);
            context.SaveChanges();

            return NoContent();
        }

    }
}
