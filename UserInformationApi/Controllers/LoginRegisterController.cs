using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserInformationApi.Models;
using UserInformationApi.Modeldto;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace UserInformationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginRegisterController : ControllerBase
    {
        private readonly EmployeeDbContext _context;

        public LoginRegisterController(EmployeeDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest model)
        {
            // Check if the username already exists
            if (await _context.EmployeeLogins.AnyAsync(e => e.Username == model.Username))
            {
                return Conflict(new { message = "Username already exists" });
            }

            // Hash the password
            string passwordHash = HashPassword(model.Password);

            // Create and save the new employee
            var employee = new Employee
            {
                EmployeeId = model.EmployeeId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Gender = model.Gender,
                DateOfBirth = model.DateOfBirth,
                Email = model.Email,
                Phone =model.Phone,
                Address = model.Address,
                City = model.City,  
                State = model.State,    
                ZipCode = model.ZipCode,
                Department = model.Department,
                Position = model.Position,
                Salary= model.Salary,   
                HireDate = model.HireDate
                
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            // Retrieve the EmployeeID after saving
            int employeeId = employee.EmployeeId;

            // Create and save the login credentials
            var employeeLogin = new EmployeeLogin
            {
                EmployeeId = employeeId,
                Username = model.Username,
                PasswordHash = passwordHash
            };

            _context.EmployeeLogins.Add(employeeLogin);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Registration successful" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest model)
        { 
            // Find the EmployeeLogin record for the provided username
            var employeeLogin = await _context.EmployeeLogins
                .FirstOrDefaultAsync(e => e.Username == model.Username);

            // Check if the username exists
            if (employeeLogin == null)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            // Verify the password
            if (!VerifyPassword(model.Password, employeeLogin.PasswordHash))
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            // Retrieve the associated employee
            var employee = await _context.Employees.FindAsync(employeeLogin.EmployeeId);

            // Generate a JWT token
            // string token = GenerateToken(employee);

            return Ok(new { message = "Login successful" });
        
    }

        private bool VerifyPassword(string password, string passwordHash)
        {
            // Implement password verification logic using a secure hashing algorithm like bcrypt
            // For demonstration purposes, we'll just compare the password hash directly

            // Decode the stored password hash from Base64
            byte[] storedPasswordHashBytes = Convert.FromBase64String(passwordHash);

            // Hash the provided password using the same hashing algorithm
            string providedPasswordHash = HashPassword(password);

            // Compare the hashed password stored in the database with the hashed password provided by the user
            return providedPasswordHash == passwordHash;
        }

       
        private string HashPassword(string password)
        {
            // Implement or call your password hashing function here
            // This is a placeholder method. Use a secure password hashing mechanism
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}
