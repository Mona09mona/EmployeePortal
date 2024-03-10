namespace UserInformationApi.Modeldto
{
    public class RegisterRequest
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
         
        public string? Gender { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? ZipCode { get; set; }

        public string? Department { get; set; }

        public string? Position { get; set; }

        public decimal? Salary { get; set; }

        public DateOnly? HireDate { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
