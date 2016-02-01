## Users Database Schema ##

Логічним чином охоплює дані повязані із збереженням користувачів

![http://iudico.googlecode.com/files/Users.png](http://iudico.googlecode.com/files/Users.png)
> Рис 1. Схема Users

### tblUsers ###
В таблиці зберігаються усі користувачі (Студенти,Викладачі,Адміністратори)
Зміни до даної таблиці можуть вносити користувачі ів правами Адміністратора

### tblGroups ###
Таблиця  є відображенням сутності "Aкадемічна група".
Зміни до даної таблиці можуть вносити користувачі ів правами Адміністратора

### fxRole ###

Таблиця в якій збергігається перелік допустимих ролей для користувачів
  * STUDENT - студент який навчається, проходить курси і т.д.
  * TRAINER - асистент лектора по даному курсу
  * LECTOR - лектор (викладач) який веде курс
  * ADMIN - адмін, керує доступом до користувачів і до груп
  * SUPER\_ADMIN - супер адмінб має доступ до керування адмінами

### tblPermissions ###

Таблиця дозволів на виконання різних операцій, доступу до контенту і т.д.

### relUserGroups relUserRoles ###

Таблиця які звязують tblUser-tblGroups та tblUser-fxRoles відповідно

Нижче наведені Activity Diagram які взаємодіють з даними таблицями

![http://iudico.googlecode.com/files/createuser.png](http://iudico.googlecode.com/files/createuser.png)
> Рис 2. Activity Diagram - Cтворення коричтувача

![http://iudico.googlecode.com/files/useractivity.png](http://iudico.googlecode.com/files/useractivity.png)
> Рис 3. Activity Diagram - Видалення коритувача, включення/ виключення із групи, зміна ролей

![http://iudico.googlecode.com/files/creategroup.png](http://iudico.googlecode.com/files/creategroup.png)
> Рис 4. Activity Diagram - Cтворення групи

![http://iudico.googlecode.com/files/groupcativity.png](http://iudico.googlecode.com/files/groupcativity.png)
> Рис 5. Activity Diagram - Видалення групи, включення/ виключення користувача із групи, зміна назви групи



