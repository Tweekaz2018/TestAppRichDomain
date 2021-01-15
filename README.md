Для развертывания, достаточно заменить Default - connection string  файле appsettings.json. База данных сформируется самостоятельно. Доступ администратора - admin@candyshop.tk, 
пароль: 123qwe123QWE@


Если есть желание не оставлять свой пароль в конфиге, его можно зашифровать через консольное приложение Passwordcrypter. 
Пароль оттуда достаточно скопировать в appsettings.json, дополнительно расскоментировав 


/*,
  
  "Connection": "Server=localhost\\SQLEXPRESS;Database=site;User Id=TestApp;Password=", //connstring without password
  "PasswordKey": "TEsZ8U+1gdg=",//Crypted password
  "PathKey": "key.txt" // path to key.txt 
  */
  
