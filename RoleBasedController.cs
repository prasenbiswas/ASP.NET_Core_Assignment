using CoreAssignmentForRollBased.Data;
using CoreAssignmentForRollBased.Models;
using CoreAssignmentForRollBased.Repository;
using CoreAssignmentForRollBased.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CoreAssignmentForRollBased.Controllers
{

    public class AssignRole
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Email { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
    public class RoleBasedController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private IGenericsRepository<UserViewModel> _UserRepository;
        private IGenericsRepository<ManagerViewModel> _managerRepository;
        public RoleBasedController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IGenericsRepository<UserViewModel> genericsRepository, IGenericsRepository<ManagerViewModel> managerRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _UserRepository = genericsRepository;
            _managerRepository = managerRepository;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = registerViewModel.Email,
                    Email = registerViewModel.Email
                };
                var result = await _userManager.CreateAsync(user, registerViewModel.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(registerViewModel);
            }
            else
            {
                return View(registerViewModel);

            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string? returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var IdentityResult = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, loginViewModel.RememberMe, lockoutOnFailure: false);

                if (IdentityResult.Succeeded)
                {
                    if (returnUrl == null || returnUrl == "/")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Username or Password incorrect");
                }
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(AssignRole assignRole)
        {
            ViewBag.RoleNameList = _roleManager.Roles.ToList();
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(assignRole.Email);

                var AllRolefind = _roleManager.Roles.Select(x => x.Name).ToList();

                if (AllRolefind.Contains(assignRole.RoleName))
                {
                    await _userManager.AddToRoleAsync(user, assignRole.RoleName);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(assignRole);
        }

        public async Task<ActionResult> ManagerList()
        {
            List<ManagerViewModel> list = new List<ManagerViewModel>();
            var users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, "Manager"))
                {
                    list.Add(new ManagerViewModel
                    {
                        ManagerId = user.Id,
                        ManagerEmail = user.Email,
                        ManagerName = user.UserName
                    });
                }
            }

            //var managerRoles = (from user in _userManager.Users
            //                      select new
            //                      {
            //                          UserId = user.Id,
            //                          Username = user.UserName,
            //                          Email = user.Email,
            //                          //RoleNames = (from userRole in _roleManager.Roles
            //                          //             join role in _roleManager.Roles on userRole.Id
            //                          //             equals role.Id
            //                          //             select role.Name).ToList()
            //                      }).ToList().Select(p => new ManagerViewModel()

            //                      {
            //                          ManagerId = p.UserId,
            //                          ManagerName = p.Username,
            //                          ManagerEmail = p.Email,
            //                          //Role = string.Join(",", p.RoleNames)
            //                      });
            return View(list);
        }

        public async Task<IActionResult> UserList()
        {
            List<UserViewModel> UserlistData = new List<UserViewModel>();
            var users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, "User"))
                {
                    UserlistData.Add(new UserViewModel
                    {
                        UserId = user.Id,
                        UserEmail = user.Email,
                        UserName = user.UserName
                    });
                }
            }
            return View(UserlistData);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("ManagerList");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View("ManagerList");
            }

        }
    }
}
