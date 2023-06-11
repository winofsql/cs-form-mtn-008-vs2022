namespace cs_form_mtn_008_vs2022
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            更新ボタン = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(24, 84);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(751, 339);
            dataGridView1.TabIndex = 0;
            // 
            // 更新ボタン
            // 
            更新ボタン.Enabled = false;
            更新ボタン.Location = new Point(24, 30);
            更新ボタン.Name = "更新ボタン";
            更新ボタン.Size = new Size(137, 36);
            更新ボタン.TabIndex = 1;
            更新ボタン.Text = "更新";
            更新ボタン.UseVisualStyleBackColor = true;
            更新ボタン.Click += 更新ボタン_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(更新ボタン);
            Controls.Add(dataGridView1);
            KeyPreview = true;
            Name = "Form2";
            StartPosition = FormStartPosition.CenterParent;
            Text = "一括更新";
            Shown += Form2_Shown;
            KeyDown += Form2_KeyDown;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private Button 更新ボタン;
    }
}