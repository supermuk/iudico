Для автоматичного створення інсталяційних пакетів створюємо окремі проекти в системі Hudson. Для прикладу розглянемо Iudico Setup. В налаштуваннях обираємо «Build after other projects are built» і вказуєм назву проекту, після якого почнеться збірка, в даному випадку «Iudico» (див. рис. 1). Інсталяційний пакет створюється за допомогою Wix.
![http://img8.imageshack.us/img8/2028/pic1jd.png](http://img8.imageshack.us/img8/2028/pic1jd.png)
<br />
Рис. 1. Налаштування проекту Iudico Setup

Усі проекти для створення інсталяційних проектів знаходяться у репозиторії за адресою http://iudico.googlecode.com/svn/branches/Setups. У файлі bat.go містяться batch команди, які виконують компіляцію Wix проекту. Ці команди можна прописувати вручну або ж скопіювати із «Output» вікна під час компіляції проекту у Visual Studio(див. рис.2).
![http://img696.imageshack.us/img696/850/pic2ff.jpg](http://img696.imageshack.us/img696/850/pic2ff.jpg)
<br />
Рис. 2. Команди для компіляції інсталяційних проектів Wix



&lt;hr /&gt;


<br />
Щоб виконання проекту "Course Editor Setup" запускалось автоматично пілся успішної компіляції проекту "Course Editor" необхідно перейти на сторінку http://localhost:8080/job/Course%20Editor/configure та додати "Post build Action" як показано на рис. 3
![http://img228.imageshack.us/img228/9272/cesetup.png](http://img228.imageshack.us/img228/9272/cesetup.png)
<br />
> рис. 3
<br />
<br />
Навідміну від першого варіанту, в якому задавався "Build Trigger" для проетку "Course Editor Setup", можна визначити чи запускати проект якшо попередній є "Unstable", тобто не пройшов усі Unit чи Automated тести.
<br />
Якщо все виконано правильно, то в "Console Output" для проекту "Course Editor" можна побачити
```
Build succeeded.
    0 Warning(s)
    0 Error(s)

Time Elapsed 00:00:00.77
Triggering a new build of Course Editor Setup #30
Finished: SUCCESS

```
Якщо проект "Course Editor Setup" виконано успішно, то інсталяційні файли .exe та .msi будуть знаходитись у папці: "..\Hundson\data\jobs\Course Editor Setup\workspace\Course Editor\Course Editor\Default Configuration\Debug\DiskImages"