## Вступ: ##

Редактор Курсів передбачає можливість застосовування наступних схем (шаблони/патерни) впорядкування:


  * [Organization Default Sequencing](#Organization_Default_Sequencing.md)
  * [Chapter Default Sequecning](#Chapter_Default_Sequecning.md)
  * [Control Chapter Default Sequencing](#Control_Chapter_Default_Sequencing.md)
  * [Forced Forward-only Order](#Forced_Forward-only_Order.md)
  * [Forced Sequential Order](#Forced_Sequential_Order.md)
  * [Post-Test](#Post-Test.md)
  * [Pre-Test or Post-Test](#Pre-Test_or_Post-Test.md)
  * [Random Post-Test](#Random_Post-Test.md)
  * [Remediation](#Remediation.md)
  * [Random Set of Tests](#Random_Set_of_Tests.md)

## Опис шаблонів: ##

_**Пояснення до схем:**_

![http://fs65.www.ex.ua/show/1903889/1903889.gif](http://fs65.www.ex.ua/show/1903889/1903889.gif)  - унеможливлений вибір

![http://fs65.www.ex.ua/show/1903890/1903890.gif](http://fs65.www.ex.ua/show/1903890/1903890.gif) - можливий вибір

![http://fs65.www.ex.ua/show/1903891/1903891.gif](http://fs65.www.ex.ua/show/1903891/1903891.gif)- автоматичний перехід

![http://fs65.www.ex.ua/show/1903892/1903892.gif](http://fs65.www.ex.ua/show/1903892/1903892.gif) - обчислення статусу

![http://fs65.www.ex.ua/show/1903893/1903893.gif](http://fs65.www.ex.ua/show/1903893/1903893.gif) - статус completed/satisfied – passed – пройдений

![http://fs65.www.ex.ua/show/1903894/1903894.gif](http://fs65.www.ex.ua/show/1903894/1903894.gif)- статус failed – провалений (не завершений)

![http://fs65.www.ex.ua/show/1903895/1903895.gif](http://fs65.www.ex.ua/show/1903895/1903895.gif)- позначення статусу, ще не відомий

# Organization Default Sequencing #

**_Застосовується до:_**Organization

**_Description:_**Is applied to Organization just created. Any sub activity may be experienced any moment of time any number of times. If Organization is attempted – selection flows to the first child activity.



**_Опис:_** Застосовується до будь-якої тільки-но створеної Organization. Будь-які прямі нащадки можуть бути пройдені в будь-який момент часу довільну кількість разів. Якщо відбувається спроба над Organization – здійснюється перехід на перший дочірній пункт.



**_Схема шаблону:_**

![http://fs65.www.ex.ua/show/1903896/1903896.gif](http://fs65.www.ex.ua/show/1903896/1903896.gif)

---


# Chapter Default Sequecning #

**_Застосовується до:_**Chapter

**_Description:_**Is applied to any Chapter activity just created. Any sub activity may be experienced at any moment of time any number of times. If Chapter is attempted– selection flows to the first child activity.



**_Опис:_** Застосовується до будь-якого тільки-но створеного Chapter’a. Будь-які прямі нащадки можуть бути пройдені в будь-який момент часу довільну кількість разів. Якщо відбувається спроба над Chapter’ом – здійснюється перехід на перший дочірній пункт.



**_Схема шаблону:_**

![http://fs65.www.ex.ua/show/1903897/1903897.gif](http://fs65.www.ex.ua/show/1903897/1903897.gif)

---



# Control Chapter Default Sequencing #

**_Застосовується до:_**Control Chapter

**_Description:_**Is applied to any Control Chapter just created. Student can attempt this chapter only once. Sub activities (children) may be passed in straight forward-only order only. Student can’t see other activities except current control chapter and it's sub activities. If Control Chapter is attempted– selection flows to the first child activity.



**_Опис:_** Застосовується до будь-якого тільки-но створеного Сontrol Chapter’a. Учень може здійснювати спробу над цим chapter’ом лише один раз. Прямі нащадки можна проходити лише у примусовому прямому безповоротному порядку. Учень не може бачити чи переходити на жодні інші пункти курсу, окрім поточного control chapter’а і його прямих нащадків. Якщо відбувається спроба над Control Chapter’ом – здійснюється перехід на перший дочірній пункт.



**_Схема шаблону:_**

![http://fs65.www.ex.ua/show/1903899/1903899.gif](http://fs65.www.ex.ua/show/1903899/1903899.gif)

---



# Forced Forward-only Order #

**_Застосовується до:_**Chapter, Control Chapter, Organization

**_Description:_**Student can experience sub activities of current activity in continuous forward-only order. Student can’t step back. But still can skip current sub activity to experience next one. All sub activities should be attempted. If current activity is attempted– selection flows to the first child activity.



**_Опис:_** Усі прямі нащадки можна проходити лише у прямому безповоротному порядку, не можна повертатись назад, наступний пункт відкривається лише після проходження поточного (completed). Тобто учень мусить пройти всі без винятку завдання. Якщо відбувається спроба над поточним пунктом (до якого застосований шаблон)– здійснюється перехід на перший дочірній пункт.



**_Схема шаблону:_**

![http://fs65.www.ex.ua/show/1903900/1903900.gif](http://fs65.www.ex.ua/show/1903900/1903900.gif)

---



# Forced Sequential Order #

**_Застосовується до:_**Chapter, Control Chapter, Organization

**_Description:_**Student can experience next sub activity of current activity only after current sub activity’s objective was satisfied. Skipping sub activity is forbidden. Jumping over the next sub activity is forbidden too. But student still can experience already satisfied/completed sub activities. If current activity is attempted– selection flows to the first child activity.



**_Опис:_** Лише пройшовши(objective satisfied) поточний пункт можна почати проходження наступного. Пропускання пунктів заборонені. Перестрибування за наступний пункт заборонені. Можна переходити на вже розглянуті(пройдені) пункти. Щоб completed курс, треба пройти всі пункти. Аналогічно satisfied. Якщо відбувається спроба над поточним пунктом (до якого застосований шаблон)– здійснюється перехід на перший дочірній пункт.



**_Схема шаблону:_**

![http://fs65.www.ex.ua/show/1903901/1903901.gif](http://fs65.www.ex.ua/show/1903901/1903901.gif)

---



# Post-Test #

**_Застосовується до:_**Organization, Chapter, Control Chapter. Останній елемент: Control Chapter або Examination

**_Description:_**Based on the Forced Sequential Order pattern. Score, completion and satisfaction evaluation of affected activity is based on the result of last Post Test.



**_Опис:_** Основа на Forced Sequential Order стратегії, Загальний score та completed/satisfied обчислюється на основі лише останнього, підсумовуючого тесту.



**_Схема шаблону:_**

![http://fs65.www.ex.ua/show/1903902/1903902.gif](http://fs65.www.ex.ua/show/1903902/1903902.gif)

---

# Pre-Test or Post-Test #

**_Застосовується до:_**Organization, Chapter, Control Chapter. Перший та останній елементи: Control chapter або Examination

**_Description:_**Student is free to experience any sub-activity. If student passed pre-test, post-test is disabled. Pre-test may be attempted only once. If pre-test failed, post-test is enabled only if all other sub activities are completed/satisfied. Score, completion and satisfaction evaluation of affected activity is based on the result of Pre-test or Post-Test.



**_Опис:_** Учень може вільно вибирати пункти курсу. Пройшовши один із тестів (попередній чи підсумовуючий) інший стає недоступний. Попередній тест можна проходити лише один раз. Підсумовуючий тест стає доступний лише після того, як не пройдено попередній і відбулось проходження всіх інших пунктів. Оцінка, completion/satisfaction обчислюються на основі попереднього чи підсумовуючого тесту.



**_Схема шаблону:_**

![http://fs65.www.ex.ua/show/1903903/1903903.gif](http://fs65.www.ex.ua/show/1903903/1903903.gif)

---



# Random Post-Test #

**_Застосовується до:_**Organization, Chapter, Control Chapter. Останній елемент: Control Chapter

**_Description:_**Student is free to experience any sub-activity. Post-test is available only after all other sub-activities are completed/satisfied. Score, completion and satisfaction evaluation of affected activity is based on the result of the Post-Test. Student has ability to try pass Post-test N times. Each time, test is selected randomly from a set of tests. After N unsuccessful tries exit from current affected activity happens.



**_Опис:_** Учень може у довільному порядку проходити пункти курсу. Підсумовуючий тест доступний лише коли учень пройшов усі пункти. У підсумовуючому тесті, учневі надається право на n спроб здачі. При кожній спробі тест обирається довільно із наявної множини. Після n-невдалих спроб відбувається вихід із курсу. Оцінка,Completed/Satisfied визначається на основі підсумовуючого тесту.



**_Схема шаблону:_**

![http://fs65.www.ex.ua/show/1903904/1903904.gif](http://fs65.www.ex.ua/show/1903904/1903904.gif)

---

# Remediation #

**_Застосовується до:_**Organization, Chapter, Control Chapter

**_Description:_**Activities are located in pairs: asset (theory) & sco (question/test). Student progresses through all sub-activities consistently (continuously). Student may skip sub-activity. After all sub-activities are “visited”, learning progress starts from the beginning. If test is passed (satisfied) pair(theory and test) is skipped. Cycle loops until all tests are satisfied.



**_Опис:_** Навчальні одиниці ідуть в парах: asset (теорія) + sco (тести). Навчальний процес проходить послідовно по всіх одиницях. Якщо тест пройдено, пара пропускається. Якщо хоча б один тест не пройдено – проходження навчальних одиниць починається від початку. Пройдені одиниці відповідно пропускаються. Мета: щоб учень точно все вивчив. Учень може пропускати пункти.



**_Схема шаблону:_**

![http://fs66.www.ex.ua/show/1903905/1903905.gif](http://fs66.www.ex.ua/show/1903905/1903905.gif)

---



# Random Set of Tests #

**_Застосовується до:_**Control Chapter, в якого всі елементи - Examination

**_Description:_**Control Chapter Default sequencing is applied to the affected activity. Student progresses through the M activities, chosen randomly from a set of Question activities.



**_Опис:_** З набору тестів у контрольному розділі вибирається довільним чином і подається учневі підмножина з M тестів.



**_Схема шаблону:_**

![http://fs66.www.ex.ua/show/1903907/1903907.gif](http://fs66.www.ex.ua/show/1903907/1903907.gif)

---

## Зведена таблиця опису: ##

| # | Назва шаблону | Умова застосування до: | Description | Опис |
|:--|:--------------|:-----------------------|:------------|:-----|
| 1 | Organization Default Sequencing | Organization           | Is applied to Organization just created. Any sub activity may be experienced any moment of time any number of times. If Organization is attempted – selection flows to the first child activity. | Застосовується до будь-якої тільки-но створеної Organization. Будь-які прямі нащадки можуть бути пройдені в будь-який момент часу довільну кількість разів. Якщо відбувається спроба над Organization – здійснюється перехід на перший дочірній пункт. |
| 2 | Chapter Default Sequecning | Chapter                | Is applied to any Chapter activity just created. Any sub activity may be experienced at any moment of time any number of times. If Chapter is attempted– selection flows to the first child activity. | Застосовується до будь-якого тільки-но створеного Chapter’a. Будь-які прямі нащадки можуть бути пройдені в будь-який момент часу довільну кількість разів. Якщо відбувається спроба над Chapter’ом – здійснюється перехід на перший дочірній пункт. |
| 3 | Control Chapter Default Sequencing | Control Chapter        | Is applied to any Control Chapter just created. Student can attempt this chapter only once. Sub activities (children) may be passed in straight forward-only order only. Student can’t see other activities except current control chapter and it's sub activities. If Control Chapter is attempted– selection flows to the first child activity. | Застосовується до будь-якого тільки-но створеного Сontrol Chapter’a. Учень може здійснювати спробу над цим chapter’ом лише один раз. Прямі нащадки можна проходити лише у примусовому прямому безповоротному порядку. Учень не може бачити чи переходити на жодні інші пункти курсу, окрім поточного control chapter’а і його прямих нащадків. Якщо відбувається спроба над Control Chapter’ом – здійснюється перехід на перший дочірній пункт. |
| 4 | Forced Forward-only Order | Chapter, Control Chapter, Organization | Student can experience sub activities of current activity in continuous forward-only order. Student can’t step back. But still can skip current sub activity to experience next one. All sub activities should be attempted. If current activity is attempted– selection flows to the first child activity. | Усі прямі нащадки можна проходити лише у прямому безповоротному порядку, не можна повертатись назад, наступний пункт відкривається лише після проходження поточного (completed). Тобто учень мусить пройти всі без винятку завдання. Якщо відбувається спроба над поточним пунктом (до якого застосований шаблон)– здійснюється перехід на перший дочірній пункт. |
| 5 | Forced Sequential Order | Chapter, Control Chapter, Organization | Student can experience next sub activity of current activity only after current sub activity’s objective was satisfied. Skipping sub activity is forbidden. Jumping over the next sub activity is forbidden too. But student still can experience already satisfied/completed sub activities. If current activity is attempted– selection flows to the first child activity. | Лише пройшовши(objective satisfied) поточний пункт можна почати проходження наступного. Пропускання пунктів заборонені. Перестрибування за наступний пункт заборонені. Можна переходити на вже розглянуті(пройдені) пункти. Щоб completed курс, треба пройти всі пункти. Аналогічно satisfied. Якщо відбувається спроба над поточним пунктом (до якого застосований шаблон)– здійснюється перехід на перший дочірній пункт. |
| 6 | Post-Test     | Organization, Chapter, Control Chapter. Last activity: Control chapter or Examination | Based on the Forced Sequential Order pattern. Score, completion and satisfaction evaluation of affected activity is based on the result of last Post Test. | Основа на Forced Sequential Order стратегії, Загальний score та completed/satisfied обчислюється на основі лише останнього, підсумовуючого тесту. |
| 7 | Pre-Test or Post-Test | Organization, Chapter, Control Chapter. Last and first sub-activity: Control chapter or Examination | Student is free to experience any sub-activity. If student passed pre-test, post-test is disabled. Pre-test may be attempted only once. If pre-test failed, post-test is enabled only if all other sub activities are completed/satisfied. Score, completion and satisfaction evaluation of affected activity is based on the result of Pre-test or Post-Test. | Учень може вільно вибирати пункти курсу. Пройшовши один із тестів (попередній чи підсумовуючий) інший стає недоступний. Попередній тест можна проходити лише один раз. Підсумовуючий тест стає доступний лише після того, як не пройдено попередній і відбулось проходження всіх інших пунктів. Оцінка, completion/satisfaction обчислюються на основі попереднього чи підсумовуючого тесту. |
| 8 | Random Post-Test | Organization, Chapter, Control Chapter. Last sub-activity: Control chapter | Student is free to experience any sub-activity. Post-test is available only after all other sub-activities are completed/satisfied. Score, completion and satisfaction evaluation of affected activity is based on the result of the Post-Test. Student has ability to try pass Post-test N times. Each time, test is selected randomly from a set of tests. After N unsuccessful tries exit from current affected activity happens. | Учень може у довільному порядку проходити пункти курсу. Підсумовуючий тест доступний лише коли учень пройшов усі пункти. У підсумовуючому тесті, учневі надається право на n спроб здачі. При кожній спробі тест обирається довільно із наявної множини. Після n-невдалих спроб відбувається вихід із курсу. Оцінка,Completed/Satisfied визначається на основі підсумовуючого тесту. |
| 9 | Remediation   | Organization, Chapter, Control Chapter | Activities are located in pairs: asset (theory) & sco (question/test). Student progresses through all sub-activities consistently (continuously). Student may skip sub-activity. After all sub-activities are “visited”, learning progress starts from the beginning. If test is passed (satisfied) pair(theory and test) is skipped. Cycle loops until all tests are satisfied. | Навчальні одиниці ідуть в парах: asset (теорія) + sco (тести). Навчальний процес проходить послідовно по всіх одиницях. Якщо тест пройдено, пара пропускається. Якщо хоча б один тест не пройдено – проходження навчальних одиниць починається від початку. Пройдені одиниці відповідно пропускаються. Мета: щоб учень точно все вивчив. Учень може пропускати пункти. |
| 10 | Random Set of Tests | Control Chapter with All Examination Sub-Activities | Control Chapter Default sequencing is applied to the affected activity. Student progresses through the M activities, chosen randomly from a set of Question activities. | З набору тестів у контрольному розділі вибирається довільним чином і подається учневі підмножина з M тестів. |