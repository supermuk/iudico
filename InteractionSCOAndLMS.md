## Модель SCORM сумісного програвача курсів в IUDICO LMS ##

Найважливішою з точки зору SCORM - сумісності є частина системи навчання, яка відповідає за програвання курсу від моменту початку тестування до формування статистики по кожній спробі проходження тестів. У системі IUDICO за це відповідальна Testing System.

![http://img210.imageshack.us/img210/6274/imsmanifest.jpg](http://img210.imageshack.us/img210/6274/imsmanifest.jpg)
> Рисунок 1. Структура imsmanifest.xml

Для впровадження повної SCORM – сумісності  в цю підсистему розроблено модель, реалізація якої повинна забезпечувати повну функціональність згідно стандарту.

Файл imsmanifest.xml входить до складу кожного SCORM - сумісного курсу. Його структура детально описана в книзі SCORM-2004 САМ. Кореневим елементом в маніфесті є "organization". Він містить дочірні елементи - "item". Однією з їх складових є Модель визначення Впорядкування, яка задає правила впорядкування для даного елемента "item". Також до кожного з листкових елементів прив’язаний певний resource - ресурс, який містить посилання на html сторінку. Ця сторінка є найменшою неподільною частинкою курсу, що програється. Якщо ця частинка комунікує з LMS через екземпляр API, то її називають об’єктом контенту (Shareable Content Object, SCO). Якщо ж це просто html-сторінка чи інший статичний ресурс, то таку частинку називають asset.

### Activity ###

Сутність activity детально описана в книзі SCORM-2004 SN. У моделі їй відповідає тип Activity, що є run-time репрезентацією елемента «item» в imsmanifest.xml. До кожного об’єкту типу Activity прив’язаний екземпляр типу Activity Run-Time Model. Activity Run-Time Model складається з:

  1. RTE Data Model _Модель Даних підчас виконання_
  1. Sequencing Model: _Модель Впорядкування_
    * Tracking Model _Модель Стеження_
    * Overall sequencing process _Загальний процес впорядкування_
    * Navigation Behavior _Навігаційна Поведінка_
    * Termination Behavior _Поведінка Завершення роботи_
    * Rollup behaviour _Поведінка обчислення значень батьківських елементів на основі значень елементів нащадків_
    * Selection and randomization Behavior _Поведінка Вибору та Перемішування_
    * Sequecning Behavior _Поведінка Впорядкування_
    * Delivery Behavior _Поведінка доставки_
  1. Navigation Model _Навігаційна Модель_

Компоненту RTE Data Model відповідає CmiDataModel у специфікації SCORM. Більш детальна інформація про кожен з цих компонентів містяться в книжках SCORM-2004 RTE і SCORM-2004 SN. Sequencing Model використовує Sequencing Definition Model з imsmanifest.xml для ініціалізації при створенні Activity. Аналогічно як і у imsmanifest.xml, до кожного листкового Activity прив’язаний відповідний resource.


### Activity Tree ###

ActivityTree – це деревовидна структура курсу. Вузлами цього дерева є об’єкти типу Activity (Рис. 2)


![http://img39.imageshack.us/img39/5476/activitytree.jpg](http://img39.imageshack.us/img39/5476/activitytree.jpg)
> Рисунок 2. Структура Activity Tree

Розглянемо найважливіші етапи взаємодії SCO та LMS (Рис. 3):

![http://fs70.www.ex.ua/get/2485680/image003.jpg](http://fs70.www.ex.ua/get/2485680/image003.jpg)
> Рисунок 3. Модель взаємодії SCO та LMS

### Завантаження курсу в LMS ###

Під час завантаження в LMS курс перевіряється на правильність: об’єкт типу ManifestValidator перевіряє imsmanifest.xml на дотримання вимог SCORM, а PackageValidator перевіряє весь курс на цілісність і правильність згідно SCORM CAM.(Рис. 4)

![http://fs70.www.ex.ua/get/2485684/Validators.jpg](http://fs70.www.ex.ua/get/2485684/Validators.jpg)
> Рисунок 4. Валідатори маніфесту та package'а.

### Програвання курсу ###

Після початку програвання курсу утворюється об'єкт типу Navigator. Цей об'єкт прив’язується до Activity Tree.(Рис. 8) Об’єкт Navigator відповідає за подачу навчального матеріалу учневі (Рис. 5). Об’єкт типу ManifestReader(Рис. 7) зчитує Sequencing Definition Model з imsmanifest.xml і записує її в підрозділ Activity Run-Time Model->Sequencing Model відповідного Activity (Рис. 5).

![http://fs66.www.ex.ua/get/2485704/LearningComponents.jpg](http://fs66.www.ex.ua/get/2485704/LearningComponents.jpg)

Рисунок 5. Діаграма класів Activity, Activity Tree, Navigator

### CMI Data Model ###

Для реалізації моделі даних CMI використовується клас LearningDataModel, а також набір класів, що відповідає за представлення та перевірку (-verifier) відповідних сутностей моделі даних. (Рис. 6)

Класи із суфіксом Verifierv1p3 відповідають за перевірку моделі даних, описаної в стандарті SCORM 2004 4th Ed.

![http://img218.imageshack.us/img218/9987/datamodelcut.jpg](http://img218.imageshack.us/img218/9987/datamodelcut.jpg)
> Рисунок 6. Діаграма класів CMI Data Model

![http://img23.imageshack.us/img23/2606/manifestreadercut.jpg](http://img23.imageshack.us/img23/2606/manifestreadercut.jpg)
> Рисунок 7. Діаграма класів Manifest Reader – парсера маніфесту imsmanifest.xml

![http://img297.imageshack.us/img297/1291/dynamicview.jpg](http://img297.imageshack.us/img297/1291/dynamicview.jpg)
> Рисунок 8. Sequence діаграма навігації по навчальному курсу згідно Activity Run-Time Data Model.

В курсах утворених за допомогою редактора FireFly Course Editor функції SCO реалізовані у файлі SCOObj.js. Взаємодія SCO із LMS відбувається за допомогою API функцій GetValue i SetValue, які відповідно зчитують і записують CmiDataModel-інформацію з підрозділу Activity Run-Time Model ‑> RTE Data Model відповідного Activity. Після закінчення програвання курсу формується статистика.


---


Модель розроблена на основі вихідного коду [Microsoft SharePoint Learning Kit ChangeSet 61722](http://slk.codeplex.com/SourceControl/changeset/changes/61722)

Ліцензія: [Microsoft Public License](http://slk.codeplex.com/license)