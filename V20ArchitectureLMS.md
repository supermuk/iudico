# LMS #

![http://iudico.googlecode.com/files/lms_usecase.png](http://iudico.googlecode.com/files/lms_usecase.png)

## Use-Cases ##

| Use-Case | Опис |
|:---------|:-----|
| Startup  | Здійснювати запуск системи. Ініціалізувати Windsor. Ініціалізувати LmsService для роботи з плагінами |
| Shutdown | Видалити з пам'яті WindsorContainer |
| Error handling | Обробляти помилки |
| Logging  | Записувати лог |
| Loading plugins | Шукати і загружати плагіни |
| Changing language | Змінювати мову |
| Resolving routes | Переадресовувати запити у відповідний плагін на відповідну дію відповідного контролера |

## Діаграма компонентів ##

![http://iudico.googlecode.com/files/lms-component.png](http://iudico.googlecode.com/files/lms-component.png)

## Діаграма класів ##

![http://iudico.googlecode.com/files/lms-class-diagram.png](http://iudico.googlecode.com/files/lms-class-diagram.png)

## Опис ##

LMS система відповідає за запуск плагінів, та реалізує LmsService, який використовується для передавання імплементацій інтерфейсів.

Також LmsService відповідає за пересилання повідомлень. Повідомлення використовуються, щоб сказати системі, що щось змінилося. Повідомлення посилаються за патерном Observer. Це означає, що всі плагіни підписуються на отримання повідомлень. Коли нове повідомленння з’являється, LmsServer передає його на оброблення всім сервісам. Посилати повідомлення можна за допомогою методу:
```
void Inform(string evt, params object[] data);
```
Щоб обробляти повідомлення інтерфейс плагіну має такий метод:
```
void Update(string evt, params object[] data);
```

Прикад оброблення повідомлення:
```
if (name == "course/delete")
{
  var course = (Course)data[0]
  var roles = Storage.GetRoles(course);
  Storage.Remove(roles);
}
```


Ще одна функція LMS – знаходження коректного ресурсу та завантажженя потрібної бібліотеки, щоб використати цей ресурс. Ця роль є одна з ключових, оскільки вона дозволяє плагінам містити контролери, які LMS знаходить за допомогою IocControllerFactory, який знаходить та інітіалізує потрібний контролер, а в кінці – знижчує його. Всі інші ресурси LMS знаходить за допомогою AssemblyResourceProvider. Нижче наведений приклад, як відбувається простий запит:
![http://iudico.googlecode.com/files/lms-sequence-plugin.png](http://iudico.googlecode.com/files/lms-sequence-plugin.png)