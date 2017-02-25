namespace WinformsDemo {
    partial class MainWindow {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.statementPanel = new System.Windows.Forms.Panel();
            this.controlPanel = new System.Windows.Forms.Panel();
            this.beginButton = new System.Windows.Forms.Button();
            this.setConclusionButton = new System.Windows.Forms.Button();
            this.addAxiomButton = new System.Windows.Forms.Button();
            this.conclusionPanel = new System.Windows.Forms.Panel();
            this.conclusionLabel = new System.Windows.Forms.Label();
            this.conclusionPromptLabel = new System.Windows.Forms.Label();
            this.rulePanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.controlPanel.SuspendLayout();
            this.conclusionPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.statementPanel);
            this.splitContainer.Panel1.Controls.Add(this.controlPanel);
            this.splitContainer.Panel1.Controls.Add(this.conclusionPanel);
            this.splitContainer.Panel1MinSize = 640;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer.Panel2.Controls.Add(this.rulePanel);
            this.splitContainer.Size = new System.Drawing.Size(1008, 602);
            this.splitContainer.SplitterDistance = 640;
            this.splitContainer.SplitterWidth = 6;
            this.splitContainer.TabIndex = 0;
            // 
            // statementPanel
            // 
            this.statementPanel.AutoScroll = true;
            this.statementPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.statementPanel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.statementPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statementPanel.Location = new System.Drawing.Point(0, 50);
            this.statementPanel.Name = "statementPanel";
            this.statementPanel.Size = new System.Drawing.Size(640, 482);
            this.statementPanel.TabIndex = 2;
            this.statementPanel.Resize += new System.EventHandler(this.statementPanel_Resize);
            // 
            // controlPanel
            // 
            this.controlPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.controlPanel.Controls.Add(this.beginButton);
            this.controlPanel.Controls.Add(this.setConclusionButton);
            this.controlPanel.Controls.Add(this.addAxiomButton);
            this.controlPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlPanel.Location = new System.Drawing.Point(0, 0);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Size = new System.Drawing.Size(640, 50);
            this.controlPanel.TabIndex = 1;
            // 
            // beginButton
            // 
            this.beginButton.Enabled = false;
            this.beginButton.Location = new System.Drawing.Point(504, 8);
            this.beginButton.Name = "beginButton";
            this.beginButton.Size = new System.Drawing.Size(128, 32);
            this.beginButton.TabIndex = 3;
            this.beginButton.Text = "Begin";
            this.beginButton.UseVisualStyleBackColor = true;
            this.beginButton.Click += new System.EventHandler(this.beginButton_Click);
            // 
            // setConclusionButton
            // 
            this.setConclusionButton.Location = new System.Drawing.Point(144, 8);
            this.setConclusionButton.Name = "setConclusionButton";
            this.setConclusionButton.Size = new System.Drawing.Size(160, 32);
            this.setConclusionButton.TabIndex = 1;
            this.setConclusionButton.Text = "Set conclusion";
            this.setConclusionButton.UseVisualStyleBackColor = true;
            this.setConclusionButton.Click += new System.EventHandler(this.setConclusionButton_Click);
            // 
            // addAxiomButton
            // 
            this.addAxiomButton.Location = new System.Drawing.Point(8, 8);
            this.addAxiomButton.Name = "addAxiomButton";
            this.addAxiomButton.Size = new System.Drawing.Size(128, 32);
            this.addAxiomButton.TabIndex = 0;
            this.addAxiomButton.Text = "Add axiom(s)";
            this.addAxiomButton.UseVisualStyleBackColor = true;
            this.addAxiomButton.Click += new System.EventHandler(this.addAxiomButton_Click);
            // 
            // conclusionPanel
            // 
            this.conclusionPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.conclusionPanel.Controls.Add(this.conclusionLabel);
            this.conclusionPanel.Controls.Add(this.conclusionPromptLabel);
            this.conclusionPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.conclusionPanel.Location = new System.Drawing.Point(0, 532);
            this.conclusionPanel.Name = "conclusionPanel";
            this.conclusionPanel.Size = new System.Drawing.Size(640, 70);
            this.conclusionPanel.TabIndex = 0;
            // 
            // conclusionLabel
            // 
            this.conclusionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.conclusionLabel.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.conclusionLabel.Location = new System.Drawing.Point(0, 30);
            this.conclusionLabel.Name = "conclusionLabel";
            this.conclusionLabel.Size = new System.Drawing.Size(640, 40);
            this.conclusionLabel.TabIndex = 1;
            this.conclusionLabel.Text = "Undefined";
            this.conclusionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // conclusionPromptLabel
            // 
            this.conclusionPromptLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.conclusionPromptLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.conclusionPromptLabel.Location = new System.Drawing.Point(0, 0);
            this.conclusionPromptLabel.Name = "conclusionPromptLabel";
            this.conclusionPromptLabel.Size = new System.Drawing.Size(640, 30);
            this.conclusionPromptLabel.TabIndex = 0;
            this.conclusionPromptLabel.Text = "Conclusion:";
            this.conclusionPromptLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rulePanel
            // 
            this.rulePanel.AutoScroll = true;
            this.rulePanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.rulePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rulePanel.Location = new System.Drawing.Point(0, 0);
            this.rulePanel.Name = "rulePanel";
            this.rulePanel.Size = new System.Drawing.Size(362, 602);
            this.rulePanel.TabIndex = 0;
            this.rulePanel.Resize += new System.EventHandler(this.rulePanel_Resize);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 602);
            this.Controls.Add(this.splitContainer);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(1024, 640);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LogicSharp - WinformsDemo";
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.controlPanel.ResumeLayout(false);
            this.conclusionPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Panel conclusionPanel;
        private System.Windows.Forms.Label conclusionLabel;
        private System.Windows.Forms.Label conclusionPromptLabel;
        private System.Windows.Forms.Panel controlPanel;
        private System.Windows.Forms.Panel statementPanel;
        private System.Windows.Forms.Button addAxiomButton;
        private System.Windows.Forms.Button beginButton;
        private System.Windows.Forms.Button setConclusionButton;
        private System.Windows.Forms.Panel rulePanel;
    }
}

