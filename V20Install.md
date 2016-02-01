# Installation #

## Development ##

  1. Install Adobe Flash Player ([Download](http://get.adobe.com/flashplayer/))
  1. Install Visual Studio 2010 (without SQL Express)
  1. Install Microsoft Web Platform Installer ([Download](http://www.microsoft.com/web/downloads/platform.aspx))
  1. Using Microsoft Web Platform Installer select following items to install:
    1. SQL Server Express 2008 `R2`
    1. SQL Server 2008 Management Studio Express
    1. IIS 7.5
    1. Default recommended settings for IIS 7.5
    1. ASP.NET MVC 2
  1. Install Tortoise SVN
  1. Check out from https://iudico.googlecode.com/svn/branches/IUDICO
  1. Create folder BasicWebPlayerPackages
  1. Change line `<add key="packageStoreDirectoryPath" value="c:\BasicWebPlayerPackages" />` in IUDICO/IUDICO.LMS/web.config to point to correct path to folder BasicWebPlayerPackages.
  1. Open SQL Management Studio
    1. connect to .\sqlexpress
    1. Create database Training
    1. Open and run file IUDICO/IUDICO.TestingSystem?/Shema/Schema.sql in context of Training database
  1. Open IUDICO in Visual Studio
    1. Deploy IUDICO.Database Project
    1. Set as StartUp Project IUDICO.LMS
    1. Сompile solution
  1. Configure IIS 7.5:
    1. Create website "IUDICO" with path IUDICO/IUDICO.LMS/
    1. Change application pool of website "IUDICO" to .NET 4.0 and identity: LocalSystem

Note: if ASP.NET 4 was not registered in IIS, you will have "HTTP 403.14 - Forbidden" error. To register ASP.NET 4 open cmd and execute next command:
> for 32bit (x86) Windows:

> %windir%\Microsoft.NET\Framework\v4.0.30319\aspnet\_regiis.exe -ir

> for 64bit (x64) Windows:

> %windir%\Microsoft.NET\Framework64\v4.0.30319\aspnet\_regiis.exe -ir