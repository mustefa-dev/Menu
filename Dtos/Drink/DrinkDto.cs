namespace Menu.Dtos.Drink{
    namespace Menu.Dtos.Drink{
        public class DrinkDto{
            public int Id { get; set; }
            public string Name { get; set; }
            public int SectionId { get; set; }
            public string SectionName { get; set; }
            public decimal Price { get; set; }
            public string Description { get; set; }
            public string Photo { get; set; }
        }
    }


    namespace Menu.Dtos.Drink{
        public class DrinkCreateDto{
            public string Name { get; set; }
            public string SectionName { get; set; }
            public decimal Price { get; set; }
            public string Description { get; set; }
            public IFormFile Photo { get; set; }
        }
    }


    public class DrinkUpdateDto{
        public string Name { get; set; }
        public int SectionId { get; set; }
        public IFormFile Photo { get; set; }
    }
}