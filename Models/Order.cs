public class Order
{
    public Guid? Id { get; set; }
    public Guid CategoryId { get; set; }
    public Guid SectionId { get; set; }
    public int TableNumber { get; set; }
    public List<Item> Items { get; set; }
}