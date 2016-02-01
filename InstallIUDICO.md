# Системні вимоги для IUDICO #

  1. SQL Server 2005 Express: http://www.microsoft.com/Sqlserver/2005/en/us/express-down.aspx <br>
<ol><li>.Net Framework 3.5: <a href='http://www.microsoft.com/downloads/details.aspx?FamilyId=333325FD-AE52-4E35-B531-508D977D32A6&displaylang=en'>http://www.microsoft.com/downloads/details.aspx?FamilyId=333325FD-AE52-4E35-B531-508D977D32A6&amp;displaylang=en</a> <br>
</li><li>Mozilla Firefox: <a href='http://www.mozilla-europe.org/uk/firefox/'>http://www.mozilla-europe.org/uk/firefox/</a>
</li><li>Internet Information Service 7 (IIS 7):<br>
<blockquote>a. Натисність меню Пуск(Start), після чого виберіть Панель Управління(Control Panel), а тоді меню Програми(Programs).      <br>
b. Натисніть Включити або відключити властивості Windows(Turn Windows features on or off).      <br>
c. Розкрийте Internet Information Services(IIS). Виберіть всі можливі пункти для інсталяції, тоді натисність OK для того, щоб розпочати інсталяцію IIS.     <br>
d. Для перевірки правильності встановлення введіть наступний URL у Вашому броузері, <a href='http://localhost'>http://localhost</a>.<br>
</blockquote></li><li>Adobe Flash Player (потрібний тільки для компільованих тестів): <a href='http://get.adobe.com/flashplayer/?promoid=DXLUJ'>http://get.adobe.com/flashplayer/?promoid=DXLUJ</a></li></ol>

<h1>Інсталяція Іудіко</h1>
Інсталяція Iudico відбувається у декілька етапів:<br>
<ol><li>Перед запуском процесу інсталяції, інсталятор перевіряє наявність встановленого IIS серверу, .NET 3.5, прав адміністратора і наявності SQL сервера.<br>
</li><li>Відбувається копіювання веб-сайту Iudico з усіма необхідними для його роботи компонентами у директорію, що вказується при інсталяції<br>
</li><li>Виконується SQL–скрипт, який встановлює базу даних IUDICO на машині користувача(сервер вказується при встановлені)<br>
</li><li>Відбувається встановлення і реєстрація веб-сайту Iudico в IIS. При цьому користувач може вибрати ір-адресу, порт і назву веб-сайту при встановленні<br>
</li><li>Встановлюється Application Pool з назвою IudicoAppPool для веб-сайту Iudico. Managed Pipeline Mode = Classic і Identity = LocalSystem<br>
</li><li>Копіюється папка tomcat-solr з пошуковим сервером у директорію, яка вказана у пункті (2) інсталяції<br>
</li><li>Запускається скрипт, який робить у файлі web.config наступні зміни:<br>
<ul><li>Змінює значення за XPath-ом “//connectionStrings/add[@name="IUDICO"][@connectionString]” на “DataSource=<a href='IUDICODATABASESERVER.md'>IUDICODATABASESERVER</a>;InitialCatalog=IUDICO;integrated Security=True;” , де IUDICODATABASESERVER – назва SQL серверу на який встановлюється база даних IUDICO<br>
</li><li>Змінює значення за XPath-ом “//appSettings/add[@key="siteName"][@value]” на “<a href='IUDICOSITENAME.md'>IUDICOSITENAME</a>”, де IUDICOSITENAME – назва сайту, яка вказується при встановленні.<br>
</li></ul></li><li>Виконуються зміни у реєстрі за ключем “Software\Microsoft\Iudico", а саме прописуються дві змінні:<br>
<ul><li>Variable="Installed" Type="integer" Value="1"<br>
</li><li>Variable="InstallPath" Type="string" Value="<a href='INSTALLLOCATION.md'>INSTALLLOCATION</a>", де INSTALLLOCATION – повний шлях до папки, де інстальований Iudico</li></ul></li></ol>

<h1>Кроки інсталяції:</h1>
<ol><li>Запустіть Iudico.msi<br>
</li><li>Виберіть папку, куди буде встановлено програму:<br>
<img src='http://img44.imageshack.us/img44/5967/iu1.png' />
<br>
Рисунок 1. Вибір папки для встановлення.<br>
</li><li>Виберіть ip-адресу, порт і назву для веб-сайту Iudico(обовязково перевірте чи порт відкритий!):<br>
<img src='http://img63.imageshack.us/img63/1675/iu2x.png' />
<br>
Рисунок 2. Вибір ip-адреси, порту і назву для веб-сайту Iudico<br>
</li><li>Виберіть сервер баз даних, на який буде встановлено базу даних IUDICO(наприклад .\ або .\SQLEXPRESS)<br>
<img src='http://img257.imageshack.us/img257/4548/iu3.png' />
<br>
Рисунок 3. Вибір серверу баз даних, на який буде встановлено базу даних IUDICO<br>
<h1>Процес деінсталяції</h1>
Control Panel->Programs and Features ->Iudico->Uninstall</li></ol>

<h1>Конфігурація Іудіко</h1>
<ol><li>Для перевірки коректності встановлення Іудіко відкрийте Internet Information Service(IIS). Ви маєте побачити щось на зразок:<br><img src='http://img96.imageshack.us/img96/3396/courseeditor.jpg' />
</li><li>При роботі з Курс Едітором використовуйте <a href='http://WEB_SERVICE_IP:WEB_SERVICE_PORT/Service1.asmx/Compile'>http://WEB_SERVICE_IP:WEB_SERVICE_PORT/Service1.asmx/Compile</a> як "Service Address" в Compiled Tests і Advanced Compiled Tests<br>
</li><li>Для запуску веб-сайту Іудіко використовуйте наступний url у Вашому броузері: <a href='http://IUDICO_IP:IUDICO_PORT/Login.aspx'>http://IUDICO_IP:IUDICO_PORT/Login.aspx</a>