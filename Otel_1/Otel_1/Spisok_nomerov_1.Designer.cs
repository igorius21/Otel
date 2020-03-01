namespace Otel_1
{
    partial class Spisok_nomerov_1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Spisok_nomerov_1));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.id_номера = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Тип_номера = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Цена_номера_в_сутки = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Свободен_Занят = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_номера,
            this.Тип_номера,
            this.Цена_номера_в_сутки,
            this.Свободен_Занят});
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(434, 571);
            this.dataGridView1.TabIndex = 0;
            // 
            // id_номера
            // 
            this.id_номера.HeaderText = "id_номера";
            this.id_номера.Name = "id_номера";
            this.id_номера.ReadOnly = true;
            this.id_номера.Width = 60;
            // 
            // Тип_номера
            // 
            this.Тип_номера.HeaderText = "Тип_номера";
            this.Тип_номера.Name = "Тип_номера";
            this.Тип_номера.ReadOnly = true;
            this.Тип_номера.Width = 80;
            // 
            // Цена_номера_в_сутки
            // 
            this.Цена_номера_в_сутки.HeaderText = "Цена_номера_в_сутки";
            this.Цена_номера_в_сутки.Name = "Цена_номера_в_сутки";
            this.Цена_номера_в_сутки.ReadOnly = true;
            this.Цена_номера_в_сутки.Width = 130;
            // 
            // Свободен_Занят
            // 
            this.Свободен_Занят.HeaderText = "Свободен_Занят";
            this.Свободен_Занят.Name = "Свободен_Занят";
            this.Свободен_Занят.ReadOnly = true;
            this.Свободен_Занят.Width = 120;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(470, 44);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 48);
            this.button1.TabIndex = 1;
            this.button1.Text = "Обновить базу";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Spisok_nomerov_1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 595);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Spisok_nomerov_1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Список номеров";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_номера;
        private System.Windows.Forms.DataGridViewTextBoxColumn Тип_номера;
        private System.Windows.Forms.DataGridViewTextBoxColumn Цена_номера_в_сутки;
        private System.Windows.Forms.DataGridViewTextBoxColumn Свободен_Занят;
    }
}