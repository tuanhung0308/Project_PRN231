using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project_PRN231_Client.Models;
using System.Text;
using System.Text.Json;

namespace Project_PRN231_Client.Controllers
{
    public class LoginController : Controller
    {
        private readonly string _apiBaseUrl = "http://localhost:5000/api/";
        private readonly HttpClient _client;

        public LoginController()
        {
            _client = new HttpClient();
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("user") == null)
            {
                return View();
            }
            return Redirect("/Home");
        }

        [HttpPost]
        public async Task<IActionResult> Index(User user)
        {
            try
            {
                HttpResponseMessage response = await _client.PostAsJsonAsync(_apiBaseUrl + "User/login", user);
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    User loggedInUser = JsonConvert.DeserializeObject<User>(data);

                    HttpContext.Session.SetString("user", JsonConvert.SerializeObject(loggedInUser));

                    if (string.Equals(loggedInUser.Role.RoleName, "admin", StringComparison.OrdinalIgnoreCase))
                    {
                        HttpContext.Session.SetString("admin", JsonConvert.SerializeObject(loggedInUser));
                        ViewBag.LoginSuccess = true;
                        return Redirect("/Home");
                    }
                    else if (string.Equals(loggedInUser.Role.RoleName, "farmer", StringComparison.OrdinalIgnoreCase))
                    {
                        HttpContext.Session.SetString("farmer", JsonConvert.SerializeObject(loggedInUser));
                        ViewBag.LoginSuccess = true;
                        return Redirect("/Home");
                    }
                    else
                    {
                        ViewBag.Error = "Unauthorized access!";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Error = "Invalid email or password!";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "An error occurred: " + ex.Message;
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/Login");
        }
    }
}
