using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using yame.Models;
using yame.ViewModels;

namespace yame.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<Users> signInManager;
        private readonly UserManager<Users> usersManager;

        public AccountController(SignInManager<Users> signInManager, UserManager<Users> usersManager)
        {
            this.signInManager = signInManager;
            this.usersManager = usersManager;
        }
        //Login
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Email or PassWord is incorrect !!");
                    return View(model);
                }
            }
            return View(model);
        }
        //Register
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                Users user = new Users
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Birthday = model.Birthday,
                    Fullname = model.FirstName + " " + model.LastName,
                    Email = model.Email,
                    UserName = model.Email,

                };
                var result = await usersManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(" ", error.Description);

                    }
                    return View(model);
                }

            }
            return View(model);
        }
        [HttpGet]
        public IActionResult ChangePassword(string username)
        {
            if (string.IsNullOrEmpty(username))
            { // If the username is null or empty, you can either show an error or just return the view without redirecting
                ModelState.AddModelError("", "Username is required.");
                return View();  // Show the change password view with an error message
            }
            return View(new ChangePasswordViewModel { Email = username });
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await usersManager.FindByNameAsync(model.Email);
                if (user != null)
                {
                    // Check if user has a password set
                    if (await usersManager.HasPasswordAsync(user))
                    {
                        var result = await usersManager.RemovePasswordAsync(user);
                        if (result.Succeeded)
                        {
                            result = await usersManager.AddPasswordAsync(user, model.NewPassword);
                            if (result.Succeeded)
                            {
                                return RedirectToAction("Login", "Account");
                            }
                            else
                            {
                                foreach (var error in result.Errors)
                                {
                                    ModelState.AddModelError(string.Empty, error.Description);
                                }
                            }
                        }
                        else
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "User does not have an existing password.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email not found.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid model state. Please try again.");
            }

            return View(model);
        }
        //logout
        public async Task<IActionResult> Logout()
        {
            // Sign the user out
            await signInManager.SignOutAsync();

            // Redirect to the Index action of the Products controller (or wherever you want)
            return RedirectToAction("Index", "Home");
        }
        //admin
        //quản lý page người dùng
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ManageUsers()
        {
            var users = await usersManager.Users.ToListAsync();
            return View(users);
        }
        //xóa người dùng
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await usersManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await usersManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("ManageUsers");
                }
            }
            return NotFound();
        }
        //gán quyền cho người dùng 
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AssignRole(string userId, string role)
        {
            var user = await usersManager.FindByIdAsync(userId);
            if (user != null)
            {
                await usersManager.AddToRoleAsync(user, role);
                return RedirectToAction("ManageUsers");
            }
            return NotFound();
        }


    }
}