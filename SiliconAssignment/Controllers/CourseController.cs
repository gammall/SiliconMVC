using Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SiliconAssignment.ViewModels;

namespace SiliconAssignment.Controllers;

[Authorize]
public class CourseController(HttpClient httpClient) : Controller
{
    private readonly HttpClient _httpClient = httpClient;

    [Route("/courses")]
    public async Task<IActionResult> Index()
    {
        var viewModel = new CourseIndexViewModel();

        var response = await _httpClient.GetAsync("https://localhost:7034/api/course");
        if (response.IsSuccessStatusCode)
        {
            var courses = JsonConvert.DeserializeObject<IEnumerable<CourseModel>>(await response.Content.ReadAsStringAsync());
            if (courses != null && courses.Any())
                viewModel.Courses = courses;
        }

        return View(viewModel);
    }
}
