﻿using System.ComponentModel.DataAnnotations;

namespace MoviesRental.Models;

public class User
{
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public required string Name { get; set; }

    [Required]
    [EmailAddress]
    public required string EmailAddress { get; set; }

    [Required]
    [StringLength(255)]
    public required string Password { get; set; }

    [Display(Name = "Date of Birth")]
    public DateOnly? BirthDate { get; set; }

    public List<string> Role { get; set; } = ["User"];

    public List<UserMovie> Movies { get; set; } = new List<UserMovie>();
}

