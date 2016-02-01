Under constraction

# Локалізація #

Для локалізації в ASP.NET використовуються файли ресурсів.
Щоб локалізувати сторінку потрібно перейти в режим дизайну і вибрати **Tools** -> **Generate Local Resources**

http://iudico.googlecode.com/files/Capture1.PNG

В Solution Explorer можна побачити що створилась ASP папка : Asp\_LocalResources
В ній згенерувався файл ресурсів для даної aspx сторінки

http://iudico.googlecode.com/files/Capture2.PNG

Якщо відкрити файл ресурсів можна побачити наступне

http://iudico.googlecode.com/files/Capture3.PNG

Tут перечислені yсі ресурси даної сторінки. При потребі їх можна змінювати.
Щоб локалізувати сторінку потрібно аналогічний ресурс-файл перекласти бажаною мовою
Копіюємо даний ресурс-файл і перейменовуємо наступним чином : Default.aspx.**uk**.resx
Як бачимо від попередньго файлу назва відрізняється лиш вставкою **uk** - Ukraine (назва локалі). Список абревіатур можна переглянути [тут](http://en.wikipedia.org/wiki/List_of_ISO_639-1_codes)

http://iudico.googlecode.com/files/Capture4.PNG

Відкриваємо цей файл і перекладаємо на українську

http://iudico.googlecode.com/files/Capture5.PNG

Якщо перейти на вкладку Соde то можна помітити що для кожного контрола згенерувалось мета-поле з ключем із файлу-ресурсів

```
<form id="form1" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Hello" 
        meta:resourcekey="Label1Resource1" />
    <asp:Button ID="Button1" runat="server" 
        meta:resourcekey="Button1Resource1" Text="Button" />
</form>
```

Змінювати локаль сторінки можна в полі uiculture
Якщо задати **auto** то буде вибиратись локаль яка вибрана в налаштуваннях браузера
```
culture="auto" uiculture="auto"
```
Також можна явно задати яку локаль використовувати на даній сторінці
```
culture="auto" uiculture="uk"
```

Для локалізації тексту який генерується динамічно під час виконання коду слід використовувати файл глобального ресурсу. Принцип роботи аналогічний як із локальними ресурс файлами