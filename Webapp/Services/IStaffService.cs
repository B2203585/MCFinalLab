using Webapp.Models;

namespace Webapp.Services;

public interface IStaffService
{
    List<Staff> GetStaffList();
    Staff? GetStaffById(string id);
    
    void AddNewStaff(Staff staff);
    

    bool ValidateEmail(string email);
    bool ValidatePhone(string phone);
    
    bool IsIdUnique(string id);
    bool IsEmailUnique(string email);
    bool IsPhoneUnique(string phone);
}
