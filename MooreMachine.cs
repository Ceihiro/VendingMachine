// ==================================================================================================
// Controls the vending machine logic using states (Idle, Selected, PaymentReceived, Dispensing).
// Handles product selection, payment, dispensing, and resetting.
// ==================================================================================================

namespace VendingMachine
{
    public class MooreMachine
    {
        // Shows the current state of the vending machine
        public string CurrentState { get; private set; }

        // Shows what the machine displays to the user
        public string Output { get; private set; }

        // Set the machine to default state when created
        public MooreMachine()
        {
            CurrentState = "Idle";
            Output = "Select a Product";
        }

        // When user selects a product
        public void SelectProduct(Product? product)
        {
            if (product == null)
            {
                CurrentState = "Idle";
                Output = "No product selected";
                return;
            }

            // If no stock
            if (!product.IsAvailable)
            {
                CurrentState = "Error";
                Output = $"{product.Name} - Sold Out!";
                return;
            }

            // Product is available
            CurrentState = "Selected";
            Output = $"{product.Name} Selected - ₱{product.Price}";
        }

        // When user inserts money
        public void InsertMoney()
        {
            // Only works if a product is selected
            if (CurrentState != "Selected") return;

            CurrentState = "PaymentReceived";
            Output = "Payment Accepted";
        }

        // Start dispensing process
        public void SetDispensing(Product? product)
        {
            if (product == null || CurrentState != "PaymentReceived") return;

            CurrentState = "Dispensing";
            Output = $"Dispensing {product.Name}...";
        }

        // Finish transaction and update stock
        public void CompleteTransaction(Product? product)
        {
            if (product == null)
            {
                CurrentState = "Idle";
                Output = "No product to dispense";
                return;
            }

            product.Dispense(); // reduce stock

            // Return machine to Idle
            CurrentState = "Idle";

            // Display correct message depending on stock
            Output = product.IsAvailable
                ? "Transaction Complete"
                : $"{product.Name} - Sold Out!";
        }

        // Bring machine back to Idle state
        public void Reset()
        {
            CurrentState = "Idle";
            Output = "Select a Product";
        }
    }
}
