using System;
using System.Web;

namespace Domain.Models.Command
{
    public class UserSaveModel
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public DateTime DOB { get; set; }
        public HttpPostedFileBase ProfilePicture { get; set; }
        public HttpPostedFileBase Document { get; set; } 
    }
}
