namespace DbUpgrader.Definition
{
    public interface IField
    {
        string Name { get; }
        FieldType Type { get; }
        int Size { get; }
        bool AllowNulls { get; }
    }
}
