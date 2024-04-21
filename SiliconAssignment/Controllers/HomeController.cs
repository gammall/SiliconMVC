using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;


namespace SiliconAssignment.Controllers;

public class HomeController(HttpClient httpClient) : Controller
{
    private readonly HttpClient _httpClient = httpClient;

    [Route("/")]
    public IActionResult Index() => View();


    [Route("/error")]
    public IActionResult Error404(int statusCode) => View();


    public async Task<IActionResult> Subscribe(SubscribeModel model)
    {
        if (ModelState.IsValid)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7034/api/subscribe", content);
            if (response.IsSuccessStatusCode)
            {
                TempData["StatusMessage"] = "You are now subscribing to our newsletter";
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                TempData["StatusMessage"] = "You are already subscribing to our newsletter";
            }
        }
        else
        {
            TempData["StatusMessage"] = "Invalid email address";
        }

        return RedirectToAction("Index", "Home", "subscribe");
    }
}
