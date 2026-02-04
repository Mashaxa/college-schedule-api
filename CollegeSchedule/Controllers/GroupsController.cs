using CollegeSchedule.Services;
using Microsoft.AspNetCore.Mvc;

namespace CollegeSchedule.Controllers
{
    [ApiController]
    [Route("api/groups")]
    public class GroupsController : ControllerBase
    {
        private readonly IScheduleService _service;

        public GroupsController(IScheduleService service)
        {
            _service = service;
        }

        // GET: api/groups
        [HttpGet]
        public async Task<IActionResult> GetAllGroups()
        {
            var groups = await _service.GetAllGroupsAsync();
            return Ok(groups);
        }

        // GET: api/groups/search?query=ПИ
        [HttpGet("search")]
        public async Task<IActionResult> SearchGroups([FromQuery] string query)
        {
            var groups = await _service.SearchGroupsAsync(query);
            return Ok(groups);
        }
    }
}