## Розширення MLC Base Schema ##

Microsoft Learning Components (MLC) складається із багатьох пов’язаних між собою сутностей, описаних у базовій схемі:

![http://img585.imageshack.us/img585/4343/baseschemaoverview.png](http://img585.imageshack.us/img585/4343/baseschemaoverview.png)

---

Інтеграція (MLC) вимагає розширення базової Schema новими сутностями, атрибутами запитами та правами доступу.

![http://img27.imageshack.us/img27/730/mlcschemaextension.png](http://img27.imageshack.us/img27/730/mlcschemaextension.png)

| **Сутність** | **Атрибут** | **Інтеграція** |
|:-------------|:------------|:---------------|
| AttemptItem  | IudicoThemeRef | Для інтеграції із підсистемою керуванням навчальними планами (Curriculum Management). Дає змогу визначити тему, в межах якої відбулась спроба над курсом. |
| PackageItem  | IudicoCourseRef | Для інтеграції із підсистемою керування курсами (Course Management). Дає змогу визначити, якому курсові у системі IUDICO відповідає інтегрований у MLC SCORM-пакет. |
| UserItem     | Key         | Для інтеграції із підсистемою керування користувачами (User Management) атрибут Key сутності UserItem використовується для збереження UID користувача IUDICO. |

---

Для технічних запитів при тестуванні, імпортуванні курсу, перевірки користувача та поверненні статистичних результатів до запитів базової Schema було додано нові. Утиліта SchemaCompiler генерує допоміжні класи для збереження стрічкових констант. Вони представлені на наступній UML діаграмі.

![http://img689.imageshack.us/img689/6941/mlcqueryextension.png](http://img689.imageshack.us/img689/6941/mlcqueryextension.png)

| **Запит** | **Опис** |
|:----------|:---------|
| Me        | Використовується для ідентифікації поточного залогованого користувача |
| PackageIdByCourse | Повертає ідентифікатор інтегрованого у MLC пакету згідно ідентифікатора курсу. Використовується підчас імпортування курсу у підсистему тестування |
| RootActivityByPackage | Повертає ідентифікатор на кореневий activity курсу, який по суті є organization’ом. |
| MyAttempts | Повертає усі спроби поточного залогованого користувача над усіма пройденими ним курсами. Використовується для тестових цілей. |
| AttemptsResultsByThemeAndUser | Статистичний запит до підсистеми тестування. Використовується для реалізації методу ITestingService.GetResults(Theme, User). |
| AllAttemptsResults | Статистичний запит до підсистеми тестування. Використовується для реалізації методу ITestingService.GetAllAttempts(). |
| InteractionResultsByAttempt | Статистичний запит до підсистеми тестування. Використовується для реалізації методу ITestingService.GetAnswers(AttemptResult). |