# Development FAQ #

## Questions ##

Q. What coding standard do we use?<br />
A. Coding standard is located here: https://docs.google.com/document/d/11UuzPA4rUd1ot05WQPvrKin2ThpgNiiXprkWwLIOVJo/edit?hl=en


---


Q. How do I add menu items?<br />
A. You can use `void IPlugin.BuildMenu(Menu menu)` method. e.g.:
```
public class CourseManagementPlugin : IWindsorInstaller, IPlugin
{
....
    public void BuildMenu(Menu menu)
    {
        menu.Add(new MenuItem("Course", "Course", "Index"));
    }
...
}
```


---


Q. How can I allow only selected roles to access some action?<br />
A. You can use `AllowAttribute`. e.g.:
```
...
[Allow(Role = Role.Teacher)]
public ActionResult Index1()
{
    ...
}
...
[Allow(Role = Role.Student)]
public ActionResult Index2()
{
    ...
}
...
[Allow]
public ActionResult Index3()
{
    ...
}
...
```

Q. How do you synchronise database?<br />
A. See: http://www.gotdotnet.ru/blogs/k0stya/8342/#cut1

## Problems ##

P. I get an error when I use strongly-typed view.<br />
A. Add this as first line of your view: `<%@ Assembly Name="IUDICO.PluginName" %>`


---


P. I don't have a list of my models when I'm trying to create strongly-typed view.<br />
A. Copy web.config from LMS\Views\web.config to your plugin's view folder.


---


P. I get an error during start.<br />
A. Stop the webdev server. Delete all files from LMS\Plugins folder. Rebuild the solution. Start LMS.