public class Section
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    public List<Item> Items { get; set; }
}