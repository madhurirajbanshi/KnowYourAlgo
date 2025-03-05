namespace API.Persistence.Entities;

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Code { get; set; }
    public bool IsElective { get; set; } = false;
    public int CreditHours { get; set; }
    public int FullMarks { get; set; }
    public int SemesterId { get; set; }
    public Semester Semester { get; set; }
    public ICollection<Algorithm> Algorithms { get; set; } = [];
}
