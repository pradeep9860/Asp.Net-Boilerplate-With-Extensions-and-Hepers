using System.ComponentModel.DataAnnotations;

namespace Mongos.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}