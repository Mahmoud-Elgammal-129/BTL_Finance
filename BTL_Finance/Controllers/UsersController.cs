using BTL_Finance.Authrization;
using BTL_Finance.Models.Identity;
using BTL_Finance.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BTL_Finance.Controllers
{

    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [CustomAuthorize("Admin")]

        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.Where(n=>n.Is_Deleted==false).ToList();
            var userRoles = new List<UserRolesViewModel>();

            foreach (var user in users)
            {
                var thisViewModel = new UserRolesViewModel();
                thisViewModel.UserId = user.Id;
                thisViewModel.Email = user.Email;
                thisViewModel.Roles = await _userManager.GetRolesAsync(user);
                userRoles.Add(thisViewModel);
            }

            return View(userRoles);
        }

        public IActionResult Login()
        {
            return View();
        }
        

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {


            if (ModelState.IsValid)
            {
                var data = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent: false, lockoutOnFailure: false);
                if (data.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.UserName);

                    if (user != null)
                    {
                        return RedirectToAction("Home", "Home");
                    }

                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");

            }

            return View(model);

        }
        [CustomAuthorize("Admin")]
        public async Task<IActionResult> Register()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            ViewBag.Roles = new SelectList(roles, "Name", "Name");
            return View();
        }
        [CustomAuthorize("Admin")]

        [HttpPost]

        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, model.Role); // Assign role to the user
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Home", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    TempData["E_Message"] = error.Description;
                }
            }
            var roles = await _roleManager.Roles.ToListAsync();
            ViewBag.Roles = new SelectList(roles, "Name", "Name");
            return View(model);
        }
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Roles = (List<string>)userRoles
            };

            ViewBag.Roles = new SelectList(_roleManager.Roles.ToList(), "Name", "Name");
            return View(model);
        }
        [CustomAuthorize("Admin")]

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (user == null)
                {
                    return NotFound();
                }

                user.Email = model.Email;

                var userRoles = await _userManager.GetRolesAsync(user);
                var selectedRoles = model.Roles ?? new List<string>();

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    var addedRoles = selectedRoles.Except(userRoles).ToList();
                    var removedRoles = userRoles.Except(selectedRoles).ToList();

                    if (addedRoles.Any())
                    {
                        await _userManager.AddToRolesAsync(user, addedRoles);
                    }

                    if (removedRoles.Any())
                    {
                        await _userManager.RemoveFromRolesAsync(user, removedRoles);
                    }
                    TempData["S_Message"] = "Your operation was successful!";
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    TempData["E_Message"] = error.Description;
                }
            }

            ViewBag.Roles = new SelectList(_roleManager.Roles.ToList(), "Name", "Name");
            return View(model);
        }
        [CustomAuthorize("Admin")]

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        [CustomAuthorize("Admin")]


        [HttpPost]
        public async Task<IActionResult> DeleteConfirm(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            user.Is_Deleted = true;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                TempData["S_Message"] = "Your operation was successful!";
                return RedirectToAction("Index");
            }
            TempData["E_Message"] = "Your operation was Failed!";
            //ModelState.AddModelError(string.Empty, "Error deleting user.");
            return View(user);
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Users");
        }
    }
}
