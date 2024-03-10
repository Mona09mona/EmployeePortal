using System;
using System.Collections.Generic;

namespace UserInformationApi.Models;

public partial class EmployeeLogin
{
    public int EmployeeId { get; set; }

    public string? Username { get; set; }

    public string? PasswordHash { get; set; }
}
