> # Логування ір адреси користувача #
Для збереження ІР адреси користувачів використовуються таблиці tblComputers та tblUsersSignIn:
> > http://iudico.googlecode.com/files/DbRelations.JPG
Додавання записів в базу даних здійснюється засобами linq в момент логування користувача:
> > [код буде замінено діаграмою]


> signInInfo = new IUDICO.DataModel.DB.TblUsersSignIn();

> signInInfo.UserId = user.ID;

> signInInfo.ComputerId = computerId;

> signInInfo.LastLogin = System.DateTime.Now;

> ServerModel.DB.TblUsersSignIn.InsertOnSubmit(signInInfo);

> ServerModel.DB.SubmitChanges();

Для отримання ідентифікатора комп’ютера використовується метод LookupOrAddComputer, котрий повертає цей ідентифікатор, а також у разі відсутності даних про машину з шуканою ІР адресою створює новий запис у базі даних. Метод приймає параметром ІР адресу комп’ютера,  якого здійснюється логування в систему. Саму ІР адресу отримаємо наступним чином:
> string IP = HttpContext.Current.Request.ServerVariables["REMOTE\_ADDR"];
Побачити ІР адресу, з якої користувач заходив востаннє, можна за допомогою меню Admin->Users:
> http://iudico.googlecode.com/files/UserIPLogging.JPG
Заповнення стовпця ІР здійснюється за допомогою наступного коду:
> > var ip = ServerModel.DB.TblUsersSignIn.Where(u => u.UserId == user.ID).FirstOrDefault();


> if (ip != null)

> {
> > lbIP.Text = ip.TblComputers.IP;

> }

> # Інсталяція IP Security сервісу #
За замовчуванням інсталяція IIS 7 не включає сервіс IP security. Для використання IIS IP security необхідно додати сервіс за наступною послідовністю. Для Windows Server 2008 та Windows Server 2008 [R2](https://code.google.com/p/iudico/source/detail?r=2):
  1. Клікнути Start на панелі задач, перейти до Administrative Tools, після чого відкрити Server Manager.
  1. В дереві Server Manager відкрити Roles, де вибрати Web Server (IIS).
  1. В Web Server (IIS) вибрати секцію Role Services і додати сервіс за допомогою Add Role Service.
  1. У вікні Select Role Service (Add Role Service Wizard) слід вибрати IP and Domain Restrictions та клікнути Next.
> > ![http://iudico.googlecode.com/files/ipSecurity_1.png](http://iudico.googlecode.com/files/ipSecurity_1.png)
  1. На сторінці Confirm Installation Selections клікнути Install.
  1. На сторінці Results клікнути Close

Для операційних систем Windows Vista та Windows 7 це здійснюється наступним чином:
  1. Клікнути Start на панелі задач, перейти до Control Panel.
  1. В Control Panel клікнути Programs and Features, потім клікнути Turn Windows Features on and off.
  1. В дереві відкрити Internet Information Services, після чого відкрити World Wide Web Services і тоді перейти до Security.
  1. Вибрати IP Security та клікнути OK.
> > ![http://iudico.googlecode.com/files/ipSecurity_2_w7.png](http://iudico.googlecode.com/files/ipSecurity_2_w7.png)



> # Блокування ір через IIS Manager #
> Контролювати список адрес, котрим заборонено доступ можна за допомогою графічного інтерфейсу менеджера сервера IIS 7. Для цього слід виконати наступне:
  1. Клікнути Start на панелі задач, перейти за наступним шляхом Control Panel → Administrative Tools →  Internet Information Services (IIS) Manager.
  1. На панелі Connections слід вибрати сайт Iudico.
  1. В розділі IIS слід відкрити IPv4 Address and Domain Restrictions.
> > http://iudico.googlecode.com/files/ip_address_and_domain_restr.JPG
  1. На панелі Actions обираємо Add Deny Entry та в діалоговому вікні вводимо потрібну нам ІР адресу або ж цілий ряд послідовних адрес.

> Також можна змінити правила доступу для адрес, не зазначених в списку. За допомогою менеджера сервера IIS 7 це здійснюється наступним чином:
  1. Здійснити вище описані пункти 1-3
  1. На панелі Actions вибрати Edit Feature Settings…
  1. Вибрати значення Allow/Deny
> Значення Allow означатиме, що всі ІР адерси, котрі явним чином не вказані в списку дозволених автоматично стають такими. Якщо ж обрати значення Deny, то не зазначені адреси будуть вважатись заблокованими. В такому випадку слід додати певні адреси як дозволені, інакше доступ до сайту буде блоковано і змінити налаштування через веб-сторінку буде неможливо.
> Діалогове вікно для додавання ІР адрес в список IP Security сервера IIS виглядає наступним чином:
> > http://iudico.googlecode.com/files/Add_deny_entry.JPG

> Список ІР адрес зберігається в конфігураційному файлі сайту. Повний шлях в XML-дереві виглядає наступним чином:
`<location path="Iudico">`
> > `<system.webServer>`
> > > `<security>`
> > > > `<ipSecurity allowUnlisted="true" >`
> > > > > `<add ipAddress="192.168.100.1" />`
> > > > > `<add ipAddress="169.254.0.0" />`

> > > > `</ipSecurity>`

> > > `</security>`

> > `</system.webServer>`
`</location>`


> Атрибут allowUnlisted зберігає значення, котре ми задаємо за допомогою діалогового вікна Edit Feature Settings:
> > http://iudico.googlecode.com/files/edit_featured_settings.JPG


> # Блокування ір через веб інтерфейс #
Для керування списком ІР адрес через веб інтерфейс використовується бібліотека Microsoft.Web.Administration.dll. Бібліотека містить класи, що дозволяють зчитувати та вносити зміни у в секцію ipSecurity. Одним з таких класів є ServerManager. Він містить метод GetWebConfiguration, котрий повертає конфігурацію для вказаного в параметрах сайту. До отриманої конфігурації застосовується метод GetSection, який дає доступ до секції ipSecurity. Всі елементи з цієї секції зберігаються в змінній класу ConfigurationElementCollection. Для додавання нової ІР адреси до конфігурації слід створити новий елемент для цієї колекції та змінити потрібні атрибути за допомогою методу SetAttributeValue чи за ключем: element["allowed"] = false.

Для доступу на запис в конфігураційний файл у секцію ipSecurity слід також зробити певні зміни в конфігураційному файлі applicationHost.config сервера IIS 7. Цей файл знаходиться в директорії C:\Windows\System32\inetsrv\config. В наступній секції

`<sectionGroup name="system.webServer">`
> `<sectionGroup name="security">`

> `</sectionGroup>`
`</sectionGroup>`

слід знайти рядок `<section name="ipSecurity" overrideModeDefault="Deny " />` та змінити значення атрибуту overrideModeDefault на “Allow”. Таким чином секція ipSecurity стає доступною для внесення у неї змін.

Додавання ІР здійснюється на сторінці Admin->IPSecurity.aspx:
> http://iudico.googlecode.com/files/customSecurityPage.JPG

В поле введення тексту слід вписати ІР адресу, котру адміністратор сайту має намір заблокувати та натиснути на кнопку поруч. Адреса одразу додається в «чорний список». Для зручності введення послідовності адрес була додана відповідна функціональність – поле підтримує введення проміжку ІР адрес. Наприклад, 192.168.10.1 – 192.168.10.50. Також можна перерахувати потрібні  адреси через кому: 192.168.10.1, 192.168.10.5, 192.168.10.7 , або ж використати обидва методи одночасно: 192.168.10.1, 192.168.10.5 – 192.168.10.50 .
Вибрані ІР одразу відображаються в списку блокованих адрес. Адміністратор має можливість видалити адреси зі списку, вибравши їх та натиснувши відповідну кнопку.