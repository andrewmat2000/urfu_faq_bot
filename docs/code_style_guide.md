# Asd C# Code Style Guide

## Базируется на [C# at Google Style Guide](https://google.github.io/styleguide/csharp-style.html).<br /><br />

# Правила наименования

## Код

* ### Использование **PascalCase**.

1. Названия классов.

```C#
    class Adsvel { }
```

2. Названия методов.

```C#
    void Foo() { }
```

3. Перечисления.

```C#
    enum Sample {
        One,
        Two,
        Three
    }
```

4. Публичные полей.

```C#
    public int Id;
```

5. Свойства.

```C#
    int Id { get; set; }
```

6. Пространства имен.

```C#
    namespace Adsvel;
```

* Использование **camelCase**.

1. Названия локальных переменных

```C#
    int a;
```

2. Названия параметров

```C#
    void Foo(int param) { }
```

* ### Использование **_camelCase**

1. Непубличные поля

```C#
    private int _field;
    internal int _field;
    protected int _field;
    protected internal int _field;
```

2. Непубличные свойства

```C#
    private int _property { get; set; }
    internal int _property { get; set; }
    protected int _property { get; set; }
    protected internal int _property { get; set; }
```

* ### Аббревиатура считается словом без внеутренних пробелов, включая акронимы.

```C#
    class Html { }
    class Grpc { }
```

* ### Названия интерфейсов начинаются с заглавной буквы '**I**'.

```C#
    interface IAdsvel { }
```

## Файлы

* ### Имена файлов пишутся в **PascalCase**.

**~~myFile.cs~~**
**MyFile.cs**

* ### Рекомендуется называть файлы по названию класса.

**~~MagicFileName.cs~~**
**ClassName.cs**

* ### В одном файле рекомендуется определять один класс.

```C#
    /*
        using ...
    */
    namespace Adsvel;
    class AdsvelWorker {
        /*
            source code.
        */
    }
    // logical conclusion.
```

# Организация

* ## Порядок написания модификаторов:

**public protected internal private new abstract virtual override sealed static readonly extern unsafe volatile async**

* ## Правила объявления пространств имён.

1. Директивы **using** должны указываться в самом верху файла.
2. Используемые пространства имён должны быть написаны в алфавитном порядке.
3. Но **System**, может нарушать алфавитный порядок, она должна идти первой.

* ## Последовательность членов класса.

1. Вложенные классы, перечисления, делегаты и события.
2. Поля статические, константные и только для чтения.
3. Поля и свойства.
4. Конструкторы и финализаторы.
5. Методы.

* ## Последовательности элементов внутри групп.

1. Public.
2. Internal.
3. Protected internal.
4. Protected.
5. Private.

* ## Члены, реализованного интерфейса лучше группировать вместе.

## Правила организации строк.

* ### В одной строке одно объявление.
* ### В одной строке одно присваивание.
* ### Вместо табуляции в новых строках ставится 2 пробела.

```C#
    /*
    default C# variant.
    public class Adsvel {
        public readonly string Name;
    }
    */

    // Style guide variant.
    public class Adsvel {
      public readonly string Name;
    }
```

* ### Не более 160 символов в одной строке.
* ### Перед, открывающей фигурной скобкой нет переноса строки.
* ### После, закрывающей фигурной скобки перед **else** нет разрыва строки.
* ### Фигурные скобки используются даже, если есть возможность написать без них.
* ### Пробелы ставятся после **if** / **for** / **while** и тд. Также пробелы ставятся  после запятых, если они в одной строке.
* ### После открывающей круглой скобки нет пробела, также нет пробела перед закрывающей.
* ### Нет пробелов перед унарными операторами.
* ### Перенос строк при разбиении одной строки на несколько:

1. Как правило продолжение строки отделяется табуляцией.

```C#
    public static VeryLongTypeNameWhichTooLongToWriteInOneLine VeryLongFooNameDontReadAll(
        int a, int b, int c, int etc) { }
```

2. Разрывы строк в фигурных скобках (например, инициализаторы списков, лямбда-выражения, инициализаторы объектов и тд.) не считаются продолжением строки.

```C#
    int[] array = { 
      1, 2, 3
    };

    SerialPort serialPort = new(portName) {
      Baudrate = baudrate,
      Parity = parity
    };
```

3. Если параметры метода или аргументы при его вызове не помещаются в одну строку, можно сделать перенос строки с выравниванием параметров/аргументов новой строки по первому аргументу/параметру.

```C#
 public NewLongTypeNameWhichTooLongToWriteInOneLine NewLongFooNameDontReadAll(int a, int b,
                                                                              int c, int d,
                                                                              int e) { }
```

# Рекомендации по написанию кода

## Константы

* Переменные и поля, которые можно сделать константными, всегда следует делать константными.
* Если невозможно сделать поле константным, альтернативой может быть модификатор только для чтения.
* Константы обязательно использовать вместо магических чисел.

## **IEnumerable** vs **IList** vs **IReadOnlyList**

* Входные данные следует получать в более строгом типе как **IEnumerable**, **IReadOnlyList**.
* Для передачи права собственности выходных данных при выборе типа следует выбирать **IList**.

## Generators vs containers

Use your best judgement, bearing in mind:

* Generator code is often less readable than filling in a container.
* Generator code can be more performant if the results are going to be processed lazily, e.g. when not all the results are needed.
* Generator code that is directly turned into a container via ToList() will be less performant than filling in a container directly.
* Generator code that is called multiple times will be considerably slower than iterating over a container multiple times.

## Свойства и поля.

* Если свойство одностройчное и только для чтения, то следует использовать лямбду.

```C#
    public int Remaining => TotalCount - Completed;
```

* В любом другом случае использутеся стандартный вариант.

```C#
    public int TotalCount {
      get {
        return _totalCount;
      }
      set {
        if(value > 0) {
          _totalCount = value;
        } else {
          _totalCount = 0;
        }
      }
    }
```

* Для членов с модификатором доступа public, protected, internal следует использовать свойства.
* Для private следует использовать поля.

```C#
  public int Id { get; set; }
  protected string FirstName { get; set; }
  internal string LastName { get; set; }
  
  private int age_;
```

## Структуры и классы:

### Структуры отличаются от классов:

* При передаче стуктуры в качестве аргумента, или если структура является возвращаемым типом, передается скопированное значение объекта, а не указатель, как в случае с классами.
* Преимущественно следует использовать классы.
* Структуры следует использовать в качестве вспомогательного типа, суть которого в представлении значения. Например Vector3, Point и Bounds.

## Рекомендация по использованию лямбд

* Если лямда нетривиальна или используется в нескольких местах, имеет смысл использовать вместо лямбды именованный метод.

## Инициализаторы полей

* Инициализаторы полей приветствуются.

```C#
  // Bad variant.
  SerialPort serialPort = new(portName);
  serialPort.Baudrate = baudrate;
  serialPort.Parity = parity;

  // Good variant.
  SerialPort serialPort = new(portName) {
    Baudrate = baudrate,
    Parity = parity
  };
```

## Рекомендации по использованию методов расширения

* Использование методов расширения допускается, если исходный код класса неизвестен, или если нет возможности изменять источник.
* Разрешается использовать методы расширения, тольео если встраиваемая функциональность в контексте будет считаться важной и необходимой.
* Рекомендуется использовать методы расширения только в стандартной библиотеке.
* Следует помнить, что расширения всегда запутывают код.

## **ref** и **out**

* Use **out** for returns that are not also inputs. !!!
* Параметры **out** следуют после всех других параметров в определении метода.
* **ref** следует использовать как можно реже.
* Не стоит использовать **ref** для передачи ссылки на структуры.
* Нет необходимости в использовании **ref** для изменения списка объектов коллекции, **ref** следует использовать только если создается новый экземпляр коллекции.

## LINQ

* **Linq** следует использовать в виде однострочных выражений, длинный цепочки **Linq** ухудшают читаемость кода.
* При наличии методов расширения следует использовать их, а не **Linq** в формате SQL.

```C#
    // Bad variant.
    myList where x ...
    // Good variant.
    myList.Where(x) ...
```

* Избегайте длинных конструкция **Container.ForEach()**. Рекомундуется не более одного оператора.

## Array vs List

* Для общедоступных значений рекомендуется использовать **List\<\>**.
* Рекомендуется использовать **List\<\>**, когда длина может быть переменной.
* Если длина коллекции постоянна и заранее известна используйте **Array**.
* При работе с многомерностью лучше использовать **Array**

## Папки и расположения файлов.

* Расположение и название файлов дожны быть логичными.
* Не рекомендуется использование большого количества вложений в каталогах.

## Использование **turple** как тип, возвращаемого значения.

* Использование **turple** допускается, но лучше использовать именованный класс.

## **Интерполяция** vs **String.Format()** vs **String.Concat** vs **+**

* Как правило следует использовать то, что в контексте будет более читаемым.
* Следует помнить, что **+** медленнее и требует больше памяти.
* Для увеличения производительности можно использовать **StringBuilder** вместо оператора сложения.

# **using**.

* Лучше не использовать **using** с длинными названиями типов.
* Не рекомендуется использовать **using** для работы с длинными **Turple\<\>**, лучше вместо этого создать класс.
* Следует помнить, что область видимости **using** ограничена в пределах одного файла, внешние пользователи не имеют доступа к внутренним **using**.

## Синтаксис инициализации объекта.

Пример:

```C#
  var x = new SomeClass {
    Property1 = value1,
    Property2 = value2,
  };
```

* Инициализпторы
* Следует избегать данного синтаксиса, при использовании конструктора, созданного не по умолчанию.
* Все строки находятся на уровне одного блока.

## Правила наименования пространства имён.

* Пространства имён не следует делать больше 2 в глубину.
* Не обязательно соотвествие между компоновкой файла/папки и пространством имён.
* Для библиотек и модульного кода необходимо использовать пространства имён. Для простого немоудльного кода пространства имен можно не использовать.
* Простанства имен верхнего уровня должны быть уникальными и узнаваемые. Во вложениях допустимы неуникальные пространства имён.

```C#
  using Grpc.Core.Utils;
```

**Grpc** - уникальное название пространства имён на верхнем уровне.

**Core** и **Utils** - вложенные пространства имён, название которых могут быть неуникальными.

## Правила использования структур, когда возвращамое значение может быть, а может и не быть.

* Рекомендуется использовать возвращаемое значение типа **bool**, которое будет сигнализировать об успехе или наоборот о неудачи выполнения, а само значение возвращать через **out** параметр.
* Если требования к оптимизации производительности низкие, то **nullable** структуры можно использовать в качестве возвращаемого значения для улучшения читаемости кода(например просто возвращать значение null, вместо использования большого количества операторов if).

## Removing from containers while iterating

C# (like many other languages) does not provide an obvious mechanism for removing items from containers while iterating. There are a couple of options:

* If all that is required is to remove items that satisfy some condition, someList.RemoveAll(somePredicate) is recommended.
* If other work needs to be done in the iteration, RemoveAll may not be sufficient. A common alternative pattern is to create a new container outside of the loop, insert items to keep in the new container, and swap the original container with the new one at the end of iteration.

## Calling delegates

* When calling a delegate, use Invoke() and use the null conditional operator - e.g. SomeDelegate?.Invoke(). This clearly marks the call at the callsite as ‘a delegate that is being called’. The null check is concise and robust against threading race conditions.

## **var**.

* Использование **var** приветствуется для улучшения читаемости кода.
* Рекомендуемое использование:
* * Если тип, замещаемый **var** очевиден

```C#
    var random = new Random();
```

* * Для элементов, которые вскоре передаются напрямую в другие методы.

```C#
    var item = GetItem();
    ProcessItem(item);
```

* **Не** рекомендуется:
* * При работе с базовыми типами;
* * When working with compiler-resolved built-in numeric types - e.g. var number = 12 \* ReturnsFloat(); !!!
* * Если получаемый тип неочевиден.

```C#
    var listOfItems = GetList();
```

## Аннотация.

* Аннотацию следует писать над членом класса, а сам член в новой строке.

```C#
    [Key]
    public int Id {get; set;}
```

* Если атрибутов несколько, каждый пишется в отдельной строке.

```C#
    [Key]
    [Column("id")]
    public int Id {get; set;}
```

## Наименование аргументов.

* При использовании константных литералов рекомендуется объявлять вместо них именованную константу, если производится использование одного и того же значения из разных методов.
* Рекомендуется использовать перечисление вместо **bool**, чтобы сигнатура метода давала больше информации.
* Большие и сложные вложенные выражения рекомендуется заменить именованными переменными.
* Необходимо помнить про возможноть указывать названия аргументов при вызове метода для читаемости.

```C#
    internal int Substract(int minuend, int subtrahend){
        return minuend - subtrahend;
    }
    // Worse variant.
    int difference = Substract(3, 1);
    // Better variant.
    int difference = Substract(minuend: 3, subtrahend: 1);
```

* При большом количестве аргументов, необходимых для конфигурации, лучше создать класс и использовать его в качестве аргумента.

```C#
// Bad - what are these arguments?
DecimalNumber product = CalculateProduct(values, 7, false, null);
```

versus:

```C#
// Good
ProductOptions options = new ProductOptions() {
  PrecisionDecimals = 7,
  UseCache = CacheUsage.DontUseCache
}
DecimalNumber product = CalculateProduct(values, options, completionDelegate: null);
```
