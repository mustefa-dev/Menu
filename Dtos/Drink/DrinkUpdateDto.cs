namespace Menu.Dtos.Drink;

public class DrinkUpdateDto{
    public string Name { get; set; }
    public int SectionId { get; set; }
    public IFormFile Photo { get; set; }
}