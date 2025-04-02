	User Opens App  -->  Enters Search Query ??  -->  Selects Filters ??  
			\                              /
			\    [If Not Found]          /
			? Adjusts Number Ranges ???  
				\                        /
	? Sorts Results ??  -->  Finds Drawing ?  -->  Opens Drawing ??  


		🟢 Start
	│
	├──> 🔍 User searches for drawing
	│        ├──> Filters refine results dynamically
	│        ├──> Search query highlights matching items
	│        ├──> File Explorer auto-updates results
	│
	├──> 📋 User selects a drawing in DataGrid
	│        ├──> Property Window displays attributes
	│        ├──> Preview (if enabled) loads in the side panel
	│
	├──> 🖱️ User right-clicks to access actions
	│        ├──> Open in AutoCAD/SolidWorks
	│        ├──> Copy details or drawing path
	│        ├──> Revise, Duplicate, or Archive
	│
	├──> 🛠️ Admin tasks (if applicable)
	│        ├──> Edit metadata, lock/unlock, delete
	│
	├──> 💾 User exports data or saves their selection
	│        ├──> CSV / Spec Sheet Generation
	│
	🔴 End



	┌────────────────────────────────────────────────────────────────────┐
	│ 🏗️ Special Build Drawing Tool                                       │
	├────────────────────────────────────────────────────────────────────┤
	│ 🔍 **Search Bar**: [🔎 Text Input] [🔘 Deep Filters]                 │
	│────────────────────────────────────────────────────────────────────│
	│ 📂 **File Explorer Panel**                                         │
	│ ┌──────────────────────────────┐ ┌──────────────────────────────┐ │
	│ │ 📂 Directory Tree View       │ │ 📋 Drawing List (DataGrid)   │ │
	│ │ - NEW HABASIT DRAWINGS       │ │ ┌─────────────────────────┐ │ │
	│ │   - 0.50 Inch Pitch          │ │ │ ✅ | 📜 File Name       │ │ │
	│ │   - 1.00 Inch Pitch          │ │ │ 🔍 Preview | 🏷️ Code    │ │ │
	│ │   - 2.00 Inch Pitch          │ │ │ 📆 Created | 🛠️ Status │ │ │
	│ │                               │ │ └─────────────────────────┘ │ │
	│ └──────────────────────────────┘ └──────────────────────────────┘ │
	│────────────────────────────────────────────────────────────────────│
	│ 📝 **Drawing Attributes Table / Property Window** (Auto Updates)  │
	│ ┌───────────────────────────────────────────────────────────────┐ │
	│ │ Attribute         | Code  | Value                              │ │
	│ │───────────────────|──────|----------------------------------│ │
	│ │ Belt Type        | M    | Modular                           │ │
	│ │ Belt Series      | 605  | MSM605                            │ │
	│ │ Color           | W    | White                             │ │
	│ │ Material        | P    | Polypropylene                     │ │
	│ │ Adder Material  | -    | N/A                               │ │
	│ │ Rod Material    | S    | Stainless Steel                   │ │
	│ │ Belt Width      | 0600 | 600 mm                            │ │
	│ │ Rollers Across  | 0090 | 9 Rollers                         │ │
	│ │ FRG Centers     | 300  | 3.00 in                           │ │
	│ │ Belt Accessories| CX   | Cleats                            │ │
	│ │ Friction/Static | X    | No Anti-Static                    │ │
	│ │ Side PL/Lane DV | -    | N/A                               │ │
	│ │ Unique ID       | 1    | Custom                            │ │
	│ │ Indent Code     | 4    | Special Build                    │ │
	│ └───────────────────────────────────────────────────────────────┘ │
	│────────────────────────────────────────────────────────────────────│
	│ 🖱️ **Right-Click Context Menu**                                    │
	│    - Open / Preview / Revise / Copy Path / Export                  │
	│    - Link to Drawing Request / View Part Details                   │
	│    - Generate Spec Sheet / Mark Favorite / Archive                 │
	│────────────────────────────────────────────────────────────────────│
	│ 📂 **Bottom Actions Panel**                                         │
	│ [💾 Export CSV] [📂 Save] [🔄 Refresh] [⚙️ Admin Tools]             │
	└────────────────────────────────────────────────────────────────────┘
											

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
						


	┌───────────────────────────┐
	│ 📜 **Right-Click Menu**   │
	├───────────────────────────┤
	│ 📂 Open Drawing           │  --> Opens DWG/PDF in default app
	│ 📁 Open Folder Location   │  --> Opens file explorer at the drawing’s path
	│ 🔍 Preview Thumbnail      │  --> Shows a small image preview (if available)
	│ 🏷️ Copy File Path         │  --> Copies full file path to clipboard
	│ 📋 Copy Drawing Number    │  --> Copies parsed drawing number
	├───────────────────────────┤
	│ ✏️ Revise Drawing         │  --> Opens in editor for modification
	│ 🗂️ Duplicate Drawing      │  --> Creates a copy for reuse
	│ 🔄 Update Metadata        │  --> Refreshes properties & attributes
	│ 🔗 Link to Request        │  --> Associates file with a DrawingRequest
	├───────────────────────────┤
	│ 📊 View Part Details      │  --> Displays structured data from filename
	│ 📃 Generate Spec Sheet    │  --> Creates an engineering report
	│ 🔄 Compare Revisions      │  --> Opens revision comparison view
	│ 📌 Mark as Favorite       │  --> Adds to a quick-access list
	├───────────────────────────┤
	│ 🚀 Search Similar         │  --> Finds similar drawings by attributes
	│ 📊 Export to CSV          │  --> Saves selected drawings as CSV
	│ 📄 Print                  │  --> Sends to default printer
	├───────────────────────────┤
	│ 🛠️ Admin Actions          │
	│    🗑️ Delete Drawing      │  --> Moves file to archive/trash
	│    🔒 Lock/Unlock Drawing │  --> Controls edit permissions
	│    🔎 Audit Log           │  --> Shows history of changes
	├───────────────────────────┤
	│ ❌ Close Menu             │
	└───────────────────────────┘



