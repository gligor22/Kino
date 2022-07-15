using Cineplex.Domain;
using Cineplex.Domain.Domain;
using Cineplex.Domain.Identity;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;

namespace CIneplex.Web.Controllers
{
    public class UserController : Controller
    {
        public readonly UserManager<CineplexApplicationUser> userManager;

        public UserController(UserManager<CineplexApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Import(IFormFile file)
        {
            string path = $"{Directory.GetCurrentDirectory()}\\files\\{file.FileName}";
            using(FileStream fileStream = System.IO.File.Create(path))
            {
                file.CopyTo(fileStream);

                fileStream.Flush();
            }

            IActionResult users = generate(file.FileName);

            return users;
        }

        private IActionResult generate(string fileName)
        {

            List<User> users = new List<User>();
            string path = $"{Directory.GetCurrentDirectory()}\\files\\{fileName}";

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using(var stream = System.IO.File.Open(path,FileMode.Open,FileAccess.Read))
            {
                using(var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while(reader.Read())
                    {


                        var email = reader.GetValue(0).ToString();
                        var password = reader.GetValue(1).ToString();
                        var Role = reader.GetValue(2).ToString();

                        var userCheck = userManager.FindByEmailAsync(email);
                        if (userCheck == null)
                        {
                            var user = new CineplexApplicationUser
                            {
                                
                                UserName = email,
                                NormalizedUserName = email,
                                Email = email,
                                EmailConfirmed = true,
                                PhoneNumberConfirmed = true,
                                ShoppingCart = new ShoppingCart()
                            };
                            var result =  userManager.CreateAsync(user, password);
                          
                        }

                    }
                    
                }
            }
            return RedirectToAction("Index");
        }
    }

}
