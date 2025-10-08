# Добро пожаловать в тестовый проект XSLT-преобразование!

XSLT-преобразование - приложение, которое преобразовывает исходный xml-файл в заданную структуру и сохраняет в новый xml-файл.

## 🛠️ Технологии

Проект создан средствами .NET 8 SDK через команду: 
```
dotnet new winforms -o MyWinFormsApp
```

## 📦 Структура проекта

- MyWinFormsApp:
  - Data1.xml — исходный файл для преобразования
  - Pay.cs — класс для описания xml стуктуры Pay
  - Employee.cs — класс для описания xml стуктуры Employee и Salary
  - Form1.cs — основные методы
  - Form1.Designer.cs — дизайн компоненотов формы

## 📥 Установка и запуск

1. Скачать и установить .NET 8 SDK с официального сайта https://dotnet.microsoft.com

2. Скачать проект на ПК и открыть проект в VS Code

3. Выполнить в терминале:
```
dotnet build
dotnet run
```

## ☕ Проверка работы программы

Исходный файл Data1.xml присутствует в проекте.
```
<?xml version="1.0" encoding="utf-8"?>
<Pay>
  <item name="Lena" surname="Ivanova" amount="1001.1" mount="march" />
  <item name="Lena" surname="Ivanova" amount="2001" mount="january" />
  <item name="Lena" surname="Ivanova" amount="3001,10" mount="february" />
  <item name="Masha" surname="Ivanova" amount="1000" mount="march" />
  <item name="Masha" surname="Ivanova" amount="2000.0" mount="january" />
  <item name="Masha" surname="Ivanova" amount="3000" mount="february" />
</Pay>
```

В программе имеется GUI (графический интерфейс пользователя). На форме расположены кнопки и таблицы.

1. Нажмите кнопку Преобразовать. Выполнится преобразование XML-структуры в новый требуемый вид. В таблицах отобразятся исходные и преобразованные данные.

```
<?xml version="1.0" encoding="utf-8"?>
<Employees>
	<Employee name="Lena" surname="Ivanova">
		<salary amount="1001.1" mount="march"/>
		<salary amount="2001" mount="january"/>
		<salary amount="3001,10" mount="february"/>
	</Employee>
	<Employee name="Masha" surname="Ivanova">
		<salary amount="1000" mount="march"/>
		<salary amount="2000.0" mount="january"/>
		<salary amount="3000" mount="february"/>
	</Employee>
</Employees>
```

Дополнительно реализовано:
2.	После xslt-преобразования, дописывает в элемент Employee атрибут, который отражает сумму всех amount/@salary этого элемента
3.	В исходный файл Data1.xml в элемент Pay дописывает атрибут, который отражает сумму всех amount
4.	Имеет GUI с кнопкой запуска программы и отображением списка всех Employee и сумму всех выплат (amount) по месяцам (mount).
5.	(НЕ ОБЯЗАТЕЛЬНЫЙ ПУНКТ) Реализовать добавление в файл Data1.xml данных для строки item с возможность пересчета всех данных (с 1 по 4 пункты).

## 📝 Объяснение архитектурных решений

В проекте для работы с данными XML используется серилизация и десирилизацуия через XmlSerializer.