# Системні вимоги #
  1. Net Framework 3.5[2](2.md)

# Інсталяція Iudico Course Editor #
Інсталяція Iudico Course Editor відбувається у декілька етапів:
  1. Перед запуском процесу інсталяції інсталятор перевіряє наявність .NET 3.5 і прав адміністратора.
  1. Відбувається копіювання всіх необхідних для роботи Iudico Course Editor  файлів у директорію, що вказується при інсталяції
  1. Виконуються зміни у реєстрі за ключем “Software\Microsoft\Iudico", а саме прописуються дві змінні:
  * Variable="Installed" Type="integer" Value="1"
  * Variable="InstallPath" Type="string" Value="[INSTALLLOCATION](INSTALLLOCATION.md)", де INSTALLLOCATION – повний шлях до папки, де інстальований Iudico
  1. Створюється ярлик на “Iudico Course Editor.exe” в меню Пуск

# Кроки інсталяції: #
  1. Запустіть Iudico Course Editor.msi
  1. Виберіть директорію, куди буде встановлено програму:<br>
<img src='http://img692.imageshack.us/img692/1238/iu4.png' />
<br>
Рисунок 4. Вибір папки для встановлення.</li></ul>

# Процес деінсталяції #
Control Panel->Programs and Features ->Iudico Course Editor->Uninstall