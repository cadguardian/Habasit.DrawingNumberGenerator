### **📜 Updated Advanced Filter System Layout (Textual Representation)**
```
┌───────────────────────────────────────────┐
│ 🎛️ **Advanced Filter System**             │
│───────────────────────────────────────────│
│ 📂 **Belt Type**        [🔽 Multi-Select]  │
│ 📂 **Belt Series**      [🔽 Multi-Select]  │
│ 🎨 **Color**           [🔽 Dropdown]      │
│ 🏗️ **Material**        [🔽 Dropdown]      │
│ 📏 **Belt Width**      [🔢 Number Range]  │
│ ⚙️ **Rollers Across**  [🔢 Number Range]  │
│ 📐 **FRG Centers**     [🔢 Number Range]  │
│ ➕ **Belt Accessories** [🔽 Multi-Select]  │
│ 🔲 **Indent Code**     [🔽 Multi-Select]  │
│ 🆔 **Unique ID**       [🔽 Multi-Select]  │
│ ⚡ **Friction/Static**  [🔽 Dropdown]      │
│───────────────────────────────────────────│
│  ✅ [Apply Filters]  ❌ [Reset]            │
└───────────────────────────────────────────┘
```

---

## **🔥 How to Implement This in WinForms**
### **🚀 WinForms Components Needed**
| **Filter Attribute**        | **WinForms Component** |
|-----------------------------|------------------------|
| **Belt Type** (Multi-Select) | `CheckedListBox` |
| **Belt Series** (Multi-Select) | `CheckedListBox` |
| **Color** (Single Selection) | `ComboBox` |
| **Material** (Single Selection) | `ComboBox` |
| **Belt Width** (Number Range) | `NumericUpDown (Min)` & `NumericUpDown (Max)` |
| **Qty Rollers Across Width** (Number Range) | `NumericUpDown (Min)` & `NumericUpDown (Max)` |
| **FRG Centers** (Number Range) | `NumericUpDown (Min)` & `NumericUpDown (Max)` |
| **Belt Accessories** (Multi-Select) | `CheckedListBox` |
| **Indent Code** (Multi-Select) | `CheckedListBox` |
| **Unique ID** (Multi-Select) | `CheckedListBox` |
| **Friction/Static** (Dropdown) | `ComboBox` |
| **Apply & Reset Buttons** | `Button (Apply)` & `Button (Reset)` |

---

## **📌 WinForms Layout Plan**
| **Row** | **Component** | **Control Type** |
|--------|--------------|---------------|
| **Row 1** | Belt Type, Belt Series | `CheckedListBox` (Multi-Select) |
| **Row 2** | Color, Material | `ComboBox` (Single Selection) |
| **Row 3** | Belt Width | `NumericUpDown` (Min/Max) |
| **Row 4** | Qty Rollers Across Width | `NumericUpDown` (Min/Max) |
| **Row 5** | FRG Centers | `NumericUpDown` (Min/Max) |
| **Row 6** | Belt Accessories | `CheckedListBox` (Multi-Select) |
| **Row 7** | Indent Code, Unique ID | `CheckedListBox` (Multi-Select) |
| **Row 8** | Friction/Static | `ComboBox` |
| **Row 9** | Apply & Reset Buttons | `Button (Apply)` & `Button (Reset)` |

---

## **📜 Advanced Filter UI in Textual Layout (Detailed Version)**
```
┌───────────────────────────────────────────────────────┐
│ 🎛️ **Advanced Filter System**                        │
│──────────────────────────────────────────────────────│
│ 📂 Belt Type:        [✔] 1220  [✔] 2533  [ ] 5067  │
│ 📂 Belt Series:      [✔] 605  [ ] 720  [✔] 1120     │
│ 🎨 Color:            [🔽 Select One ⌄]              │
│ 🏗️ Material:         [🔽 Select One ⌄]              │
│ 📏 Belt Width:       [ 🔢 Min: ___ ]  [ 🔢 Max: ___ ]│
│ ⚙️ Rollers Across:   [ 🔢 Min: ___ ]  [ 🔢 Max: ___ ]│
│ 📐 FRG Centers:      [ 🔢 Min: ___ ]  [ 🔢 Max: ___ ]│
│ ➕ Belt Accessories: [✔] SC [✔] TX  [ ] FL         │
│ 🔲 Indent Code:      [✔] 00 [ ] 10  [✔] 20         │
│ 🆔 Unique ID:        [✔] 5  [✔] 7  [ ] 9           │
│ ⚡ Friction/Static:   [🔽 Select One ⌄]              │
│──────────────────────────────────────────────────────│
│ ✅ [Apply Filters]    ❌ [Reset]                     │
└──────────────────────────────────────────────────────┘
```

---

