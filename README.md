
#### В решении представлено 7 проектов:
1.	*DataManager*
2.	*ServiceLayer*
3.	*DataAccessLayer*
4.	*ExceptionLogger*
5.	*Models*
6.	*FileWatcher*
7.	*ConfigurationProvider*

**DataManager** содержит класс *FileGeneration*, который является оболочкой для генерации xml файла.

**ServiceLayer** содержит сервис для генерации xml файла: универсальный интерфейс *IGenerator* и класс *XmlGenerator*, реализующий этот интерфейс. В классе на базе передаваемой модели создается xml файл и его схема xsd.

**DataAccessLayer** содержит класс *PersonRepository*, который получает и обрабатывает данные из базы данных. С помощью метода GetPerson мы получаем все данные, с помощью GetPersonByID мы получаем данные по ID. Все операции с базой данных выполняются с использованием хранимых процедур.

**ExceptionLogger** содержит класс *Logger*, который обрабатывает исключения и помещает их в созданную таблицу ExceptionLog.

**Models** содержит все модели проекта: *Options*(модель для хранения конфигурационных данных), *Person*(модель для хранения информации из базы данных), *Procedure*(модель для хранения названия хранимых процедур).

**FileWatcher** это сама служба. В классе *FileManager* она запускается и с помощью метода FileTransfer переносит файл из изначальной папки в исходную с шифрованием, сжатием, архивированием.

Ну и **ConfigurationProvider** из прошлой лабы предоставляет конфигурации. Все конфигурации предоставляются в виде json/xml файлов, учитывая, что если не передан путь к конфигурационному файлу, то файл находится рядом с исполняемым файлом.
