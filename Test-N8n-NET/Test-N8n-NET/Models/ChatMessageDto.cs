using System.ComponentModel.DataAnnotations;

namespace Test_N8n_NET.Models;

public sealed record ChatMessageDto(
    [Required(ErrorMessage = "Message is required")]
    [StringLength(500, ErrorMessage = "Message cannot be longer than 500 characters")]
    [Display(Name = "Chat Message")]
        string Message,
    [Display(Name = "User Name")]
    [StringLength(100, ErrorMessage = "User name cannot be longer than 100 characters")]
        string? UserName,
    [Display(Name = "Message Type")]
        string MessageType = "chat");