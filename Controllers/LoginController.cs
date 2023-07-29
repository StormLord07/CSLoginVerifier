using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CSLoginVerifier
{
    public class UserCredentials
    {
        public UserCredentials()
        {
            Login = "";
            Password = "";
        }

        public string Login { get; set; }
        public string Password { get; set; }

    }

    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private Dictionary<string, string> validCredentials = new Dictionary<string, string>();

        public LoginController()
        {

            foreach (var line in System.IO.File.ReadAllLines("credentials.txt"))
            {
                var parts = line.Split(' ');
                if (parts.Length == 2)
                {
                    validCredentials[parts[0]] = parts[1];
                }
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserCredentials credentials)
        {
            bool isValid = validCredentials.ContainsKey(credentials.Login) && validCredentials[credentials.Login] == credentials.Password;
            return Ok(new { access = isValid });
        }
    }
}
