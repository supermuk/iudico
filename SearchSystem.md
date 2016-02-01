Реалізація підсистеми пошуку в Iudico

Пошукоова система для Iudico реалізована для пошуку за навчальним матеріалом – лекціями, тестами, групами, користувачами. Скаладається вона з двох  частин – це індексування та пошук даних.

Для виконання завдання було вибрано бібліотеку Lucene, а саме її збірку для  .NET Lucene.NET.

> ![http://img690.imageshack.us/img690/9849/unled3ts.png](http://img690.imageshack.us/img690/9849/unled3ts.png)


Індексування даних

Індексування даних – це процес, який запускається і триває поки запущений сайт. А саме коли викликається при переході на сторінку пошуку контролер пошуку і його метод Process().Коли це відбувається, старий індекс стирається і дані індексуються заново. Такий спосіб переіндексування вибраний для оптимізації спостережень за діями над базою даних, адже крім додавання може здійснюватись і видалення даних. Іншими словами, пошукова система орієнтована на перспективу, коли навчальний сайт Iudico буде перебувати у постійному русі, коли він буде зберігати великі об’єми інформації.

```
 Directory directory = FSDirectory.Open(new System.IO.DirectoryInfo(Server.MapPath("~/Data/Index")));
            Analyzer analyzer = new StandardAnalyzer(Version.LUCENE_29);
            IndexWriter writer = new IndexWriter(directory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED);
            Document document;

            try
            {

                foreach (Course course in courses)
                {
                   
                    document = new Document();

                    document.Add(new Field("Type", "Course", Field.Store.YES, Field.Index.NO));
                    document.Add(new Field("ID", course.Id.ToString(), Field.Store.YES, Field.Index.NO));
                    document.Add(new Field("Name", course.Name, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));
                    document.Add(new Field("Owner", course.Owner, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.YES));

                    writer.AddDocument(document);

                    List<Node> nodes = _CourseService.GetNodes(course.Id).ToList();

                    foreach (Node node in nodes)
                    {

                        ProcessNode(writer, node);
                    }
                }
```
> Рисунок 1. Створення індексу

На рисунку 1 показано фрагмент коду із методу Process(), додавання нового документу до індексу. Тут використані відповідні стандартні методи та класи бібліотеки Lucene.NET. В даному випадку створюється  документ із інформацією про назву курсу, його id та інформація про власника курсу. Дальше цей документ додається до самого індексу.
> Створені індекси зберігаються в проекті LMS папка Data/Index.

> ![http://img171.imageshack.us/img171/2200/unled1yg.png](http://img171.imageshack.us/img171/2200/unled1yg.png)

> Рисунок 2. Розташування індексу

Для пошуку додана окрема сторінка – Search.aspx. Згідно архітектури патерну MVC кожна сторінка повинна мати свого контролера. Для Search.aspx додано SearchController, який займається обробкою подій сторінки пошуку.

Після цього виконується власне пошук співпадінь фраз із введеним словом. Такі співпадіння шукаються у назві курсу, назві його дочірніх елементів, в контенті цих дочірніх елементів, в назві навчального плану та його темах, також виконується пошук в назвах груп та користувачів. Отримавши результати, користувач може відкрити будь-який із знайдених результатів. Для перегляду результату використано вже існуючи контролери для відкривання відповідних сторінок.
> Система також проводить фільтрацію виведення результатів для різних ролей аплікації. Користувач, маючи роль студента, прикріплений до певної групи та відповідного курсу може знайти лише ці курси і групу прикріплену до нього.. Адміністратор і лектор можуть здійснювати пошук по всьому контенту.

Use case діаграма для підсистеми пошуку:

![http://img15.imageshack.us/img15/8716/usecasesearch.jpg](http://img15.imageshack.us/img15/8716/usecasesearch.jpg)

Activity діаграма для підсистеми пошуку:

![http://img821.imageshack.us/img821/2259/searchow.jpg](http://img821.imageshack.us/img821/2259/searchow.jpg)

sequence діаграма для підсистеми пошуку:

![http://img835.imageshack.us/img835/6995/searchc.png](http://img835.imageshack.us/img835/6995/searchc.png)

class діаграма для підсистеми пошуку:

![http://img819.imageshack.us/img819/6397/classsearch.jpg](http://img819.imageshack.us/img819/6397/classsearch.jpg)