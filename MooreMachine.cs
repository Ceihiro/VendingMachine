// Moore machine, outputs depend ONLY on the current state, not on inputs.
// Each state produces a specific output when entered.

namespace VendingMachine
{
    public class MooreMachine
    {
        // PROPERTIES

        // Current state of the machine [Idle, Selected, PaymentReceived, Dispensing]
        public string CurrentState { get; private set; }

        // Output message [Moore: depends only on state]
        public string Output { get; private set; }

        // Total amount of money inserted so far
        public decimal InsertedAmount { get; private set; } = 0m;

        // Coin ₱5 only
        public const decimal CoinValue = 5m;

        // Number of coins inserted
        public int CoinsInserted => (int)(InsertedAmount / CoinValue);

        // Calculate how many coins are needed for a product
        public int CoinsRequired(Product product) => (int)Math.Ceiling(product.Price / CoinValue);

        // CONSTRUCTOR
        // Initializes machine in Idle state
        public MooreMachine()
        {
            CurrentState = "Idle";
            Output = "Select a Product";
        }

        // STATE TRANSITION: IDLE → SELECTED
        // Triggered when user selects a product
        // Validates product availability before transitioning
        // Returns inserted money if user changes selection
        public decimal SelectProduct(Product? product)
        {
            decimal refundAmount = 0m;

            // If user already had money inserted and changes selection, refund it
            if (InsertedAmount > 0)
            {
                refundAmount = InsertedAmount;
                InsertedAmount = 0m; // Reset payment for new transaction
            }

            if (product == null)
            {
                CurrentState = "Idle";
                Output = "No product selected";
                return refundAmount;
            }

            if (!product.IsAvailable)
            {
                CurrentState = "Idle";
                Output = $"{product.Name} - Sold Out!";
                return refundAmount;
            }

            // Transition to Selected state
            CurrentState = "Selected";
            Output = $"{product.Name} Selected - ₱{product.Price}";
            return refundAmount;
        }

        // STATE TRANSITION: SELECTED → PAYMENTRECEIVED [or stay in SELECTED]
        // Triggered when user inserts a ₱5 coin
        // Returns true if payment is complete, false if more money needed
        public bool InsertMoney(Product product)
        {
            // Can only insert money in Selected or PaymentReceived state
            if (CurrentState != "Selected" && CurrentState != "PaymentReceived")
                return false;

            InsertedAmount += CoinValue;

            // Check if payment is sufficient
            if (InsertedAmount >= product.Price)
            {
                // Transition to PaymentReceived state
                CurrentState = "PaymentReceived";
                Output = "Payment Complete";
                return true; // Ready to dispense
            }
            else
            {
                // Stay in Selected state, show progress
                CurrentState = "Selected";
                Output = $"Inserted ₱{InsertedAmount} / ₱{product.Price}";
                return false; // Need more coins
            }
        }

        // CANCEL TRANSACTION
        // Allows user to cancel and get money back
        // Returns the amount to refund
        public decimal CancelTransaction()
        {
            decimal refundAmount = InsertedAmount;
            InsertedAmount = 0m;
            CurrentState = "Idle";
            Output = refundAmount > 0
                ? $"Transaction Cancelled - Refunded ₱{refundAmount}"
                : "Select a Product";
            return refundAmount;
        }

        // STATE TRANSITION: PAYMENTRECEIVED → DISPENSING
        // Triggered when dispensing process begins
        public void SetDispensing(Product? product)
        {
            if (product == null || CurrentState != "PaymentReceived")
                return;

            // Transition to Dispensing state
            CurrentState = "Dispensing";
            Output = $"Dispensing {product.Name}...";
        }

        // STATE TRANSITION: DISPENSING → IDLE
        // Triggered when transaction is complete
        // Reduces product stock and resets payment
        public void CompleteTransaction(Product? product)
        {
            if (product == null)
            {
                CurrentState = "Idle";
                Output = "No product to dispense";
                InsertedAmount = 0;
                return;
            }

            // Reduce product stock
            product.Dispense();

            // Reset payment
            InsertedAmount = 0;

            // Return to Idle state
            CurrentState = "Idle";
            Output = "Transaction Complete";
        }

        // RESET MACHINE [used after restock]
        public void Reset()
        {
            CurrentState = "Idle";
            Output = "Select a Product";
            InsertedAmount = 0;
        }
    }
}