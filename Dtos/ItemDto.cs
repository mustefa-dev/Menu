namespace Menu.Dtos
{
    namespace Menu.Dtos
    {
        public class ItemDto
        {
            public int Id { get; set; }
            public int Price { get; set; }
            public string? Name { get; set; }
            public IFormFile? Photo { get; set; } // Add a property for the photo file
        }
    }

}