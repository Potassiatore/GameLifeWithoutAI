namespace GiocoDellaVitaFattoMeglio
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
            label1 = new Label();
            MappaDiGioco = new TableLayoutPanel();
            ChatDiAggiornamento = new TableLayoutPanel();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Verdana", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(220, 29);
            label1.TabIndex = 0;
            label1.Text = "Gioco della Vita";
            label1.Click += label1_Click;
            // 
            // MappaDiGioco
            // 
            MappaDiGioco.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
            MappaDiGioco.ColumnCount = 6;
            MappaDiGioco.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            MappaDiGioco.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            MappaDiGioco.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            MappaDiGioco.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            MappaDiGioco.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            MappaDiGioco.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            MappaDiGioco.Location = new Point(293, 41);
            MappaDiGioco.Name = "MappaDiGioco";
            MappaDiGioco.RowCount = 6;
            MappaDiGioco.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6666679F));
            MappaDiGioco.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6666679F));
            MappaDiGioco.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6666679F));
            MappaDiGioco.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6666679F));
            MappaDiGioco.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6666679F));
            MappaDiGioco.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6666679F));
            MappaDiGioco.Size = new Size(527, 341);
            MappaDiGioco.TabIndex = 1;
            MappaDiGioco.Paint += MappaDiGioco_Paint;
            // 
            // ChatDiAggiornamento
            // 
            ChatDiAggiornamento.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
            ChatDiAggiornamento.ColumnCount = 1;
            ChatDiAggiornamento.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            ChatDiAggiornamento.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            ChatDiAggiornamento.Location = new Point(12, 41);
            ChatDiAggiornamento.Name = "ChatDiAggiornamento";
            ChatDiAggiornamento.RowCount = 8;
            ChatDiAggiornamento.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            ChatDiAggiornamento.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            ChatDiAggiornamento.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            ChatDiAggiornamento.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            ChatDiAggiornamento.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            ChatDiAggiornamento.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            ChatDiAggiornamento.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            ChatDiAggiornamento.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
            ChatDiAggiornamento.Size = new Size(257, 341);
            ChatDiAggiornamento.TabIndex = 2;
            ChatDiAggiornamento.Paint += ChatDiAggiornamento_Paint;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Highlight;
            ClientSize = new Size(832, 459);
            Controls.Add(ChatDiAggiornamento);
            Controls.Add(MappaDiGioco);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TableLayoutPanel MappaDiGioco;
        private TableLayoutPanel ChatDiAggiornamento;
    }
}