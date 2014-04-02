using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using AspNetGroupBasedPermissions.Models;
using Microsoft.AspNet.Identity;

namespace AspNetGroupBasedPermissions.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        private const string InitialUserName = "test@test.com";
        private const string InitialUserFirstName = "TestFirstName";
        private const string InitialUserLastName = "TestLastName";
        private const string InitialUserEmail = "test@test.com";
        private const string InitialUserPassword = "Password1";

        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        private readonly string[] _groupAdminRoleNames = {"CanEditUser", "CanEditGroup", "User"};
        private readonly IdentityManager _idManager = new IdentityManager();

        private readonly string[] _initialGroupNames = {"SuperAdmins", "GroupAdmins", "UserAdmins", "Users"};


        private readonly string[] _superAdminRoleNames = {"Admin", "CanEditUser", "CanEditGroup", "CanEditRole", "User"};
        private readonly string[] _userAdminRoleNames = {"CanEditUser", "User"};
        private readonly string[] _userRoleNames = {"User"};

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            AddGroups();
            AddRoles();
            AddUsers();
            AddRolesToGroups();
            AddUsersToGroups();
        }

        public void AddGroups()
        {
            foreach (var groupName in _initialGroupNames)
            {

                try
                {
                    _idManager.CreateGroup(groupName);
                }
                catch (GroupExistsException)
                {
                    // intentionally catched for seeding
                }
            }
        }

        private void AddRoles()
        {
            // Some example initial roles. These COULD BE much more granular:
            _idManager.CreateRole("Admin", "Global Access");
            _idManager.CreateRole("CanEditUser", "Add, modify, and delete Users");
            _idManager.CreateRole("CanEditGroup", "Add, modify, and delete Groups");
            _idManager.CreateRole("CanEditRole", "Add, modify, and delete roles");
            _idManager.CreateRole("User", "Restricted to business domain activity");
        }

        private void AddRolesToGroups()
        {
            // Add the Super-Admin Roles to the Super-Admin Group:
            IDbSet<Group> allGroups = _db.Groups;
            Group superAdmins = allGroups.First(g => g.Name == "SuperAdmins");
            foreach (string name in _superAdminRoleNames)
            {
                _idManager.AddRoleToGroup(superAdmins.Id, name);
            }

            // Add the Group-Admin Roles to the Group-Admin Group:
            Group groupAdmins = _db.Groups.First(g => g.Name == "GroupAdmins");
            foreach (string name in _groupAdminRoleNames)
            {
                _idManager.AddRoleToGroup(groupAdmins.Id, name);
            }

            // Add the User-Admin Roles to the User-Admin Group:
            Group userAdmins = _db.Groups.First(g => g.Name == "UserAdmins");
            foreach (string name in _userAdminRoleNames)
            {
                _idManager.AddRoleToGroup(userAdmins.Id, name);
            }

            // Add the User Roles to the Users Group:
            Group users = _db.Groups.First(g => g.Name == "Users");
            foreach (string name in _userRoleNames)
            {
                _idManager.AddRoleToGroup(users.Id, name);
            }
        }

        // Change these to your own:

        private void AddUsers()
        {
            var newUser = new ApplicationUser
            {
                UserName = InitialUserName,
                FirstName = InitialUserFirstName,
                LastName = InitialUserLastName,
                Email = InitialUserEmail
            };

            // Be careful here - you  will need to use a password which will 
            // be valid under the password rules for the application, 
            // or the process will abort:
            var userCreationResult = _idManager.CreateUser(newUser, InitialUserPassword);
            if (!userCreationResult.Succeeded)
            {
                // warn the user that it's seeding went wrong
                throw new DbEntityValidationException("Could not create InitialUser because: " + String.Join(", ", userCreationResult.Errors));
            }
        }

        // Configure the initial Super-Admin user:
        private void AddUsersToGroups()
        {
            Console.WriteLine(String.Join(", ", _db.Users.Select(u => u.Email)));
            ApplicationUser user = _db.Users.First(u => u.UserName == InitialUserName);
            IDbSet<Group> allGroups = _db.Groups;
            foreach (Group group in allGroups)
            {
                _idManager.AddUserToGroup(user.Id, group.Id);
            }
        }
    }
}