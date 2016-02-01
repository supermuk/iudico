## Загальна структура підсистеми Curriculum Management ##

Система керування навчальними планами являє собою плагін(**Curriculum****Management**) до навчальної системи IUDICO-2. Вона реалізована за допомогою архітектури MVC 2(Model-View-Controller). Її шаблон є таким:

·         **Модель** (Model). Модель представляє собою дані (зазвичай створені для відображення у View), а також реагує на запити (зазвичай від контролера), змінюючи свій стан.

·         **Вигляд** (View). Відповідає за відображення інформаціі (користувацький інтерфейс).

·         **Контролер** (Controller). Інтерпретує дані, введені користувачем, інформує модель і вигляд про необхідність відповідної реакції.

Відповідно до цієї архітектури:

·         Моделю у ПКНП виступає клас **MixedCurriculumStorage**, який реалізує інтерфейс **I****CurriculumStorage**. Він відповідає за операції з базою даних і забезпечує контролер всіма необхідними даними і операціями над ними. Для забезпечення інкапсуляції даних жоден інший плагін не може користуватись цим класом. Для комунікації з іншими плагінами було побудовано інтерфейс **ICurriculumService** і його реалізацію **CurriculumService**, яка дає змогу іншим плагінам використовувати функціональність ПКНП. CurriculumService надає часткову функціональність CurriculumStorage-у. Зокрема у ньому відсутні методи які змінюють або видаляють дані.

·         У ПКНП є 7 контролерів, кожен з яких відповідає за свою частину управління навчальними планами. Наведемо загальну схему.

·         Кожному контролеру відповідає набір з трьох виглядів – **Index****, Create****, Edit**, які відповідно відповідають за відображення списку сутностей, створення нової і редагування існуючої сутності.

| CurriculumController | Керування навчальними планами |
|:---------------------|:------------------------------|
| StageController      | Керування етапами             |
| ThemeController      | Керування темами              |
| CurriculumAssignmentController | Керування прив’язками до навчальних планів |
| CurriculumAssignmentTimelineController | Керування часовими межами для прив’язок |
| StageTimelineController | Керування часовими межами для етапів |
| ThemeAssignmentController | Керування прив’язками до тем  |

Таблиця 1. Схема контролерів

## 4.2.Модель даних ##

Схема даних ПКНП представлена на рис. 1. Дані зберігаються в системі управління реляційними базами даних(СУБД) **Microsoft****SQL****Server**. Для доступу до даних використовується технологія **LINQ****to****SQL**. Модель даних складається з таких сутностей(таблиць):

·         **Curriculums** - навчальні плани. Поля: назва плану (Name), дата створення (Created), дата модифікації (Updated).

·         **Stages** - етапи. Поля: назва етапу (Name), дата створення (Created), дата модифікації (Updated), id навчального плану(CurriculumRef)

·         **Themes** - теми. Поля: назва теми (Name), дата створення (Created), дата модифікації (Updated), id етапу(StageRef), id курсу(CourseRef), id типу теми(ThemeTypeRef), порядок сортування(SortOrder)

·         **CurriculumAssignments** - прив’язки до навчальних планів. Поля: id групи(GroupRef), id навчального плану(CurriculumRef)

·         **ThemeAssignments** - прив’язки до тем. Поля: id теми(ThemeRef), id прив’язки до навчального плану(CurriculumAssignmentRef), максимальна оцінка(MaxScore).

·         **Timelines** - часові межі. Поля: дата початку(StartDate), дата кінця(EndDate), id прив’язки до навчального плану(CurriculumAssignmentRef), id етапу(StageRef). Якщо часова межа відноситься до всієї прив’язки до навчального плану, тоді StageRef=null.

·         **ThemeTypes** - тип теми. Поля: назва типу теми(Name). Можливі тільки два значення назви -Theory(теорія), Test(тест) .

![http://img59.imageshack.us/img59/2493/classcurr.jpg](http://img59.imageshack.us/img59/2493/classcurr.jpg)

Рис. 1: Classes діаграма даних ПКНП:

![http://img31.imageshack.us/img31/3413/usecasemodel.jpg](http://img31.imageshack.us/img31/3413/usecasemodel.jpg)

Рис. 2. Usecase діаграма

На рис. 3 і рис. 4 показано сіквенс діаграми роботи підсистем менеджменту Curriculum-ів i CurriculumAssignment-ів відповідно

![http://img716.imageshack.us/img716/8901/curriculums.png](http://img716.imageshack.us/img716/8901/curriculums.png)

Рис. 3. Сіквенс діаграма створення сутностей

![http://img215.imageshack.us/img215/4513/curriculumz.png](http://img215.imageshack.us/img215/4513/curriculumz.png)

Рис. 4. Сіквенс діаграма створення сутностей

![http://img51.imageshack.us/img51/9862/currmanage.jpg](http://img51.imageshack.us/img51/9862/currmanage.jpg)

Рис. 5. Activity діаграма Curriculum management-у

![http://img856.imageshack.us/img856/7254/common.jpg](http://img856.imageshack.us/img856/7254/common.jpg)

Рис. 6. Загальна Activity діаграма

![http://img820.imageshack.us/img820/2060/component.jpg](http://img820.imageshack.us/img820/2060/component.jpg)

Рис. 7: Component діаграма

![http://img594.imageshack.us/img594/2769/getthemes.png](http://img594.imageshack.us/img594/2769/getthemes.png)

Рис. 8: sequence діаграма для getAvaibleThemes