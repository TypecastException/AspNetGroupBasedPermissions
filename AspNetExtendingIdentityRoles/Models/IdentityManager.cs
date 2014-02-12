using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using AspNetExtendingIdentityRoles.Models;
using System.Linq;

namespace AspNetExtendingIdentityRoles.Models
{
    public class IdentityManager
    {
        RoleManager<IdentityRole> _roleManager = new RoleManager<IdentityRole>(
            new RoleStore<IdentityRole>(new ApplicationDbContext()));

        UserManager<ApplicationUser> _userManager = new UserManager<ApplicationUser>(
            new UserStore<ApplicationUser>(new ApplicationDbContext()));

        ApplicationDbContext _db = new ApplicationDbContext();


        public bool RoleExists(string name)
        {
            return _roleManager.RoleExists(name);
        }


        public bool CreateRole(string name, string description = "")
        {
            var idResult = _roleManager.Create(new ApplicationRole(name, description));
            return idResult.Succeeded;
        }


        public bool CreateUser(ApplicationUser user, string password)
        {
            var idResult = _userManager.Create(user, password);
            return idResult.Succeeded;
        }


        public bool AddUserToRole(string userId, string roleName)
        {
            var idResult = _userManager.AddToRole(userId, roleName);
            return idResult.Succeeded;
        }


        public void ClearUserRoles(string userId)
        {
            var user = _userManager.FindById(userId);
            var currentRoles = new List<IdentityUserRole>();
            currentRoles.AddRange(user.Roles);
            foreach (var role in currentRoles)
            {
                _userManager.RemoveFromRole(userId, role.Role.Name);
            }
        }


        public void RemoveFromRole(string userId, string roleName)
        {
            _userManager.RemoveFromRole(userId, roleName);
        }


        public void DeleteRole(string roleId)
        {
            var roleUsers = _db.Users.Where(u => u.Roles.Any(r => r.RoleId == roleId));
            var role = _db.Roles.Find(roleId);

            foreach (var user in roleUsers)
            {
                this.RemoveFromRole(user.Id, role.Name);
            }
            _db.Roles.Remove(role);
            _db.SaveChanges();
        }
    }
}