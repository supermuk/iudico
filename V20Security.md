# Підсистема керування безпекою #

Перший юз-кейс:

![http://iudico.googlecode.com/files/security_usecase1.png](http://iudico.googlecode.com/files/security_usecase1.png)


**Actor:** Admin
| Ban IP | Забанити айпішку |
|:-------|:-----------------|
| Unban IP | Розбанити айпішку |

|Get user activity stats |Відслідковувати активність юзера на сайті|
|:-----------------------|:----------------------------------------|
|Limit user activity     |Обмеження на обєм даних і трафік для різних ролей|
|Get user's requests     |Отримати всі запити юзера                |
|Find 'similar' user requests |Відслідковування похожих перевантажуючих запитів|
| Customize blocking     | Налаштування параметрів безпеки         |

**Actor:** Bot

|Get user's requests |Сканування запитів до системи|
|:-------------------|:----------------------------|
|Find 'similar' user requests |Обробка даних про схожі запити користувачів та реагування|
|Obfuscate executable data| Обфускація виконуваних скриптів (JS)|
|Encrypt critical info| Зашифрувати дані, важливі для системи|


Взаємодія IUDICO, Security та користувачів:

![http://iudico.googlecode.com/files/security_activity_overall.png](http://iudico.googlecode.com/files/security_activity_overall.png)

Принцип роботи перевірки запиту:

![http://iudico.googlecode.com/files/sequence_diagram_2.png](http://iudico.googlecode.com/files/sequence_diagram_2.png)

Блокування користувачів за IP:

![http://iudico.googlecode.com/files/new.png](http://iudico.googlecode.com/files/new.png)