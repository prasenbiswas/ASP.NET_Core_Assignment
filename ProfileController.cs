using CoreAssignmentForRollBased.Models;
using CoreAssignmentForRollBased.Repository;
using CoreAssignmentForRollBased.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System.Web;

namespace CoreAssignmentForRollBased.Controllers
{
    [Authorize]

    public class ProfileController : Controller
    {
        private IGenericsRepository<Profile> _genericsRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public ProfileController(IGenericsRepository<Profile> profile, IWebHostEnvironment webHostEnvironment)
        {
            _genericsRepository = profile;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var data = _genericsRepository.GetAll();
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(ProfileViewModel model)
        {
            string stringFileName = UploadFile(model);
            var profile = new Profile
            {
                ApplicatioUserId = User.Claims.First().Value,
                Address = model.Address,
                FirstName = model.FirstName,
                ProfilePhoto = stringFileName,
                LastName = model.LastName,
                MobileNumber = model.MobileNumber,
            };
            _genericsRepository.Insert(profile);
            _genericsRepository.Save();
            return RedirectToAction("Index", "Profile");
        }

        private string UploadFile(ProfileViewModel model)
        {
            string fileName = null;
            if (model.ProfilePhoto != null)
            {
                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "_" + model.ProfilePhoto.FileName;
                string filePath = Path.Combine(serverFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfilePhoto.CopyTo(fileStream);
                }
            }
            return fileName;
        }

        [HttpGet]
        public async Task<ActionResult> EditEmployee(int id)
        {
            Profile model = _genericsRepository.GetById(id);
            ProfileViewModel viewModel = new ProfileViewModel
            {
                ApplicatioUserId = User.Claims.First().Value,
                FirstName = model.FirstName,
                LastName = model.LastName,
                MobileNumber = model.MobileNumber,
                Address = model.Address,
                ProfileId = model.ProfileId
            };
            ViewBag.Photo = model.ProfilePhoto;

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult EditEmployee(ProfileViewModel model)
        {

            string stringFileName = UploadFile(model);
            var profile = new Profile
            {
                ApplicatioUserId = User.Claims.First().Value,
                Address = model.Address,
                FirstName = model.FirstName,
                MobileNumber = model.MobileNumber,
                LastName = model.LastName,
                ProfileId = model.ProfileId,
                ProfilePhoto = stringFileName
            };
            _genericsRepository.Update(profile);
            _genericsRepository.Save();
            return RedirectToAction("Index", "Profile");
        }
        [HttpGet]
        public ActionResult DeletePro(int id)
        {
            _genericsRepository.Delete(id);
            _genericsRepository.Save();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _genericsRepository.Delete(id);
            _genericsRepository.Save();
            return RedirectToAction("Index", "Home");
        }
    }
}
