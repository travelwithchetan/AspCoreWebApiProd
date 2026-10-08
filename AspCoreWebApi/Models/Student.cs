using System;
using System.Collections.Generic;

namespace AspCoreWebApi.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string Email { get; set; } = null!;

    public string? Mobile { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? City { get; set; }

    public DateTime? CreatedDate { get; set; }
}
