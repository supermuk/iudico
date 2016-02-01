# Автоматизовані Тести #


## Selenium ##
Selenium Remote Control (RC) являє собою сервер, написаний на Java, що приймає команди для браузера через HTTP протокол. Він вміє запускати браузер і виконувати в ньому різні дії, наприклад натискання кнопки, пошуку елемента, очікування завершення загрузки сторінки. RC дозволяє написати автоматизовані тести для вебсайтів на будь-якій мові програмування, що дозволяє гнучку інтеграцію Selenium з існуючим тестовим фреймворком. Для того щоб писати тести легше, Selenium в даний час надає драйвери на мовах Python, Ruby, .NET, Perl, Java і PHP. Він також є добрим для тестування складних веб інтерфейсів базованих на AJAX в системі постійної інтеграції (Continuous Integration system).

![http://selenium.openqa.org/selenium-rc.png](http://selenium.openqa.org/selenium-rc.png)

Тестування в Selenium виглядають так:
![http://seleniumhq.org/docs/_images/chapt5_img02_Architecture_Diagram_1.png](http://seleniumhq.org/docs/_images/chapt5_img02_Architecture_Diagram_1.png)

### Інсталяція ###
  1. Скачати Selenium RC з [Selenium RC сайту](http://seleniumhq.org/projects/remote-control/)
  1. Розархувувати в довільну папку. Запустити Selenium сервер за допомогою команди _java -jar selenium-server.jar_. Якщо не виникне помилок то на консолі повинно появитися щось подібне до:
```
14:23:08.312 INFO - Java: Sun Microsystems Inc. 10.0-b22
14:23:08.312 INFO - OS: Windows XP 5.1 x86
14:23:08.312 INFO - v1.0-beta-1 [2201], with Core v1.0-beta-1 [1994]
14:23:08.390 INFO - Version Jetty/5.1.x
14:23:08.406 INFO - Started HttpContext[/selenium-server/driver,/selenium-server/driver]
14:23:08.406 INFO - Started HttpContext[/selenium-server,/selenium-server]
14:23:08.406 INFO - Started HttpContext[/,/]
14:23:08.406 INFO - Started SocketListener on 0.0.0.0:4444
14:23:08.406 INFO - Started org.mortbay.jetty.Server@201f9
```

### Механізм роботи Selenium ###
Selenium сервер працює виключно на рівні javascript, без прив'язок до API, DLL та іншим внутрішнім процесам браузера. Таким чином, він може запускати любий javascript код, який буде виконуватися на тому ж домені, і відповідно буде мати доступ до cookies, змісту сторінки і т.д.

### Підтримка Selenium ###
| |Internet Explorer|Mozilla|Firefox|Safari|
|:|:----------------|:------|:------|:-----|
|Windows XP|6.0              |1.6+, 1.7+|0.8+, 0.9+, 1.0|      |
|Red Hat Linux|                 |1.6+, 1.7+|0.8+, 0.9+, 1.0+|      |
|Mac OS X 10.3|не поддерживается|1.6+, 1.7+|0.8+, 0.9+, 1.0+|1.3+  |


### Найбільш використовувані команди Selenium ###
|Команда|Опис|
|:------|:---|
|open   |відкрити сторінку за даним URL|
|click/clickAndWait|клікнути, з можливістю очікування загрузки сторінки|
|verifyTitle/assertTitle|перевірити заголовок сторінки з очікуваним|
|verifyTextPresent|перевірити існування тексту на сторінці|
|verifyElementPresent|перевірити існування UI елементу на сторінці заданим HTML тегом|
|verifyText|перевірити існування тексту на сторінці з заданим HTML тегом|
|verifyTable|перевірити вміст таблиці|
|waitForPageToLoad|чекати загрузки очікуваної сторінки. Викликаний автоматично при виклиці clickAndWait|
|waitForElementPresent|чекати появи UI елементу, заданого HTML тегом|


### Приклад ###
```
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using Selenium;

namespace SeleniumTests
{
    [TestFixture]
    public class NewTest
    {
        private ISelenium selenium;
        private StringBuilder verificationErrors;
        
        [SetUp]
        public void SetupTest()
        {
            selenium = new DefaultSelenium("localhost", 4444, "*firefox",
                              "http://localhost:4444");
            selenium.Start();
            verificationErrors = new StringBuilder();
        }
        
        [TearDown]
        public void TeardownTest()
        {
            try
            {
                selenium.Stop();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }
        
        [Test]
        public void TheNewTest()
        {
            selenium.Open("/selenium-ide/");
            selenium.Click("link=About");
            selenium.WaitForPageToLoad("30000");
        }
    }
}
```