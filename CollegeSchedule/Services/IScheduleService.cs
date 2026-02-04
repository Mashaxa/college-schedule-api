using CollegeSchedule.DTO;
using CollegeSchedule.Models;

namespace CollegeSchedule.Services
{
    public interface IScheduleService
    {
        Task<List<ScheduleByDateDto>> GetScheduleForGroup(string groupName, DateTime startDate, DateTime endDate);

        Task<List<GroupResponse>> GetAllGroupsAsync();
        Task<List<GroupResponse>> SearchGroupsAsync(string query);
    }
}