using Infrastructure.Models;

namespace SiliconAssignment.ViewModels;

public class CourseIndexViewModel
{
    public IEnumerable<CourseModel> Courses { get; set; } = [];
}
