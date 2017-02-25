namespace WinformsDemo {
    partial class StatementControl {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.sourceLabel = new System.Windows.Forms.Label();
            this.statementLabel = new System.Windows.Forms.Label();
            this.indexLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // sourceLabel
            // 
            this.sourceLabel.AutoEllipsis = true;
            this.sourceLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.sourceLabel.Location = new System.Drawing.Point(440, 0);
            this.sourceLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.sourceLabel.Name = "sourceLabel";
            this.sourceLabel.Size = new System.Drawing.Size(160, 40);
            this.sourceLabel.TabIndex = 0;
            this.sourceLabel.Text = "source";
            this.sourceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // statementLabel
            // 
            this.statementLabel.AutoSize = true;
            this.statementLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statementLabel.Location = new System.Drawing.Point(80, 0);
            this.statementLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.statementLabel.MaximumSize = new System.Drawing.Size(360, 0);
            this.statementLabel.MinimumSize = new System.Drawing.Size(360, 40);
            this.statementLabel.Name = "statementLabel";
            this.statementLabel.Size = new System.Drawing.Size(360, 40);
            this.statementLabel.TabIndex = 1;
            this.statementLabel.Text = "statement";
            this.statementLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // indexLabel
            // 
            this.indexLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.indexLabel.Location = new System.Drawing.Point(0, 0);
            this.indexLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.indexLabel.Name = "indexLabel";
            this.indexLabel.Size = new System.Drawing.Size(80, 40);
            this.indexLabel.TabIndex = 2;
            this.indexLabel.Text = "index";
            this.indexLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StatementControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.statementLabel);
            this.Controls.Add(this.indexLabel);
            this.Controls.Add(this.sourceLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.MinimumSize = new System.Drawing.Size(600, 40);
            this.Name = "StatementControl";
            this.Size = new System.Drawing.Size(600, 40);
            this.Resize += new System.EventHandler(this.StatementControl_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label sourceLabel;
        private System.Windows.Forms.Label statementLabel;
        private System.Windows.Forms.Label indexLabel;
    }
}
