// UI Layout for Vending Machine

namespace VendingMachine
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        // UI CONTROL DECLARATIONS

        // Header/Footer
        private Panel headerPanel;
        private Label lblTitle;
        private Panel footerPanel;
        private Label lblMembers;

        // Left Panel [Vending Machine]
        private Panel leftPanel;
        private Panel machinePanel;
        private Label lblMachineTitle;
        private Panel displayPanel;
        private Label lblOutput;
        private Label lblMoney;

        // Product Controls
        private Button btnChips;
        private Button btnWater;
        private Label lblChipsPrice;
        private Label lblWaterPrice;
        private Label lblChipsStock;
        private Label lblWaterStock;
        private Button btnRestock;

        // Right Panel [Diagram & Definition]
        private Panel rightPanel;
        private Panel diagramPanel;
        private Label lblDiagramTitle;
        private Panel definitionPanel;
        private Label lblDefinitionTitle;
        private RichTextBox txtDefinition;

        // DISPOSE METHOD
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        // INITIALIZE COMPONENT - MAIN UI BUILDER
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // Form properties 
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1400, 900);
            this.Text = "Simple Vending Machine - Moore Machine";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(236, 240, 241);
            this.MinimumSize = new Size(1400, 900);
            this.Font = new Font("Segoe UI", 9);

            // HEADER PANEL
            headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.FromArgb(60, 65, 75),
            };

            lblTitle = new Label
            {
                Text = "SIMPLE VENDING MACHINE\nwith Product Availability",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            headerPanel.Controls.Add(lblTitle);

            // FOOTER PANEL
            footerPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                BackColor = Color.FromArgb(60, 65, 75),
            };

            lblMembers = new Label
            {
                Text = "GROUP 6 | BSCS 2B",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            footerPanel.Controls.Add(lblMembers);

            // CONTENT PANEL [Container for left and right sections]
            Panel contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(236, 240, 241),
                Padding = new Padding(20)
            };

            // LEFT PANEL - VENDING MACHINE UI
            leftPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Padding = new Padding(10)
            };

            // Create a container to center the machine panel
            Panel machineCenterContainer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                AutoScroll = true  // Enable scrolling
            };

            // Create a shadow panel
            Panel machineBackgroundPanel = new Panel
            {
                Width = 570,
                Height = 800,
                BackColor = Color.FromArgb(44, 62, 80),
                Padding = new Padding(0)
            };

            machinePanel = new Panel
            {
                Width = 550,
                Height = 780,
                BackColor = Color.FromArgb(52, 73, 94),
                Padding = new Padding(0),
                Location = new Point(10, 10)
            };

            // Center the background panel horizontally, and position machine inside it
            machineCenterContainer.Resize += (s, e) =>
            {
                machineBackgroundPanel.Left = (machineCenterContainer.Width - machineBackgroundPanel.Width) / 2;
                machineBackgroundPanel.Top = 10;
            };

            // Add machine panel to background panel, then background to container
            machineBackgroundPanel.Controls.Add(machinePanel);

            // Machine Title 
            lblMachineTitle = new Label
            {
                Text = "₱5 SNACK MACHINE",
                Location = new Point(30, 15),
                Size = new Size(490, 40),
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(41, 128, 185),
                BorderStyle = BorderStyle.FixedSingle
            };

            // DISPLAY PANEL 
            displayPanel = new Panel
            {
                Location = new Point(30, 230),
                Size = new Size(490, 80),
                BackColor = Color.FromArgb(39, 174, 96),
                BorderStyle = BorderStyle.FixedSingle
            };

            lblOutput = new Label
            {
                Text = "Ready",
                Location = new Point(10, 8),
                Size = new Size(470, 40),
                Font = new Font("Consolas", 16, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };

            lblMoney = new Label
            {
                Text = "Inserted: ₱0.00",
                Location = new Point(10, 48),
                Size = new Size(470, 28),
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(46, 204, 113),
                TextAlign = ContentAlignment.MiddleCenter,
                BorderStyle = BorderStyle.FixedSingle
            };

            displayPanel.Controls.Add(lblOutput);
            displayPanel.Controls.Add(lblMoney);

            // PRODUCT SECTION - CHIPS
            btnChips = new Button
            {
                Location = new Point(30, 325),
                Size = new Size(490, 80),
                Text = "🍪 CHIPS",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnChips.FlatAppearance.BorderSize = 0;
            btnChips.Click += BtnChips_Click;

            lblChipsPrice = new Label
            {
                Text = "₱20.00",
                Location = new Point(40, 410),
                Size = new Size(230, 28),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(241, 196, 15),
                BackColor = Color.Transparent
            };

            lblChipsStock = new Label
            {
                Text = "Stock: 5",
                Location = new Point(280, 410),
                Size = new Size(240, 28),
                Font = new Font("Segoe UI", 13),
                ForeColor = Color.FromArgb(149, 165, 166),
                TextAlign = ContentAlignment.TopRight,
                BackColor = Color.Transparent
            };

            // PRODUCT SECTION - WATER
            btnWater = new Button
            {
                Location = new Point(30, 450),
                Size = new Size(490, 80),
                Text = "💧 WATER",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnWater.FlatAppearance.BorderSize = 0;
            btnWater.Click += BtnWater_Click;

            lblWaterPrice = new Label
            {
                Text = "₱15.00",
                Location = new Point(40, 535),
                Size = new Size(230, 28),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(241, 196, 15),
                BackColor = Color.Transparent
            };

            lblWaterStock = new Label
            {
                Text = "Stock: 5",
                Location = new Point(280, 535),
                Size = new Size(240, 28),
                Font = new Font("Segoe UI", 13),
                ForeColor = Color.FromArgb(149, 165, 166),
                TextAlign = ContentAlignment.TopRight,
                BackColor = Color.Transparent
            };

            // RESTOCK BUTTON 
            btnRestock = new Button
            {
                Location = new Point(165, 573),
                Size = new Size(220, 50),
                Text = "🔄 RESTOCK ALL",
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                BackColor = Color.FromArgb(241, 196, 15),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnRestock.FlatAppearance.BorderSize = 0;
            btnRestock.Click += BtnRestock_Click;

            // Add all controls to machine panel
            machinePanel.Controls.Add(lblMachineTitle);
            machinePanel.Controls.Add(displayPanel);
            machinePanel.Controls.Add(btnChips);
            machinePanel.Controls.Add(lblChipsPrice);
            machinePanel.Controls.Add(lblChipsStock);
            machinePanel.Controls.Add(btnWater);
            machinePanel.Controls.Add(lblWaterPrice);
            machinePanel.Controls.Add(lblWaterStock);
            machinePanel.Controls.Add(btnRestock);

            // Add background panel to center container, then container to left panel
            machineCenterContainer.Controls.Add(machineBackgroundPanel);
            leftPanel.Controls.Add(machineCenterContainer);

            // RIGHT PANEL - MOORE DIAGRAM & FORMAL DEFINITION
            rightPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Padding = new Padding(10)
            };

            // TableLayoutPanel 
            TableLayoutPanel rightLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1,
                BackColor = Color.Transparent
            };
            rightLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            rightLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));

            // DIAGRAM SECTION 
            Panel diagramContainer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 0, 0, 10)
            };

            lblDiagramTitle = new Label
            {
                Text = "MOORE MACHINE DIAGRAM",
                Height = 40,
                Dock = DockStyle.Top,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            diagramPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            diagramPanel.Paint += DiagramPanel_Paint;

            diagramContainer.Controls.Add(diagramPanel);
            diagramContainer.Controls.Add(lblDiagramTitle);

            // DEFINITION SECTION 
            Panel definitionContainer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 10, 0, 0)
            };

            lblDefinitionTitle = new Label
            {
                Text = "FORMAL DESCRIPTION",
                Height = 40,
                Dock = DockStyle.Top,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            definitionPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(10)
            };

            txtDefinition = new RichTextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Consolas", 12),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                ReadOnly = true,
                ScrollBars = RichTextBoxScrollBars.Vertical,
                Text =
@"This is a finite-state Moore machine representing the vending machine, 
including product availability, restock, and cancellation functionality.

Moore machine is defined as M = (Q, q0, Σ, O, δ, λ) where:

Q = {Idle, Selected, PaymentReceived, Dispensing}
    State set: A finite set of states the machine can be in

q0 = Idle
     Initial state: The state the machine starts in

Σ = {Select, InsertCoin, Dispense, Complete, Restock, Cancel}
    Input alphabet: The set of possible inputs to the machine

O = {Ready, ItemSelected, PaymentAccepted, DispensingItem}
    Output alphabet: The set of possible outputs

δ: Q × Σ → Q
   Transition function: Describes state transitions based on current state and input
   
   Current State      | Input       | Next State
   ─────────────────────────────────────────────────
   Idle               | Select      | Selected
   Idle               | Restock     | Idle
   Selected           | InsertCoin  | PaymentReceived
   Selected           | Cancel      | Idle
   PaymentReceived    | Dispense    | Dispensing
   PaymentReceived    | Cancel      | Idle
   Dispensing         | Complete    | Idle

λ: Q → O
   Output function: Produces output based solely on current state (Moore property)
   
    State              | Output
   ───────────────────────────────────
   Idle               | Ready
   Selected           | ItemSelected
   PaymentReceived    | PaymentAccepted
   Dispensing         | DispensingItem
"
            };

            definitionPanel.Controls.Add(txtDefinition);
            definitionContainer.Controls.Add(definitionPanel);
            definitionContainer.Controls.Add(lblDefinitionTitle);

            // ASSEMBLE RIGHT PANEL
            rightLayout.Controls.Add(diagramContainer, 0, 0);
            rightLayout.Controls.Add(definitionContainer, 0, 1);
            rightPanel.Controls.Add(rightLayout);

            // ASSEMBLE MAIN FORM 
            TableLayoutPanel mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 1,
                ColumnCount = 2,
                BackColor = Color.Transparent
            };
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            mainLayout.Controls.Add(leftPanel, 0, 0);
            mainLayout.Controls.Add(rightPanel, 1, 0);

            contentPanel.Controls.Add(mainLayout);

            this.Controls.Add(contentPanel);
            this.Controls.Add(footerPanel);
            this.Controls.Add(headerPanel);
        }
    }
}