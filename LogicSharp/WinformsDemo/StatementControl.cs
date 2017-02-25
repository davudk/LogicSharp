using LogicSharp;
using System.Drawing;
using System.Windows.Forms;
using System;

namespace WinformsDemo {
    public partial class StatementControl : UserControl {
        Statement stmt;
        public Statement Statement {
            get { return stmt; }
            set {
                stmt = value;
                if (stmt != null) {
                    indexLabel.Text = (stmt.Index + 1).ToString();
                    statementLabel.Text = stmt.Node.ToString();
                    sourceLabel.Text = stmt.GetSourceString();
                }
            }
        }
        static readonly Color DefaultColor = Color.FromArgb(255,255,255);
        static readonly Color HighlightColor = Color.FromArgb(224, 224, 224);
        static readonly Color SelectedColor = Color.FromArgb(192, 192, 192);
        bool mouseOver;

        public StatementControl() {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            TabStop = true;

            MouseEnter += StatementControl_MouseEnter;
            indexLabel.MouseEnter += StatementControl_MouseEnter;
            statementLabel.MouseEnter += StatementControl_MouseEnter;
            sourceLabel.MouseEnter += StatementControl_MouseEnter;

            MouseLeave += StatementControl_MouseLeave;
            indexLabel.MouseLeave += StatementControl_MouseLeave;
            statementLabel.MouseLeave += StatementControl_MouseLeave;
            sourceLabel.MouseLeave += StatementControl_MouseLeave;

            Click += StatementControl_Click;
            indexLabel.Click += StatementControl_Click;
            statementLabel.Click += StatementControl_Click;
            sourceLabel.Click += StatementControl_Click;
        }

        private void StatementControl_MouseEnter(object sender, EventArgs e) {
            mouseOver = true;
            BackColor = ContainsFocus ? SelectedColor : HighlightColor;
        }

        private void StatementControl_MouseLeave(object sender, EventArgs e) {
            mouseOver = false;
            BackColor = ContainsFocus ? SelectedColor : SystemColors.Window;
        }

        protected override void OnGotFocus(EventArgs e) {
            BackColor = SelectedColor;
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e) {
            BackColor = mouseOver ? HighlightColor : DefaultColor;
            base.OnLostFocus(e);
        }

        private void StatementControl_Click(object sender, EventArgs e) {
            Focus();
        }
        
        private void StatementControl_Resize(object sender, EventArgs e) {
            statementLabel.MinimumSize = new Size(Width - 240, 40);
            statementLabel.MaximumSize = new Size(Width - 240, 0);
        }
    }
}
