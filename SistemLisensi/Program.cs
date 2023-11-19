using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using Newtonsoft.Json;

namespace SistemLisensi
{
    class Program
    {
        static void Main()
        {
            string jsonFilePath = "https://unrelization.github.io/data.json";
            string jsonContent = new WebClient().DownloadString(jsonFilePath);

            List<UserCredentials> userCredentialsList = JsonConvert.DeserializeObject<List<UserCredentials>>(jsonContent);

            Console.WriteLine("Write Username : ");
            string enteredUsername = Console.ReadLine();
            Console.WriteLine("Write Password : ");
            string enteredPassword = Console.ReadLine();

            UserCredentials userCredentials = userCredentialsList.FirstOrDefault(u => u.Username == enteredUsername);

            if (userCredentials != null)
            {
                if (BCrypt.Net.BCrypt.Verify(enteredPassword, userCredentials.Password))
                {
                    Console.WriteLine("Login successful!");
                    Console.WriteLine("Owned Items:");
                    foreach (var item in userCredentials.Owned)
                    {
                        Console.WriteLine(item);
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect password.");
                }
            }
            else
            {
                Console.WriteLine("Username does not exist.");
            }

            Thread.Sleep(10000);
        }
    }

    public class UserCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public List<string> Owned { get; set; }
    }
}