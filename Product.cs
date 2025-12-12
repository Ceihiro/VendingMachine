// ==============================================================================
// Represents a product in the vending machine (name, price, stock, and state).
// Handles checking availability, dispensing, and restocking.
// ==============================================================================

namespace VendingMachine
{
    public class Product
    {
        // Product name
        public string Name { get; set; }

        // Product price
        public decimal Price { get; set; }

        // How many items left
        public int Stock { get; set; }

        // Shows "In Stock" or "Sold Out"
        public string State { get; set; } = string.Empty;

        // Creates a new product with name, price, and starting stock
        public Product(string name, decimal price, int initialStock)
        {
            Name = name;
            Price = price;
            Stock = initialStock;
            UpdateState(); 
        }

        // Returns true if item is still available
        public bool IsAvailable => Stock > 0;

        // Reduces stock by 1 when dispensed
        public void Dispense()
        {
            if (Stock > 0)
            {
                Stock--;
                UpdateState(); 
            }
        }

        // Updates the state text based on stock
        public void UpdateState()
        {
            State = Stock > 0 ? "In Stock" : "Sold Out";
        }

        // Adds more stock
        public void Restock(int amount)
        {
            Stock += amount;
            UpdateState();
        }
    }
}
