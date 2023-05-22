using Microsoft.AspNetCore.Components.Forms;

namespace Interview.Models
{
    public class MailRequest
    {
        public string? ToEmail { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public List<IBrowserFile> Attachments { get; set; } = new();
        public string? pathMail { get; set; }
        public string? Name { get; set; }
        public DateTime timeMeeting { get; set; }
        public string? location { get; set; } 
        public string? role { get; set; }
        public string? who { get; set; }
        public string? flexibleBody { get; set; }
        public int statusTrack { get; set; }
        
    }
}
