using System;
using System.Windows.Forms;

namespace WinformsDemo {
    public partial class RuleControl : UserControl {
        public string Rule { get { return ruleLabel.Text; } set { ruleLabel.Text = value; } }
        public string Info { get { return infoLabel.Text; } set { infoLabel.Text = value; } }
        public event EventHandler RuleSelect;

        public RuleControl() {
            InitializeComponent();
        }

        private void selectButton_Click(object sender, System.EventArgs e) {
            RuleSelect?.Invoke(this, e);
        }
    }
}
