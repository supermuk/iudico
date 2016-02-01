|вхідні дані веб-сервісу|
|:----------------------|
|Параметр               |Тип                    |Опис                   |
|source                 |string                 |текстове представлення відповіді користувача, тобто коду програми|
|language               |string                 |мова програмування, яка визначена у тесті|
|input                  |string[.md](.md)       |набір вхідних даних програми|
|output                 |string[.md](.md)       |відповідний набір до input очікуваних результатів виконання програми|
|timelimit              |int                    |максимальний допустимий час виконання програми у мілісекундах|
|memorylimit            |Int                    |максимальний допустимий об’єм оперативної пам’яті в кілобайтах, який може використовувати програма|

|Відповідь веб-сервісу|Опис|
|:--------------------|:---|
|Accepted             |програма вклалася в обмеження за часом і об’ємом пам'яті. Програма вважається правильною|
|WrongAnswer Test: N  |неправильна відповідь на N-ому тесті|
|TimeLimit Test: N    |програма виконувалась довше зазначеного часу на      N-ому тесті|
|MemoryLimit Test: N  |програма використала більше зазначеного обєму пам’яті на N-ому тесті|
|CompilationError Test: 0|програму не вдалося скомпілювати. Мова програмування вказана неправильно або допущені синтаксичні помилки|
|Crasged Test: N      |під час виконання програми на N-ому тесті відбулась помилка, що призвела до аварійного завершення|
<br />

### Testing System ###
Набір класів для роботи з компільованими тестами

|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`CompilationTester`|  Main component class. Compiles provided source and runnes **.exe files againts tests.**|
|:--------------------------------------------------------------------------------------------------|:------------------|:-----------------------------------------------------------|
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`MemoryCounter`    |                                                            |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`Program`          |  This class represents program to test.                    |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`Result`           |                                                            |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`Runner`           |                                                            |
|![http://iudico.googlecode.com/files/pubclass.gif](http://iudico.googlecode.com/files/pubclass.gif)|`Settings`         |                                                            |