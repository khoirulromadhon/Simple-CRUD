namespace Simple_CRUD.ViewModels
{
    public class VMProduct
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Price { get; set; }
    }
}
