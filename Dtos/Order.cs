namespace Menu.Dtos.Order
{
    public class OrderReadDto
    {
        public Guid? Id { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SectionId { get; set; }
        public int TableNumber { get; set; }
        public List<ItemReadDto> Items { get; set; }
    }

    public class OrderCreateDto
    {
        public Guid CategoryId { get; set; }
        public Guid SectionId { get; set; }
        public int TableNumber { get; set; }
        public List<Guid> ItemIds { get; set; }
    }

    public class OrderUpdateDto
    {
        public int TableNumber { get; set; }
        public List<Guid> ItemIds { get; set; }
    }
}