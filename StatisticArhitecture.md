**Структура таблиць DB необхідних для статистики.**

![http://iudico.googlecode.com/files/statistic_db.jpg](http://iudico.googlecode.com/files/statistic_db.jpg)

  * tblGroups_-  містить дані про групу;
  * tblCurriculums_- навчальні плани;
  * tblStages_-  етапи;
  * tblThemes_- теми, що містяться у відповідному етапі;
  * tblLearnerAttempts_- спроби користувача пройти тест від початку до кінця;
  * tblLearnerSessions_-  ;
  * ? tblVarsInteractionCorrectResponses_- запитання для користувача;
  * ? tblVarsInteractions_- відповіді користувача;
  * ?tblVarsScore_- відповіді користувача(чи правильно він дав відповідь на  конкретнеретне    запитання)._

Взаємодію користувача з статистикою можна описати наступною структурою:

![http://iudico.googlecode.com/files/statistic_structure.jpg](http://iudico.googlecode.com/files/statistic_structure.jpg).

  * торінки та код реалізації.**Для того щоб краще зрозуміти  саму архітектуру статистики варто розглянути сторінки та код реалізації.**


Після того як користувач відкрив сторінку  >Lector>Statistic, керування передається відповідному контроллеру -  StatisticSelectController. У ньому заповнюються відповідні контейнери для можливості вибору групи та відповідного навчального плану(Curriculumn), який закріплений за групою.

![http://iudico.googlecode.com/files/statistic_page_select.jpg](http://iudico.googlecode.com/files/statistic_page_select.jpg).

Після переходу на сторінку результатів керування передаєтся іншому контроллеру- StatisticShowController, який і буде відповідати за відображення статистики. В даному випадку будуть відображатися результати тестування по групі для відповідного навчального плану.

![http://iudico.googlecode.com/files/statistic_page_show_result.jpg](http://iudico.googlecode.com/files/statistic_page_show_result.jpg).

Як саме повинні заповнюватися дані можна побачити розглянувши фрагмент коду:
```
```