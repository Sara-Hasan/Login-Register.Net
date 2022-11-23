using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using UserMvc.Models;
using UserMvc.Repository;
using Aes = System.Security.Cryptography.Aes;

namespace UserMvc.Controllers
{
    
    public class UserController : Controller
    {
        private IGenericRepository<User> _context;
        private readonly AppDbContext _db;
       

        public UserController(IGenericRepository<User> context, AppDbContext db)
        {
            _context = context;
            _db = db;
        }

        [HttpGet]
 
        public IActionResult Index()
        {
            try
            {
                var users = _context.GetAll();
                return View(users);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View();
        }
        [HttpGet]
       
        public IActionResult Edit(int Id)
        {
            try
            {
                ViewBag.Date = DateTime.Now;
                var user = _db.users.Find(Id);
                return View(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View();

        }
        [HttpPost]
        public IActionResult Edit(User user)
        {
            try
            {
                _context.Edit(user);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginViewModel user)
        {
            try
            {
                var data = _db.users.FirstOrDefault(x => x.UserName == user.UserName);
                if (data == null)
                {
                    ViewData["Err"] = "invalid user name/password";
                    return View(user);
                }
                if (!VerifyPasswordHash(user.Password, data.passwordHash, data.passwordSalt))
                {
                    return View(user);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View();

            //try
            //{
            //    var log = _db.users.Where(x => x.UserName.Equals(user.UserName) && x.Password.Equals(user.Password));
            //    if (log.Any())
            //    {
            //        return RedirectToAction("Index");
            //    }
            //    else
            //    {
            //        ViewData["Err"] = "invalid user name/password";
            //        return View(user);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            //return View();

        }

        [NonAction]
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
                return true;
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            try
            {
                var user = new User();
                user.CreationDate = DateTime.Now;
                return View(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User user)
        {
            byte[] passwordHash, passwordSalt;
            GenerateHash(user.Password,
                         out passwordHash,
                         out passwordSalt);
            user.passwordHash = passwordHash;
            user.passwordSalt = passwordSalt;
            try
            {
                _context.Add(user);
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View();
        }

        [NonAction]
        private void GenerateHash(string Password,out byte[] passwordHash ,out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
                passwordSalt = hmac.Key;
            }
        }
    }
}
