namespace ProductsList;

public class ProductDTO
{
    public ProductDTO(Guid id, string name, int price, string imagePath)
    {
        Id = id;
        Name = name;
        Price = price;
        ImagePath = imagePath;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public string ImagePath { get; set; }


}
