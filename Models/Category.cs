public class Category
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public List<Section> Sections { get; set; }
    public List<Order> Orders { get; set; }
}