// This represents the main Windows Form for a vending machine simulation.

using System.Net.Http;

namespace VendingMachine
{
    public partial class MainForm : Form
    {
        // CORE COMPONENTS
        private MooreMachine machine = null!;      // Moore machine state logic
        private Product chips = null!;             // Chips product instance
        private Product water = null!;             // Water product instance
        private Product? selectedProduct;          // Currently selected product [null if none]

        // UI CONTROLS 
        private Label lblOutputTray = null!;       // Bottom tray showing dispensed product
        private Button btnInsertMoney = null!;     // Button to insert ₱5 coin
        private Button btnCancel = null!;          // Button to cancel transaction

        // CONSTANTS
        private const int MaxStock = 5;            // Maximum stock capacity per product

        // VISUAL PRODUCT DISPLAY
        private PictureBox[,] productScreenSlots = new PictureBox[2, 5]; // 2 rows [Chips, Water] x 5 slots each
        private Image chipImage = null!;           // Loaded chip product image
        private Image waterImage = null!;          // Loaded water product image
        private static readonly HttpClient httpClient = new HttpClient(); // Reusable HTTP client for image loading

        // CONSTRUCTOR
        public MainForm()
        {
            InitializeComponent();                 // Build UI layout 
            InitializeMachine();                   // Setup Moore machine and products

            _ = InitializeProductScreenAsync();    // Load and display product images 

            AddCancelButton();                     // Create cancel button
            AddInsertMoneyButton();                // Create coin insertion button
            AddOutputTray();                       // Create bottom output tray
            UpdateDisplay();                       // Initialize display labels and states
        }

        // MACHINE INITIALIZATION
        // Creates Moore machine instance and initializes products with prices and stock
        private void InitializeMachine()
        {
            machine = new MooreMachine();
            chips = new Product("Chips", 20m, 5);  // ₱20, 5 in stock
            water = new Product("Water", 15m, 5);  // ₱15, 5 in stock
        }

        // IMAGE LOADING
        // Downloads an image from a URL and converts it to System.Drawing.Image
        // Includes timeout to prevent hanging
        private async Task<Image> LoadImageFromUrlAsync(string url)
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5)); // 5 second timeout
            var data = await httpClient.GetByteArrayAsync(url, cts.Token);
            using (var ms = new System.IO.MemoryStream(data))
            {
                return Image.FromStream(ms);
            }
        }

        // CREATE PLACEHOLDER IMAGE
        // Creates a simple colored rectangle with text when internet fails
        private Image CreatePlaceholderImage(string text, Color bgColor)
        {
            Bitmap bmp = new Bitmap(88, 68);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Fill background
                g.FillRectangle(new SolidBrush(bgColor), 0, 0, 88, 68);

                // Draw text
                using (Font font = new Font("Arial", 10, FontStyle.Bold))
                {
                    var stringFormat = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    g.DrawString(text, font, Brushes.White, new RectangleF(0, 0, 88, 68), stringFormat);
                }

                // Draw border
                g.DrawRectangle(Pens.White, 0, 0, 87, 67);
            }
            return bmp;
        }

        // PRODUCT SCREEN INITIALIZATION 
        // Creates a visual grid showing product images [5 slots per product row]
        // Top row: Chips, Bottom row: Water
        // Images are loaded asynchronously from URLs with error handling
        private async Task InitializeProductScreenAsync()
        {
            try
            {
                // Load product images from URLs with timeout
                chipImage = await LoadImageFromUrlAsync("https://ph-test-11.slatic.net/p/4fd81edf760932482a6a4da30e3972d2.jpg");
                waterImage = await LoadImageFromUrlAsync("https://corporativo.esperanza.mx/filemanager/9cbf228f-7eb7-446e-9344-851e0a4fa984.jpg");
            }
            catch (Exception ex)
            {
                // If internet fails, create placeholder images
                chipImage = CreatePlaceholderImage("CHIPS", Color.FromArgb(255, 193, 7));
                waterImage = CreatePlaceholderImage("WATER", Color.FromArgb(33, 150, 243));

                // Optional: Log the error [don't show popup to avoid annoying user]
                Console.WriteLine($"Failed to load images: {ex.Message}");
            }

            // Create container panel for product screen 
            Panel productScreenPanel = new Panel
            {
                Location = new Point(30, 65),
                Size = new Size(490, 155),
                BackColor = Color.Black,
                BorderStyle = BorderStyle.FixedSingle
            };
            machinePanel.Controls.Add(productScreenPanel);
            productScreenPanel.BringToFront();

            // Grid layout settings 
            int slotWidth = 88;
            int slotHeight = 68;
            int padding = 10;
            int startX = 12;
            int startY = 10;

            // Create 2x5 grid of product image slots
            for (int row = 0; row < 2; row++)      // 2 rows: Chips and Water
            {
                for (int col = 0; col < 5; col++)  // 5 slots per product
                {
                    PictureBox slot = new PictureBox
                    {
                        Size = new Size(slotWidth, slotHeight),
                        Location = new Point(startX + col * (slotWidth + padding), startY + row * (slotHeight + padding)),
                        SizeMode = PictureBoxSizeMode.Zoom,
                        BackColor = Color.Gray,
                        BorderStyle = BorderStyle.FixedSingle
                    };

                    // Assign image based on row [0=Chips, 1=Water]
                    slot.Image = row == 0 ? chipImage : waterImage;
                    productScreenPanel.Controls.Add(slot);
                    productScreenSlots[row, col] = slot;
                }
            }
        }

        // PRODUCT SCREEN UPDATE
        // Hides slots when products are sold out 
        private void RefreshProductScreen()
        {
            for (int col = 0; col < 5; col++)
            {
                productScreenSlots[0, col].Visible = col < chips.Stock; // Hide sold-out chip slots
                productScreenSlots[1, col].Visible = col < water.Stock; // Hide sold-out water slots
            }
        }

        // UI SETUP: CANCEL BUTTON
        // Creates the cancel button 
        private void AddCancelButton()
        {
            btnCancel = new Button
            {
                Location = new Point(30, 633),
                Size = new Size(235, 55),
                Text = "❌ CANCEL",
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Enabled = false
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += BtnCancel_Click;
            machinePanel.Controls.Add(btnCancel);
        }

        // UI SETUP: INSERT MONEY BUTTON 
        // Creates the button for inserting ₱5 coins 
        // Disabled until a product is selected
        private void AddInsertMoneyButton()
        {
            btnInsertMoney = new Button
            {
                Location = new Point(285, 633),
                Size = new Size(235, 55),
                Text = "💰 Insert ₱5",
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                BackColor = Color.FromArgb(39, 174, 96),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Enabled = false                    // Disabled until product selected
            };
            btnInsertMoney.FlatAppearance.BorderSize = 0;
            btnInsertMoney.Click += BtnInsertMoney_Click;
            machinePanel.Controls.Add(btnInsertMoney);
        }


        // UI SETUP: OUTPUT TRAY 
        // Creates the bottom tray label where dispensed products appear
        private void AddOutputTray()
        {
            lblOutputTray = new Label
            {
                Location = new Point(30, 698),
                Size = new Size(490, 55),
                BackColor = Color.FromArgb(44, 62, 80),
                ForeColor = Color.White,
                Font = new Font("Consolas", 15, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "Output Tray: Empty"
            };
            machinePanel.Controls.Add(lblOutputTray);
        }


        // EVENT: CHIPS BUTTON CLICKED
        private void BtnChips_Click(object? sender, EventArgs e)
        {
            if (!chips.IsAvailable)
            {
                lblOutput.Text = "CHIPS - Sold Out!";
                btnChips.BackColor = Color.FromArgb(192, 57, 43); // Color for sold out
                return;
            }
            SelectProduct(chips);
        }

        // EVENT: WATER BUTTON CLICKED
        private void BtnWater_Click(object? sender, EventArgs e)
        {
            if (!water.IsAvailable)
            {
                lblOutput.Text = "WATER - Sold Out!";
                btnWater.BackColor = Color.FromArgb(192, 57, 43); // Color for sold out
                return;
            }
            SelectProduct(water);
        }

        // PRODUCT SELECTION LOGIC - WITH REFUND HANDLING
        // Handles selecting a product and transitioning to "Selected" state
        // Now returns money if user changes selection
        private void SelectProduct(Product product)
        {
            if (product == null || !product.IsAvailable)
            {
                MessageBox.Show($"{product?.Name ?? "Product"} is sold out!",
                    "Product Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if user is changing selection with money already inserted
            decimal refundAmount = machine.SelectProduct(product);

            if (refundAmount > 0)
            {
                // User changed their mind - show refund message
                MessageBox.Show(
                    $"Previous selection cancelled.\n" +
                    $"Refunded: ₱{refundAmount:0.00}\n\n" +
                    $"Please insert coins for {product.Name}.",
                    "Selection Changed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                lblMoney.Text = "Inserted: ₱0.00";
            }

            selectedProduct = product;
            lblOutputTray.Text = "Output Tray: Empty";
            btnInsertMoney.Enabled = true;         // Enable coin insertion
            btnCancel.Enabled = true;              // Enable cancel button

            UpdateDisplay();
            diagramPanel.Invalidate();             // Redraw Moore diagram
        }

        // EVENT: CANCEL BUTTON CLICKED
        // Cancels transaction and refunds money
        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            decimal refundAmount = machine.CancelTransaction();

            if (refundAmount > 0)
            {
                MessageBox.Show(
                    $"Transaction cancelled.\n" +
                    $"Refunded: ₱{refundAmount:0.00}",
                    "Transaction Cancelled",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            selectedProduct = null;
            btnInsertMoney.Enabled = false;
            btnCancel.Enabled = false;
            lblOutputTray.Text = "Output Tray: Empty";
            lblMoney.Text = "Inserted: ₱0.00";

            UpdateDisplay();
            diagramPanel.Invalidate();
        }

        // EVENT: INSERT MONEY BUTTON CLICKED
        // Inserts a ₱5 coin and checks if payment is complete
        private void BtnInsertMoney_Click(object? sender, EventArgs e)
        {
            if (selectedProduct == null) return;

            bool paidEnough = machine.InsertMoney(selectedProduct);

            lblMoney.Text = $"Inserted: ₱{machine.InsertedAmount:0.00}";
            lblOutput.Text = machine.Output;

            diagramPanel.Invalidate();

            if (paidEnough)
            {
                btnInsertMoney.Enabled = false;    // Disable further coin insertion
                btnCancel.Enabled = false;         // Disable cancel during dispensing
                DispenseSelectedProduct();         // Start dispensing
            }
        }

        // PRODUCT DISPENSING
        // Handles the dispensing animation and transaction completion
        private async void DispenseSelectedProduct()
        {
            if (selectedProduct == null) return;

            var productToDispense = selectedProduct;

            // Transition to "Dispensing" state
            await Task.Delay(500);
            machine.SetDispensing(productToDispense);
            UpdateDisplay();
            diagramPanel.Invalidate();

            // Complete transaction and update stock
            await Task.Delay(800);
            machine.CompleteTransaction(productToDispense);

            // Show product name in output tray
            lblOutputTray.Text = $"Output Tray: {productToDispense.Name}";

            // Highlight the dispensed product slot briefly
            int row = productToDispense == chips ? 0 : 1;
            for (int col = 0; col < 5; col++)
            {
                if (productScreenSlots[row, col].Visible)
                {
                    productScreenSlots[row, col].BackColor = Color.Yellow;
                    await Task.Delay(500);
                    productScreenSlots[row, col].BackColor = Color.Gray;
                    break;
                }
            }

            // Reset state
            selectedProduct = null;
            btnInsertMoney.Enabled = false;

            UpdateDisplay();
            diagramPanel.Invalidate();
            lblMoney.Text = "Inserted: ₱0.00";

            RefreshProductScreen();                // Hide sold-out slots
        }

        // DISPLAY UPDATE
        // Updates all UI elements [labels, buttons, colors] based on current state
        private void UpdateDisplay()
        {
            lblOutput.Text = machine.Output;

            lblChipsStock.Text = $"Stock: {chips.Stock}";
            lblWaterStock.Text = $"Stock: {water.Stock}";

            btnChips.Enabled = true;
            btnWater.Enabled = true;

            // Color code buttons: Blue for available, Red for sold out
            btnChips.BackColor = chips.IsAvailable ? Color.FromArgb(52, 152, 219) : Color.FromArgb(192, 57, 43);
            btnWater.BackColor = water.IsAvailable ? Color.FromArgb(52, 152, 219) : Color.FromArgb(192, 57, 43);
        }

        // EVENT: RESTOCK BUTTON CLICKED
        // Refills all products to maximum stock and resets machine state
        private void BtnRestock_Click(object? sender, EventArgs e)
        {
            int chipsRestock = Math.Min(5, MaxStock - chips.Stock);
            int waterRestock = Math.Min(5, MaxStock - water.Stock);

            chips.Restock(chipsRestock);
            water.Restock(waterRestock);

            machine.Reset();
            selectedProduct = null;
            btnInsertMoney.Enabled = false;
            btnCancel.Enabled = false;
            lblOutputTray.Text = "Output Tray: Empty";

            UpdateDisplay();
            RefreshProductScreen();
            diagramPanel.Invalidate();

            MessageBox.Show("Products restocked successfully!", "Restock",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // MOORE DIAGRAM RENDERING
        // Paint event handler for the diagram panel
        private void DiagramPanel_Paint(object? sender, PaintEventArgs e)
        {
            DrawMooreDiagram(e.Graphics);
        }

        // DRAW MOORE MACHINE DIAGRAM
        // Renders the state diagram with circles for states and arrows for transitions
        // Highlights the current state in green
        private void DrawMooreDiagram(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            int yOffset = 130;

            int circleDiameter = 150;
            int radius = circleDiameter / 2;

            // Define state positions (x, y coordinates)
            var states = new Dictionary<string, Point>
            {
                { "Idle", new Point(150, 50 + yOffset) },
                { "Selected", new Point(420, 50 + yOffset) },
                { "PaymentReceived", new Point(420, 220 + yOffset) },
                { "Dispensing", new Point(150, 220 + yOffset) }
            };

            using (Font defaultFont = new Font("Segoe UI", 14, FontStyle.Bold))
            using (Font smallFont = new Font("Segoe UI", 12, FontStyle.Bold))
            {
                // Draw each state as a circle
                foreach (var state in states)
                {
                    bool isActive = machine.CurrentState == state.Key;

                    // Color: Green for active state, Blue for inactive
                    Color stateColor = isActive ? Color.FromArgb(46, 204, 113) : Color.FromArgb(52, 152, 219);
                    Color borderColor = isActive ? Color.FromArgb(39, 174, 96) : Color.FromArgb(41, 128, 185);

                    using (Brush brush = new SolidBrush(stateColor))
                    using (Pen pen = new Pen(borderColor, isActive ? 4 : 2))
                    {
                        g.FillEllipse(brush, state.Value.X - radius, state.Value.Y - radius, circleDiameter, circleDiameter);
                        g.DrawEllipse(pen, state.Value.X - radius, state.Value.Y - radius, circleDiameter, circleDiameter);
                    }

                    // Draw state label text
                    using (Brush textBrush = new SolidBrush(Color.White))
                    {
                        string label = state.Key;
                        Font fontToUse = defaultFont;

                        // Show payment progress for PaymentReceived state
                        if (state.Key == "PaymentReceived")
                        {
                            fontToUse = new Font(defaultFont.FontFamily, defaultFont.Size - 2, FontStyle.Bold);
                            if (selectedProduct != null)
                                label += $"\n₱{machine.InsertedAmount:0.00} / ₱{selectedProduct.Price:0.00}";
                        }

                        // Center text in circle
                        var stringFormat = new StringFormat
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center
                        };

                        g.DrawString(label, fontToUse, textBrush,
                                     new RectangleF(state.Value.X - radius, state.Value.Y - radius, circleDiameter, circleDiameter),
                                     stringFormat);
                    }
                }

                // Draw transition arrows between states
                using (Pen arrowPen = new Pen(Color.FromArgb(52, 73, 94), 3))
                {
                    arrowPen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;

                    // Idle → Selected
                    g.DrawLine(arrowPen, states["Idle"].X + radius, states["Idle"].Y, states["Selected"].X - radius, states["Selected"].Y);

                    // Selected → PaymentReceived
                    g.DrawLine(arrowPen, states["Selected"].X, states["Selected"].Y + radius, states["PaymentReceived"].X, states["PaymentReceived"].Y - radius);

                    // PaymentReceived → Dispensing
                    g.DrawLine(arrowPen, states["PaymentReceived"].X - radius, states["PaymentReceived"].Y, states["Dispensing"].X + radius, states["Dispensing"].Y);

                    // Dispensing → Idle
                    g.DrawLine(arrowPen, states["Dispensing"].X, states["Dispensing"].Y - radius, states["Idle"].X, states["Idle"].Y + radius);

                    // Initial state arrow [pointing to Idle from above]
                    Point idle = states["Idle"];
                    Point arrowStart = new Point(idle.X, idle.Y - radius - 35);
                    Point arrowEnd = new Point(idle.X, idle.Y - radius);
                    g.DrawLine(arrowPen, arrowStart, arrowEnd);

                    // Draw transition labels
                    using (Brush labelBrush = new SolidBrush(Color.FromArgb(52, 73, 94)))
                    {
                        g.DrawString("Select", smallFont, labelBrush, (states["Idle"].X + states["Selected"].X) / 2 - 35, states["Idle"].Y - 25);
                        g.DrawString("Insert Coin", smallFont, labelBrush, states["Selected"].X + 15, (states["Selected"].Y + states["PaymentReceived"].Y) / 2 - 10);
                        g.DrawString("Dispense", smallFont, labelBrush, (states["PaymentReceived"].X + states["Dispensing"].X) / 2 - 35, states["PaymentReceived"].Y + 10);
                        g.DrawString("Complete", smallFont, labelBrush, states["Dispensing"].X - 90, (states["Dispensing"].Y + states["Idle"].Y) / 2 - 10);
                        g.DrawString("Restock", smallFont, labelBrush, idle.X - 25, arrowStart.Y - 20);
                    }
                }
            }
        }
    }
}