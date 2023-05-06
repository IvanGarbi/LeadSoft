﻿using LeadSoft.Core.Models;

namespace LeadSoft.API.ViewModels;

public class GetAuthorViewModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; }
    public ICollection<Article> Articles { get; set; }
}

public class PostAuthorViewModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; }
}