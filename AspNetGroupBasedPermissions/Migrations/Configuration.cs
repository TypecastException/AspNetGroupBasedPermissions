using AspNetGroupBasedPermissions.Models;
using System.Data.Entity.Migrations;

namespace AspNetGroupBasedPermissions.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        IdentityManager _idManager = new IdentityManager();
        ApplicationDbContext _db = new ApplicationDbContext();

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }


        protected override void Seed(ApplicationDbContext context)
        {
            this.AddGroups();
            this.AddRoles();
            this.AddUsers();
            this.AddRolesToGroups();
            this.AddUsersToGroups();
        }

        string[] _initialGroupNames = new string[] { "SuperAdmins", "GroupAdmins", "UserAdmins", "Users" };

        public void AddGroups()
        {
            foreach (var groupName in _initialGroupNames)
            {
                _idManager.CreateGroup(groupName);
            }
        }


        void AddRoles()
        {
            // Some example initial roles. These COULD BE much more granular:
            _idManager.CreateRole("Admin", "Global Access");
            _idManager.CreateRole("CanEditUser", "Add, modify, and delete Users");
            _idManager.CreateRole("CanEditGroup", "Add, modify, and delete Groups");
            _idManager.CreateRole("CanEditRole", "Add, modify, and delete roles");
            _idManager.CreateRole("User", "Restricted to business domain activity");
        }


        string[] _superAdminRoleNames = new string[] { "Admin", "CanEditUser", "CanEditGroup", "CanEditRole", "User" };
        string[] _groupAdminRoleNames =
            new string[] { "CanEditUser", "CanEditGroup", "User" };
        string[] _userAdminRoleNames =
            new string[] { "CanEditUser", "User" };
        string[] _userRoleNames =
            new string[] { "User" };
        void AddRolesToGroups()
        {
            // Add the Super-Admin Roles to the Super-Admin Group:
            var allGroups = _db.Groups;
            var superAdmins = allGroups.First(g => g.Name == "SuperAdmins");
            foreach (string name in _superAdminRoleNames)
            {
                _idManager.AddRoleToGroup(superAdmins.Id, name);
            }

            // Add the Group-Admin Roles to the Group-Admin Group:
            var groupAdmins = _db.Groups.First(g => g.Name == "GroupAdmins");
            foreach (string name in _groupAdminRoleNames)
            {
                _idManager.AddRoleToGroup(groupAdmins.Id, name);
            }

            // Add the User-Admin Roles to the User-Admin Group:
            var userAdmins = _db.Groups.First(g => g.Name == "UserAdmins");
            foreach (string name in _userAdminRoleNames)
            {
                _idManager.AddRoleToGroup(userAdmins.Id, name);
            }

            // Add the User Roles to the Users Group:
            var users = _db.Groups.First(g => g.Name == "Users");
            foreach (string name in _userRoleNames)
            {
                _idManager.AddRoleToGroup(users.Id, name);
            }
        }


        // Change these to your own:
        string _initialUserName = "jatten";
        string _InitialUserFirstName = "John";
        string _initialUserLastName = "Atten";
        string _initialUserEmail = "jatten@typecastexception.com";
        void AddUsers()
        {
            var newUser = new ApplicationUser()
            {
                UserName = _initialUserName,
                FirstName = _InitialUserFirstName,
                LastName = _initialUserLastName,
                Email = _initialUserEmail
            };

            // Be careful here - you  will need to use a password which will 
            // be valid under the password rules for the application, 
            // or the process will abort:
            _idManager.CreateUser(newUser, "Password1");
        }


        // Configure the initial Super-Admin user:
        void AddUsersToGroups()
        {
            var user = _db.Users.First(u => u.UserName == _initialUserName);
            var allGroups = _db.Groups;
            foreach (var group in allGroups)
            {
                _idManager.AddUserToGroup(user.Id, group.Id);
            }
        }
    }
}
