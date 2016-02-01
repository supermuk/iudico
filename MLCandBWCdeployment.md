# SLK. Microsoft Learning Components & Basic Web Player #

## Компілювання MLC  та запуск Basic Web Player ##

**_Документація згідно SLK 1.5 станом на 30.10.2010_**

### Необхідне програмне забезпечення: ###

Впевніться, що на комп’ютері інстальовано та працює (напр. запущені відповідні сервіси SQL Server’а) наступне програмне забезпечення:

  * Microsoft Visual Studio 2008+  (із Visual C++)
  * Microsoft SQL SERVER 2008+   (із SQL Server Management Studio)
  * Microsoft Internet Explorer 7+

### Завантаження SDK ###

  * На веб-сайті **http://slk.codeplex.com** зайдіть в розділ **Source** та завантажте останню версію SLK SDK
![http://img525.imageshack.us/img525/1323/image013gd.jpg](http://img525.imageshack.us/img525/1323/image013gd.jpg)

  * Розпакуйте скачаний архів **SLK-XXXXXXXXXXXX.zip**  до будь-якої директорії.  Тут замість **XXXXXXXXXXXX** – номер версії SLK SDK.
(Для прикладу використовуватимемо шлях **D:\**. У архіві міститься папка, що дублює його назву, тож результуючий шлях у нашому випадку:  **D:\SLK\_790e978065d4\** )
> Надалі всі шляхи будуть вказуватись відносно кореневого каталогу: **{шлях розпакованого архіву}\SLK\_XXXXXXXXXXXX****\**

### Компілювання проектів MLC ###

  * Вихідні коди компонент MLC містяться в папці **/****Src**

  * Відкрийте проект **Src/Compression/****Compression.sln**у MS Visual Studio

  * Відкрийте вікно властивостей солюшена, вибравши пункт Properties із контекстного меню**Solution ‘Compression’ (3 projects).**
![http://img831.imageshack.us/img831/7633/image001al.jpg](http://img831.imageshack.us/img831/7633/image001al.jpg)

  * У вікні **Solution ‘Compression’ Property Pages**відкрийте розділ **Configuration Properties -> Configuration.**Для проекту **Microsoft.LearningComponents.MRCI** виберіть платформу, що відповідає вашій операційній системі. В більшості випадків **Win32**буде більш універсальним вибором.
> Натисніть кнопку **ОК**, щоб підтвердити внесені зміни.
![http://img178.imageshack.us/img178/1885/image002fx.jpg](http://img178.imageshack.us/img178/1885/image002fx.jpg)

  * Скомпілюйте проект **Microsoft.LearningComponents.MRCI**, оскільки він використовується при побудові проекту **Compression**
![http://img835.imageshack.us/img835/6797/image003wo.jpg](http://img835.imageshack.us/img835/6797/image003wo.jpg)

  * Після цього скомпілюйте весь солюшн. Якщо трапились помилки підчас компілювання – спробуйте повторити компілювання знову.
![http://img525.imageshack.us/img525/289/image004kn.jpg](http://img525.imageshack.us/img525/289/image004kn.jpg)

  * Відкрийте проект **Src/Storage/Storage.sln** у MS Visual Studio

  * Скомпілюйте весь солюшн. Якщо трапились помилки підчас компілювання – спробуйте повторити компілювання знову.
![http://img138.imageshack.us/img138/7302/image005ca.jpg](http://img138.imageshack.us/img138/7302/image005ca.jpg)

  * Тепер компоненти MLC, не включаючи підтримки для SharePoint відкомпільовані.
> Їх можна побачити у папці **Src/Storage/bin/Debug**.

### Запуск Basic Web Player ###

  * Вихідні коди BasicWebPlayer (BWP) містяться в папці **/Samples/BasicWebPlayer/**

  * Створіть папку **Bin** в каталозі  **/Samples/BasicWebPlayer/**

  * Скопіюйте вказані файли в із каталогу **Src/Storage/bin/Debug** в каталог **/Samples/BasicWebPlayer/Bin**
> - 'Microsoft.LearningComponents.MRCI.dll'

> - 'Microsoft.LearningComponents.MRCI.pdb'

> - 'Microsoft.LearningComponents.Compression.dll'

> - 'Microsoft.LearningComponents.Compression.pdb'

> - 'Microsoft.LearningComponents.dll'

> - 'Microsoft.LearningComponents.pdb'

> - 'Microsoft.LearningComponents.Storage.dll'

> - 'Microsoft.LearningComponents.Storage.pdb'


  * За допомогою SQL Management Studio створіть базу даних із назвою **Training**

![http://img841.imageshack.us/img841/8066/image014gsw.jpg](http://img841.imageshack.us/img841/8066/image014gsw.jpg)

![http://img838.imageshack.us/img838/750/image015up.jpg](http://img838.imageshack.us/img838/750/image015up.jpg)

  * Виконайте скрипт **Schema.sql** із **/Samples/BasicWebPlayer/**над базою даних **Training****.**Пересвідчіться, що вибрана саме ця база даних
![http://img521.imageshack.us/img521/595/image016gv.jpg](http://img521.imageshack.us/img521/595/image016gv.jpg)

  * Basic Web Player по замовчуванню зберігає всі зааплоуджені курси у папку **c:\BasicWebPlayerPackages**. Тож створіть її.

  * Відкрийте **/Samples/BasicWebPlayer/BasicWebPlayer.sln** у Visual Studio.

  * Відкрийте файл **Web.Сonfig.** Замініть значення сервера у параметрах підключення. Замість **localhost** введіть назву SQL сервера, що й для бази даних **Training**.
Переважно це **.\SQLEXPRESS**

![http://img259.imageshack.us/img259/6008/image017n.jpg](http://img259.imageshack.us/img259/6008/image017n.jpg)

  * Запустіть проект. (F5 або Ctrl+F5)

  * Використовуйте Internet Explorer для коректного відображення.
![http://img221.imageshack.us/img221/5835/image018fd.jpg](http://img221.imageshack.us/img221/5835/image018fd.jpg)