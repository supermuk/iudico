# Development Environment #

Required software:
  1. TortoiseSVN: http://tortoisesvn.net/downloads
  1. SQL Server 2005 Express: http://www.microsoft.com/Sqlserver/2005/en/us/express-down.aspx
  1. Microsoft SQL Server Management Studio Express: http://www.microsoft.com/downloads/details.aspx?FamilyID=c243a5ae-4bd1-4e3d-94b8-5a0f62bf7796&DisplayLang=en
  1. Microsoft Visual Studio 2008
  1. .Net Framework 3.5: http://www.microsoft.com/downloads/details.aspx?FamilyId=333325FD-AE52-4E35-B531-508D977D32A6&displaylang=en
  1. Microsoft Visual J#® 2.0 Redistributable Package: http://www.microsoft.com/downloads/details.aspx?familyid=E9D87F37-2ADC-4C32-95B3-B5E3A21BAB2C&displaylang=en
  1. Internet Information Service 7 (IIS 7):
> > a. Click Start then click Control Panel then click Programs.      <br>
<blockquote>b. Click Turn Windows features on or off.      <br>
c. Expand Internet Information Services. Select all features to install, and then click OK to start installation.      <br>
d. To confirm that the installation succeeded, type the following URL into your browser, <a href='http://localhost'>http://localhost</a>.     <br>
</blockquote><ol><li>Adobe Flash Player (needed only for compiled tests): <a href='http://get.adobe.com/flashplayer/?promoid=DXLUJ'>http://get.adobe.com/flashplayer/?promoid=DXLUJ</a></li></ol>



<h1>Build HOWTO</h1>

To start adding features to IUDICO you need to perform the following steps.<br>
<br>
<h2>Getting Code</h2>

<ol><li>Download the latest version of IUDICO:<br>
<blockquote>a. Create new folder <br>
b. Right-click mouse <br>
c. SVN Checkout <br>
d. URL of repository <a href='https://iudico.googlecode.com/svn/trunk/'>https://iudico.googlecode.com/svn/trunk/</a> or some branch as in the example (check where is the latest version) <br>
<img src='http://img341.imageshack.us/img341/5996/70008924.jpg' /></blockquote></li></ol>

<h1>Setup Database</h1>
<ol><li>Open Microsoft SQL Server Management Studio Express and create new database IUDICO<br>
</li><li>Open \iudico\DBScripts\iudico.sql and click "Execute"<br>
<blockquote><img src='http://img709.imageshack.us/img709/7117/92598931.jpg' />
</blockquote></li><li>Open \iudico\DataModel\GenerateLinqClasses.cmd (if needed change name of datebase and SQL Server “/server:.\SQLEXPRESS /database:IUDICO”)</li></ol>

<h2>Configure Web Service</h2>
<ol><li>Open iudico.sln in Visual Studio and press Ctrk+F5<br>
</li><li>Open Login.aspx page (ex. <a href='http://localhost:2935/Login.aspx'>http://localhost:2935/Login.aspx</a>). To login as Administrator use: User Name: Lex Password: <code>***</code>.<br>
</li><li>Publish WebService (ex. to folder C:\WebService\).<br>
<blockquote><img src='http://img69.imageshack.us/img69/599/37697356.jpg' />
</blockquote></li><li>Download Compilers <a href='http://iudico.googlecode.com/files/Compilers.rar'>http://iudico.googlecode.com/files/Compilers.rar</a>. Unpack into folder where is published WebService.<br>
</li><li>Open IIS 7 Manager<br>
<blockquote><img src='http://img168.imageshack.us/img168/7682/47194024.jpg' />
</blockquote></li><li>Publishing Web Service in IIS. To check if Web Service published successful go to URL <a href='http://localhost/Service1.asmx'>http://localhost/Service1.asmx</a> (or <a href='http://localhost:8080/Service1.asmx'>http://localhost:8080/Service1.asmx</a> or another port that you specified)<br>
<blockquote><img src='http://img682.imageshack.us/img682/8833/63975742.jpg' />
<img src='http://img94.imageshack.us/img94/9700/38862181.jpg' />
</blockquote></li><li>Use <a href='http://HOST_NAME/Service1.asmx/Compile'>http://HOST_NAME/Service1.asmx/Compile</a> as "Service Address" in Compiled Tests and Advanced Compiled Tests<br>
<h2>Setup IP Security service</h2>
The default installation of IIS 7 does not include the role service for IP security. To use IP security on IIS, you must install the role service using the following steps for Windows Server 2008 or Windows Server 2008 <a href='https://code.google.com/p/iudico/source/detail?r=2'>R2</a>:<br>
</li><li>On the taskbar, click Start, point to Administrative Tools, and then click Server Manager.<br>
</li><li>In the Server Manager hierarchy pane, expand Roles, and then click Web Server (IIS).<br>
</li><li>In the Web Server (IIS) pane, scroll to the Role Services section, and then click Add Role Services.<br>
</li><li>On the Select Role Services page of the Add Role Services Wizard, select IP and Domain Restrictions, and then click Next.<br>
<blockquote><img src='http://i1.iis.net/resources/images/configreference/ipSecurity_setup_1.png' />
</blockquote></li><li>On the Confirm Installation Selections page, click Install.<br>
</li><li>On the Results page, click Close.</li></ol>

For installation security service on Windows Vista or Windows 7 you should do next:<br>
<ol><li>On the taskbar, click Start, and then click Control Panel.<br>
</li><li>In Control Panel, click Programs and Features, and then click Turn Windows Features on or off.<br>
</li><li>Expand Internet Information Services, then World Wide Web Services, then Security.<br>
<blockquote><img src='http://i2.iis.net/resources/images/configreference/ipSecurity_setup_2.png' />
</blockquote></li><li>Select IP Security, and then click OK.</li></ol>
