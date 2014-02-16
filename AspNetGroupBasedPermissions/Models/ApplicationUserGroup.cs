using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace AspNetGroupBasedPermissions.Models
{
    public class ApplicationUserGroup
    {
        [Required]
        public virtual string UserId { get; set; }
        [Required]
        public virtual int GroupId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Group Group { get; set; }
    }
}