<div align="center">

# 🏪 Simple Vending Machine - Moore Machine

### *A Windows Forms application demonstrating Moore finite-state machine*

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![Windows](https://img.shields.io/badge/Windows-0078D6?style=for-the-badge&logo=windows&logoColor=white)
![License](https://img.shields.io/badge/License-Educational-blue?style=for-the-badge)

[Features](#-features) • [Installation](#-installation) • [Usage](#-usage) • [Structure](#-project-structure)

---

</div>

## ✨ Features

<table>
<tr>
<td width="50%">

### 🎯 Core Functionality
- ₱5 coin-based payment system
- Product availability tracking
- Transaction cancellation & refunds
- Automatic stock management
- Product restocking

</td>
<td width="50%">

### 🎨 Visual Features
- Real-time state diagram
- Product image display (2×5 grid)
- Active state highlighting
- Output tray simulation
- Payment progress display

</td>
</tr>
</table>

## 🛒 Products

<div align="center">

| Icon | Product | Price | Initial Stock |
|:----:|:-------:|:-----:|:-------------:|
| 🍪 | **Chips** | ₱20.00 | 5 units |
| 💧 | **Water** | ₱15.00 | 5 units |

</div>

## 🚀 Installation

### Prerequisites

```bash
✓ Visual Studio 2022 or later
✓ .NET 8.0 SDK or later
✓ Windows 10/11
```

### Quick Start

```bash
1. Open Visual Studio 2022
2. Click "Open a project or solution"
3. Navigate to and select `VendingMachine.csproj`
4. Press "F5" or click the "Start" button to run the application
```

## 📖 Usage

<table>
<tr>
<td width="5%">1️⃣</td>
<td><b>Select Product</b><br/>Click <code>Chips</code> or <code>Water</code> button</td>
</tr>
<tr>
<td>2️⃣</td>
<td><b>Insert Money</b><br/>Click <code>Insert ₱5</code> button repeatedly until payment complete<br/><i>Chips = 4 coins, Water = 3 coins</i></td>
</tr>
<tr>
<td>3️⃣</td>
<td><b>Collect Item</b><br/>Product dispenses automatically to output tray</td>
</tr>
<tr>
<td>🔄</td>
<td><b>Optional Actions</b><br/>• Click <code>Cancel</code> to refund money<br/>• Change selection (auto-refunds previous amount)<br/>• Click <code>Restock All</code> to refill products</td>
</tr>
</table>

## 📁 Project Structure

```
VendingMachine/
│
├── 📄 Program.cs                    # Application entry point
├── 📄 MainForm.cs                   # UI logic & event handlers
├── 📄 MainForm.Designer.cs          # UI layout & controls
├── 📄 MooreMachine.cs               # State machine implementation
├── 📄 Product.cs                    # Product model (name, price, stock)
└── 📄 VendingMachine.csproj         # Project configuration
```

## 🎓 Educational Value

<div align="center">

| Concept | Implementation |
|:-------:|:---------------|
| 🔄 | **Finite-State Machines** - Moore machine with pure state-based outputs |
| 🎯 | **Event-Driven Programming** - Windows Forms event handling |
| 🎨 | **Graphics Programming** - Custom GDI+ state diagram rendering |
| ⚡ | **Async Programming** - Image loading with async/await |
| 🏗️ | **Design Patterns** - State pattern, Observer pattern, MVC separation |

</div>

## 🛠️ Technical Highlights

- **Pure Moore Machine**: Outputs depend solely on current state
- **Async Image Loading**: Product images loaded from URLs with 5-second timeout
- **Fallback Placeholders**: Creates colored placeholders if internet unavailable
- **Real-time Visualization**: State diagram updates with current state highlighting
- **Transaction Safety**: Handles selection changes and cancellations with auto-refund

## 📝 License

**For Educational Purposes Only**

This project is provided for learning and academic purposes.

**You may:**
- ✅ Study and learn from the code
- ✅ Use for school/university assignments (with proper citation)
- ✅ Modify for personal learning
- ✅ Fork and experiment with the code

**You may NOT:**
- ❌ Submit as your own work without attribution
- ❌ Use commercially without permission
- ❌ Remove author credits

---

<br>

<div align="center">

**© 2025 GROUP 6 | BSCS 2B - All Rights Reserved**

<br>

### 👥 Authors

**GROUP 6 | BSCS 2B**

<br>

### 🌟 Star this repository if you found it helpful!

<br>

</div>