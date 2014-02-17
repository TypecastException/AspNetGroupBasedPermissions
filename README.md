ASP.NET Extending Identity Roles
======================================

This is an example project to accompany a blog post describing how to extend the ASP.NET MVC 5 Identity system and implement a Group-based permission scheme. Users belong to Groups, and Groups have sets of authorization permissions to exxecute code within the application (using [Authorize]).

This project builds upon the foundation created by another example, [ASP.NET Identity: Extending Identity Roles][3], covered in the article [Extending Identity Accounts and Implementing Role-Based Authentication in ASP.NET MVC 5][1]. You can see in the commit history the basic steps taken to move from the previous project structure to one with a basic, but flexible Group-based security model. 

You may need to enable Nuget Package Restore in Visual Studio in order to download and restore Nuget packages during build. If you are not sure how to do this, see [Keep Nuget Packages Out of Source Control with Nuget Package Manager Restore][2] Apparrently, this is supposedly not required with Nuget anymore, but in case you need to . . .

You will also need to run Entity Framework Migrations `Update-Database` command per the article. The migration files are included in the repo, so you will NOT need to `Enable-Migrations` or run `Add-Migration Init`. 

[1]: http://typecastexception.com/post/2014/02/13/ASPNET-MVC-5-Identity-Extending-and-Modifying-Roles.aspx "ASP.NET MVC 5 Identity: Extending and Modifying Roles"

[2]: http://www.typecastexception.com/post/2013/11/10/Keep-Nuget-Packages-Out-of-Source-Control-with-Nuget-Package-Manager-Restore.aspx "Keep Nuget Packages Out of Source Control with Nuget Package Manager Restore"

[3]: https://github.com/TypecastException/AspNetExtendingIdentityRoles "ASP.NET Identity: Extending Identity Roles"

