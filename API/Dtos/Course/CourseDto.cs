namespace API.Dtos.Course;

public class CourseDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public long CreditHours { get; set; }
    public long FullMarks { get; set; }
    public bool IsElective { get; set; }
}
