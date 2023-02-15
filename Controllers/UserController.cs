using System;
using System.Runtime.ConstrainedExecution;
using SPATest.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SPATest.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : Controller
    {


        [HttpGet]
        [Route("list")]
        public dynamic listUser()
        {
            var user = GetUsersJsonFromFile();
            return user;
        }

        [HttpGet]
        [Route("login")]
        public dynamic loginUser(string user, string password)
        {
            return new User
            {
                user = user,
                password = password

            };
        }

        [HttpPost]
        [Route("save")]
        public dynamic save(User user)
        {
            user.user = "juan";
            user.password = "juan";

            var listJson = GetUsersJsonFromFile();
            User datalist = JsonConvert.DeserializeObject<User>(listJson);
            Console.WriteLine(datalist);
            return new
            {
                success = true,
                message = "Saved new cliente",
                result = user
            };
        }

        public static string GetUsersJsonFromFile()
        {
            string userJson = @"users.json";

            string userJsonFromFile;
            using (var reader = new StreamReader(userJson))
            {
                userJsonFromFile = reader.ReadToEnd();

            };

            return userJsonFromFile;
        }
    }
}

