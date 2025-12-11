namespace VendingMachine
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel headerPanel;
        private Label lblTitle;
        private Panel leftPanel;
        private Panel machinePanel;
        private Label lblMachineTitle;
        private Panel displayPanel;
        private Label lblOutput;
        private Button btnChips;
        private Button btnWater;
        private Label lblChipsPrice;
        private Label lblWaterPrice;
        private Label lblChipsStock;
        private Label lblWaterStock;
        private Button btnRestock;
        private Panel rightPanel;
        private Panel diagramPanel;
        private Label lblDiagramTitle;
        private Panel definitionPanel;
        private Label lblDefinitionTitle;
        private RichTextBox txtDefinition;
        private Panel footerPanel;
        private Label lblMembers;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1200, 750);
            this.Text = "Simple Vending Machine - Moore Machine";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(236, 240, 241);
            this.MinimumSize = new Size(1200, 750);
            this.Font = new Font("Segoe UI", 9);

            // Header
            headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.FromArgb(44, 62, 80)
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

            // Footer
            footerPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                BackColor = Color.FromArgb(44, 62, 80)
            };

            lblMembers = new Label
            {
                Text = "", 
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            footerPanel.Controls.Add(lblMembers);

            // Content Panel
            Panel contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(236, 240, 241),
                Padding = new Padding(20)
            };

            // Left Panel - Machine
            leftPanel = new Panel
            {
                Width = 380,
                BackColor = Color.White,
                Dock = DockStyle.Left,
                Padding = new Padding(0, 60, 0, 0) 
            };


            machinePanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(52, 73, 94),
                Margin = new Padding(10)
            };

            lblMachineTitle = new Label
            {
                Text = "VENDING MACHINE",
                Location = new Point(10, 10),
                Size = new Size(340, 30),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter
            };

            displayPanel = new Panel
            {
                Location = new Point(20, 50),
                Size = new Size(320, 80),
                BackColor = Color.FromArgb(39, 174, 96),
                BorderStyle = BorderStyle.FixedSingle
            };

            lblOutput = new Label
            {
                Text = "Ready",
                Dock = DockStyle.Fill,
                Font = new Font("Consolas", 14, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter
            };
            displayPanel.Controls.Add(lblOutput);

            // Buttons and Labels
            btnChips = new Button
            {
                Location = new Point(30, 160),
                Size = new Size(300, 80),
                Text = "🥔 CHIPS",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
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
                Location = new Point(30, 245),
                Size = new Size(150, 25),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft
            };

            lblChipsStock = new Label
            {
                Text = "Stock: 5",
                Location = new Point(180, 245),
                Size = new Size(150, 25),
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.FromArgb(149, 165, 166),
                TextAlign = ContentAlignment.MiddleRight
            };

            btnWater = new Button
            {
                Location = new Point(30, 290),
                Size = new Size(300, 80),
                Text = "💧 WATER",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
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
                Location = new Point(30, 375),
                Size = new Size(150, 25),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft
            };

            lblWaterStock = new Label
            {
                Text = "Stock: 5",
                Location = new Point(180, 375),
                Size = new Size(150, 25),
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.FromArgb(149, 165, 166),
                TextAlign = ContentAlignment.MiddleRight
            };

            btnRestock = new Button
            {
                Location = new Point(70, 440),
                Size = new Size(220, 50),
                Text = "🔄 RESTOCK ALL",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                BackColor = Color.FromArgb(241, 196, 15),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnRestock.FlatAppearance.BorderSize = 0;
            btnRestock.Click += BtnRestock_Click;

            machinePanel.Controls.Add(lblMachineTitle);
            machinePanel.Controls.Add(displayPanel);
            machinePanel.Controls.Add(btnChips);
            machinePanel.Controls.Add(lblChipsPrice);
            machinePanel.Controls.Add(lblChipsStock);
            machinePanel.Controls.Add(btnWater);
            machinePanel.Controls.Add(lblWaterPrice);
            machinePanel.Controls.Add(lblWaterStock);
            machinePanel.Controls.Add(btnRestock);
            leftPanel.Controls.Add(machinePanel);

            // Right Panel
            rightPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Padding = new Padding(60)
            };

            // Diagram
            Panel diagramContainer = new Panel
            {
                Height = 350,
                Dock = DockStyle.Top,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 0, 0, 10)
            };


            lblDiagramTitle = new Label
            {
                Text = "MOORE MACHINE DIAGRAM",
                Height = 30,
                Dock = DockStyle.Top,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
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

            diagramContainer.Controls.Add(lblDiagramTitle);
            diagramContainer.Controls.Add(diagramPanel);

            // Definition
            Panel definitionContainer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Padding = new Padding(0)
            };

            lblDefinitionTitle = new Label
            {
                Text = "FORMAL DEFINITION",
                Height = 30,
                Dock = DockStyle.Top,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
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
                Margin = new Padding(0, 10, 0, 0)
            };

            txtDefinition = new RichTextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Consolas", 9),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                ReadOnly = true,
                Text =
@"M = (Q, Σ, δ, q0, Λ, G)

Q = {Idle, Selected, PaymentReceived, Dispensing}   
Σ = {Select, InsertMoney, Dispense, Complete}       
q0 = Idle                                           
Λ = {Ready, Selected, Payment Accepted, Dispensing, Complete} 

δ (Transitions):
  Idle + Select - Selected
  Selected + InsertMoney - PaymentReceived
  PaymentReceived + Dispense - Dispensing
  Dispensing + Complete - Idle

G (Output function):
  Idle - Ready
  Selected - Selected
  PaymentReceived - Payment Accepted
  Dispensing - Dispensing",
            };

            definitionPanel.Controls.Add(txtDefinition);
            definitionContainer.Controls.Add(lblDefinitionTitle);
            definitionContainer.Controls.Add(definitionPanel);

            rightPanel.Controls.Add(definitionContainer);
            rightPanel.Controls.Add(diagramContainer);

            // Add to content
            contentPanel.Controls.Add(rightPanel);
            contentPanel.Controls.Add(leftPanel);

            // Add all to form
            this.Controls.Add(headerPanel);
            this.Controls.Add(contentPanel);
            this.Controls.Add(footerPanel);
        }
    }
}
