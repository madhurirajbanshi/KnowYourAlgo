namespace API.Persistence.Entities;

public class Semester
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int TotalCreditHour { get; set; }
    public int TotalFullMark { get; set; }

    public ICollection<Course> Courses { get; set; } = [];
}
