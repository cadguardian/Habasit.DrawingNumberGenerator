using DNG.Library.Models.Base;
using DNG.Library.Models.BeltSpecs;
using DNG.Library.Services.Base;
using DNG.Library.Utility;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.Text.RegularExpressions; // Ensure you have this using directive

namespace SBP_DWG_PDM_Winforms
{
    public partial class Form1 : Form
    {
        private readonly IDrawingNumberDecipherService _decipherService;
        private readonly IDrawingFileService _drawingFileService;

        // Store all loaded files
        private BindingList<FileItem> allFiles = new();

        private BindingList<FileItem> filteredFiles = new();

        private List<Dictionary<string, string>> validDrawings = new List<Dictionary<string, string>>();

        private Panel pnlFilters;
        private FlowLayoutPanel flpFilterContainer;
        private Button btnApplyFilters, btnResetFilters;
        private CheckBox chkLiveFiltering;

        private string drawingNumberInput = string.Empty;
        private Dictionary<string, (string DrawingCode, string DrawingRequestValue)> decipheredResult;
        private bool isDeciphered = false;

        public Form1(IDrawingNumberDecipherService decipherService, IDrawingFileService drawingFileService, HttpClient httpClient)
        {
            _decipherService = decipherService;
            _drawingFileService = drawingFileService;
            InitializeComponent();
            textBox1.TextChanged += (s, ev) => ApplySearchFilter();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            InitializeContextMenu();
            IntializeDrawingListGrid();
            InitializeBeltListGrid();
            InitializeDrawingFiles(@"K:\Operations\Modular\Special Builds\Belts\NEW HABASIT DRAWINGS");
            //InitializeAdvancedFilters();
            dgvDrawingList.SelectionChanged += DgvDrawingList_SelectionChanged;
            dgvBelts.CellValueNeeded += dgvBelts_CellValueNeeded;

            //dgvDrawingList.CellValueNeeded += dgvDrawingList_CellValueNeeded;
        }

        // Private field to store filtered belt drawings
        private List<Dictionary<string, string>> filteredDrawings = new();

        private void dgvBelts_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            if (e.RowIndex < validDrawings.Count && e.ColumnIndex < dgvBelts.ColumnCount)
            {
                var drawing = validDrawings[e.RowIndex];
                string columnName = dgvBelts.Columns[e.ColumnIndex].Name;

                if (drawing.TryGetValue(columnName, out string value))
                {
                    e.Value = value;
                }
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.SetProperty,
                null, dgvBelts, new object[] { true });

            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.SetProperty,
                null, dgvDrawingList, new object[] { true });
        }

        private void DgvDrawingList_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvDrawingList.SelectedCells.Count == 0)
                return;

            // Get the selected cell's column index
            int selectedColumnIndex = dgvDrawingList.SelectedCells[0].ColumnIndex;

            // ✅ Only process if the selected cell is in the first column (index 0)
            if (selectedColumnIndex != 0)
            {
                dgvDrawingAttributes.Rows.Clear();
                return;
            }

            // Get the selected filename (Column 0: "Filename")
            string selectedFileName = dgvDrawingList.SelectedCells[0].Value?.ToString() ?? string.Empty;

            if (string.IsNullOrEmpty(selectedFileName))
            {
                dgvDrawingAttributes.Rows.Clear();
                return;
            }

            // Remove file extension (if applicable)
            string drawingNumber = Path.GetFileNameWithoutExtension(selectedFileName);

            // Call DecipherDrawingNumber service
            Dictionary<string, (string DrawingCode, string DrawingRequestValue)> attributes =
                _decipherService.DecipherDrawingNumber(drawingNumber);

            // ✅ Count occurrences of "Unknown" in the returned dictionary values
            int unknownCount = attributes.Values.Count(v => v.DrawingRequestValue.Equals("Unknown", StringComparison.OrdinalIgnoreCase));

            if (unknownCount >= 2)
            {
                DisplayBadDrawingNumberMessage();
            }
            else
            {
                // Populate the DataGridView
                InitializeDrawingAttributes(attributes);
            }
        }

        /// <summary>
        /// Displays "Bad drawing number selected" in dgvDrawingAttributes.
        /// </summary>
        private void DisplayBadDrawingNumberMessage()
        {
            // Clear existing rows & columns
            dgvDrawingAttributes.Rows.Clear();
            dgvDrawingAttributes.Columns.Clear();

            // Add a single row with an error message
            dgvDrawingAttributes.ColumnCount = 1;
            dgvDrawingAttributes.Columns[0].Name = "Error";
            dgvDrawingAttributes.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvDrawingAttributes.Rows.Add("Bad drawing number selected");

            // Set as read-only
            dgvDrawingAttributes.ReadOnly = true;
            dgvDrawingAttributes.AllowUserToAddRows = false;
            dgvDrawingAttributes.AllowUserToResizeRows = false;
            dgvDrawingAttributes.RowHeadersVisible = false;
        }

        private void InitializeAdvancedFilters()
        {
            // 🟢 MAIN PANEL (Fixed Width + Scrollable When Needed)
            pnlFilters = new Panel
            {
                Dock = DockStyle.Left,
                Width = 250,
                Padding = new Padding(5),
                BorderStyle = BorderStyle.FixedSingle,
                AutoScroll = true // Enables scrolling when necessary
            };

            // 🟢 FILTER CONTAINER (Strict Vertical Layout)
            flpFilterContainer = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                AutoSize = true,
                AutoScroll = true,
                WrapContents = false // Ensures strict vertical alignment
            };

            var cbBeltType = new CheckedListBox { Height = 100, Width = 200 };

            foreach (var item in BeltType.Options.Values)
            {
                cbBeltType.Items.Add(item);
            }

            var cbSeries = new CheckedListBox { Height = 100, Width = 200 };

            foreach (var item in BeltSeries.Options.Keys)
            {
                cbSeries.Items.Add(item);
            }

            var cbColors = new CheckedListBox { Height = 100, Width = 200 };

            foreach (var item in MaterialColor.Options.Values)
            {
                cbColors.Items.Add(item);
            }

            var cbMaterials = new CheckedListBox { Height = 100, Width = 200 };

            foreach (var item in BeltMaterial.Options.Values)
            {
                cbMaterials.Items.Add(item);
            }

            var cbRodMaterials = new CheckedListBox { Height = 100, Width = 200 };

            foreach (var item in RodMaterial.Options.Values)
            {
                cbRodMaterials.Items.Add(item);
            }

            var cbFlight_Rollers_Grips = new CheckedListBox { Height = 100, Width = 200 };

            foreach (var item in Flights_Rollers_Grips.Options.Values)
            {
                cbFlight_Rollers_Grips.Items.Add(item);
            }

            var cbAdderMaterial = new CheckedListBox { Height = 100, Width = 200 };

            foreach (var item in AdderMaterial.Options.Values)
            {
                cbAdderMaterial.Items.Add(item);
            }

            var cbAccessories = new CheckedListBox { Height = 100, Width = 200 };

            foreach (var item in BeltAccessories.Options.Values)
            {
                cbAccessories.Items.Add(item);
            }

            // 🟢 ADD FILTER CONTROLS (All Fixed Width)
            AddFilterControls(flpFilterContainer, new (string, Control)[]
            {
            ("📂 Belt Type", cbBeltType),
    ("🔗 Belt Series", cbSeries),
    ("🏗️ Material", cbMaterials),
    ("🎨 Color", cbColors),
    ("🔩 Rod Material", cbRodMaterials),
    ("⚙️ Flight Roller Grip", cbFlight_Rollers_Grips),
    ("🛠️ Adder Material", cbAdderMaterial),
    ("🧩 Accessories", cbAccessories),
    ("📏 Belt Width (Min - Max)", CreateRangePanel()),
    ("📐 Indent Size (Min - Max)", CreateRangePanel()),
    ("📁 File Extension", new ComboBox { Width = 200, DropDownStyle = ComboBoxStyle.DropDownList }),
    ("📅 Created Date Range", CreateDateRangePanel()),
    ("📦 File Size (MB Min - Max)", CreateRangePanel())
            });

            // 🟢 APPLY & RESET BUTTONS (Fixed Size)
            chkLiveFiltering = new CheckBox { Text = "⚡ Live Update", AutoSize = true };
            btnApplyFilters = new Button { Text = "✅ Apply", Width = 200, Height = 40, FlatStyle = FlatStyle.Flat };
            btnResetFilters = new Button { Text = "❌ Reset", Width = 200, Height = 40, FlatStyle = FlatStyle.Flat };

            // 🟢 ADD BUTTONS
            flpFilterContainer.Controls.AddRange(new Control[]
            {
            chkLiveFiltering,
            btnApplyFilters,
            btnResetFilters
            });

            // 🟢 ADD TO MAIN PANEL
            pnlFilters.Controls.Add(flpFilterContainer);
            this.Controls.Add(pnlFilters);
        }

        // 🔵 HELPER: Adds Filters with Fixed Width
        private void AddFilterControls(FlowLayoutPanel section, (string Label, Control Control)[] controls)
        {
            foreach (var (label, control) in controls)
            {
                section.Controls.Add(new Label { Text = label, AutoSize = true });
                section.Controls.Add(control);
            }
        }

        // 🔵 HELPER: Creates Min/Max Numeric Input Panel (Fixed Width)
        private FlowLayoutPanel CreateRangePanel()
        {
            FlowLayoutPanel pnlRange = new FlowLayoutPanel { AutoSize = true };
            NumericUpDown numMin = new NumericUpDown { Width = 80, Minimum = 0, Maximum = 10000 };
            NumericUpDown numMax = new NumericUpDown { Width = 80, Minimum = 0, Maximum = 10000 };
            pnlRange.Controls.AddRange(new Control[] { numMin, new Label { Text = "to" }, numMax });
            return pnlRange;
        }

        // 🔵 HELPER: Creates Date Range Picker Panel (Fixed Width)
        private FlowLayoutPanel CreateDateRangePanel()
        {
            FlowLayoutPanel pnlDateRange = new FlowLayoutPanel { AutoSize = true };
            DateTimePicker dtpFrom = new DateTimePicker { Format = DateTimePickerFormat.Short, Width = 130 };
            DateTimePicker dtpTo = new DateTimePicker { Format = DateTimePickerFormat.Short, Width = 130 };
            pnlDateRange.Controls.AddRange(new Control[] { dtpFrom, new Label { Text = "to" }, dtpTo });
            return pnlDateRange;
        }

        private void InitializeContextMenu()
        {
            // Create the ContextMenuStrip
            ContextMenuStrip contextMenu = new ContextMenuStrip();

            // 📂 File Actions
            ToolStripMenuItem fileActions = new ToolStripMenuItem("📂 File Actions");
            fileActions.DropDownItems.Add(new ToolStripMenuItem("📄 Open", null, OpenDrawing));
            fileActions.DropDownItems.Add(new ToolStripMenuItem("📁 Open Folder", null, OpenFolder));
            fileActions.DropDownItems.Add(new ToolStripMenuItem("🔍 Preview", null, PreviewDrawing));
            fileActions.DropDownItems.Add(new ToolStripMenuItem("📋 Copy File Path", null, CopyFilePath));
            fileActions.DropDownItems.Add(new ToolStripMenuItem("📝 Copy Drawing Number", null, CopyDrawingNumber));

            // 🗂️ Drawing Management
            ToolStripMenuItem drawingManagement = new ToolStripMenuItem("🗂️ Drawing Management");
            drawingManagement.DropDownItems.Add(new ToolStripMenuItem("🖨️ Print", null, PrintDrawing));
            drawingManagement.DropDownItems.Add(new ToolStripMenuItem("📂 Duplicate", null, DuplicateDrawing));
            drawingManagement.DropDownItems.Add(new ToolStripMenuItem("✏️ Revise", null, ReviseDrawing));
            drawingManagement.DropDownItems.Add(new ToolStripMenuItem("🔄 Update Metadata", null, UpdateMetadata));
            drawingManagement.DropDownItems.Add(new ToolStripMenuItem("🔗 Link to Request", null, LinkToRequest));
            drawingManagement.DropDownItems.Add(new ToolStripMenuItem("🔒 Lock/Unlock", null, LockUnlockDrawing));

            // 📊 Information Analysis
            ToolStripMenuItem infoAnalysis = new ToolStripMenuItem("📊 Information Analysis");
            infoAnalysis.DropDownItems.Add(new ToolStripMenuItem("📑 View BOM", null, ViewBOM));
            infoAnalysis.DropDownItems.Add(new ToolStripMenuItem("📋 See Request", null, SeeRequest));
            infoAnalysis.DropDownItems.Add(new ToolStripMenuItem("🔄 Compare", null, CompareRevisions));
            infoAnalysis.DropDownItems.Add(new ToolStripMenuItem("⭐ Mark as Favorite", null, MarkAsFavorite));
            infoAnalysis.DropDownItems.Add(new ToolStripMenuItem("📊 Export to CSV", null, ExportToCSV));

            // Add categories to Context Menu
            contextMenu.Items.Add(fileActions);
            contextMenu.Items.Add(new ToolStripSeparator());
            contextMenu.Items.Add(drawingManagement);
            contextMenu.Items.Add(new ToolStripSeparator());
            contextMenu.Items.Add(infoAnalysis);

            // Assign the context menu to the DataGridView
            dgvDrawingList.ContextMenuStrip = contextMenu;
        }

        // Example event handlers
        private void OpenDrawing(object sender, EventArgs e)
        { }

        private void OpenFolder(object sender, EventArgs e)
        { /* Open folder logic */ }

        private void PreviewDrawing(object sender, EventArgs e)
        { /* Preview logic */ }

        private void CopyFilePath(object sender, EventArgs e)
        { /* Copy file path logic */ }

        private void CopyDrawingNumber(object sender, EventArgs e)
        { /* Copy drawing number logic */ }

        private void PrintDrawing(object sender, EventArgs e)
        { /* Print drawing logic */ }

        private void DuplicateDrawing(object sender, EventArgs e)
        { /* Duplicate logic */ }

        private void ReviseDrawing(object sender, EventArgs e)
        { /* Revise drawing logic */ }

        private void UpdateMetadata(object sender, EventArgs e)
        { /* Update metadata logic */ }

        private void LinkToRequest(object sender, EventArgs e)
        { /* Link logic */ }

        private void LockUnlockDrawing(object sender, EventArgs e)
        { /* Lock/unlock logic */ }

        private void ViewBOM(object sender, EventArgs e)
        { /* View BOM logic */ }

        private void SeeRequest(object sender, EventArgs e)
        { /* See request logic */ }

        private void CompareRevisions(object sender, EventArgs e)
        { /* Compare logic */ }

        private void MarkAsFavorite(object sender, EventArgs e)
        { /* Mark as favorite logic */ }

        private void ExportToCSV(object sender, EventArgs e)
        { /* Export logic */ }

        private void HandleDecipher()
        {
            if (!string.IsNullOrWhiteSpace(drawingNumberInput))
            {
                decipheredResult = _decipherService.DecipherDrawingNumber(drawingNumberInput);
                isDeciphered = true;
            }
        }

        private void ClearInput()
        {
            drawingNumberInput = string.Empty;
            decipheredResult = null;
            isDeciphered = false;
        }

        private void IntializeDrawingListGrid()
        {
            // Clear existing columns to prevent redundancy
            dgvDrawingList.Columns.Clear();

            // Set grid properties for better UX
            dgvDrawingList.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvDrawingList.AllowUserToResizeColumns = true;
            dgvDrawingList.MultiSelect = false;
            dgvDrawingList.RowHeadersVisible = false;
            dgvDrawingList.DefaultCellStyle.SelectionBackColor = Color.LightBlue; // Highlight selection

            // 🖱 Enable Copy-Paste Functionality
            dgvDrawingList.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dgvDrawingList.SelectionMode = DataGridViewSelectionMode.CellSelect; // Allow cell selection for copying

            // 🎨 Add Alternate Row Colors for Readability
            dgvDrawingList.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            // ⚡ Optimize Rendering Performance
            //dgvDrawingList.DoubleBuffered(true); // Extension method for smoother rendering
        }

        private void InitializeBeltListGrid()
        {
            // Clear existing columns to prevent redundancy
            dgvBelts.Columns.Clear();

            // Set grid properties for better UX
            dgvBelts.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvBelts.AllowUserToResizeColumns = true;
            dgvBelts.MultiSelect = false;
            dgvBelts.RowHeadersVisible = false;
            dgvBelts.DefaultCellStyle.SelectionBackColor = Color.LightBlue; // Highlight selection

            // 🖱 Enable Copy-Paste Functionality
            dgvBelts.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dgvBelts.SelectionMode = DataGridViewSelectionMode.CellSelect; // Allow cell selection for copying

            // 🎨 Add Alternate Row Colors for Readability
            dgvBelts.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            // ⚡ Optimize Rendering Performance
            //dgvBelts.DoubleBuffered(true); // Extension method for smoother rendering
        }

        private void button23_Click(object sender, EventArgs e)
        {
        }

        private void ApplySearchFilter()
        {
            string searchQuery = textBox1.Text.Trim().ToLower();

            // ✅ Ensure wildcard '*' is always at the end if missing
            if (!searchQuery.EndsWith("*"))
            {
                searchQuery += "*";
            }

            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                // ✅ Reset to original full lists
                filteredFiles = new BindingList<FileItem>(new List<FileItem>(allFiles));
                filteredDrawings = new List<Dictionary<string, string>>(validDrawings);

                dgvDrawingList.DataSource = new BindingSource { DataSource = filteredFiles };
                PopulateDgvBelts(filteredDrawings);
                return;
            }

            // ✅ Convert wildcard (*) to regex pattern
            string regexPattern = "^" + Regex.Escape(searchQuery).Replace("\\*", ".*") + "$";
            Regex regex = new Regex(regexPattern, RegexOptions.IgnoreCase);

            // ✅ Filter dgvDrawingList (uses Filename)
            filteredFiles = new BindingList<FileItem>(
                allFiles.AsParallel()
                .Where(f => regex.IsMatch(f.Filename.ToLower()))
                .ToList()
            );

            // ✅ Filter dgvBelts (uses DrawingNumber)
            filteredDrawings = validDrawings
                .AsParallel()
                .Where(drawing => drawing.TryGetValue("DrawingNumber", out var drawingNumber) &&
                                  regex.IsMatch(drawingNumber.ToLower()))
                .ToList();

            dgvDrawingList.SuspendLayout();

            // ✅ Update UI
            dgvDrawingList.DataSource = new BindingSource { DataSource = filteredFiles };

            dgvDrawingList.ResumeLayout();

            dgvDrawingList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvDrawingList.AllowUserToAddRows = false;
            dgvDrawingList.ReadOnly = true;

            PopulateDgvBelts(filteredDrawings);
        }

        /// <summary>
        /// Populates dgvDrawingAttributes with deciphered data.
        /// </summary>
        private void InitializeDrawingAttributes(Dictionary<string, (string DrawingCode, string DrawingRequestValue)> attributes)
        {
            // Clear existing rows & columns to prevent redundancy
            dgvDrawingAttributes.Rows.Clear();
            dgvDrawingAttributes.Columns.Clear();

            // Define DataGridView columns
            dgvDrawingAttributes.ColumnCount = 3;
            dgvDrawingAttributes.Columns[0].Name = "Attribute";
            dgvDrawingAttributes.Columns[1].Name = "Code";
            dgvDrawingAttributes.Columns[2].Name = "Value";

            // Set column widths for best readability
            dgvDrawingAttributes.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDrawingAttributes.Columns[1].Width = 80;
            dgvDrawingAttributes.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            // Set read-only properties (prevent accidental edits)
            dgvDrawingAttributes.ReadOnly = true;
            dgvDrawingAttributes.AllowUserToAddRows = false;
            dgvDrawingAttributes.AllowUserToResizeRows = false;
            dgvDrawingAttributes.RowHeadersVisible = false;

            // Populate the DataGridView dynamically
            foreach (var attribute in attributes)
            {
                dgvDrawingAttributes.Rows.Add(attribute.Key, attribute.Value.DrawingCode, attribute.Value.DrawingRequestValue);
            }

            // Auto-size rows for best display
            dgvDrawingAttributes.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void InitializeDrawingFiles(string folderPath)
        {
            try
            {
                if (!UtilityHelper.IsInternetAvailable())
                {
                    MessageBox.Show("The internet is not available.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!UtilityHelper.IsFolderPathReachable(folderPath))
                {
                    MessageBox.Show("The specified network drive or folder does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var allowedExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            ".pdf", ".dwg", ".sldprt", ".sldasm", ".slddrw"
        };

                var files = Directory.EnumerateFiles(folderPath, "*.*", SearchOption.AllDirectories)
                                     .Where(file => allowedExtensions.Contains(Path.GetExtension(file)))
                                     .Select(file =>
                                     {
                                         var info = new FileInfo(file);
                                         var filenameRaw = Path.GetFileNameWithoutExtension(file);
                                         var filename = filenameRaw.Length > 35 ? filenameRaw[..35] : filenameRaw;

                                         return new FileItem
                                         {
                                             Filename = filename,
                                             RelativePath = file,
                                             FileType = info.Extension,
                                             SizeMb = Math.Round(info.Length / (1024.0 * 1024.0), 2),
                                             CreatedDate = info.CreationTime
                                         };
                                     })
                                     .ToList();

                if (files.Count == 0) return;

                allFiles = new BindingList<FileItem>(files);
                filteredFiles = new BindingList<FileItem>(new List<FileItem>(allFiles));
                dgvDrawingList.DataSource = new BindingSource { DataSource = filteredFiles };

                dgvDrawingList.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvDrawingList.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                dgvDrawingList.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

                var tempValidDrawings = new ConcurrentBag<Dictionary<string, string>>();

                Parallel.ForEach(files, file =>
                {
                    try
                    {
                        string drawingNumber = Path.GetFileNameWithoutExtension(file.Filename);
                        if (string.IsNullOrWhiteSpace(drawingNumber)) return;

                        var attributes = _decipherService.DecipherDrawingNumber(drawingNumber);
                        if (attributes == null || attributes.Count == 0) return;

                        int unknownCount = attributes.Values.Count(v => v.DrawingRequestValue.Equals("Unknown", StringComparison.OrdinalIgnoreCase));
                        if (unknownCount > 2) return;

                        var row = new Dictionary<string, string> { { "DrawingNumber", drawingNumber } };

                        foreach (var kvp in attributes)
                        {
                            if (!row.ContainsKey(kvp.Key))
                                row[kvp.Key] = kvp.Value.DrawingRequestValue;
                        }

                        tempValidDrawings.Add(row);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing file: {file.Filename}. Exception: {ex.Message}");
                    }
                });

                validDrawings = tempValidDrawings.ToList();

                dgvBelts.SuspendLayout();
                PopulateDgvBelts(validDrawings);
                dgvBelts.ResumeLayout();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading drawing files:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateDgvBelts(List<Dictionary<string, string>> drawings)
        {
            dgvBelts.SuspendLayout();
            dgvBelts.Rows.Clear();
            dgvBelts.Columns.Clear();

            if (drawings.Count == 0)
            {
                // MessageBox.Show("No valid drawings found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvBelts.ResumeLayout();
                return;
            }

            // Fast column name extraction
            var columnNamesSet = new HashSet<string>();
            var columnNames = new List<string> { "DrawingNumber" };

            foreach (var drawing in drawings)
            {
                foreach (var key in drawing.Keys)
                {
                    if (key != "DrawingNumber" && columnNamesSet.Add(key))
                        columnNames.Add(key);
                }
            }

            foreach (var columnName in columnNames)
                dgvBelts.Columns.Add(columnName, columnName);

            dgvBelts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None; // Improve performance

            var rowArray = new DataGridViewRow[drawings.Count];
            for (int i = 0; i < drawings.Count; i++)
            {
                var drawing = drawings[i];
                var row = new DataGridViewRow();
                row.CreateCells(dgvBelts);

                for (int j = 0; j < columnNames.Count; j++)
                {
                    string colName = columnNames[j];
                    row.Cells[j].Value = drawing.TryGetValue(colName, out var val) ? val : string.Empty;
                }

                rowArray[i] = row;
            }

            dgvBelts.Rows.AddRange(rowArray);

            dgvBelts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvBelts.AllowUserToAddRows = false;
            dgvBelts.ReadOnly = true;
            dgvBelts.ResumeLayout();
        }
    }
}