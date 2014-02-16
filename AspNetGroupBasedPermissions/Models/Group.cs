using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace AspNetGroupBasedPermissions.Models
{
    public class Group
    {
        public Group()
        {
        }


        public Group(string name) : this()
        {
            this.Roles = new List<ApplicationRoleGroup>();
            this.Name = name;
        }


        [Key]
        [Required]
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }
        public virtual ICollection<ApplicationRoleGroup> Roles { get; set; }
    }
}