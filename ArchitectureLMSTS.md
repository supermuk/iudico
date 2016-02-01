### Testing System ###
Частина яка відповідає за відображення тестування, проходження, зберігання та показ результатів тестування.
Нижче наведена схема проходження тестів та взаємодія користувача, LMS, бази даних, веб-сервісу для компільованих тестів та об'єктів навчального курсу:

![http://img405.imageshack.us/img405/8154/scolmsweb.png](http://img405.imageshack.us/img405/8154/scolmsweb.png)
> Рис1. Sequence діаграма тестування

Student розпочинає проходження тесту. Створюється контролер  - OpenTestController – який буде існувати весь час поки проходиться тест. Цей об'єкт в свою чергу дає команду сторінці показати перший тест, при цьому створюючи LearningSession (унікальні сесія для користувача, який проходить тест). Коли проходження тесту завершене результат за допомогою javascript записується в  CmiDataModel.
Також потрібно звернути увагу на те, коли тест є компільованим то відповідь користувача відсилається на Web Service який поверне результат в  CmiDataModel

![http://iudico.googlecode.com/files/testing_s_diagram.png](http://iudico.googlecode.com/files/testing_s_diagram.png)
> Рис2. діаграма Testing System

### SCORM Management System ###

Частина яка відповідає за завантаження, впорядкування, зберігання SCORM курсів згенерованих за допомогою Course Editor

![http://iudico.googlecode.com/files/scorm_ms_diagram.png](http://iudico.googlecode.com/files/scorm_ms_diagram.png)
> Рис3. діаграма SCORM Management System