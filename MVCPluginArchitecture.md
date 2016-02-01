# MVCPluginArchitecture #

The following diagram describes how MVC plugin architecture is organised:

![http://img692.imageshack.us/img692/374/iudico.png](http://img692.imageshack.us/img692/374/iudico.png)

![http://www.websequencediagrams.com/cgi-bin/cdraw?lz=VXNlci0-TE1TOiBSZXF1ZXN0CkxNUy0-SW9jQ29udHJvbGxlckZhY3Rvcnk6IEdldCAADQoKABEULT5QbHVnaW4ANAoAUAoAChAtPkFzc2VtYmx5UmVzb3VyY2VQcm92aWRlcgBlBlZpZXcKAAsYAFUUVmlldyBmcm9tIGEAUgcAYRNVcwAjCAoAgXENAHwGAIF5BgB6HgAjCQCBAhoAXwYAgU8I&s=rose&.jpg](http://www.websequencediagrams.com/cgi-bin/cdraw?lz=VXNlci0-TE1TOiBSZXF1ZXN0CkxNUy0-SW9jQ29udHJvbGxlckZhY3Rvcnk6IEdldCAADQoKABEULT5QbHVnaW4ANAoAUAoAChAtPkFzc2VtYmx5UmVzb3VyY2VQcm92aWRlcgBlBlZpZXcKAAsYAFUUVmlldyBmcm9tIGEAUgcAYRNVcwAjCAoAgXENAHwGAIF5BgB6HgAjCQCBAhoAXwYAgU8I&s=rose&.jpg)

How-to create own plugin:

  1. Create a class, which inherits from IWindsorInstaller & IPlugin. Implement interfaces.
  1. Controllers in Plugins must inherit from **PluginController** because it overrides **View()** method, which searches for view in plugin assembly. Also, **PluginController** has **lmsService** property, which allows using API by accessing services registered with LMS.
  1. Services have to be registered in **Install** method from **IWindsorInstaller** interface using the following syntax: Component.For`<`_ISpecificService_>().ImplementedBy`<`_ServiceClassName_>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton)