namespace CollegeSchedule.DTO
{
    public class ScheduleByDateDto //содержит дату и список занятий на этот день
    {
        public DateTime LessonDate { get; set; }
        public string Weekday { get; set; } = null!;
        public List<LessonDto> Lessons { get; set; } = new();
    }
}