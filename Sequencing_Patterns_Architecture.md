## Архітектура Шаблонів Впорядкування ##

На основі [логічної моделі шаблонів впорядкування](http://code.google.com/p/iudico/wiki/SequecningPatterns) та моделі визначення впорядкування розроблено низку класів (Рис. 1, Табл. 1)  для зручного впровадження нових шаблонів та керування застосуванням розроблених.

Таблиця 1. Призначення класів
| **№** | **Клас/ інтерфейс** | **Опис** |
|:------|:--------------------|:---------|
| 1     | SequencingType      | Містить всю модель визначення впорядкування |
| 2     | SequencingManager   | Реалізує допоміжні методи при визначенні впорядкування для новостворених вузлів. |
| 3     | ISequencingPattern  | Інтерфейс, що визначає основні властивості та методи необхідні для будь-якого шаблону впорядкування |
| 4     | ISequencingPatternCollection | Інтерфейс для підтримки роботи із набором шаблонів впорядкування. |
| 5     | ISequenсing         | Інтерфейс, що передбачає наявність в об’єкта властивостей типів SequencingType та ISequencingPatternCollection |
| 6     | ItemType            | Реалізує ISequencing. Елемент ієрархічної структури курсу, відповідає елементу `<item>` у imsmanifest.xml, |
| 7     | OrganizationType    | Реалізує ISequencing. Елемент ієрархічної структури курсу, відповідає елементу `<organization>` у imsmanifest.xml |
| 8     | SequencingPaternList | Реалізує методи та властивості ISequencingPatternCollection на основі колекції `List<ISequencingPattern>` |
| 9     | SequencingPattern   | Реалізує ISequencingPattern. Є базовим класом для всіх інших шаблонів впорядкування. Визначає базову функціональність при використанні шаблону для певного вузла. Містить допоміжні статичні методи для роботи шаблонів впорядкування |
| 10    | OrganizationDefaultSequencingPattern | Разом із SequencingPattern утворюють ієрархію класів, що реалізують функціональність конкретних шаблонів впорядкування. |
| 11    | ChapterDefaultSequencingPattern | ---//--- |
| 12    | ControlChapterDefaultSequencingPattern | ---//--- |
| 13    | ForcedForwardOnlySequencingPattern  | ---//--- |
| 14    | ForcedSequentialOrderSequencingPattern | ---//--- |
| 15    | PostTestSequencingPattern | ---//--- |
| 16    | RandomSetSequencingPattern | ---//--- |
| 17    | PrePostTestSequencingPattern | ---//--- |
| 18    | RandomPostTestSequencingPattern | ---//--- |

![http://img256.imageshack.us/img256/4426/seqpatternsclassdiagram.jpg](http://img256.imageshack.us/img256/4426/seqpatternsclassdiagram.jpg)
> Рисунок 1. Діаграма класів підтримки шаблонів впорядкування

На Рис. 2 зображено sequence діаграму, що відображає процес застосування шаблонів у редакторі курсів при:

  * Створенні item’a (chapter/control chapter/examination…)
  * При зміні структури курсу (додані/видалені будь-які item’и, змінені властивості ,реорганізація порядку, інше)
  * При явному застосуванні деякого шаблону впорядкування (з контекстного меню Дизайнера курсу)
  * При видаленні item’а, до якого застосований даний шаблон впорядкування)

![http://img294.imageshack.us/img294/9733/seqpatternssequencediag.jpg](http://img294.imageshack.us/img294/9733/seqpatternssequencediag.jpg)
> Рисунок 2. Sequence діаграма застосування шаблонів впорядкування