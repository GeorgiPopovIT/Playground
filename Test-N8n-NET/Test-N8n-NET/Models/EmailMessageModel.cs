using System.ComponentModel.DataAnnotations;

namespace Test_N8n_NET.Models;

public sealed record EmailMessageModel(
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    [Display(Name = "Email Address")]
        string Email,
    [Required(ErrorMessage = "Subject is required")]
    [StringLength(200, ErrorMessage = "Subject cannot be longer than 200 characters")]
    [Display(Name = "Subject")]
        string Subject,
    [Required(ErrorMessage = "Message is required")]
    [StringLength(1000, ErrorMessage = "Message cannot be longer than 1000 characters")]
    [Display(Name = "Your Message")]
        string Message);
