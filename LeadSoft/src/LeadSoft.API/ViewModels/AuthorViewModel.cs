using LeadSoft.Core.Models;
using System.ComponentModel.DataAnnotations;

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
    [Required(ErrorMessage = "The field {0} is required.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "The field {0} is required.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "The field {0} is required.")]
    public DateTime DateOfBirth { get; set; }

    [Required(ErrorMessage = "The field {0} is required.")]
    [EmailAddress(ErrorMessage = "The field {0} is invalid.")]
    public string Email { get; set; }
}