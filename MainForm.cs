// ============================================================
// This class represents the main Windows Form for a simple vending machine.
// It handles:
// 1. Product selection (Chips and Water)
// 2. Inserting money
// 3. Dispensing products
// 4. Updating display and stock
// 5. Drawing a Moore Machine diagram that reflects the machine's state
// ============================================================

namespace VendingMachine
{
    public partial class MainForm : Form
    {
        // Controls
        private MooreMachine machine = null!;      // The underlying Moore machine logic
        private Product chips = null!;             // Chips product
        private Product water = null!;             // Water product
        private Product? selectedProduct;          // Currently selected product

        private Label lblOutputTray = null!;       // Label for the output tray
        private Button btnInsertMoney = null!;     // Button to insert money

        private const int MaxStock = 10;           // Maximum stock for any product

        // Constructor
        public MainForm()
        {
            InitializeComponent();     // Build UI
            InitializeMachine();       // Initialize products and machine
            AddInsertMoneyButton();    // Add "Insert Money" button to machine panel
            AddOutputTray();           // Add output tray label
            UpdateDisplay();           // Initial display update
        }

        // Intialize the Moore machine and products
        private void InitializeMachine()
        {
            machine = new MooreMachine();
            chips = new Product("Chips", 20m, 5);
            water = new Product("Water", 15m, 5);
        }

        // UI: Insert Money Button
        private void AddInsertMoneyButton()
        {
            Size buttonSize = new Size(320, 50);

            btnInsertMoney = new Button
            {
                Location = new Point(20, 510),
                Size = buttonSize,
                Text = "💰 Insert Money",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                BackColor = Color.FromArgb(39, 174, 96),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Enabled = false
            };
            btnInsertMoney.FlatAppearance.BorderSize = 0;
            btnInsertMoney.Click += BtnInsertMoney_Click;
            machinePanel.Controls.Add(btnInsertMoney);
        }

        // UI: Output Tray Label
        private void AddOutputTray()
        {
            lblOutputTray = new Label
            {
                Location = new Point(20, 570),
                Size = new Size(320, 50),
                BackColor = Color.FromArgb(44, 62, 80),
                ForeColor = Color.White,
                Font = new Font("Consolas", 14, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "Output Tray: Empty"
            };
            machinePanel.Controls.Add(lblOutputTray);
        }

        // Product Selection Events
        private void BtnChips_Click(object? sender, EventArgs e)
        {
            if (!chips.IsAvailable)
            {
                lblOutput.Text = "CHIPS - Sold Out!";
                btnChips.BackColor = Color.FromArgb(192, 57, 43);
                return;
            }
            SelectProduct(chips);
        }

        private void BtnWater_Click(object? sender, EventArgs e)
        {
            if (!water.IsAvailable)
            {
                lblOutput.Text = "WATER - Sold Out!";
                btnWater.BackColor = Color.FromArgb(192, 57, 43);
                return;
            }
            SelectProduct(water);
        }

        // Select product logic
        private void SelectProduct(Product product)
        {
            if (product == null || !product.IsAvailable)
            {
                MessageBox.Show($"{product?.Name ?? "Product"} is sold out!",
                    "Product Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            selectedProduct = product;
            machine.SelectProduct(product);
            lblOutputTray.Text = "Output Tray: Empty";
            btnInsertMoney.Enabled = true;

            UpdateDisplay();
            diagramPanel.Invalidate(); // Refresh Moore diagram
        }

        // Insert Money Event
        private void BtnInsertMoney_Click(object? sender, EventArgs e)
        {
            if (selectedProduct == null)
            {
                MessageBox.Show("Please select a product first!", "Insert Money",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnInsertMoney.Enabled = false;
                return;
            }

            machine.InsertMoney();
            UpdateDisplay();
            diagramPanel.Invalidate();

            // Dispense product automatically after payment
            if (selectedProduct != null)
                DispenseSelectedProduct();
        }

        // Dispense the selected product
        private async void DispenseSelectedProduct()
        {
            if (selectedProduct == null) return;

            var productToDispense = selectedProduct;

            await Task.Delay(500);
            machine.SetDispensing(productToDispense);
            UpdateDisplay();
            diagramPanel.Invalidate();

            await Task.Delay(800);
            machine.CompleteTransaction(productToDispense);

            lblOutputTray.Text = $"Output Tray: {productToDispense.Name}";

            selectedProduct = null;
            btnInsertMoney.Enabled = false;

            UpdateDisplay();
            diagramPanel.Invalidate();
        }

        // Update the display labels and button states
        private void UpdateDisplay()
        {
            lblOutput.Text = machine.Output;

            lblChipsStock.Text = $"Stock: {chips.Stock}";
            lblWaterStock.Text = $"Stock: {water.Stock}";

            btnChips.Enabled = true;
            btnWater.Enabled = true;

            btnChips.BackColor = chips.IsAvailable ? Color.FromArgb(52, 152, 219) : Color.FromArgb(192, 57, 43);
            btnWater.BackColor = water.IsAvailable ? Color.FromArgb(52, 152, 219) : Color.FromArgb(192, 57, 43);
        }

        // Restock Button Event
        private void BtnRestock_Click(object? sender, EventArgs e)
        {
            int chipsRestock = Math.Min(5, MaxStock - chips.Stock);
            int waterRestock = Math.Min(5, MaxStock - water.Stock);

            chips.Restock(chipsRestock);
            water.Restock(waterRestock);

            machine.Reset();
            selectedProduct = null;
            btnInsertMoney.Enabled = false;
            lblOutputTray.Text = "Output Tray: Empty";

            UpdateDisplay();
            diagramPanel.Invalidate();

            MessageBox.Show("Products restocked successfully!", "Restock",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Moore Diagram Paint Event
        private void DiagramPanel_Paint(object? sender, PaintEventArgs e)
        {
            DrawMooreDiagram(e.Graphics);
        }

        // Draw the Moore machine diagram
        private void DrawMooreDiagram(Graphics g)
        {
            // ---------------------------
            // This draws a Moore machine diagram for the vending machine
            // States: Idle, Selected, PaymentReceived, Dispensing
            // Arrows show transitions: Select → InsertMoney → Dispense → Complete → Idle
            // Active state highlighted in green
            // Sold-out indicators shown on Idle if needed
            // ---------------------------
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            int yOffset = 80;

            var states = new Dictionary<string, Point>
            {
                { "Idle", new Point(100, 50 + yOffset) },
                { "Selected", new Point(300, 50 + yOffset) },
                { "PaymentReceived", new Point(300, 150 + yOffset) },
                { "Dispensing", new Point(100, 150 + yOffset) }
            };

            int circleDiameter = 90;
            int radius = circleDiameter / 2;

            using (Font font = new Font("Segoe UI", 8, FontStyle.Bold))
            using (Font smallFont = new Font("Segoe UI", 8))
            {
                foreach (var state in states)
                {
                    bool isActive = machine.CurrentState == state.Key;

                    Color stateColor = state.Key == "Idle" && (chips.Stock == 0 || water.Stock == 0)
                        ? Color.Gray
                        : isActive ? Color.FromArgb(46, 204, 113) : Color.FromArgb(52, 152, 219);

                    Color borderColor = isActive ? Color.FromArgb(39, 174, 96) : Color.FromArgb(41, 128, 185);

                    using (Brush brush = new SolidBrush(stateColor))
                    using (Pen pen = new Pen(borderColor, isActive ? 4 : 2))
                    {
                        g.FillEllipse(brush, state.Value.X - radius, state.Value.Y - radius, circleDiameter, circleDiameter);
                        g.DrawEllipse(pen, state.Value.X - radius, state.Value.Y - radius, circleDiameter, circleDiameter);
                    }

                    using (Brush textBrush = new SolidBrush(Color.White))
                    {
                        string label = state.Key;
                        if (state.Key == "Idle")
                        {
                            if (chips.Stock == 0 && water.Stock == 0) label = "Idle (All Sold Out)";
                            else if (chips.Stock == 0) label = "Idle (Chips Sold Out)";
                            else if (water.Stock == 0) label = "Idle (Water Sold Out)";
                        }

                        SizeF textSize = g.MeasureString(label, font);
                        g.DrawString(label, font, textBrush, state.Value.X - textSize.Width / 2, state.Value.Y - textSize.Height / 2);
                    }
                }

                using (Pen arrowPen = new Pen(Color.FromArgb(52, 73, 94), 2))
                {
                    arrowPen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;

                    // Main transitions
                    g.DrawLine(arrowPen, states["Idle"].X + radius, states["Idle"].Y, states["Selected"].X - radius, states["Selected"].Y);
                    g.DrawLine(arrowPen, states["Selected"].X, states["Selected"].Y + radius, states["PaymentReceived"].X, states["PaymentReceived"].Y - radius);
                    g.DrawLine(arrowPen, states["PaymentReceived"].X - radius, states["PaymentReceived"].Y, states["Dispensing"].X + radius, states["Dispensing"].Y);
                    g.DrawLine(arrowPen, states["Dispensing"].X, states["Dispensing"].Y - radius, states["Idle"].X, states["Idle"].Y + radius);

                    // Restock arrow
                    Point idle = states["Idle"];
                    Point arrowStart = new Point(idle.X, idle.Y - radius - 20);
                    Point arrowEnd = new Point(idle.X, idle.Y - radius);
                    g.DrawLine(arrowPen, arrowStart, arrowEnd);

                    using (Brush labelBrush = new SolidBrush(Color.FromArgb(52, 73, 94)))
                    {
                        g.DrawString("Select", smallFont, labelBrush, 190, 30 + yOffset);
                        g.DrawString("InsertMoney", smallFont, labelBrush, 320, 92 + yOffset);
                        g.DrawString("Dispense", smallFont, labelBrush, 190, 130 + yOffset);
                        g.DrawString("Complete", smallFont, labelBrush, 20, 92 + yOffset);
                        g.DrawString("Restock", smallFont, labelBrush, idle.X - 20, idle.Y - radius - 40);
                    }
                }
            }
        }
    }
}
