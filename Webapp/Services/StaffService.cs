using Webapp.Models;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Webapp.Services;

public class StaffService : IStaffService
{
    private readonly List<Staff> _staffList;
    private readonly string _appDataPath;
    private readonly string _staffDataFile;
    private const string DefaultPhotoUrl = "https://thumbs.dreamstime.com/b/default-avatar-profile-icon-vector-social-media-user-portrait-176256935.jpg";
    
    private static readonly Regex EmailRegex = new Regex(@"^[^\s@]+@[^\s@.]+(\.[^\s@.]+)+$");
    private static readonly Regex PhoneRegex = new Regex(@"^(\+?[1-9]\d{0,3}[-\s]?)?\d{3}[-\s]?\d{3}[-\s]?\d{4}$");
    
    private readonly object _fileLock = new object();

    public StaffService()
    {
        _staffList = new List<Staff>();
        _appDataPath = Path.Combine(Directory.GetCurrentDirectory(), "AppData");
        _staffDataFile = Path.Combine(_appDataPath, "staff.json");
        
        InitializeData();
    }

    private void InitializeData()
    {
        if (!Directory.Exists(_appDataPath))
        {
            Directory.CreateDirectory(_appDataPath);
        }

        if (File.Exists(_staffDataFile))
        {
            LoadStaffData();
        }
        else
        {
            // Default staff data
            _staffList.Add(new Staff
            {
                Id = "S001",
                Name = "John Wick",
                Email = "SparklingDaisy@gmail.com",
                Phone = "679-116-9420",
                StartingDate = new DateTime(2009, 10, 25),
                PhotoUrl = DefaultPhotoUrl
            });
            SaveStaffData();
        }
    }

    private void SaveStaffData()
    {
        lock (_fileLock)
        {
            var json = JsonSerializer.Serialize(_staffList, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_staffDataFile, json);
        }
    }

    private void LoadStaffData()
    {
        lock (_fileLock)
        {
            var json = File.ReadAllText(_staffDataFile);
            var loadedStaff = JsonSerializer.Deserialize<List<Staff>>(json);
            if (loadedStaff != null)
            {
                _staffList.Clear();
                _staffList.AddRange(loadedStaff);
            }
        }
    }

    // Public interface methods
    public List<Staff> GetStaffList()
    {
        return _staffList.ToList();
    }

    public Staff? GetStaffById(string id)
    {
        return _staffList.FirstOrDefault(x => x.Id == id);
    }

    public void AddNewStaff(Staff staff)
    {
        if (string.IsNullOrEmpty(staff.PhotoUrl))
        {
            staff.PhotoUrl = DefaultPhotoUrl;
        }
        
        _staffList.Add(staff);
        SaveStaffData();
    }

    // Validation methods
    public bool ValidateEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
            return false;
        return EmailRegex.IsMatch(email);
    }

    public bool ValidatePhone(string phone)
    {
        if (string.IsNullOrEmpty(phone))
            return false;
        return PhoneRegex.IsMatch(phone);
    }

    public bool IsIdUnique(string id)
    {
        return !_staffList.Any(s => s.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
    }

    public bool IsEmailUnique(string email)
    {
        return !_staffList.Any(s => s.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
    }

    public bool IsPhoneUnique(string phone)
    {
        return !_staffList.Any(s => s.Phone.Equals(phone, StringComparison.OrdinalIgnoreCase));
    }
}
