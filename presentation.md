## Welcome part

#### слайд --> (лого црт + spbdotnet)

_Дима:_

Добрый день, друзья, рады приветствовать вас на 58 встрече Петербургского .net сообщества, проходящей в коллаборации с Центром Речевых Технологий. 

#### слайд --> (фото)
Меня зовут Елисеев Дмитрий, я занимаюсь базами данных в ЦРТ.

#### слайд --> (фото)
_Рома:_

Меня зовут Щербаков Роман, и я занимаюсь разработкой бэк-энда также в ЦРТ и мы хотели бы рассказать вам о внедрении MongoDB в .net проект.

#### слайд --> (лого монгодб)

- Для начала хотелось бы узнать, сколько из вас слышали о noSQL решениях? 
- а о MongoDB? 
- А кто применял её в своей работе? 
- а на продакшене?
Ну, как и ожидалось, с каждым вопросом количество рук сокращается...
Что ж, прежде чем начать, давайте очень кратенько посмотрим, что же такое MongoDB.

#### слайд --> (общее описание и историческая сводка)
Итак, MongoDB это кросс-платформенная документо-ориентированная база данных с открым исходным кодом, относящаяся к классу NoSql (not only sql) решений, Использует JSON-подобные документы для хранения.
Важно понимать, что документ это более сложная структура, чем просто строка в реляционной базе данных, но об этом чуть позже.

Первый релиз был в 2009 году, написана на с++.

#### слайд --> (картинка)
Что мы имеем внутри mongodb? У нас может быть n баз данных, в каждой из которых по m коллекций в каждой из которых может хранится l абсолютно любых документов.

#### слайд --> (лого монгодб)

_Дима:_

И теперь когда в общих чертах мы все имеем базовое представление о том, что такое MongoDB хотелось бы начать наще повествование.
Когда я пришёл на новый проект, то начал анализировать чем он живёт, какие перспективы, какие технологии используются.
Одной из первых задач которая мне прилетела была - тормозит postgres. 

-- слайд сравнения чтения из прошлого доклада

Углубившись в суть проблемы, я обнаружил, что тормозит поиск по json документу, проблема заключалась в фильтрации по вложенным полям

#### слайд --> (pg json и jsonb)
в постгрес cуществуют два типа данных JSON: json и jsonb. 
Они принимают на вход почти одинаковые наборы значений, а отличаются главным образом с точки зрения эффективности. Тип json сохраняет точную копию введённого текста, которую функции обработки должны разбирать заново при каждом выполнении запроса, тогда как данные jsonb сохраняются в разобранном двоичном формате, что несколько замедляет ввод из-за преобразования, но значительно ускоряет обработку, не требуя многократного разбора текста. Кроме того, jsonb поддерживает индексацию, что тоже может быть очень полезно, 

в MongoDB данные хранятся в bson документах.

#### слайд --> (bson)
BSON (англ. Binary JavaScript Object Notation) — формат электронного обмена цифровыми данными, основанный на JavaScript, бинарная форма представления простых структур данных и ассоциативных массивов (которые в контексте обмена называют объектами или документами). Является надмножеством JSON, включая дополнительно регулярные выражения, двоичные данные и даты 

После поверхностного сравнения постгреса и монги, я пришёл к выводу, что для нашего проекта больше подходит MongoDB

#### слайд --> (круги ада, агенда)
И  именно в этот момент начинается "Божественная комедия" внедрения MongoDB в .net проект. 
Раскрыть тему кругов (тут слайд с картинкой данте) - рассказать что сейчас мы пройдемся вместе по 9 кругам "ада" внедрения монгодб.

#  Circles of Hell

#### слайд --> (1. лимб)

## 1 круг - Лимб. 

_Дима:_

#### слайд --> (слайд с проблемами монги которых боятся)

- Предложение внедрить монгу. 
Когда я подошёл к архитектору нашего проекта с вопросом, а почему собственно не Mongo? 

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
и документа в документе показал, что по производительности mongoDB выигрывает у пг. 

#### слайд --> (бенчмарки mongo vs pg)

Jabson test for acid.
Бенчмарки производительности для любого руководства  всегда выглядят весомо.
 
#### слайд --> (реплика) + (шарды)
Простота репликации и шардирования, не требующая глубокого понимания принципов работы БД. -- слайд со схемой шардирования
Самое главное отказоусточивость - **КЛАСТЕР**!

#### слайд --> (лицензии)
Ну и самое главное вопрос лицензирования.
MongoDB изначально выходил под лицензией GNU Affero General Public версии 3. Языковые драйверы доступны под лицензией Apache. MongoDB можно бесплатно получить по общедоступной лицензии Affero (AGPL) GNU. В октябре 2018 года компания-разработчик объявила о переходе к более жёсткой по сравнению с AGPL копилефтной лицензии SSPL (Server Side Public License)[10].[11]. Вслед за этим было начато изучение новой лицензии представителями Open Source Initiative и Free Software Foundation на предмет соответствия определениям открытого и свободного программного обеспечения[12].

Кроме того, компания MongoDB выпускает коммерческую версию СУБД, включающую дополнительные функции (например, интеграцию с SASL, LDAP, Kerberos, SNMP), инструменты управления, мониторинг и резервное копирование, а также поддержку.

Показав все достоинства, и когда они взяты во внимание, мы переходим ко второму кругу

#### слайд --> (2. Сладострастие)
## 2 круг - Сладострастие.

_Дима:_ 

Проанализировав проект, и поняв что большинство сервисов хранит jobject в пг,
и воодушевившись показателями синтетик тестов сразу чешутся руки творить добро и справедливость,
и воткнуть монго везде, но эта эйфория обманчива
Если есть сервис аутентификации, и его работа с ролями, пользователями и сессиями вполне себе реляционна, и работает он стабильно и быстро, то трудозатраты по переезду на монгу явно не окупятся профитом.
Но если говорить об остальных сервисах, где переезд на документоориентированную БД даст явный профит, и тут не все так просто. то часть разработчиков как всегда занята допиливанием фич,
часть на баг-фиксинге. к тому же в условиях довольно сжатых сроков никто не даст экспериментировать над центральными сервисами системы
Нужна была такая подсистема, которая не заденет ядро системы, и  тем не менее сможет показать профит от технологии

#### слайд --> (ETL)

В нашем случае выбор пал на импорт из внешних систем и вот почему
- Данные могут приходить в каком угодно формате
- Схема данных заранее неизвестна
- Необходима высокая скорость записи и чтения

Реализация стандартного  ETL процесса: source - mongodb - destination. 

Когда мною был продуман принцип работы сервиса внешней интеграции, получено разрешение архитектора творить под мою ответственность, я пришёл к Роме, и тут начался третий круг

#### слайд --> (3. Чревоугодие)
## 3 круг - Чревоугодие. 

_Рома:_
Итак,  задача - импорт из черного ящикаю Можно разбить на этапы: get from source, check if exists in db, insert or update to destination, update db.
Решение на PG - универсальное через хранение json в базе или жесткое под каждую систему с ранее определенной схемой. 
Показать зачем нужна монга - пришла на помощь, так как в ней нет схемы.

Предлагаю рассмотреть решение на примере. Показать боевой код мы к сожалению не можем, так что давайте предаставим, что в будущем у нас бует некая 
CRM для ада и нам достаточно просто записать данные в монго дб, а потом когда-нибудь мы их прогрузим в саму CRM.

#### слайд -->  (live coding)

Дальше показываем как сделать простейший импорт:

- Показываем yaml. Поднимаем stadlalone mongodb. 

- Поясняем Connection string.
Строка подключения имеет следующий вид: mongodb://[username:password@]hostname[:port][/[database][?options]]

- Показываем mongoCompass и пустую монгу и режим Standalone.

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

Without any arguments, constructing a MongoClient instance will connect to “localhost” port 27017.

Добавили профилирование в код, чтобы понимать где конкретно происходят тормоза.

```c#
public sealed class Profiler : IDisposable
```

Показать декоратор с подключенным профилированием:
```c#
public class HellRepository : MongoDBBaseRepository<HellModel>
public class HellRepositoryWithProfiling : HellRepository
```

- показываем импортер и forEachAsync. Проливаем данные в несколько потоков. 
- Перфоманс из коробки. Показать результаты профилирования в консоль. примерно 0.5 мс на операцию.
- Показываем что схемы не было и автоматически создались база и коллекции через монго компасс. 
- Поясняем что для ПГ надо было постоянно контролировать схему и миграции.

Показываем данные в компасе, обращаем внимание на _id и про ObjectId.

ObjectIds are small, likely unique, fast to generate, and ordered. ObjectId values are 12 bytes in length, consisting of:
- a 4-byte timestamp value, representing the ObjectId’s creation, measured in seconds since the Unix epoch
- a 5-byte random value
- a 3-byte incrementing counter, initialized to a random value

#### слайд --> (4. Жадность)

## 4 круг - Жадность.

_Дима:_

Глядя на результат, как красиво у нас бегут и обрабатываются данные в многопоточке,
мы с чистой душой, думая что всё настроили и дописали сервис отдаем его в нагрузочное тестирование,
рассказывая о том какая монго крутая, и что время ответа в 0.1ms она сама считает медленным...

#### слайд --> (график тормозов)
И тут получаем статистику.
(тут картинки с деградацией)
Которая идёт в разрез со всем, что мы видели.

Да, первое время на малом объёме данных монго работает великолепно, но по мере увеличения количества документов в ней
она деградирует в ответах, и начинает потреблять cpu за себя, да и за все остальные сервисы.
Пиковая нагрузка выдавала 10 из 12 ядер стенда в потолок, и ответ от 300мс до нескольких секунд

Получаем данные профилирования и убеждаемся что тормозит исключительно MongoDB...

Вопрос стал на столько остро, что нам было сказано, если вы не найдёте ответ до конца спринта, мы отказываемся от монго в принципе. (что и требовалось доказать)
И наша небольшая команда стала ломать голову, в чём же проблема.

Если честно, уже не вспомнить всех теорий заговора которые мы тогда придумывали, пытаясь понять что же происходит.
Я вспомнил, что в MS SQL сервере происходит деградация записи если довольно крупный кластерный индекс.
И тут нас всех озарило
По коллекции просто идёт фулскан. 

#### слайд --> (картинка - магии нет)
Оказывается MongoDB обычная база данных, и никакой магии не существует.
Хотя, пожалуй немного её было, стоило на лету создать индекс по полю фильтрации, на сервере моментально наступила тишина, и Mongo потребляла не более положеных для себя 50-70% cpu
Естественно когда все показатели вернулись в привычную норму, наше детище прошло тестирование, и следующим этапом была подготовка ci/cd

#### слайд --> (5. Гнев и Лень)

## 5 круг - Гнев и лень.

_Дима:_

Во время разработки и тестирования, всё проходило на одной ноде mongoDB,
однако согласно документации мининимальная конфигурация монги - реплика сет

#### слайд --> (replica set схема)

схема

#### слайд --> (replica set yaml)

yaml

#### слайд --> (replica set init)

init replica set

#### слайд --> (replica set write concern)

write concern

#### слайд --> (sharded cluster схема)

схема sharded cluster

#### слайд --> (sharded cluster yaml)

yaml + mongos + config server

#### слайд --> (sharded cluster shardes)

шарды

#### слайд --> (sharded cluster init config)

init config

#### слайд --> (sharded cluster init router)

init router

#### слайд --> (sharded cluster init shardes)

init shardes

#### слайд --> (sharded cluster отказоусточивая схема)

почему 15 шт? Проверка отказоусточивости. split brain

#### слайд --> (6. Для еретиков и лжеучителей)

## 6 круг - Для еретиков и лжеучителей.

_Рома:_

- Решились сменить БД перед релизом во избежание сложных миграций.
Централизация использования технологий.
- Проблемы реляционного мышления при работе с noSQL(человеческий фактор).
Я сам .net разработчик с бэкграундом MS SQL SERVER и что я не понимал.

#### слайд --> (сollection != table)
- collection != table. В статьях часто пишут что коллекцию можно представлять как некую таблицу. 
Я считаю что тут как раз и кроется самое главное упущение. 
МонгоДБ позволяет хранить разные документы в одной коллекции.
Давайте представим у нас есть клиенты (физлица и юрлица), у каждого есть номер телефона. Нам надо разослать им смски.

#### слайд --> (реляционный пример)
реляционная модель: таблица клиенты, таблица юрлица, таблица физлица

#### слайд --> (реляционные запросы)
делаем запрос с union: 
```sql
select phone from clients inner join (ur union fiz) where clients.id = 1;
```

#### слайд --> (nosql пример)
nosql: коллекция клиенты и два типа документов

#### слайд --> (nosql запрос)
один запрос (забегая немного вперед)
```js
db.clients.find({_id: 1});
```

#### слайд --> (data-modeling-embedding)

вообще в документации есть примеры как можно проектировать структуры для хранения. например как показано выше - embedding.

когда документ сложный и все хранится в нем.

https://docs.mongodb.com/manual/core/data-model-design/#data-modeling-embedding

Сложный документ - значит обновление можно выполнять за одну операцию, что удобно.

#### слайд --> (data-modeling-referencing)
или другой вариант со ссылками - referencing, по сути так же как и в реляционных базах.
https://docs.mongodb.com/manual/core/data-model-design/#data-modeling-referencing

на практике чаще получается использовать комбинацию этих приемов.

#### слайд --> (7. Для насильников и убийц всех мастей)

## 7 круг - Для насильников и убийц всех мастей.

_Дима:_

- Измения в архитектуре и Переписываем реализацию старых(реляционных) репозиториев.
- Высокая трудоемкость при проектировании - plain-table перевести просто, остальное сложно и требуется переработка. 

#### слайд --> (ограничения монги)

https://docs.mongodb.com/manual/reference/limits/

- Ограничение на размер (16мб) и глубину документа (100). 
- MongoDB uses B-trees for its indexes. A single collection can have no more than 64 indexes.

#### слайд --> (8. Для обманувших недоверившихся)

## 8 круг - Для обманувших недоверившихся.

_Рома:_

Особенности при переходе на MongoDB.

#### слайд --> (MongoShell)
Начать изучение монги лучше отсюда.

MongoShell (https://docs.mongodb.com/manual/mongo/). 

Монгошелл - это интерфейс командной строки на javascript для работы с mongodb. Можно использовать как для запросов и обновления данных так и для администрирования.
Монгошелл включен в поставку сервера монгодб, но можно установить отдельно как самостоятельный пакет.

Как уже было видно ранее у монгодб свой DSL. 

#### слайд --> (MongoShell примеры)
(тут примеры js кода - картинки с сайта https://docs.mongodb.com/manual/crud/ )

Можно писать свои генераторы в js. 

Обратить внимание на перенос строк \n и ограниченную длину строки (не все документы помещаются)
The mongo shell prompt has a limit of 4095 codepoints for each line. If you enter a line with more than 4095 codepoints, the shell will truncate it.

#### слайд --> (спикок nuget)
Рассказать про nuget MongoDB.Driver (Core, Bson). Bson - может потребоваться отдельно для моделек и генераторов.

#### слайд --> (mongo client)
Выделение отдельной сущности для singleton MongoClient - управление пулом коннектов и dispose автоматические.

A MongoClient object will be the root object. It is thread-safe and is all that is needed to handle connecting to servers, monitoring servers, and performing operations against those servers.
It is recommended to store a MongoClient instance in a global place, either as a static variable or in an IoC container with a singleton lifetime.

The implementation of IMongoDatabase provided by a MongoClient is thread-safe and is safe to be stored globally or in an IoC container.

The implementation of IMongoCollection<TDocument> ultimately provided by a MongoClient is thread-safe and is safe to be stored globally or in an IoC container. 

Подправить код:
```c#
public sealed class ConnectionManager : IConnectionManager
{    
    public IMongoDatabase Db { get; private set; }

    public void ConnectionManager(string connectionString, string dbName)
    {
        var url = new MongoUrl(connectionString);
        var mongoClientSettings = MongoClientSettings.FromUrl(url);     
        _client = new MongoClient(mongoClientSettings);
        Db = _client.GetDatabase(dbName);
    }

    private MongoClient _client;
}
```

#### слайд --> (filter)
- Непривычный DSL - вкусовщина, кому-то нравится, кому-то нет.

Фильтрация: 
    1. Условные операторы
    1. Логические операторы
    1. Поиск по массивам

```c#
FilterDefinition<BsonDocument> filter = "{ x: 1 }";
FilterDefinition<BsonDocument> filter = new BsonDocument("x", 1);

var filter = Builders<BsonDocument>.Filter.Eq("i", 71);
var filter = Builders<BsonDocument>.Filter.Gt("i", 50);

var filterBuilder = Builders<BsonDocument>.Filter;
var filter = filterBuilder.Gt("i", 50) & filterBuilder.Lte("i", 100);
```

Поддерживаются лямбды. Попадались issue с лямбдами, но на практике не сталкивался.

Также есть билдеры для обновления:

```c#
var filter = Builders<BsonDocument>.Filter.Eq("i", 10);
var update = Builders<BsonDocument>.Update.Set("i", 110);
await collection.UpdateOneAsync(filter, update);
```

Также есть экстеншены для сортировки:
```c#
var filter = Builders<BsonDocument>.Filter.Exists("i");
var sort = Builders<BsonDocument>.Sort.Descending("i");
var document = await collection.Find(filter).Sort(sort).FirstAsync();

var builder = Builders<BsonDocument>.Sort;
var sort = builder.Ascending("x").Descending("y");
```

Projecting Fields:
```c#
var projection = Builders<BsonDocument>.Projection.Exclude("_id");
var document = await collection.Find(new BsonDocument()).Project(projection).FirstAsync();

var projection = Builders<BsonDocument>.Projection.Include("x").Include("y").Exclude("_id");
```

#### слайд --> (BSON)
- Сериализация. BSON документ (произвольные иерархические структуры данных) и простота сериализации. 

ToBsonDocument, ToJson. 

Можно писать и регестрировать свои сериализаторы.

https://mongodb.github.io/mongo-csharp-driver/2.7/reference/bson/mapping/
BsonIgnore и другие атрибуты, показать пример в коде. 
Нет атрибутов для индексов, кроме BsonId. 

#### слайд --> (конвенции)
Конвенции - https://github.com/mongodb/mongo-csharp-driver/tree/master/src/MongoDB.Bson/Serialization/Conventions.

When automapping a class, there are a lot of decisions that need to be made. For example:

Which members of the class should be serialized
Which member of the class is the “Id”
What element name should be used in the BSON document
If the class is being used polymorphically, what discriminator values are used
What should happen if a BSON document has elements we don’t recognize
Does the member have a default value
Should the default value be serialized or ignored

#### слайд --> (транзакции)

- Транзакции - основы работы.

https://www.mongodb.com/blog/post/mongodb-multi-document-acid-transactions-general-availability

https://docs.mongodb.com/manual/core/transactions/

В июне 2018 года (в версии 4.0) добавлена поддержка транзакций, удовлетворяющих требованиям ACID:
- In version 4.0, MongoDB supports multi-document transactions on replica sets.
- In version 4.2 (август 2019), MongoDB introduces distributed transactions, which adds support for multi-document transactions on sharded clusters and incorporates the existing support for multi-document transactions on replica sets.

Call AbortTransaction or AbortTransactionAsync to abort a transaction. Since any transaction in progress is automatically aborted when a session is ended, you can also implicitly abort an uncommitted transaction by simply ending the session.

#### слайд --> (транзакции код)

```c#
var sessionOptions = new ClientSessionOptions
{
    DefaultTransactionOptions = new TransactionOptions(
        readConcern: new Optional<ReadConcern>(ReadConcern.Local),
        writeConcern: new Optional<WriteConcern>(WriteConcern.WMajority))
};
using var session = await _mongoConnectionManager.StartSession(sessionOptions);
session.StartTransaction(transactionOptions);
// var result = do some work with mongo db using the session; 
if (result.Succeeded) await session.CommitTransactionAsync();
```

или полностью асинхронно
```c#
using (var session = await client.StartSessionAsync())
{
    try
    {
        // execute async operations using the session
    }
    catch
    {
        await session.AbortTransactionAsync(); // now Dispose on the session has nothing to do and won't block
        throw;
    }
    await session.CommitTransactionAsync();
}
```

#### слайд --> (9. Для отступников и предателей всех сортов)

## 9 круг - Для отступников и предателей всех сортов.

_Дима:_

здесь мы расскажем о проблемах с которыми мы столкнулись при тестировании и внедрении.

#### слайд --> (список UI)

- Проблемы с поиском по датам (фильтры, непонимание DSL) и Проблемы с UI для администрирования данных.

Неудобный MongoDB Compass. Есть DataGrip, Robo 3T.

#### слайд --> (compass start)
компас вход

#### слайд --> (compass doc)
компас документ

#### слайд --> (compass query)
компас explain query

- Анализ индексов на бою под нагрузкой. Особенности использования индексов при построении запросов - один индекс на запрос (поискать hint в монго-клиенте).
Performance:
- Because the index contains all fields required by the query, MongoDB can both match the query conditions and return the results using only the index.
- Querying only the index can be much faster than querying documents outside of the index. Index keys are typically smaller than the documents they catalog, and indexes are typically available in RAM or located sequentially on disk.
- In most cases the query optimizer selects the optimal index for a specific operation; however, you can force MongoDB to use a specific index using the hint() method. Use hint() to support performance testing, or on some queries where you must select a field or field included in several indexes.

план запросов - https://docs.mongodb.com/manual/reference/explain-results/

#### слайд --> (compass stat)
компас performance. компас это инструмент для админов и dba.

#### слайд --> (robo3t doc)
robo3t json

#### слайд --> (data grip start)
data grip вход

#### слайд --> (проблема с транзациями)
- Проблема с транзакциями (работают только в режиме кластера, но админы решили сэкономить на тестовых стендах). 
 
разные ошибки в монге и в монго клиенте (retry writes=true - https://docs.mongodb.com/manual/core/retryable-writes/).
акцент именно на некорректной ошибке и о том что документацию надо читать не только нам.
https://github.com/mongodb/mongo-csharp-driver/pull/389

#### слайд --> (проблема GUID)
_Рома:_

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

#### слайд --> (проблема int32)
- Проблемы сериализации int32.

https://docs.mongodb.com/manual/core/shell-types

https://jira.mongodb.org/browse/CSHARP-2602

сделать PR на github

#### слайд --> (Дорога к раю)

## Дорога к раю.

_Дима:_

Или почему MongoDB осталась основным хранилищем данных в проекте. 

#### слайд --> (выводы)
Производительность + нет миграций + отказоусточивость + стоимость.

выводы:

- _начать с малого._
- _все просто. Начать работать с монгой легко._
- _не все гладко, но дорогу осилит идущий. (надо как-то переписать вывод, например что сильная сторона монги - легко поднимаемый кластер)_
- _надо заранее готовить аргументы и примеры для разработчиков._
- _Отсутствие жёсткой схемы данных(если надо, то можно настроить), если Вы работаете с динамически изменяемой структурой объекта, это то, что Вам нужно._
- _BSON это гибкость, но сериализация требует особого внимания. необходимость интеграционных тестов на сериализацию/десериализацию. (mongo2go nuget для тестов)_
- _сначала лучше попробовать вручную поработать через mongoshell._

#### слайд --> (ссылки)

## Ссылки

MongoDB:
- https://en.wikipedia.org/wiki/MongoDB
- https://jira.mongodb.org/
- https://github.com/mongodb/
- https://docs.mongodb.com/
- https://www.mongodb.com/

Доклад:

- https://github.com/daredever/MongoHell/

#### слайд --> (Q&A и контакты)

## Q&A и контакты

@rowcount

@daredever
