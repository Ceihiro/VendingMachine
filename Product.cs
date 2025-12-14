// Store product information (name, price, stock)
// Track availability status
// Handle stock reduction (dispensing)
// Handle stock increase (restocking)
// Automatically update state based on stock level

namespace VendingMachine
{
    public class Product
    {
        // PROPERTIES

        // Product name displayed to user
        public string Name { get; set; }

        // Price in Philippine Pesos (₱)
        public decimal Price { get; set; }

        // Current stock quantity (0 = sold out)
        public int Stock { get; set; }

        // State text: "In Stock" or "Sold Out"
        public string State { get; set; } = string.Empty;

        // AVAILABILITY CHECK
        // Returns true if product can be purchased (stock > 0)
        public bool IsAvailable => Stock > 0;

        // CONSTRUCTOR
        // Creates a new product with specified name, price, and initial stock
        public Product(string name, decimal price, int initialStock)
        {
            Name = name;
            Price = price;
            Stock = initialStock;
            UpdateState(); 
        }

        // DISPENSE PRODUCT
        // Reduces stock by 1 when product is dispensed
        public void Dispense()
        {
            if (Stock > 0)
            {
                Stock--;
                UpdateState(); 
            }
        }

        // UPDATE STATE TEXT
        // Called automatically after dispensing or restocking
        public void UpdateState()
        {
            State = Stock > 0 ? "In Stock" : "Sold Out";
        }

        // RESTOCK PRODUCT
        // Automatically updates state after restocking
        public void Restock(int amount)
        {
            Stock += amount;
            UpdateState();
        }
    }
}
