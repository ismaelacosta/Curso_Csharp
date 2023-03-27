using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Data;
using Shopping.Data.Entities;
using Shopping.Enums;
using Shopping.Helpers;
using Shopping.Models;

namespace Shopping.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IBlobHelper _blobHelper;
        private readonly IComboHelper _comboHelper;

        public UsersController(DataContext context,IUserHelper userHelper, IBlobHelper blobHelper, IComboHelper comboHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _blobHelper = blobHelper;
            _comboHelper = comboHelper;
        }

        public async Task<IActionResult> Index()
        {
            /*return _context.Categories != null ?
                        View(await _context.Categories.ToListAsync()) :
                        Problem("Entity set 'DataContext.Categories'  is null.");*/
            try
            {
                return View(await _context.Users
                    .Include(u => u.City)
                    .ThenInclude(s => s.State)
                    .ThenInclude(c => c.Country)
                    .ToListAsync());
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IActionResult> Create()
        {
            AddUserViewModel model = new()
            {
                Id = Guid.Empty.ToString(),
                Countries = await _comboHelper.GetComboCountriesAsync(),
                States = await _comboHelper.GetComboStatesAsync(0), // No hay Estados con 0
                Cities = await _comboHelper.GetComboCitiesAsync(0),
                UserType = UserType.Admin,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = Guid.Empty;

                if (model.ImageFile != null)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "users");
                }

                model.ImageId = imageId;
                User user = await _userHelper.AddUserAsync(model);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Este correo ya esta siendo usado.");
                    model.Countries = await _comboHelper.GetComboCountriesAsync();
                    model.States = await _comboHelper.GetComboStatesAsync(model.CountryId);
                    model.Cities = await _comboHelper.GetComboCitiesAsync(model.StateId);


                    return View(model);
                }
                return RedirectToAction("Create", "Users");
                
            }
            model.Countries = await _comboHelper.GetComboCountriesAsync();
            model.States = await _comboHelper.GetComboStatesAsync(model.CountryId);
            model.Cities = await _comboHelper.GetComboCitiesAsync(model.StateId);
            return View(model);
        }


        public JsonResult GetStates(int countryId)
        {
            Country country = _context.Countries
                .Include(c => c.States)
                .FirstOrDefault(c => c.Id == countryId);

            if (country == null)
            {
                return null;
            }

            return Json(country.States.OrderBy(d => d.Name));
        }

        public JsonResult GetCities(int stateId)
        {
            State state = _context.States
                .Include(c => c.Cities)
                .FirstOrDefault(c => c.Id == stateId);

            if (state == null)
            {
                return null;
            }

            return Json(state.Cities.OrderBy(d => d.Name));
        }
    

}
}
