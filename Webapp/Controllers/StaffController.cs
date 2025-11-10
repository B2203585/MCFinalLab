using Microsoft.AspNetCore.Mvc;
using Webapp.Models;
using Webapp.Services;

namespace Webapp.Controllers;

public class StaffController : Controller
{
    private readonly IStaffService _staffService;

    public StaffController(IStaffService staffService)
    {
        _staffService = staffService;
    }

    // View routes
    [Route("")]
    public IActionResult Index()
    {
        var staffListData = _staffService.GetStaffList();
        return View(staffListData);
    }

    [Route("StaffDetail/{id}")]
    public IActionResult Details(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return NotFound();
        }

        var staff = _staffService.GetStaffById(id);
        if (staff == null)
        {
            return NotFound();
        }

        return View(staff);
    }

    [Route("AddStaff")]
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [Route("AddStaff")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add(Staff staff)
    {
        bool isInputValid = ValidateStaffInput(staff);
        bool isFormatValid = ValidateStaffFormat(staff);
        bool isUniquenessValid = ValidateStaffUniqueness(staff);

        if (!isInputValid || !isFormatValid || !isUniquenessValid)
        {
            return View(staff);
        }

        _staffService.AddNewStaff(staff);
        return RedirectToAction(nameof(Index));
    }

    // Validates
    private bool ValidateStaffInput(Staff staff)
    {
        bool isValid = true;

        if (string.IsNullOrEmpty(staff.Id))
        {
            ModelState.AddModelError("Id", "Staff ID cannot be empty.");
            isValid = false;
        }
        if (string.IsNullOrEmpty(staff.Name))
        {
            ModelState.AddModelError("Name", "Staff Name cannot be empty.");
            isValid = false;
        }
        if (string.IsNullOrEmpty(staff.Email))
        {
            ModelState.AddModelError("Email", "Email cannot be empty.");
            isValid = false;
        }
        if (string.IsNullOrEmpty(staff.Phone))
        {
            ModelState.AddModelError("Phone", "Phone cannot be empty.");
            isValid = false;
        }

        return isValid;
    }

    private bool ValidateStaffFormat(Staff staff)
    {
        bool isValid = true;

        if (!string.IsNullOrEmpty(staff.Email) && !_staffService.ValidateEmail(staff.Email))
        {
            ModelState.AddModelError("Email", "Invalid email format.");
            isValid = false;
        }
        if (!string.IsNullOrEmpty(staff.Phone) && !_staffService.ValidatePhone(staff.Phone))
        {
            ModelState.AddModelError("Phone", "Invalid phone format. (e.g., +1 123 456 7890)");
            isValid = false;
        }

        return isValid;
    }

    private bool ValidateStaffUniqueness(Staff staff)
    {
        bool isValid = true;

        if (!string.IsNullOrEmpty(staff.Id) && !_staffService.IsIdUnique(staff.Id))
        {
            ModelState.AddModelError("Id", "This Staff ID is already in use. Please enter a unique one.");
            isValid = false;
        }
        if (!string.IsNullOrEmpty(staff.Email) && !_staffService.IsEmailUnique(staff.Email))
        {
            ModelState.AddModelError("Email", "This Email is already in use by another staff member.");
            isValid = false;
        }
        if (!string.IsNullOrEmpty(staff.Phone) && !_staffService.IsPhoneUnique(staff.Phone))
        {
            ModelState.AddModelError("Phone", "This Phone number is already in use by another staff member.");
            isValid = false;
        }

        return isValid;
    }
}