using LogicSharp;
using System.Windows.Forms;

namespace WinformsDemo {
    public partial class InputLogicDialog : Form {
        public string Statement => inputTextBox.Text;
        Timer replaceTimer;

        public InputLogicDialog() {
            InitializeComponent();
            replaceTimer = new Timer();
            replaceTimer.Interval = 600;
            replaceTimer.Tick += ReplaceTimer_Tick;
        }

        private void inputTextBox_TextChanged(object sender, System.EventArgs e) {
            replaceTimer.Stop();
            replaceTimer.Start();
        }

        private void ReplaceTimer_Tick(object sender, System.EventArgs e) {
            ReplaceText();

            replaceTimer.Stop();
        }

        private void okButton_Click(object sender, System.EventArgs e) {
            ReplaceText(true);
        }

        void ReplaceText(bool replaceDash = false) {
            int loc = inputTextBox.SelectionStart;

            string text = inputTextBox.Text;
            text = text.Replace("=", Equivalence.Character.ToString());
            text = text.Replace("<->", Equivalence.Character.ToString());
            text = text.Replace("->", Implication.Character.ToString());
            text = text.Replace("^", Conjunction.Character.ToString());
            text = text.Replace("&", Conjunction.Character.ToString());
            text = text.Replace("v", Disjunction.Character.ToString());
            text = text.Replace("|", Disjunction.Character.ToString());
            text = text.Replace("!", LogicNode.NegationCharacter.ToString());
            if (replaceDash) text = text.Replace("-", LogicNode.NegationCharacter.ToString());
            text = text.Replace("~", LogicNode.NegationCharacter.ToString());

            inputTextBox.TextChanged -= inputTextBox_TextChanged;
            inputTextBox.Text = text;
            inputTextBox.SelectionStart = loc;
            inputTextBox.TextChanged += inputTextBox_TextChanged;
        }
    }
}
