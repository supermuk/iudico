Currently there are 3 roles:
```
public enum Role
{
    None = 0,
    Student = 1,
    Teacher = 2,
    Admin = 3
}
```

  * If anyone is allowed to access the page, no action is necessary.
  * If anyone authorized can access the page use just [Allow](Allow.md) attribute:
```
[Allow]
public ActionResult Index()
{
    ...
}
```
  * To allow only specified or "higher" role to access the page use:
```
[Allow(Role = Role.Teacher)]
public ActionResult Index()
{
    ...
}
```