namespace VendingMachine
{
    public class MooreMachine
    {
        public string CurrentState { get; private set; }
        public string Output { get; private set; }

        public MooreMachine()
        {
            CurrentState = "Idle";
            Output = "Select a Product";
        }

        public void SelectProduct(Product product)
        {
            if (!product.IsAvailable)
            {
                CurrentState = "Error";
                Output = $"{product.Name} - Sold Out!";
                return;
            }

            CurrentState = "Selected";
            Output = $"{product.Name} Selected - ₱{product.Price}";
        }

        public void InsertMoney()
        {
            if (CurrentState != "Selected") return;
            CurrentState = "PaymentReceived";
            Output = "Payment Accepted";
        }

        public void SetDispensing(Product product)
        {
            if (CurrentState != "PaymentReceived") return;
            CurrentState = "Dispensing";
            Output = $"Dispensing {product.Name}...";
        }

        public void CompleteTransaction(Product product)
        {
            product.Dispense(); // reduce stock
            CurrentState = "Idle";
            Output = product.IsAvailable
                ? "Transaction Complete"
                : $"{product.Name} - Sold Out!";
        }

        public void Reset()
        {
            CurrentState = "Idle";
            Output = "Ready";
        }
    }
}
