using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;

namespace Interview.Models
{
    public class Users
    {
        public int userID { get; set; }


        public string userName { get; set; }


        
        public string Email { get; set; }

       
        
        public string Phone { get; set; }

       
        public string Address { get; set; }

   
        public DateTime Birthday { get; set; }
       


        public string Position { get; set; }
        public string Source { get; set; }
        public string Role { get; set; }
        public int Status { get; set; }
        public string Applied { get; set; }
        public string pathCV { get; set; }
        public DateTime TimeMeeting { get; set; } = DateTime.Now;
        public string Place { get; set; } 
        public string statusContact { get; set; }
        public bool isChecked { get; set; }
        public bool isCheckAll { get; set; }
        public bool isCreateMail { get; set; }
        public string To { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string failedValue { get; set; } = string.Empty;
        public List<string>  SelectedOption { get; set; }
        public string Room { get; set; }
        public string PvEr_Name { get; set; }

        public string PvEr_Mail { get; set; }
        public string who { get; set; }
        public string Comment { get; set; }
        public int typeMail { get; set; } = 0;
        public bool isClicked { get; set; } = true;
    }
}
