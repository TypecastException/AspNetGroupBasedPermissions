using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace AspNetExtendingIdentityRoles.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }


        public ApplicationRole(string name, string description) : base(name)
        {
            this.Description = description;
        }
        public virtual string Description { get; set; }
    }
}