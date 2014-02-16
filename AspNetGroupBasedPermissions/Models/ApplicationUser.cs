using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace AspNetGroupBasedPermissions.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
            : base()
        {
            this.Groups = new HashSet<ApplicationUserGroup>();
        }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public virtual ICollection<ApplicationUserGroup> Groups { get; set; }

    }
}