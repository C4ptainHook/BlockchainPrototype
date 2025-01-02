namespace Blockchain.Data.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class RepositoryAttribute : Attribute
{
    public string Name { get; }

    public RepositoryAttribute(string name)
    {
        Name = name;
    }
}
