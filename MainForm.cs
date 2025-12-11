namespace VendingMachine
{
    public partial class MainForm : Form
    {
        private MooreMachine machine = null!;
        private Product chips = null!;
        private Product water = null!;
        private Product? selectedProduct;

        public MainForm()
        {
            InitializeComponent();
            InitializeMachine();
            UpdateDisplay();
        }

        private void InitializeMachine()
        {
            machine = new MooreMachine();
            chips = new Product("Chips", 20m, 5);
            water = new Product("Water", 15m, 5);
        }

        private void BtnChips_Click(object? sender, EventArgs e)
        {
            SelectProduct(chips);
        }

        private void BtnWater_Click(object? sender, EventArgs e)
        {
            SelectProduct(water);
        }

        private async void SelectProduct(Product product)
        {
            selectedProduct = product;
            machine.SelectProduct(product);
            UpdateDisplay();
            diagramPanel.Invalidate();

            if (!product.IsAvailable)
            {
                await Task.Delay(1500);
                machine.Reset();
                UpdateDisplay();
                diagramPanel.Invalidate();
                selectedProduct = null;
                return;
            }

            // Insert money
            await Task.Delay(800);
            machine.InsertMoney();
            UpdateDisplay();
            diagramPanel.Invalidate();

            // Dispensing
            await Task.Delay(800);
            machine.SetDispensing(product);
            UpdateDisplay();
            diagramPanel.Invalidate();

            // Finish transaction
            await Task.Delay(1000);
            product.Dispense();
            machine.CompleteTransaction(product); 
            UpdateDisplay();
            diagramPanel.Invalidate();
            selectedProduct = null;
        }


        private void UpdateDisplay()
        {
            lblOutput.Text = machine.Output;
            lblChipsStock.Text = $"Stock: {chips.Stock}";
            lblWaterStock.Text = $"Stock: {water.Stock}";

            btnChips.Enabled = chips.IsAvailable && machine.CurrentState == "Idle";
            btnWater.Enabled = water.IsAvailable && machine.CurrentState == "Idle";

            btnChips.BackColor = chips.IsAvailable ? Color.FromArgb(52, 152, 219) : Color.FromArgb(149, 165, 166);
            btnWater.BackColor = water.IsAvailable ? Color.FromArgb(52, 152, 219) : Color.FromArgb(149, 165, 166);
        }

        private void BtnRestock_Click(object? sender, EventArgs e)
        {
            chips.Restock(5);
            water.Restock(5);
            machine.Reset();
            UpdateDisplay();
            diagramPanel.Invalidate();
            MessageBox.Show("Products restocked successfully!", "Restock", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DiagramPanel_Paint(object? sender, PaintEventArgs e)
        {
            DrawMooreDiagram(e.Graphics);
        }

        private void DrawMooreDiagram(Graphics g)
        {
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
                // Draw states
                foreach (var state in states)
                {
                    bool isActive = machine.CurrentState == state.Key;
                    Color stateColor = isActive ? Color.FromArgb(46, 204, 113) : Color.FromArgb(52, 152, 219);
                    Color borderColor = isActive ? Color.FromArgb(39, 174, 96) : Color.FromArgb(41, 128, 185);

                    using (Brush brush = new SolidBrush(stateColor))
                    using (Pen pen = new Pen(borderColor, isActive ? 4 : 2))
                    {
                        g.FillEllipse(brush, state.Value.X - radius, state.Value.Y - radius, circleDiameter, circleDiameter);
                        g.DrawEllipse(pen, state.Value.X - radius, state.Value.Y - radius, circleDiameter, circleDiameter);
                    }

                    using (Brush textBrush = new SolidBrush(Color.White))
                    {
                        SizeF textSize = g.MeasureString(state.Key, font);
                        g.DrawString(state.Key, font, textBrush,
                            state.Value.X - textSize.Width / 2, state.Value.Y - textSize.Height / 2);
                    }
                }

                using (Pen arrowPen = new Pen(Color.FromArgb(52, 73, 94), 2))
                {
                    arrowPen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;

                    // Horizontal arrows
                    g.DrawLine(arrowPen, states["Idle"].X + radius, states["Idle"].Y, states["Selected"].X - radius, states["Selected"].Y);
                    g.DrawLine(arrowPen, states["PaymentReceived"].X - radius, states["PaymentReceived"].Y, states["Dispensing"].X + radius, states["Dispensing"].Y);

                    // Vertical arrows
                    g.DrawLine(arrowPen, states["Selected"].X, states["Selected"].Y + radius, states["PaymentReceived"].X, states["PaymentReceived"].Y - radius);
                    g.DrawLine(arrowPen, states["Dispensing"].X, states["Dispensing"].Y - radius, states["Idle"].X, states["Idle"].Y + radius);

                    // Labels
                    using (Brush labelBrush = new SolidBrush(Color.FromArgb(52, 73, 94)))
                    {
                        g.DrawString("Select", smallFont, labelBrush, 190, 30 + yOffset);
                        g.DrawString("Pay", smallFont, labelBrush, 320, 92 + yOffset);
                        g.DrawString("Dispense", smallFont, labelBrush, 190, 130 + yOffset);
                        g.DrawString("Complete", smallFont, labelBrush, 20, 92 + yOffset);
                    }
                }
            }
        }


    }
}
