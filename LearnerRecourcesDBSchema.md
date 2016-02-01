# LearnerRecourcesDBSchema #

![http://iudico.googlecode.com/files/LearningResources.png](http://iudico.googlecode.com/files/LearningResources.png)

> Рис.1 Схема бази даних

### tblCources ###
Зберігається інформація про імпортовані курси

### tblCurriculums ###
Зберігається інформація про навчальні плани.
Навчальний план формується із Themes і поділяється на Stage

### tblThemes ###
Зберігається взаємозвязок між Course і Сurriculum

### tblStages ###
Зберігається інформація про рівні в навчальному плані

### tblLearnerSessions ###
Зберігається Інформація про навчаьну сесію користувача
Звязує таблиці tblLearnerAttempt і tblItems

### tblLearnerAttempt ###
Зберігається інформація про спроби(завершені та незавершені) проходження тестів студентом

### tblItems ###

### tblOrganizations ###

### tblResources ###


Нижче наведені Activity Diagram які взаємодіють з даними таблицями

![http://iudico.googlecode.com/files/coursemanage_activity.png](http://iudico.googlecode.com/files/coursemanage_activity.png)
> Рис 2. Activity Diagram - Робота з Course
![http://iudico.googlecode.com/files/course_activity.png](http://iudico.googlecode.com/files/course_activity.png)
> Рис 3. Activity Diagram - Робота з Curriculum
![http://iudico.googlecode.com/files/stage_activity.png](http://iudico.googlecode.com/files/stage_activity.png)
> Рис 4. Activity Diagram - Робота із Stage
![http://iudico.googlecode.com/files/theme_activity.png](http://iudico.googlecode.com/files/theme_activity.png)
> Рис 5. Activity Diagram - Додавання Theme