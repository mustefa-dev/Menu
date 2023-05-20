namespace Menu.Dtos{
    public class OrderDto{
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int WaterId { get; set; }

        public DateTime OrderDate { get; set; }
        // Other order properties as needed

        public CustomerDto Customer { get; set; }
        public WaterDto Water { get; set; }
    }
}