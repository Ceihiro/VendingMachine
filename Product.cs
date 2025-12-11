namespace VendingMachine
{
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string State { get; set; } = string.Empty;

        public Product(string name, decimal price, int initialStock)
        {
            Name = name;
            Price = price;
            Stock = initialStock;
            UpdateState();
        }

        public bool IsAvailable => Stock > 0;

        public void Dispense()
        {
            if (Stock > 0)
            {
                Stock--;
                UpdateState();
            }
        }

        public void UpdateState()
        {
            State = Stock > 0 ? "In Stock" : "Sold Out";
        }

        public void Restock(int amount)
        {
            Stock += amount;
            UpdateState();
        }
    }
}
