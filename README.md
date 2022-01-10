[![Website](https://img.shields.io/website-up-down-green-red/http/shields.io.svg?label=elky-essay)](https://elky84.github.io)
![Made with](https://img.shields.io/badge/made%20with-.NET6-blue.svg)

![GitHub forks](https://img.shields.io/github/forks/elky84/JsonTable.svg?style=social&label=Fork)
![GitHub stars](https://img.shields.io/github/stars/elky84/JsonTable.svg?style=social&label=Stars)
![GitHub watchers](https://img.shields.io/github/watchers/elky84/JsonTable.svg?style=social&label=Watch)
![GitHub followers](https://img.shields.io/github/followers/elky84.svg?style=social&label=Follow)

![GitHub](https://img.shields.io/github/license/mashape/apistatus.svg)
![GitHub repo size in bytes](https://img.shields.io/github/repo-size/elky84/JsonTable.svg)
![GitHub code size in bytes](https://img.shields.io/github/languages/code-size/elky84/JsonTable.svg)

# JsonTable

## introduce

Easily usable Json to Table (dictionary and mores).

Use Newtonsoft.json.

## usage

### definition

```csharp
[Table(@"json")]
public partial class TableItem : BaseDict<string, Item>
{
}
```

### load

```csharp
MasterTable.Load("ItemGenerator.Table");
```

### use

```csharp
var data = MasterTable.From<TableItem>();
```

```csharp
MasterTable.From<TableItem>().FirstOrDefault(x => x.Id == id);
```

## Support Types

* BaseList
  * `List<T>`
* BaseDict
  * `Dictionary<K, T>`
* BaseMultiDict
  * `Dictionary<K, List<T>>`
  * Similar C++ stl Multimap
    * Example
    ```csharp
    [Table(@"json")]
    public partial class TableItemByType : BaseMultiDict<ItemType, Item>
    {
        public TableItemByType() : base("Type") // input key property name
        {
        }
    }
    ```

## Reference

[Cli](https://github.com/elky84/JsonTable/Cli/Program.cs)

## Version History

### v1.0.6

Exception process reinforcement.

### v1.0.5
Features added MultiDict. (like c++ stl multimap)

### v1.0.4
Supports OnLoad() function to set configuration rules when container is loaded.

### v1.0.3
Tables now only require a folder path. (The full path is not required.) 

### v1.0.1
removed nullable property on container (BaseDict and BaseList).