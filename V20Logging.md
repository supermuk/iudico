# Logging system #
Для створення логів було використано бібліотеку log4net. Ця бібліотека є портом фреймворка log4j, написаного на Java, для використання в .NET.
У бібліотеці log4net є 5 рівнів ведення логу:

  * FATAL
  * ERROR
  * WARN
  * INFO
  * DEBUG
Ми використовуєм рівень Info.
## Конфігурація log4net ##
Для настройки був створений файл _log.xml_  в якому міститься конфігурація для нашої бібліотеки.
Конфігурація складається з таких частин:

  * `<Logger>`
  * `<root`
  * `<appender>`

Логером є тег `<log4net>`.
Основа буде наступного вигляду, в ній вказаний потрібний нам рівень логування, це Info, і посилання на назву аппендера який буде записувати всі дані у файл.

```
<root>
    <priority value="Info"/>
    <appender-ref ref="RollingFileAppender"/>
</root>
```

В ролі аппендера був використаний  RollingFileAppender .

```
  <appender name="RollingFileAppender" type="log4net.Appender.RollingFileAppender">
    <file value="Data/Logs/log4net.log" />
    <appendToFile value="true" />
    <rollingStyle value="Composite" />
    <maxSizeRollBackups value="14" />
    <maximumFileSize value="50MB" />
    <datePattern value="yyyyMMdd" />
    <staticLogFileName value="true" />
    
    <layout type="log4net.Layout.PatternLayout">
      <conversionPattern value="%date{dd/MM/yyyy HH:mm:ss} %-5level %logger - %message%newline" />
    </layout>
  </appender>
```

В даній конфігурації RollingFileAppender в атрибуті file вказано, що буде створюватись файл _log4net.log_. Він створює  кожного дня новий файл завдяки параметру datePattern. Обмеження на кількість таких файлів задається в параметрі maxSizeRollBackups, і становить 14, це означає, що термін логування буде два тижні. appendToFile забезпечує продовження писання логу в один файл при кожному запуску системи.
Блок layout який відповідає за формат виведення даних. В цій конфігурації  %date це формат виведення дати,  %-5level – рівень логування, з якого поступає повідомлення,  %logger – вказує тип логера, і  %message - саме інформаційне повідомлення.


## Інтеграція бібліотеки у систему ##

Підключення до системи проводилося у підсистемі IUDICO.LMS та IUDICO.Common.
Для інтеграції  log4net  у систему IUDICO потрібно було виконати наступні кроки:
  * Розмістити файл бібліотеки log4net.dll у директорії References.
  * Розмістити xml файл log.xml з настройками конфігурації в корені підсистеми IUDICO.LMS.
![http://img684.imageshack.us/img684/8933/123cyo.jpg](http://img684.imageshack.us/img684/8933/123cyo.jpg)
  * Додати у двох підсистемах референси на бібліотеку.
![http://img600.imageshack.us/img600/9936/1234ui.jpg](http://img600.imageshack.us/img600/9936/1234ui.jpg)
  * У підсистемі IUDICO.Common створити клас _Log4NetLogger.cs_, в якому визначено структуру елементів для логування.
  * В файлі _Global.asax_ вставити код для ініціалізації логера при старті системи.
```
Common.Log4NetLoggerService.InitLogger();
log4net.Config.XmlConfigurator.Configure(
     new System.IO.FileInfo(Server.MapPath("log.xml")));
```
Після виконання всіх цих процедур в потрібних місцях у коді можна вставляти код логера, який буде заносити у лог дані які він буде отримувати.