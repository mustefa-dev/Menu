namespace Menu.Models;

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int WaterId { get; set; }
    public DateTime OrderDate { get; set; }
    // Other order properties as needed

    public Customer Customer { get; set; }
    public Water Water { get; set; }


}