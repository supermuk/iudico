# Source Code Structure LMS #

### Проекти ###

  * Site        (View)
  * DataModel   (Model + Controllers)
  * DBUpdater
  * Testing System
  * BoxOver

### Site ###
Відображає інформацію на сторінках використовуюючи контроли. (View)

Складається з
  * Admin            - сторінки адміна
  * Student          - сторінки студента
  * Teacher          - сторінки викладача
  * User             - спільні сторінки для всіх користувачів (інформація,дозволи)
  * Controls         - набір web контролів
  * Login/Home pages - домашня сторінка та сторінка авторизації
  * Scripts          - javascripts (API.js т.д.)
  * Assets           - збережені імпортовані курси

### Data Model ###
Проект містить в собі набір класів які відповідають за логіку системи і взаємодію між собою

  * Сommon
  * Controllers
  * DB
  * Import Managers
  * Security
  * Web Control

### Common ###

| **Cmi**|
|:-------|
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif) | `Cmi.cs`| Base class |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif) | `DataModelVerifiers.cs`| Validator for cmi properties elements |
|![http://iudico.googlecode.com/files/privclass.gif](http://iudico.googlecode.com/files/privclass.gif) | `InteractionCorrectResponses.cs` |        |
|![http://iudico.googlecode.com/files/privclass.gif](http://iudico.googlecode.com/files/privclass.gif) | `InteractionObjectives.cs`       |        |
|![http://iudico.googlecode.com/files/privclass.gif](http://iudico.googlecode.com/files/privclass.gif) | `Interactions.cs`                |        |
|![http://iudico.googlecode.com/files/privclass.gif](http://iudico.googlecode.com/files/privclass.gif) | `SchemaDataComponents.cs `       | Constants|
|![http://iudico.googlecode.com/files/privclass.gif](http://iudico.googlecode.com/files/privclass.gif) | `Score.cs`                       |        |
| **ImportUtils**|
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif) | `FileExtentions`| Necessary constants of file extentions |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif) | `PageType`| Necessary constants of courses pages types|
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif) | `ProjectPaths`| Necessary constants of filepathes |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif) | `XmlAttributes`|        |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif) | `XmlUtility`|  Constants to work with xml file (manifest.xml etc) |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif) ![http://iudico.googlecode.com/files/static.gif](http://iudico.googlecode.com/files/static.gif)| `Zipper`|  Puts folder's content to zip archive |
| **StatisticUtils** |
| ![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`StatisticManager.cs`| Constants of necessary types of courses pages |
| ![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`UserResultForItem.cs`|        |
| ![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`UserResultForPageForDate.cs`|        |
| ![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`UserResultForQuestion.cs`|        |
| **StudentUtils**|
|![http://iudico.googlecode.com/files/privclass.gif](http://iudico.googlecode.com/files/privclass.gif) |`ControlInfo.cs`| Class to represent data control which shows tests|
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif) |`DatePeriod.cs`| Deprecated |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)![http://iudico.googlecode.com/files/static.gif](http://iudico.googlecode.com/files/static.gif) |`LanguageHelper.cs`| Class to represent data of programming language |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)![http://iudico.googlecode.com/files/static.gif](http://iudico.googlecode.com/files/static.gif) |`StudentPermissionsHelper.cs`|Class to work with student permissions|
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif) |`StudentRecordFinder.cs`|  Class to get records (bind to Student) from database|
| ![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`UserSubmitCountChecker.cs`| Deprecated |
| **Testing Utils** |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif) |`AnswerFiller`|        |
|![http://iudico.googlecode.com/files/privclass.gif](http://iudico.googlecode.com/files/privclass.gif) |`CompilationTestManager`|        |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif) ![http://iudico.googlecode.com/files/static.gif](http://iudico.googlecode.com/files/static.gif)|`ConditionChecker`|        |
|![http://iudico.googlecode.com/files/privclass.gif](http://iudico.googlecode.com/files/privclass.gif) ![http://iudico.googlecode.com/files/static.gif](http://iudico.googlecode.com/files/static.gif)|`SettingFactory`|        |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif) ![http://iudico.googlecode.com/files/static.gif](http://iudico.googlecode.com/files/static.gif)|`TestManager`|        |
| **Other**|
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif) ![http://iudico.googlecode.com/files/static.gif](http://iudico.googlecode.com/files/static.gif)|`  AbstractionLayerExtenders`|        |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif) ![http://iudico.googlecode.com/files/static.gif](http://iudico.googlecode.com/files/static.gif) |`  CacheUtility`|        |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)  |` 	CmiDataModel`| DataModel interface which allows to get or set values of DataModelElements. Use GetValue and SetValue methods |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)  |` 	CmiElement`| 	Simple Data Model Element e.g cmi.exit or cmi.interactions.id |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)  |` 	CmiFirstLevelCollectionElement`| Base class for collections of elements in cmi request which lie in first level e.g cmi.interactions |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)  |` 	CmiSecondLevelCollectionElement`| Base class for collections of elements in cmi request which lie in second level e.g cmi.interactions.n.objectives |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)  |` 	ComparableDictionary`|	Dictionary which implements Comparation |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)  |` 	ComparableList`| Deprecated |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)  |` 	DMError `| Data Model Errors |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)  |` 	DataObjectDictionary`|        |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)  |` 	DataObjectDictionary`|        |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)  |` 	DynamicClass `|        |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)  |` 	DynamicClassView`|        |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif) ![http://iudico.googlecode.com/files/static.gif](http://iudico.googlecode.com/files/static.gif) |`  EnglishLanguage `|	Class to work with english text|
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif) ![http://iudico.googlecode.com/files/static.gif](http://iudico.googlecode.com/files/static.gif) |`  Extenders `|	Deprecated |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)  |` 	HtmlStyleReader `|        |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)  |` 	IdendtityNode `|Node for course tree |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif) ![http://iudico.googlecode.com/files/static.gif](http://iudico.googlecode.com/files/static.gif) |`  Logger`| Class to log data|
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif) ![http://iudico.googlecode.com/files/static.gif](http://iudico.googlecode.com/files/static.gif) |`  MemorizeHelper`|        |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)  |` 	Memorizer`|        |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)  |` 	PersistantFieldAttribute `|        |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif) ![http://iudico.googlecode.com/files/static.gif](http://iudico.googlecode.com/files/static.gif) |`  SqlUtils `|Some sql queries |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif) ![http://iudico.googlecode.com/files/static.gif](http://iudico.googlecode.com/files/static.gif) |`  TeacherHelper`| Class to retrive data (bind to Teacher) from database |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif) ![http://iudico.googlecode.com/files/static.gif](http://iudico.googlecode.com/files/static.gif) |`  Utils`|        |

### Controllers ###

Набір класів які відповідають за взаємодію Представлення з Моделлю.
Кожен клас має відповідну aspx сторінку із проекту Site

| **Student** |
|:------------|
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`CompiledQuestionsDetailsController`| Controller for CompiledQuestionsDetails.aspx page |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`CurriculumnResultController`|   Controller for CurriculumnResult.aspx page|
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`OpenTestController`|   Controller for OpenTest.aspx page|
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`StageResultController`|   Controller for StageResult.aspx page|
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`StudentPageController`|   Controller for StudentPage.aspx page|
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`TestDetailsController`|   Controller for TestDetails.aspx page|
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`ThemeResultController`|   Controller for ThemeResult.aspx page|
| **Teacher** |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`BaseTeacherController`|  Base Teacher Controller |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`CourseBehaviorController`|  Controller for CourseBehavior.aspx page|
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`CourseDeleteConfirmationController`|  Controller for CourseDeleteConfirmation.aspx page |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`CourseEditController`|  Controller for CourseEdit.aspx page |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`CurriculumAssignmentController`|  Controller for CurriculumAssignment.aspx page|
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`CurriculumDeleteConfirmationController`|  Controller for    CurriculumDeleteConfirmation.aspx page |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`CurriculumEditController`| Controller for CurriculumEdit.aspx page|
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`CurriculumTimelineController`|  Controller for CurriculumTimeline.aspx page|
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`ReCompilePageController`|  Controller for ReCompilationPage.aspx page|
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`ShareController`|  Controller for Share.aspx page|
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`StatisticSelectController`|  Controller for StatisticSelect.aspx page|
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`StatisticShowController`|  Controller for StatisticShow.aspx page |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`TeacherObjectsController`|  Controller for TeacherObjects.aspx page |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`TeachersListController`|  Controller for TeachersList.aspx page |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`ThemePagesController`|  Controller for ThemePages.aspx page|
| **Admin**   |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`Admin_CreateBulkUserController`|  Controller for CreateBulkUser.aspx page |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`Admin_CreateUserController`|  Controller for CreateUser.aspx page |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`Admin_EditGroupController`|  Controller for EditGroup.aspx page |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`Admin_EditUserController`|  Controller for EditUser.aspx page |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`Admin_GroupsController`|  Controller for Groups.aspx page|
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`Admin_IncludeUserIntoGroupController`|  Controller for IncludeUserIntoGroup.aspx page |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`Admin_RemoveGroupConfirmationController`|  Controller for RemoveGroupConfirmation.aspx page |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`Admin_RemoveUserConfirmationController`|             |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`Admin_RemoveUserFromGroupController`|  Controller for RemoveUserFromGroup.aspx page  |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`Admin_SelectGroupController`|  Controller for SelectGroup.aspx page |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`Admin_User_GroupOperationControllerBase`|             |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`Admin_UsersController`| Controller for Users.aspx page |
| **Other**   |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`ControllerBase`|  Base Controller class|
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`ControllerParameterAttribute`|             |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)![http://iudico.googlecode.com/files/static.gif](http://iudico.googlecode.com/files/static.gif)|`ControllerParametersUtility<TController>`|             |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`CreateGroupController`|  Controller for CreateGroup.aspx page|
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`HomeController`|  Controller for Home.aspx page |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`IdentityTreeView`|             |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`LoginController`|  Controller for Login.aspx page|
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`UserInfoController`|  Controller for UserInfo.aspx page |

### DB ###
Класи для роботи з базою даних
| **Base**|
|:--------|
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`DatabaseModelBase.cs`|         |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif) |`DataObjectLookupHelper.cs`|         |
| ![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`DataObjectSqlSerializer.cs`|         |
| ![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`DBModelBase.cs`|         |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif) |`DBOpInterfaces.cs`|         |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif) |`DBOps.cs`|         |
| **Other**|
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`DatabaseModel.cs`|         |
| ![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`DatabaseModel.generated.cs`|         |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif) |`DataObjectCleaner.cs`|         |
| ![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`DBObjectType.cs`|         |

### ImportManagers ###

Набір класів для імпорту різних даних у систему

|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`CourseManager`|  Class to work with courses |
|:--------------------------------------------------------------------------------------------------|:--------------|:----------------------------|
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`ItemManager`  |  Class to work with items   |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`OrganizationManager`|                             |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`ResourceManager`|  Class to work with resources |


### Security ###

Набір класів для роботи з Permissions для різних користувачів
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`CustomMembershipProvider `||
|:--------------------------------------------------------------------------------------------------|:--------------------------|:|
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`CustomRoleProvider`       ||
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`CustomUser`               ||
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)![http://iudico.googlecode.com/files/static.gif](http://iudico.googlecode.com/files/static.gif)|`PermissionsManager `      ||
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`SecuredObjectTypeAttribute `||


### Webcontrol ###

Розширена реалізація деяких webcontrols
|![http://iudico.googlecode.com/files/privclass.gif](http://iudico.googlecode.com/files/privclass.gif)|`ITestControl`||
|:----------------------------------------------------------------------------------------------------|:-------------|:|
|![http://iudico.googlecode.com/files/privclass.gif](http://iudico.googlecode.com/files/privclass.gif)|`WebButton`   ||
|![http://iudico.googlecode.com/files/privclass.gif](http://iudico.googlecode.com/files/privclass.gif)|`WebCodeSnippet`||
|![http://iudico.googlecode.com/files/privclass.gif](http://iudico.googlecode.com/files/privclass.gif)|`WebComboBox` ||
|![http://iudico.googlecode.com/files/privclass.gif](http://iudico.googlecode.com/files/privclass.gif)|`WebCompiledTest`||
|![http://iudico.googlecode.com/files/privclass.gif](http://iudico.googlecode.com/files/privclass.gif)|`WebHighlightedCode`||
|![http://iudico.googlecode.com/files/privclass.gif](http://iudico.googlecode.com/files/privclass.gif)|`WebSimpleQuestion`||
|![http://iudico.googlecode.com/files/privclass.gif](http://iudico.googlecode.com/files/privclass.gif)|`WebTextBox`  ||
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)  |`ItemListTestControlBase`||
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)  |`TestControlBase`||
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)  |`TextBoxTestControlBase`||
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)  |`WebControlBase`||
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)  |`WebTestControlBase`||


### Testing System ###
Набір класів для роботи з компільованими тестами

|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`CompilationTester`|  Main component class. Compiles provided source and runnes **.exe files againts tests.**|
|:--------------------------------------------------------------------------------------------------|:------------------|:-----------------------------------------------------------|
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`MemoryCounter`    |                                                            |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`Program`          |  This class represents program to test.                    |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`Result`           |                                                            |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`Runner`           |                                                            |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`Settings`         |                                                            |


### BoxOver ###
Проект для відображення вспливаючих підказок для різних контролів