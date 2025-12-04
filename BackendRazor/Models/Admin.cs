using System;
using System.Collections.Generic;

namespace BackendRazor.Models;

public partial class Admin
{
    public string Idadmin { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;
}
