public class Item
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public Guid SectionId { get; set; }
    public Section? Section { get; set; }
}