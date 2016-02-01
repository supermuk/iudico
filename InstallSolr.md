Встановлення Solr
Встановлення Apache Tomcat.
> Для роботи Solr необхідно спершу встановити Apache Tomcat. Для цього потрібно скачати zip-файл (http://tomcat.apache.org/download-55.cgi) Tomcat V5.5. Створіть нову папку з назвою «tomcat-solr» (c:\apps\tomcat-solr). Для встановлення Tomcat просто перемістить zip-архів, який ви викачали у вашу папку «tomcat-solr». Тепер розпакуйте цей архів і встановіте Catalina\_home як назву змінної середовища (c:\apps\tomcat-solr\apache-tomcat-5.5.25). Для цього зайдіть в меню правою кнопкою миші у MyComputer і виберіть Properties. Далі виберіть закладку Advanced, і виберіть Environment Variables.
У секції System variables виберіть New. Для поля Variable name введіть CATALINA\_HOME, а у Variable value вставте шлях до папки apache-tomcat-5.5.25.Натисніть кнопку ОК.
Тепер потрібно додати змінну JAVA\_HOME. Для цього потрібно мати на машині java jdk, яка розташована переважно у папаці C:\Program Files\Java.
Натисніть кнопку New у секції System variables. Для поля Variable name введіть JAVA\_HOME, а у Variable value вставте шлях до папки jdk.Натиснікь ОК. Закрийте вікно Properties. Перегрузіть комп’ютер.

Встановлення Apache Solr.
> Скачайте zip-файл Solr1.4 (http://www.apache.org/dyn/closer.cgi/lucene/solr). Розпакуйте його у папку «tomcat-solr». Скопіюйте файл «tomcat-solr\apache-solr-1.4.0\dist\ apache-solr-1.4.0.war» у папку «tomcat-solr\apache-tomcat-5.5.28\webapps». Після того скопіюйте папку «tomcat-solr\apache-solr-1.4.0\example\solr» у папку «tomcat-solr». І нарешті, скопіюйте файл «tomcat-solr\solr\conf\solrconfig.xml» у папку «tomcat-solr\apache-tomcat-5.5.28\conf».

Запуск Apache Solr.
> Для запуску сервера відкрийте командну консоль, перейдіть у папку «tomcat-solr\solr» і введіть команду «apache-tomcat-5.5.25\bin\startup.bat». Після цього має запуститись нова консоль Tomcat.
Пошуковий сервер Solr запущений. Для того, щоб в цьому переконатися зайдіть на посилання http://localhost:8080/apache-solr-1.4.0, де ви маєте побачити текст "Welcome to Solr!".