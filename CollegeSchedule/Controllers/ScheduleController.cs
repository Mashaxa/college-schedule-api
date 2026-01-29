using CollegeSchedule.Data;
using CollegeSchedule.Models;
using CollegeSchedule.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollegeSchedule.Controllers
{
    [ApiController]
    [Route("api/schedule")]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _service;
        public ScheduleController(IScheduleService service, AppDbContext db)
        {
            _service = service;
        }
        //GET: api/shedule/group/{groupName}?start=2026-01-29
        [HttpGet("group/{groupName}")]
        public async Task<IActionResult> GetSchedule(string groupName, DateTime start, DateTime end)
        {
            //Вызов бизнес-логики из сервиса
            var result = await _service.GetScheduleForGroup(groupName,start.Date,end.Date);
            //Возвращаем результат со статусом 200 ОК
            return Ok(result);
        }
    }
}