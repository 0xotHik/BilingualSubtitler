﻿using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Управление общими сведениями о сборке осуществляется с помощью 
// набора атрибутов. Измените значения этих атрибутов, чтобы изменить сведения,
// связанные со сборкой.
[assembly: AssemblyTitle("BilingualSubtitler")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("BilingualSubtitler")]
[assembly: AssemblyCopyright("Copyright ©  2020")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Параметр ComVisible со значением FALSE делает типы в сборке невидимыми 
// для COM-компонентов.  Если требуется обратиться к типу в этой сборке через 
// COM, задайте атрибуту ComVisible значение TRUE для этого типа.
[assembly: ComVisible(false)]

// Следующий GUID служит для идентификации библиотеки типов, если этот проект будет видимым для COM
[assembly: Guid("2a6d16a4-1d5e-49e2-9b93-b43ffc8a2730")]

// Сведения о версии сборки состоят из следующих четырех значений:
//
//      Основной номер версии
//      Дополнительный номер версии 
//      Номер построения
//      Редакция
//
// Можно задать все значения или принять номер построения и номер редакции по умолчанию, 
// используя "*", как показано ниже:
// [assembly: AssemblyVersion("1.0.*")]
// [assembly: AssemblyFileVersion("0.1.*")]

[assembly: AssemblyVersion(version: "11.0.*")]
[assembly: AssemblyFileVersion("11.0.*")]

// https://github.com/kfirprods/NonInvasiveKeyboardHook

// https://github.com/michaelnoonan/inputsimulator
// https://stackoverflow.com/questions/3047375/simulating-key-press-c-sharp
// https://stackoverflow.com/questions/16076681/wpf-enqueue-and-replay-key-events/16255097#16255097

//http://icons.iconarchive.com/icons/blackvariant/button-ui-requests-3/1024/Subtitles-icon.png

//Версия 0.1 от 27.06.2015 — Первый прототип. Принимает небольшие субтитры, перекрашивает подстрочник в желтый.
//Версия 0.2 от 28.06.2015 — Поправлен баг с пересылкой строки в переводчик Яндекса, из-за которого принимались не все субтитры.
//Поправлен баг с символом # в неположенном месте.
//Версия 0.3 от 29.06.2015 — Форма с прогресс-баром для отслеживания состояния перевода субтитров.
//Версия 0.4 от 30.06.2015 — Прототип финального интерфейса программы
//Версия 0.5 от 03.07.2015 — Второй вариант интерфейса, основанный на Panel.
//Добавлен отдельный класс под структуру субтитра, внутренняя логика программы переделана под него.
//Версия 0.6 от 12.07.2015 |Первая бета| — Крупные баг-фиксы
//Версия 0.7 от 15.07.2015 — Считывание субтитров и их перевод вынесены в фоновые работы, исправлен баг с считыванием более чем одного файла субтитров
//Версия 0.8 от 25.07.2015 — Добавлено окно настроек и ввода ключа API Яндекса
//Версия 0.9 от 03.08.2015 — Добавлено чтение субтитров из файлов MKV и соответствующая работа с MKVToolnix
//Версия 0.10 от 08.08.2015 — Добавлено редактирование уже созданных двуязычных субтитров
//Версия 0.90 от 16.02.2016 — Первая публичная бета-версия
//Версия 0.91 от 17.02.2016 — Небольшие фиксы перед релизом