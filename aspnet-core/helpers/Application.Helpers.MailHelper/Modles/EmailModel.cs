using System.Collections.Generic;

namespace Application.Helpers.MailHelper.Models
{
    public class EmailModel<TEmailDataEntity> where TEmailDataEntity : class
    {
        public string To { get; set; }
        public List<string> Ccs { get; set; } 
        public string Subject { get; set; } 
        public List<string> ExtraEmailAddresses { get; set; } = new List<string>(); 
        public TEmailDataEntity Data { get; set; } 
    } 

    public class EmailModel
    {
        public string To { get; set; }
        public List<string> Ccs { get; set; }
        public string Subject { get; set; } 
        public List<string> ExtraEmailAddresses { get; set; } = new List<string>();
        public string Body { get; set; } 
    }
} 