using Menu.Models;

namespace Menu.Dtos.Category;

public class CategoryCreateDto{
    public string Name { get; set; }
    public CategoryType Type { get; set; }
}

public class CategoryUpdateDto{
    public string Name { get; set; }
    public CategoryType Type { get; set; }
}

public class CategoryReadDto{
    public int Id { get; set; }
    public string Name { get; set; }
    public CategoryType Type { get; set; }
}