namespace ProductAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Descricao { get; set; }
        public decimal price { get; set; }
    }
}
