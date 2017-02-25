namespace WinformsDemo {
    partial class RuleControl {
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
            this.ruleLabel = new System.Windows.Forms.Label();
            this.infoLabel = new System.Windows.Forms.Label();
            this.selectButton = new System.Windows.Forms.Button();
            this.lowerStrip = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.lowerStrip)).BeginInit();
            this.SuspendLayout();
            // 
            // ruleLabel
            // 
            this.ruleLabel.AutoEllipsis = true;
            this.ruleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ruleLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ruleLabel.Location = new System.Drawing.Point(0, 0);
            this.ruleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ruleLabel.Name = "ruleLabel";
            this.ruleLabel.Size = new System.Drawing.Size(336, 32);
            this.ruleLabel.TabIndex = 0;
            this.ruleLabel.Text = "Rule name";
            this.ruleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // infoLabel
            // 
            this.infoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.infoLabel.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoLabel.Location = new System.Drawing.Point(0, 32);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(240, 56);
            this.infoLabel.TabIndex = 1;
            this.infoLabel.Text = "3.rows-----------------\r\n23.col-----------------\r\n-----------------------";
            // 
            // selectButton
            // 
            this.selectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectButton.Location = new System.Drawing.Point(240, 48);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(96, 40);
            this.selectButton.TabIndex = 2;
            this.selectButton.Text = "Select";
            this.selectButton.UseVisualStyleBackColor = true;
            this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
            // 
            // lowerStrip
            // 
            this.lowerStrip.BackColor = System.Drawing.SystemColors.ControlDark;
            this.lowerStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lowerStrip.Location = new System.Drawing.Point(0, 88);
            this.lowerStrip.Name = "lowerStrip";
            this.lowerStrip.Size = new System.Drawing.Size(336, 8);
            this.lowerStrip.TabIndex = 3;
            this.lowerStrip.TabStop = false;
            // 
            // RuleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lowerStrip);
            this.Controls.Add(this.selectButton);
            this.Controls.Add(this.infoLabel);
            this.Controls.Add(this.ruleLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "RuleControl";
            this.Size = new System.Drawing.Size(336, 96);
            ((System.ComponentModel.ISupportInitialize)(this.lowerStrip)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label ruleLabel;
        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.Button selectButton;
        private System.Windows.Forms.PictureBox lowerStrip;
    }
}
