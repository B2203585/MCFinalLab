Don't forget to add the unit test project to the solution file:
dotnet sln add <test project name>

Also add the reference to the Webapp:
dotnet add <test project name> reference Webapp



### **Intermediary Layer** - Service
- IStaffService.cs
- StaffService.cs

### **Interface Layer** - Controller + Views
- Controller (`Controllers/StaffController.cs`)
- Views (`Views/Staff/`)
    + Index.cshtml
    + Add.cshtml
    + Details.cshtmlmember



### Methods to test in StaffService.cs:
- GetStaffList()
- GetStaffById(string Id)
- AddNewStaff(Staff staff)
- ValidateEmail(string email)
- ValidatePhone(string phone)
- IsIdUnique(string id)
- IsEmailUnique(string email)
- IsPhoneUnique(string phone)



## Test Selectors (data-testid attributes)
- Index.cshtml (`/`)
    + data-testid="staff-card-{id}"
    + data-testid="view-details-button-{id}"

- Details.cshtml (`/StaffDetail/{id}`)
    + data-testid="staff-details-card"
    + data-testid="staff-photo"
    + data-testid="staff-id-display"
    + data-testid="staff-name-display"
    + data-testid="staff-email-display"
    + data-testid="staff-phone-display"
    + data-testid="staff-startdate-display"
    + data-testid="back-to-list-button"

- Add.cshtml (`/AddStaff`)
    + data-testid="staff-id-input"
    + data-testid="staff-name-input"
    + data-testid="email-input"
    + data-testid="phone-input"
    + data-testid="start-date-input"
    + data-testid="photo-url-input"
    + data-testid="submit-button"



### Regex test case
### ValidateEmail - `^[^\s@]+@[^\s@.]+(\.[^\s@.]+)+$`
- Good case:
    + test@example.com
    + john.doe@company.co.uk
    + user123@my-server.net
    + test@subdomain.domain.com

- Bad case:
    + test@gmail
    + test@.com
    + test@@gmail.com
    + test @gmail.com


### ValidatePhone - `^(\+?[1-9]\d{0,3}[-\s]?)?\d{3}[-\s]?\d{3}[-\s]?\d{4}$`
- Good case:
    + 123-456-7890
    + 123 456 7890
    + 1234567890
    + +1 123 456 7890
    + +1-123-456-7890
    + +44 123 456 7890

- Bad case:
    + (123) 456-7890
    + 1-800-ACBDEF
    + 123-456-789
    + 123 456 78901
    + +01 123 456 7890



**That's all, may the (AI) slop be with you**