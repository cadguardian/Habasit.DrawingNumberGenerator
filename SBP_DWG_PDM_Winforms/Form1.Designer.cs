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
            propertyGrid1 = new PropertyGrid();
            DrawingsGridView = new DataGridView();
            textBox1 = new TextBox();
            label1 = new Label();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            label2 = new Label();
            Status = new Label();
            button1 = new Button();
            propertyGrid2 = new PropertyGrid();
            ((System.ComponentModel.ISupportInitialize)DrawingsGridView).BeginInit();
            SuspendLayout();
            // 
            // propertyGrid1
            // 
            propertyGrid1.Location = new Point(12, 12);
            propertyGrid1.Name = "propertyGrid1";
            propertyGrid1.Size = new Size(224, 433);
            propertyGrid1.TabIndex = 0;
            // 
            // DrawingsGridView
            // 
            DrawingsGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DrawingsGridView.Location = new Point(242, 151);
            DrawingsGridView.Name = "DrawingsGridView";
            DrawingsGridView.RowHeadersWidth = 51;
            DrawingsGridView.Size = new Size(551, 294);
            DrawingsGridView.TabIndex = 1;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(298, 5);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(416, 27);
            textBox1.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(242, 12);
            label1.Name = "label1";
            label1.Size = new Size(53, 20);
            label1.TabIndex = 3;
            label1.Text = "Search";
            // 
            // groupBox1
            // 
            groupBox1.Location = new Point(242, 38);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(280, 107);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Request";
            // 
            // groupBox2
            // 
            groupBox2.Location = new Point(528, 38);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(265, 107);
            groupBox2.TabIndex = 5;
            groupBox2.TabStop = false;
            groupBox2.Text = "Delivered";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 458);
            label2.Name = "label2";
            label2.Size = new Size(50, 20);
            label2.TabIndex = 6;
            label2.Text = "label2";
            // 
            // Status
            // 
            Status.AutoSize = true;
            Status.Location = new Point(937, 458);
            Status.Name = "Status";
            Status.Size = new Size(49, 20);
            Status.TabIndex = 7;
            Status.Text = "Status";
            // 
            // button1
            // 
            button1.Location = new Point(720, 4);
            button1.Name = "button1";
            button1.Size = new Size(72, 29);
            button1.TabIndex = 8;
            button1.Text = "Enter";
            button1.UseVisualStyleBackColor = true;
            // 
            // propertyGrid2
            // 
            propertyGrid2.Location = new Point(808, 5);
            propertyGrid2.Name = "propertyGrid2";
            propertyGrid2.Size = new Size(182, 440);
            propertyGrid2.TabIndex = 9;
            // 
            // Form1
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(998, 487);
            Controls.Add(propertyGrid2);
            Controls.Add(button1);
            Controls.Add(Status);
            Controls.Add(label2);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Controls.Add(DrawingsGridView);
            Controls.Add(propertyGrid1);
            Name = "Form1";
            Text = "Special Build Plastic Drawing PDM";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)DrawingsGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PropertyGrid propertyGrid1;
        private DataGridView DrawingsGridView;
        private TextBox textBox1;
        private Label label1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label2;
        private Label Status;
        private Button button1;
        private PropertyGrid propertyGrid2;
    }
}
