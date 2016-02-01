## Notification ##

Notification is used to let all plugins know about changes that some plugin made.

## Sending notification ##

To send notification you need to call:

`void ILmsService.Inform(string evt, params object[] data);`

`string evt` - string describing event that happened.

`params object[] data` - arguments sent with notification.
e.g.:
`ILmsService.Inform("course/delete", course);`

## Handling message ##

Each plugin has method to handle notifications:

`void IPlugin.Update(string name, params object[] data);`

Example of handling a notification:
```
if (name == "course/delete")
{
  var course = (Course)data[0]
  var roles = Storage.GetRoles(course);
  Storage.Remove(roles);
}
```

## Notification name constants ##

Each notification name should be taken out to 'IUDICO.Common/Models/Notifications/PluginNameNotifications.cs' file.
Also full descriptions about notification should be added.
See example:
```
/// <summary>
/// Course Delete notification is sent when course has been just deleted.
/// <param name="courseId">Integer value represents identifier of deleted course.</param>
/// </summary>
public const string CourseExported = "course/deleted";
```


## List of notifications ##

| Events | Params | Sent by |
|:-------|:-------|:--------|
| course/add | Course | CourseManagement |
| course/delete | Course | CourseManagement |