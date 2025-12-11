# Simple Vending Machine - Moore Machine Simulation

A Windows Forms application simulating a simple vending machine with product availability, modeled as a Moore machine. Tracks inventory states and displays availability to inform users before selection.

## Features

- **Moore Machine Simulation**: Finite state machine where states represent inventory levels and outputs depend solely on the current state
- **Product Availability Display**: Dynamically shows status based on state transitions
- **Interactive Vending Interface**: Select products, simulate purchases, and see real-time updates
- **State Diagram Visualization**: Visual representation with real-time highlighting during state transitions
- **Error Handling**: Alerts for out-of-stock items and prevents invalid selections
- **Philippine Peso Currency**: Prices displayed in PHP

## Products

- **Chips** - ₱20.00 (Initial stock: 5)
- **Water** - ₱15.00 (Initial stock: 5)

## How to Run

### Prerequisites
- Visual Studio 2022
- .NET 8.0 SDK or later
- Windows OS

### Steps

1. Open Visual Studio 2022
2. Click "Open a project or solution"
3. Navigate to and select `VendingMachine.csproj`
4. Press **F5** or click the "Start" button to run the application

## Usage

1. **Launch the app** - The vending machine starts in "Idle" state
2. **View the Moore diagram** - Watch the state diagram on the right side
3. **Browse products** - See product prices and availability on the left
4. **Click a product button** - The machine will:
   - Highlight the current state in the diagram
   - Show state transitions (Idle → Selected → PaymentReceived → Dispensing → Idle)
   - Dispense the product if available
   - Show "Sold Out" message if unavailable
5. **Restock products** - Click the "RESTOCK ALL" button to replenish inventory

## Moore Machine States

- **Idle**: Waiting for product selection
- **Selected**: Product chosen, awaiting payment
- **PaymentReceived**: Payment accepted, ready to dispense
- **Dispensing**: Product being dispensed
- **Error**: Invalid action or out-of-stock

## Project Structure

```
VendingMachine/
├── VendingMachine.csproj     # Project file
├── Program.cs                # Application entry point
├── MainForm.cs               # Main form logic
├── MainForm.Designer.cs      # UI designer code
├── Product.cs                # Product model
└── MooreMachine.cs           # State machine implementation
```

## License

Educational project for learning finite state machines and Windows Forms development.