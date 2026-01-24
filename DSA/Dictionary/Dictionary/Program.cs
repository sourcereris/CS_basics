// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

Dictionary<MyStruct, int> obj = new();

MyStruct key1 = new MyStruct
{
    Name = "Example",
    Description = "This is an example struct"
};
MyStruct key2 = new MyStruct
{
    Name = "Ex1",
    Description = "This is an example struct"
};

AddToDictionary(key1, 3);

obj.TryGetValue(key2, out int value);
int val = value;
Console.WriteLine($"Value retrieved: {val}");

void AddToDictionary(MyStruct key, int value)
{
    obj[key] = value;
}

struct MyStruct : IEquatable<MyStruct>
{
    public string Name { get; set; }
    public string Description;

    public bool Equals(MyStruct other)
    {
        return Name == other.Name && Description == other.Description;
    }
}