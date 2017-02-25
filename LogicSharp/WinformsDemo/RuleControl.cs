using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinformsDemo {
    public partial class RuleControl : UserControl {
        public string Rule { get { return ruleLabel.Text; } set { ruleLabel.Text = value; } }
        public string Info { get { return infoLabel.Text; } set { infoLabel.Text = value; } }
        public event EventHandler RuleSelect;

        public RuleControl() {
            InitializeComponent();
        }

        private void selectButton_Click(object sender, EventArgs e) {
            RuleSelect?.Invoke(this, e);
        }

        private void RuleControl_Resize(object sender, EventArgs e) {
            infoLabel.MaximumSize = new Size(Width - selectButton.Width, 0);
        }
    }
}
