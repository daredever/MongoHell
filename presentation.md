## Welcome part

*Дима:*

Добрый день, друзья, рады приветствовать вас на 58 встрече Петербургского .net сообщества, проходящей в коллаборации с Центром Речевых Технологий. 
Меня зовут Елисеев Дмитрий, я занимаюсь базами данных в ЦРТ. -- Дима

*Рома:*

Меня зовут Роман Щербаков, и я занимаюсь разработкой бэк-енда в ЦРТ и мы хотели бы рассказать вам о внедрении MongoDB в .net проект.

Для начала хотелось бы узнать, сколько из вас слышали о noSQL решениях? а о MongoDB? А кто применял её в своей работе? а на продакшене? -- логотип монго
Ну, как и ожидалось,с каждым вопросом количество рук сокращалось...
Что ж, прежде чем начать, давайте очень кратенько посмотрим, что такое MongoDB.

https://github.com/amyasnov/stepic-db-intro/blob/master/week_6/spreadsheets/NoSQL.pdf

MongoDB это  not only sql документоориентированная база данных, оснавная направленность которой это хранение иерархически сложных документов -- слайд с общим описанием и исторической сводкой
В принципе, подход к хранению очень похож на то, что мы привыкли видеть в реляционных базах, но об особенностях будет чуть позже,
Что мы имеем внутри mongodb? У нас может быть n баз данных, в каждой из которых по n коллекций в каждой из которых может хранится n абсолютно любых документов. -- Рома

-- здесь в качестве перебивки будет логотип црт и спб.нет

*Дима:*

И теперь когда в общих чертах мы все имеем базовое представление о том, что такое MongoDB хотелось бы начать наще повествование.
Когда я пришёл на новый проект, то начал анализировать чем он живёт, какие перспективы, какие технологии используются.
Одной из первых задач которая мне прилетела была - тормозит postgtres. -- слайд сравнения чтения из прошлого доклада
Углубившись в суть проблемы, я обнаружил, что тормозит поиск по json документу, проблема заключалась в фильтрации по вложенным полям
в постгрес cуществуют два типа данных JSON: json и jsonb. 
Они принимают на вход почти одинаковые наборы значений, а отличаются главным образом с точки зрения эффективности. Тип json сохраняет точную копию введённого текста, которую функции обработки должны разбирать заново при каждом выполнении запроса, тогда как данные jsonb сохраняются в разобранном двоичном формате, что несколько замедляет ввод из-за преобразования, но значительно ускоряет обработку, не требуя многократного разбора текста. Кроме того, jsonb поддерживает индексацию, что тоже может быть очень полезно, -- слайд логотип jsonb текстом плюсы минусы
в MongoDB данные хранятся в bson документах.
BSON (англ. Binary JavaScript Object Notation) — формат электронного обмена цифровыми данными, основанный на JavaScript, бинарная форма представления простых структур данных и ассоциативных массивов (которые в контексте обмена называют объектами или документами). Является надмножеством JSON, включая дополнительно регулярные выражения, двоичные данные и даты -- слайд логотип bson текстом плюсы минусы
После поверхностного сравнения постгреса и монги, я пришёл к выводу, что для нашего проекта больше подходит MongoDB
И  именно в этот момент начинается "Божественная комедия" внедрения MongoDB в .net проект. 

Раскрыть тему кругов (тут слайд с картинкой данте).

#  Circles of Hell

## 1 круг - Лимб. 

*Дима:*

- Предложение внедрить монгу. 
Когда я подошёл к архитектору нашего проекта с вопросом, а почему собственно не Mongo? --слайд с проблемами монго которых боятся
Я услышал стандартные ответы:
- она не стабильна, у нас был опыт использования, но он оказался неудачным в 2013 году
- течёт память (режим standalone, не было человека который мог поднять кластер, 
поддержка монги сказала нужен кластер)
- ошибки на бою database corrupted и не восстанавливалась
- нет транзакций
- не ACID

Железобетонные аргументы, через которые казалось бы не пробиться.
Однако, понимая что постгрес не даст таких же показателей перформанса как Mongo,
я решил обратиться к ресёрчу который проводился к 51 встрече, и на примере документов с ссылкой
и документа в документе показал, что по производительности mongoDB выигрывает у пг. -- слайд из предыдущего доклада

Jabson test for acid.
Бенчмарки производительности для любого руководства  всегда выглядят весомо.
 
Простота репликации и шардирования, не требующая глубокого понимания принципов работы БД. -- слайд со схемой шардирования
Самое главное отказоусточивость - **КЛАСТЕР**!

Ну и самое главное вопрос лицензирования.
MongoDB изначально выходил под лицензией GNU Affero General Public версии 3. Языковые драйверы доступны под лицензией Apache. MongoDB можно бесплатно получить по общедоступной лицензии Affero (AGPL) GNU. В октябре 2018 года компания-разработчик объявила о переходе к более жёсткой по сравнению с AGPL копилефтной лицензии SSPL (Server Side Public License)[10].[11]. Вслед за этим было начато изучение новой лицензии представителями Open Source Initiative и Free Software Foundation на предмет соответствия определениям открытого и свободного программного обеспечения[12].

Кроме того, компания MongoDB выпускает коммерческую версию СУБД, включающую дополнительные функции (например, интеграцию с SASL, LDAP, Kerberos, SNMP), инструменты управления, мониторинг и резервное копирование, а также поддержку.

Показав все достоинства, и когда они взяты во внимание, мы переходим ко второму кругу


## 2 круг - Сладострастие.

*Дима:* 

Проанализировав проект, и поняв что большинство сервисов хранит jobject в пг,
и воодушевившись показателями синтетик тестов сразу чешутся руки творить добро и справедливость,
и воткнуть монго везде, но эта эйфория обманчива
Если есть сервис аутентификации, и его работа с ролями, пользователями и сессиями вполне себе реляционна, и работает он стабильно и быстро, то трудозатраты по переезду на монгу явно не окупятся профитом.
Но если говорить об остальных сервисах, где переезд на документоориентированную БД даст явный профит, и тут не все так просто. то часть разработчиков как всегда занята допиливанием фич,
часть на баг-фиксинге. к тому же в условиях довольно сжатых сроков никто не даст экспериментировать над центральными сервисами системы
Нужна была такая подсистема, которая не заденет ядро системы, и  тем не менее сможет показать профит от технологии
В нашем случае выбор пал на импорт из внешних систем и вот почему
- Данные могут приходить в каком угодно формате
- Схема данных заранее неизвестна
- Необходима высокая скорость записи и чтения

Когда мною был продуман принцип работы сервиса внешней интеграции, получено разрешение архитектора творить под мою ответственность, я пришёл к Роме, и тут начался третий круг

## 3 круг - Чревоугодие.  (live coding)

*Рома:*

- Реализация стандартного  ETL процесса: source - mongodb - destination. 
Рассказать этапы импорта из черного ящика (get from source, check if exists in db, insert or update to destination, update db).
Описать суть проблемы. Описать решение на PG. 
Показать структуру таблиц, рассказать о генерации схемы данных. Ппредаствим что у нас некая CRM для ада 
Показать зачем нужна монга.   

Дальше показываем как сделать простейший импорт:

- Показываем что ПГ поднят и там есть данные. Показываем репозиторий ПГ для чтения записей.
Методы - получить список HellModel items. 

- Показываем yaml. Поднимаем stadlalone mongodb. Поясняем Connection string.
Строка подключения имеет следующий вид: mongodb://[username:password@]hostname[:port][/[database][?options]]

Показываем mongoCompass и пустую монгу и режим Standalone.

- Простота использования (Пример репозитория CRUD) - глянуть nuget MongoRepository. 
Показываем nuget пакет MongoDB.Driver
Показываем generic repository.

```c#
public class MongoDBBaseRepository<T> : IBaseRepository<T> where T : IBaseModel

```
рассказ про mongo client.
После выполнения всех необходимых операций нам необязательно закрывать подключение, 
как, например, в случае с подключениями к другим базам данных, 
так как MongoDB сама выполнит всю работу (найти инфу в документации).

Добавили профилирование в код, чтобы понимать где конкретно происходят тормоза.

```c#
public sealed class Profiler : IDisposable
```

Показать декоратор с подключенным профилированием:
```c#
public class HellRepository : MongoDBBaseRepository<HellModel>
public class HellRepositoryWithProfiling : HellRepository
```

- показываем импортер и foreachAsync. Проливаем данные в несколько потоков. 
- Перфоманс из коробки. Показать результаты профилирования в консоль. 0.3-0.5 мс на операцию.
- Показываем что схемы не было и автоматически создались база и коллекции чяерез монго компасс. 
- Можно повторить N раз меняя имя базы и коллекции. Поясняем что для ПГ надо было постоянно контролировать схему и миграции.

Показываем даныне в компасе, обращаем внимание на _id и про ObjectId.

ObjectIds are small, likely unique, fast to generate, and ordered. ObjectId values are 12 bytes in length, consisting of:
- a 4-byte timestamp value, representing the ObjectId’s creation, measured in seconds since the Unix epoch
- a 5-byte random value
- a 3-byte incrementing counter, initialized to a random value
## 4 круг - Жадность.

*Дима:*

Глядя на результат, как красиво у нас бегут и обрабатываются данные в многопоточке,
мы с чистой душой, думая что всё настроили и дописали сервис отдаем его в нагрузочное тестирование,
рассказывая о том какая монго крутая, и что время ответа в 0.1ms она сама считает медленным...

И тут получаем статистику.
Которая идёт в разрез со всем, что мы видели.

Да, первое время на малом объёме данных монго работает великолепно, но по мере увеличения количества документов в ней
она деградирует в ответах, и начинает потреблять cpu за себя, да и за все остальные сервисы.
Пиковая нагрузка выдавала 10 из 12 ядер стенда в потолок, и ответ от 300мс до нескольких секунд

Получаем данные профилирования и убеждаемся что тормозит исключительно MongoDB...

(тут картинки с деградацией)

Вопрос стал на столько остро, что нам было сказано, если вы не найдёте ответ до конца спринта, мы отказываемся от монго в принципе. (что и требовалось доказать)
И наша небольшая команда стала ломать голову, в чём же проблема.

Если честно, уже не вспомнить всех теорий заговора которые мы тогда придумывали, пытаясь понять что же происходит.
Я вспомнил, что в MS SQL сервере происходит деградация записи если довольно крупный кластерный индекс.
И тут нас всех озарило
По коллекции просто идёт фулскан. Оказывается MongoDB обычная база данных, и никакой магии не существует.
Хотя, пожалуй немного её было, стоило на лету создать индекс по полю фильтрации, на сервере моментально наступила тишина, и Mongo потребляла не более положеных для себя 50-70% cpu
Естественно когда все показатели вернулись в привычную норму, наше детище прошло тестирование, и следующим этапом была подготовка ci/cd

## 5 круг - Гнев и лень.

*Дима:*

Во время разработки и тестирования, всё проходило на одной ноде mongoDB, и наш yaml выглядел так -- слайд standalone

Однако поскольку наша система должна была пройти демо у заказчика, я как ответственый базист задался вопрсоом, как же мы это всё будем кластеризовать
И сейчас мы с вами углубимся в этот момент, для начала рассмотрим самый простой  пример реплика сет, минимальная форма продакшн сервера рекоммендуемая разработчиками

  => replica set => sharded cluster (yaml config) - почему 15 шт?
- mongod + mongos.
- Проверка отказоусточивости. priority. write concern. split brain

## 6 круг - Для еретиков и лжеучителей.

*Рома:*

- Решились сменить БД перед релизом во избежание сложных миграций.
Централизация использования технологий.
- Проблемы реляционного мышления при работе с noSQL(человеческий фактор).
Я сам .net разработчик с бэкграундом MS SQL SERVER и что я не понимал.

(тут картинка - несколько таблиц превращаются в одну коллекцию)

https://docs.mongodb.com/manual/core/data-model-design/#data-modeling-embedding

https://docs.mongodb.com/manual/core/data-model-design/#data-modeling-referencing

- collection != table. Хранение произвольных объектов в одной коллекции - добавить примеры (контрагенты - физлица и юрлица в одной коллекции).
- Сложный документ - значит транзакции частно не нужны - можно хранить все в одном документе.

## 7 круг - Для насильников и убийц всех мастей.

*Дима:*

- Измения в архитектуре и Переписываем реализацию старых(реляционных) репозиториев.
- Высокая трудоемкость при проектировании - plain-table перевести просто, остальное сложно и требуется переработка. 
- Ограничение на размер (16мб) и глубину документа (100). https://docs.mongodb.com/manual/reference/limits/
- Анализ индексов при проектирование и на бою под нагрузкой. MongoDB uses B-trees for its indexes. A single collection can have no more than 64 indexes.
- Особенности использования индексов при построении запросов - один индекс на запрос (поискать hint в монго-клиенте).
Performance:
- Because the index contains all fields required by the query, MongoDB can both match the query conditions and return the results using only the index.
- Querying only the index can be much faster than querying documents outside of the index. Index keys are typically smaller than the documents they catalog, and indexes are typically available in RAM or located sequentially on disk.
- In most cases the query optimizer selects the optimal index for a specific operation; however, you can force MongoDB to use a specific index using the hint() method. Use hint() to support performance testing, or on some queries where you must select a field or field included in several indexes.

- план запросов - https://docs.mongodb.com/manual/reference/explain-results/

_Вывод - надо аккуратно проектировать._

## 8 круг - Для обманувших недоверившихся.

*Рома:*

Особенности при переходе на MongoDB.

- MongoShell - добавить определение(https://docs.mongodb.com/manual/mongo/). 

DSL - добавить определение. Начать изучение монги лучше отсюда. (https://docs.mongodb.com/manual/crud/)

Рассказать про генераторы в js. 

Обратить внимание на перенос строк \n и ограниченную длину строки (не все документы помещаются)
The mongo shell prompt has a limit of 4095 codepoints for each line. If you enter a line with more than 4095 codepoints, the shell will truncate it.

(тут примеры js кода)

- Рассказать про nuget MongoDB.Driver, Core, Bson.
- Выделение отдельной сущности для singleton MongoClient - управление пулом коннектов и dispose автоматические.

```c#
internal sealed class ConnectionManager : IConnectionManager
{    
    public IMongoDatabase Db { get; private set; }

    public void Initialize(string connectionString, string dbName)
    {
        var url = new MongoUrl(connectionString);
        var mongoClientSettings = MongoClientSettings.FromUrl(url);     
        _client = new MongoClient(mongoClientSettings);
        Db = _client.GetDatabase(dbName);
    }

    private MongoClient _client;
}
```

(тут примеры кода)

- Непривычная фильтрация - вкусовщина, кому-то нравится, кому-то нет. Проблемы с лямбдами - найти issue.
    1. Условные операторы
    1. Логические операторы
    1. Поиск по массивам

(тут примеры кода)

- Сериализация. BSON документ (произвольные иерархические структуры данных) и простота сериализации. JObject отсутствует из коробки - сравнить в Newtown.JSON и рассказать как писать свои сериализаторы. BsonIgnore и другие атрибуты, показать пример в коде. 

Конвенции - https://github.com/mongodb/mongo-csharp-driver/tree/master/src/MongoDB.Bson/Serialization/Conventions.

(тут примеры кода)

- Транзакции - основы работы.

https://www.mongodb.com/blog/post/mongodb-multi-document-acid-transactions-general-availability

https://docs.mongodb.com/manual/core/transactions/

В июне 2018 года (в версии 4.0) добавлена поддержка транзакций, удовлетворяющих требованиям ACID:
- In version 4.0, MongoDB supports multi-document transactions on replica sets.
- In version 4.2 (август 2019), MongoDB introduces distributed transactions, which adds support for multi-document transactions on sharded clusters and incorporates the existing support for multi-document transactions on replica sets.

```c#
var sessionOptions = new ClientSessionOptions
{
    DefaultTransactionOptions = new TransactionOptions(
        readConcern: new Optional<ReadConcern>(ReadConcern.Local),
        writeConcern: new Optional<WriteConcern>(WriteConcern.WMajority))
};
using var session = await _mongoConnectionManager.StartSession(sessionOptions);
session.StartTransaction(transactionOptions);
// var result = do some work with mongo db; 
if (result.Succeeded) await session.CommitTransactionAsync();
```

- Нет атрибутов для индексов, кроме BsonId. 

## 9 круг - Для отступников и предателей всех сортов.

*Дима:*

- Проблемы с поиском по датам (фильтры, непонимание DSL) и Проблемы с UI для администрирования данных.

Неудобный MongoDB Compass. Есть DataGrip, Robo 3T.
Рассказать про средства мониторинга запросов, explain, план запроса.

- Проблема с транзакциями (работают только в режиме кластера, но админы решили сэкономить на тестовых стендах). 
надо найти текст ошибок в монге и монго драйвере (retry writes=true - https://docs.mongodb.com/manual/core/retryable-writes/).
акцент именно на некорректной ошибке и о том что документацию надо читать не только нам.

https://github.com/mongodb/mongo-csharp-driver/pull/389

*Рома:*

Проблемы выявленные при тестировании:

- Проблемы сериализации GUID.
 
https://studio3t.com/knowledge-base/articles/mongodb-best-practices-uuid-data/#mongodb-uuid-data

список - https://api.mongodb.com/csharp/current/html/T_MongoDB_Bson_GuidRepresentation.htm

```c#
BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
```

```c#
public enum GuidRepresentation
{
    /// <summary>
    /// The representation for Guids is unspecified, so conversion between Guids and Bson binary data is not possible.
    /// </summary>
    Unspecified = 0,
    /// <summary>
    /// Use the new standard representation for Guids (binary subtype 4 with bytes in network byte order).
    /// </summary>
    Standard,
    /// <summary>
    /// Use the representation used by older versions of the C# driver (including most community provided C# drivers).
    /// </summary>
    CSharpLegacy,
    /// <summary>
    /// Use the representation used by older versions of the Java driver.
    /// </summary>
    JavaLegacy,
    /// <summary>
    /// Use the representation used by older versions of the Python driver.
    /// </summary>
    PythonLegacy
}

... test ->
switch (BsonDefaults.GuidRepresentation)
{
    case GuidRepresentation.CSharpLegacy: expectedGuidJson = "CSUUID(\"00112233-4455-6677-8899-aabbccddeeff\")"; break;
    case GuidRepresentation.JavaLegacy: expectedGuidJson = "JUUID(\"00112233-4455-6677-8899-aabbccddeeff\")"; break;
    case GuidRepresentation.PythonLegacy: expectedGuidJson = "PYUUID(\"00112233-4455-6677-8899-aabbccddeeff\")"; break;
    case GuidRepresentation.Standard: expectedGuidJson = "UUID(\"00112233-4455-6677-8899-aabbccddeeff\")"; break;
    default: throw new Exception("Invalid GuidRepresentation.");
}
```

- Проблема версий пакета Mongo.Bson (DateTimeOffset) и необходимость интеграционных тестов на сериализацию/десериализацию. (mongo2go nuget для тестов)
(надо воспроизвести, указать версии пакетов, сравнить код сериализатора):
- https://jira.mongodb.org/browse/CSHARP-1483
- https://stackoverflow.com/questions/16765543/properly-using-handling-datetimeoffset-in-mongodb
- https://stackoverflow.com/questions/10480127/mongodb-and-datetimeoffset-type
- https://blog.krusen.dk/c-mongodb-serialization-of-datetimeoffset/

- Проблемы сериализации int32.

https://docs.mongodb.com/manual/core/shell-types

https://jira.mongodb.org/browse/CSHARP-2602

сделать PR на github

## Дорога к раю.

*Дима:*

Или почему MongoDB осталась основным хранилищем данных в проекте. 

Производительность + нет миграций + отказоусточивость + стоимость.

выводы:

- _начать с малого._
- _все просто. Начать работать с монгой легко._
- _не все гладко, но дорогу осилит идущий. (надо как-то переписать вывод, например что сильная сторона монги - легко поднимаемый кластер)_
- _надо заранее готовить аргументы и примеры для разработчиков._
- _Отсутствие жёсткой схемы данных(если надо, то можно настроить), если Вы работаете с динамически изменяемой структурой объекта, это то, что Вам нужно._
- _BSON это гибкость, но сериализация требует особого внимания._
- _сначала лучше попробовать вручную поработать через mongoshell._

## Q&A + Ссылки и контакты

MongoDB:
- https://en.wikipedia.org/wiki/MongoDB
- https://jira.mongodb.org/
- https://github.com/mongodb/
- https://docs.mongodb.com/

Доклад:

- https://github.com/daredever/MongoHell/
