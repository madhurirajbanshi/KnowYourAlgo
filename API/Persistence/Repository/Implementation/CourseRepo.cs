using API.Persistence.Context;
using API.Persistence.Entities;
using API.Persistence.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Persistence.Repository.Implementation;

public class CourseRepo : Repository<Course>, ICourseRepo
{
    public CourseRepo(ApplicationDbContext context) : base(context)
    {
    }
}
