## LMS (Learning Management System) ##

Система управління навчанням (LMS) являє собою програмний додаток для управління, документації, відстеження та звітності навчальних програм

Дана LMS є веб орієнтованою і реалізована засобами мови C# на ASP.NET
Складається з таких частин:
  * Site
  * User Management System
  * Testing System
  * SCORM Management System

### Site ###
Site – візуальне представлення системи
Умовно Site можна поділити на 4 частини:
  * Система логування
  * Сторінки для Student
  * Сторінки для Admin
  * Сторінки для Teacher

Кожна aspx cторінка має свій відповідник серед
Класів контролерів

### User Management System ###
Набір класів які відповідають за управління користувачами (студент,вчитель, адміністратор ). Кожен з користувачів має схему дій, допустимих для його ролі:

|Роль	  |Допустима дія	        |Опис дії|
|:------|:---------------------|:-------|
|Student |Read Teory	                |перегляд теретичного матеріалу|
|	      |Pass Test	                |проходження тестового завдання|
|	      |View Tests Result	        |перегляд результатів тестування|
|Admin	  |Create/Add/Delete Users	|робота з User|
|	      |Create/Add/Delete Group	|робота з Group|
|	      |Include User into Group	|включення User в Group|
|Teacher |Import/Delete Course	 |Імпортувати /видалити курс навчання|
|	      |Create/Add/Delete Curriculum|робота з навчальним планом|
|	      |Assign Course	        |прикріпити курс до навчального плану|
|	      |Share Course	         |поділитись курсом з iншим Teacher|
|	      |View Statistic	        |переглянути статитистику проходження тестів|
|	      |View Teacher List	        |переглянути список Teacher|


![http://iudico.googlecode.com/files/use_case_student.png](http://iudico.googlecode.com/files/use_case_student.png)

> Рис1. Use Case діаграма Student

![http://iudico.googlecode.com/files/use_case_admin.png](http://iudico.googlecode.com/files/use_case_admin.png)

> Рис2. Use Case діаграма Admin

![http://iudico.googlecode.com/files/use_case_teacher.png](http://iudico.googlecode.com/files/use_case_teacher.png)

> Рис3. Use Case діаграма Teacher