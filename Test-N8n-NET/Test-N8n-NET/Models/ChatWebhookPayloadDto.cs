using System.ComponentModel.DataAnnotations;

namespace Test_N8n_NET.Models
{
    public class ChatWebhookPayloadDto
    {
        [Required]
        public string ChatInput { get; set; } = string.Empty;
    }
}