using System;
using System.Collections.Generic;

namespace EmployeeManagement.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public float? Salary { get; set; }

    public string? Address { get; set; }

    public string Email { get; set; } = null!;

    public int Number { get; set; }
}
