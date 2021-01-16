Для развертывания, требуется запустить проект FluentMigrator.CreateDBAndData.
Вам будет предложено ввести строку подключения, после чего, будет создана база данных, таблицы и тестовые данные. Далее нужно ввести строку подключения в  файле appsettings.json (поле ConnectionStrings:Default) и запускать проект Aspnet core 3.1 - TestAppRichDomain.UI.

(Если не использовать FluentMigrator, то будет автоматически запущено создание БД через Ef core).

Если есть желание не оставлять свой пароль в конфиге, его можно зашифровать через консольное приложение Passwordcrypter. 
Пароль оттуда достаточно скопировать в appsettings.json, дополнительно расскоментировав область, которая представлена ниже(на данный момент зашифрован пароль qwerty:
/*,
  
  "Connection": "Server=localhost\\SQLEXPRESS;Database=siteDB;User Id=TestApp;Password=", //connstring without password
  "PasswordKey": "TEsZ8U+1gdg=",//Crypted password
  "PathKey": "key.txt" // path to key.txt 
  */

