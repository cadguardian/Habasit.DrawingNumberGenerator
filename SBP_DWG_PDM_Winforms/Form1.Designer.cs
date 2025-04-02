namespace SBP_DWG_PDM_Winforms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvDrawingList = new DataGridView();
            textBox1 = new TextBox();
            label1 = new Label();
            button1 = new Button();
            flowLayoutPanel1 = new FlowLayoutPanel();
            button2 = new Button();
            label2 = new Label();
            groupBox3 = new GroupBox();
            dgvDrawingAttributes = new DataGridView();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            dgvBelts = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvDrawingList).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDrawingAttributes).BeginInit();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBelts).BeginInit();
            SuspendLayout();
            // 
            // dgvDrawingList
            // 
            dgvDrawingList.AllowUserToOrderColumns = true;
            dgvDrawingList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDrawingList.Dock = DockStyle.Fill;
            dgvDrawingList.Location = new Point(3, 2);
            dgvDrawingList.Margin = new Padding(3, 2, 3, 2);
            dgvDrawingList.MultiSelect = false;
            dgvDrawingList.Name = "dgvDrawingList";
            dgvDrawingList.RowHeadersWidth = 51;
            dgvDrawingList.Size = new Size(902, 471);
            dgvDrawingList.TabIndex = 1;
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI", 9F);
            textBox1.Location = new Point(51, 11);
            textBox1.Margin = new Padding(3, 11, 3, 2);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(259, 23);
            textBox1.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 14);
            label1.Margin = new Padding(3, 14, 3, 0);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 3;
            label1.Text = "Search";
            // 
            // button1
            // 
            button1.Location = new Point(316, 2);
            button1.Margin = new Padding(3, 2, 3, 2);
            button1.Name = "button1";
            button1.Size = new Size(63, 38);
            button1.TabIndex = 8;
            button1.Text = "Enter";
            button1.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(label1);
            flowLayoutPanel1.Controls.Add(textBox1);
            flowLayoutPanel1.Controls.Add(button1);
            flowLayoutPanel1.Controls.Add(button2);
            flowLayoutPanel1.Controls.Add(label2);
            flowLayoutPanel1.Dock = DockStyle.Top;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(916, 43);
            flowLayoutPanel1.TabIndex = 10;
            // 
            // button2
            // 
            button2.Location = new Point(385, 2);
            button2.Margin = new Padding(3, 2, 3, 2);
            button2.Name = "button2";
            button2.Size = new Size(63, 38);
            button2.TabIndex = 9;
            button2.Text = "Filters";
            button2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(461, 0);
            label2.Margin = new Padding(10, 0, 10, 4);
            label2.Name = "label2";
            label2.Size = new Size(0, 15);
            label2.TabIndex = 18;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(dgvDrawingAttributes);
            groupBox3.Dock = DockStyle.Right;
            groupBox3.Location = new Point(916, 0);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(325, 576);
            groupBox3.TabIndex = 6;
            groupBox3.TabStop = false;
            groupBox3.Text = "Drawing Attributes";
            // 
            // dgvDrawingAttributes
            // 
            dgvDrawingAttributes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDrawingAttributes.Dock = DockStyle.Fill;
            dgvDrawingAttributes.Location = new Point(3, 19);
            dgvDrawingAttributes.Margin = new Padding(3, 75, 3, 3);
            dgvDrawingAttributes.Name = "dgvDrawingAttributes";
            dgvDrawingAttributes.RowHeadersWidth = 51;
            dgvDrawingAttributes.Size = new Size(319, 554);
            dgvDrawingAttributes.TabIndex = 0;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.ItemSize = new Size(74, 50);
            tabControl1.Location = new Point(0, 43);
            tabControl1.Margin = new Padding(3, 2, 3, 2);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(916, 533);
            tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(dgvDrawingList);
            tabPage1.Location = new Point(4, 54);
            tabPage1.Margin = new Padding(3, 2, 3, 2);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3, 2, 3, 2);
            tabPage1.Size = new Size(908, 475);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Files";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(dgvBelts);
            tabPage2.Location = new Point(4, 54);
            tabPage2.Margin = new Padding(3, 2, 3, 2);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3, 2, 3, 2);
            tabPage2.Size = new Size(908, 475);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Drawings";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvBelts
            // 
            dgvBelts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBelts.Dock = DockStyle.Fill;
            dgvBelts.Location = new Point(3, 2);
            dgvBelts.Margin = new Padding(3, 2, 3, 2);
            dgvBelts.MultiSelect = false;
            dgvBelts.Name = "dgvBelts";
            dgvBelts.RowHeadersWidth = 51;
            dgvBelts.Size = new Size(902, 471);
            dgvBelts.TabIndex = 0;
            // 
            // Form1
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1241, 576);
            Controls.Add(tabControl1);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(groupBox3);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Special Build Plastic Drawing PDM | Developed by Thomas Smith (DEV) | Steve Powers (SBP SME)";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvDrawingList).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDrawingAttributes).EndInit();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvBelts).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private DataGridView DrawingsGridView;
        private TextBox textBox1;
        private Label label1;
        private Button button1;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button button2;
        private GroupBox groupBox3;
        private DataGridView dgvDrawingAttributes;
        private GroupBox groupBox4;
        private FlowLayoutPanel flowLayoutPanel5;
        private DataGridView dgvDrawingList;
        private Label label3;
        private Label label2;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private DataGridView dgvBelts;
    }
}
