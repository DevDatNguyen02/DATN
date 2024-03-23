using System.ComponentModel.DataAnnotations;

namespace HotelMarriotter.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}