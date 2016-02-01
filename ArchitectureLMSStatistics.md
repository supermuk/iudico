# Cтатистика IUDICO #

Право переглядати статистику мають викладач та адміністратор.

![http://iudico.googlecode.com/files/usecase_statistic.jpg](http://iudico.googlecode.com/files/usecase_statistic.jpg).

> Переглянути статистику можна на сторінці Teacher>Statistic. Користувач повинен вибрати
групу та навчальні плани(curriculums), по яких він би хотів переглянути статистику. У статистиці відображаються результати тестування конкретної групи по навчальних планах. Для того щоб переглянути результати необхідно перейти на іншу сторінку за допомогою клацання по кнопці "Show". Далі користувач може вибрати конкретний навчальний план у якому будуть відображатися результати по темах які він містить. Для більш детального перегляду можна переглянути тему для конкретного студента.

![http://iudico.googlecode.com/files/activity_diagram_statistic.jpg](http://iudico.googlecode.com/files/activity_diagram_statistic.jpg).


Опис діаграми видів діяльності.
|№|Дія|Стан|Опис|
|:|:--|:---|:---|
|1|Teacher>Statistic|StatisticPage|перехід на сторінку статистики|
|2|select many curriculums|CurriculumsResultPage|результати по декількох навчальних планах|
|3|select one curriculum|ThemesResultPage|результати по одному навчальному плану|
|4|select theme|ThemeResultPage|результати по темі|



Для того щоб краще зрозуміти  саму архітектуру статистики розглянемо деяку взаємодію між
об'єктами системи.

**SiquenceDiagram Teacher\_Statistic.**
![http://iudico.googlecode.com/files/siquence_diagram1_statistic.jpg](http://iudico.googlecode.com/files/siquence_diagram1_statistic.jpg).

Після того як користувач відкрив сторінку  >Lector>Statistic, керування передається відповідному контроллеру -  StatisticSelectController. У ньому заповнюються відповідні контейнери для можливості вибору групи та навчальних планів, закріплених за групою. Щоб відобразити статистику для декількох навчальних планів керування передається StatisticShowсСurriculumsController контроллеру. Якщо ж користувач вибрав один curriculum то дані відображаються за допомогою StatisticShowController контроллера.
Користувач має змогу переглянути результати тестування  для конкретного студента який входить в групу.  Відображається сумарна кількість балів по навчальному плану, яку він міг набрати, і яку набрав практично. Аналогічно відображаються дані для тем відповідного плану.

![http://iudico.googlecode.com/files/class_diagram_statistic.jpg](http://iudico.googlecode.com/files/class_diagram_statistic.jpg).

Опис класів
|№|Клас|Опис|
|:|:---|:---|
|1|Controller Base|базовий клас|
|2|BaseTeacherController|базовий клас |
|3|ServerModel|статичний клас. Використовується для доступу до бази даних|
|4|StatisticSelectController|контроллер який відповідає за StatisticSelect.aspx сторінку(сторінка статистики) |
|5|StatisticShowCurriculumsController|контроллер що відповідає за StatisticShowCurriculum.aspx сторінку(сторінка для відображення результатів тестування по навчальних планах)|
|6|StatisticShowCurriculum|контроллер що відповідає за StatisticShow.aspx сторінку(сторінка для відображення результатів тестування по темах навчального плану)|
|7|TeacherHelper|статичний клас, що містить методи для доступу до бази даних|
|8|CmiDataModel|інтерфейс, за допомогою якого можна редагувати базу даних|

## Структура таблиць DB необхідних для статистики. ##

![http://iudico.googlecode.com/files/statistic_db.jpg](http://iudico.googlecode.com/files/statistic_db.jpg).


> Опис таблиць бази даних.

|№|Таблиця|Опис|
|:|:------|:---|
|1|tblGroups|групи|
|2|tblCurriculums|навчальні плани|
|3|tblStages|етапи|
|4|tblThemes|теми|
|5|tblLearnerAttempts|спроби проходження тесту|
|6|tblLearnerSessions|набір  питань|
|7|tblVarsInteractionCorrectResponses|питання по тесту(темі)|
|8|tblVarsInteractions|відповіді користувача|