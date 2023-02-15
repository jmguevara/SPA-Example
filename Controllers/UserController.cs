using System;
using System.Runtime.ConstrainedExecution;
using SPATest.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using System.IO;

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
        public dynamic loginUser(User user)
        {
            var listJson = GetUsersJsonFromFile();
            List<User>? users = JsonConvert.DeserializeObject<List<User>>(listJson);
            User? element = users.Find(x => x.user == user.user && x.password == user.password);
            if (element != null)
            {
                return new
                {
                    success = true,
                    message = "Login Success",
                    result = element
                };

            }
            return new
            {
                    success = false,
                    message = "Login Failed",
                    result = element
            };
        }

        [HttpPost]
        [Route("save")]
        public dynamic save(User user)
        {
            var newUser = new User();
            newUser.user = user.user;
            newUser.password = user.password;

            var listJson = GetUsersJsonFromFile();
            List<User>? users = JsonConvert.DeserializeObject<List<User>>(listJson);
            users.Add(newUser);
            SaveUsersJsonFromFile(users);
            return new
            {
                success = true,
                message = "Saved new cliente",
                result = users
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

        public static void SaveUsersJsonFromFile(List<User> users)
        {
            string contactsJson = JsonConvert.SerializeObject(users.ToArray(), Formatting.Indented);
            string userJson = @"users.json";

            System.IO.File.WriteAllText(userJson, contactsJson);

        }
    }
}

