using System;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using AspNetGroupBasedPermissions.Models;

namespace AspNetGroupBasedPermissions.Helpers
{
    public class AuthorizeGroupAttribute : AuthorizeAttribute
    {
        private string _groups;
        private string[] _groupSplit = new string[0];
        public string Groups
        {
            get { return _groups ?? String.Empty; }
            set
            {
                _groups = value;
                _groupSplit = SplitString(value);
            }
        }


        private static string[] SplitString(string original)
        {
            if (String.IsNullOrEmpty(original))
            {
                return new string[0];
            }

            var split = from piece in original.Split(',')
                        let trimmed = piece.Trim()
                        where !String.IsNullOrEmpty(trimmed)
                        select trimmed;
            return split.ToArray();
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized)
                return false;

            if (httpContext.Session == null)
                return false;

            using (var db = new ApplicationDbContext())
            {
                IPrincipal user = httpContext.User;
                ApplicationUser loggedUser = db.Users.FirstOrDefault(u => u.UserName == user.Identity.Name);

                if (loggedUser == null)
                    return false;

                var group = loggedUser.Groups.FirstOrDefault(g => _groupSplit.Any(sp => sp.Contains(g.Group.Name)));

                return _groupSplit.Length <= 0 || @group != null;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //            filterContext.Result = new RedirectToRouteResult(
            //                        new RouteValueDictionary(
            //                            new
            //                            {
            //                                controller = "Error",
            //                                action = "Unauthorised"
            //                            })
            //                        );
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}