namespace Projekt_Damian_I_Marek
{
    partial class Clues
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
            this.Clues_table = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // Clues_table
            // 
            this.Clues_table.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(7)))), ((int)(((byte)(17)))));
            this.Clues_table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Clues_table.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Clues_table.ForeColor = System.Drawing.Color.Gainsboro;
            this.Clues_table.Location = new System.Drawing.Point(0, 0);
            this.Clues_table.Margin = new System.Windows.Forms.Padding(4);
            this.Clues_table.Name = "Clues_table";
            this.Clues_table.Size = new System.Drawing.Size(744, 865);
            this.Clues_table.TabIndex = 0;
            this.Clues_table.UseCompatibleStateImageBehavior = false;
            this.Clues_table.View = System.Windows.Forms.View.Details;
            this.Clues_table.SelectedIndexChanged += new System.EventHandler(this.Clues_table_SelectedIndexChanged);
            // 
            // Clues
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 865);
            this.Controls.Add(this.Clues_table);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Clues";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView Clues_table;
    }
}