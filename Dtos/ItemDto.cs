namespace Auth.Dtos.Item
{
    public class ItemCreateDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public IFormFile Photo { get; set; }
    }

    public class ItemReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Photo { get; set; }
    }
}
