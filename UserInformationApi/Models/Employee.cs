using System;
using System.Collections.Generic;

namespace UserInformationApi.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

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
}
