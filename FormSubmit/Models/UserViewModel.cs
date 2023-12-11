using System.ComponentModel.DataAnnotations;
using System.Xml;
using System.Xml.Linq;

namespace FormSubmit.Models
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Fast Name")]
        public string FirstName { set; get; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set;}
    }
}
