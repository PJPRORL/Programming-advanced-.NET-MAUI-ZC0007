namespace Publishers.Models;

public class Store
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }

    public override string ToString()
    {
        return $"{Name} ({Address}, {City} {Zip})";
    }
}