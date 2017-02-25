using LogicSharp;
using LogicSharp.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WinformsDemo {
    public partial class MainWindow : Form {

        [DllImport("user32")]
        private static extern long ShowScrollBar(IntPtr hwnd, int wBar, bool bShow);

        Scope scope;
        List<GivenStmt> given;
        LogicNode conclusion;
        int selectedIndex = -1;

        public MainWindow() {
            InitializeComponent();            
            splitContainer.Panel1MinSize += SystemInformation.VerticalScrollBarWidth;
            given = new List<GivenStmt>();
        }

        private void addAxiomButton_Click(object sender, EventArgs e) {

            addAnotherAxiom:
            InputLogicDialog dialog = new InputLogicDialog();
            if (dialog.ShowDialog() == DialogResult.OK) {
                string stmt = dialog.Statement;
                if (!string.IsNullOrWhiteSpace(stmt)) {
                    LogicNode node = LogicNode.Parse(stmt);
                    if (node == null) MessageBox.Show("Failed to parse input.");
                    else {                        
                        given.Add(new GivenStmt(null, node, given.Count));
                        statementPanel.SetStatements(given);
                    }
                    goto addAnotherAxiom;
                }
            }

            beginButton.Enabled = given.Count > 0 && conclusion != null;
        }

        private void setConclusionButton_Click(object sender, EventArgs e) {
            InputLogicDialog dialog = new InputLogicDialog();
            if (dialog.ShowDialog() == DialogResult.OK) {
                string stmt = dialog.Statement;
                LogicNode node = LogicNode.Parse(stmt);
                if (node == null) MessageBox.Show("Failed to parse input.");
                else {
                    conclusion = node;
                }
                conclusionLabel.Text = conclusion.ToString();
            }

            beginButton.Enabled = given.Count > 0 && conclusion != null;
        }

        private void beginButton_Click(object sender, EventArgs e) {
            try {
                scope = new Scope(given.Select((stmt) => stmt.Node), conclusion);
                addAxiomButton.Enabled = false;
                setConclusionButton.Enabled = false;
                beginButton.Enabled = false;

                statementPanel.SetStatements(scope.Statements);
                //List<Statement> stmts = new List<Statement>(scope.Statements);

                //for (int i = 0; i < statementPanel.Controls.Count; i++) {
                //    (statementPanel.Controls[i] as StatementControl).Statement = stmts[i];
                //}
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void rulePanel_Resize(object sender, EventArgs e) {
            int y = 0;
            foreach (Control c in rulePanel.Controls) {
                c.Top = y;
                c.Width = rulePanel.Width - SystemInformation.VerticalScrollBarWidth;
                y = c.Bottom;
            }
            ShowScrollBar(rulePanel.Handle, 0 /* SB_HORZ */, false);
        }

        private void statementPanel_SelectedStatementChanged(object sender, int e) {
            selectedIndex = e;
            if (!scope.Solved) {
                UpdateRules();
            }
        }

        static LogicRule[] simpleRules = {
            ModusPonensRule.Instance,
            ModusTolensRule.Instance,
            HypotheticalSyllogismRule.Instance,
            DisjunctiveSyllogismRule.Instance,
            ConstructiveDilemmaRule.Instance,
            AbsorptionRule.Instance,
            SimplificationRule.Instance,
            ConjunctionRule.Instance
        };
        void UpdateRules() {
            rulePanel.Controls.Clear();

            if (scope.Solved) {
                MessageBox.Show("Solved!");
                return;
            }

            if (selectedIndex < 0) return;

            Statement selected = scope.Statements.ElementAt(selectedIndex);
            // simple rules (1-7)
            foreach (var rule in simpleRules) {
                AddSimpleRuleControl(rule, selected);
                foreach (Statement stmt in scope.Statements) {
                    if (stmt == selected)
                        continue;
                    AddSimpleRuleControl(rule, stmt, selected);
                    AddSimpleRuleControl(rule, selected, stmt);
                }
            }

            // conjunction (8)

            // addition (9)
        }

        void AddSimpleRuleControl(LogicRule rule, params Statement[] stmts) {
            LogicNode result = rule.Do(stmts.Select((s) => s.Node).ToArray());            
            if (result == null) return;
            if (scope.Statements.Count((stmt) => stmt.Node.Equals(result)) > 0) return;

            RuleControl ruleControl = new RuleControl();
            ruleControl.Rule = rule.Name;
            string info = rule.Abbreviation;
            foreach (var s in stmts) {
                info += ", " + (s.Index + 1);
            }
            info += Environment.NewLine + result.ToString();
            ruleControl.Info = info;
            //ruleControl.Tag = result;
            ruleControl.RuleSelect += (s, e) => {
                bool success = false;
                if (stmts.Length == 1) success = scope.TryRule(rule, stmts[0].Index);
                else if (stmts.Length == 2) success = scope.TryRule(rule, stmts[0].Index, stmts[1].Index);
                if (success) {
                    statementPanel.SetStatements(scope.Statements);
                    UpdateRules();
                } else {
                    MessageBox.Show("Failed to apply rule.");
                }
            };

            ruleControl.Top = rulePanel.Controls.Count > 0 ? rulePanel.Controls[rulePanel.Controls.Count - 1].Bottom : 0;
            ruleControl.Width = rulePanel.Width - SystemInformation.VerticalScrollBarWidth;
            rulePanel.Controls.Add(ruleControl);
        }
    }
}
