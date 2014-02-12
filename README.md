ASP.NET Extending Identity Roles
======================================

This is an example project to accompany a blog post describing how to extend the `AspNet.Identity.EntityFramework.IdentityRole` class as implemented in the ASP.NET MVC 5 Identity system. 

This project builds upon the foundation created by another example, [ASP.NET Role-Based Security Example][3], covered in the article [Extending Identity Accounts and Implementing Role-Based Authentication in ASP.NET MVC 5][1]. You can see in the commit history the basic steps taken to move from the previous project structure to one where I have added some very basic extensions to the `IdentityRole` class. 

In the previous article, we discuss the relative ease with which the `IdentityUser` class may be extended to add additional properties, and the implementation of very basic role management. Unlike `IdentityUser`, extension of the `IdentityRole` class requires a bit more effort, a little insight into the internals of the ASP.NET 5 Identity system, and a few work-arounds. 

You will need to enable Nuget Package Restore in Visual Studio in order to download and restore Nuget packages during build. If you are not sure how to do this, see [Keep Nuget Packages Out of Source Control with Nuget Package Manager Restore][2]

You will also need to run Entity Framework Migrations `Update-Database` command per the article. The migration files are included in the repo, so you will NOT need to `Enable-Migrations` or run `Add-Migration Init`. 

[1]: http://www.typecastexception.com/post/2013/11/11/Extending-Identity-Accounts-and-Implementing-Role-Based-Authentication-in-ASPNET-MVC-5.aspx "Extending Identity Accounts and Implementing Role-Based Authentication in ASP.NET MVC 5"

[2]: http://www.typecastexception.com/post/2013/11/10/Keep-Nuget-Packages-Out-of-Source-Control-with-Nuget-Package-Manager-Restore.aspx "Keep Nuget Packages Out of Source Control with Nuget Package Manager Restore"

[3]: https://github.com/xivSolutions/AspNetRoleBasedSecurityExample "ASP.NET Role-Based Security Example"

