namespace webapi_video3.Models
{
    public class Product
    {
        public int  Id { get; set; } // por defecto la toma como una llave primaria

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }  

        public DateTime Date { get; set; }

        public bool Active { get; set; }

    }
}