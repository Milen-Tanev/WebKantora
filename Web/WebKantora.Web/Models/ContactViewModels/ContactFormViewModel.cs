using System.ComponentModel.DataAnnotations;

namespace WebKantora.Web.Models.ContactViewModels
{
    public class ContactFormViewModel
    {
        //TODO: Min/Max Length
        [Required]
        [Display(Name = "Име")]
        public string FirstName { get; set; }

        //TODO: Min/Max Length
        [Required]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; }

        //TODO: Min/Max Length
        [Required]
        [Display(Name = "Съобщение")]
        public string Content { get; set; }
    }
}
