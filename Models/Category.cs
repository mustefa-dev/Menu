namespace Menu.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public CategoryType Type { get; set; }
}

public enum CategoryType
{
    Drink,
    Food,
    Other
}