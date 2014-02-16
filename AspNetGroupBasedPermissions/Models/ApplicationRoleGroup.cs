using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace AspNetGroupBasedPermissions.Models
{
    public class ApplicationRoleGroup
    {
        public virtual string RoleId { get; set; }
        public virtual int GroupId { get; set; }

        public virtual ApplicationRole Role { get; set; }
        public virtual Group Group { get; set; }
    }
}