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
        List<LogicNode> axioms;
        LogicNode conclusion;
        StatementControl selected;

        public MainWindow() {
            InitializeComponent();            
            splitContainer.Panel1MinSize += SystemInformation.VerticalScrollBarWidth;
            axioms = new List<LogicNode>();
        }

        protected override void OnShown(EventArgs e) {
            MessageBox.Show("This was written fast, so don't expect it to be efficient.");
            base.OnShown(e);
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
                        StatementControl stmtControl = new StatementControl();
                        stmtControl.Statement = new GivenStmt(null, node, axioms.Count);
                        stmtControl.Top = statementPanel.Controls.Count > 0 ? statementPanel.Controls[statementPanel.Controls.Count - 1].Bottom : 0;
                        stmtControl.Width = statementPanel.Width - SystemInformation.VerticalScrollBarWidth;
                        statementPanel.Controls.Add(stmtControl);

                        axioms.Add(node);
                        goto addAnotherAxiom;
                    }
                }
            }

            beginButton.Enabled = axioms.Count > 0 && conclusion != null;
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

            beginButton.Enabled = axioms.Count > 0 && conclusion != null;
        }

        private void beginButton_Click(object sender, EventArgs e) {
            try {
                scope = new Scope(axioms, conclusion);
                addAxiomButton.Enabled = false;
                setConclusionButton.Enabled = false;
                beginButton.Enabled = false;

                RefreshStatements();
                //List<Statement> stmts = new List<Statement>(scope.Statements);

                //for (int i = 0; i < statementPanel.Controls.Count; i++) {
                //    (statementPanel.Controls[i] as StatementControl).Statement = stmts[i];
                //}
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void statementPanel_Resize(object sender, EventArgs e) {
            int y = 0;
            foreach (Control c in statementPanel.Controls) {
                c.Top = y;
                c.Width = statementPanel.Width - SystemInformation.VerticalScrollBarWidth;
                y = c.Bottom;
            }

            statementPanel.HorizontalScroll.Visible = false;
            ShowScrollBar(statementPanel.Handle, 0 /* SB_HORZ */, false);
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

        private void StmtControl_GotFocus(object sender, EventArgs e) {
            StatementControl stmtControl = sender as StatementControl;
            if (stmtControl == null) return;
            selected = stmtControl;

            UpdateRules();
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
            
            // simple rules (1-7)
            foreach (var rule in simpleRules) {
                AddSimpleRuleControl(rule, selected.Statement);
                foreach (Statement stmt in scope.Statements) {
                    if (stmt == selected.Statement)
                        continue;
                    AddSimpleRuleControl(rule, stmt, selected.Statement);
                    AddSimpleRuleControl(rule, selected.Statement, stmt);
                }
            }

            // conjunction (8)

            // addition (9)
        }

        void AddSimpleRuleControl(LogicRule rule, params Statement[] stmts) {
            LogicNode result = rule.Do(stmts.Select((s) => s.Node).ToArray());            
            if (result == null) return;

            RuleControl ruleControl = new RuleControl();
            ruleControl.Rule = rule.Name;
            string info = rule.Abbreviation;
            foreach (var s in stmts) {
                info += ", " + (s.Index + 1);
            }
            info += Environment.NewLine + result.ToString();
            ruleControl.Info = info;
            ruleControl.Tag = result;
            ruleControl.RuleSelect += (s, e) => {
                bool success = false;
                if (stmts.Length == 1) success = scope.TryRule(rule, stmts[0].Index);
                else if (stmts.Length == 2) success = scope.TryRule(rule, stmts[0].Index, stmts[1].Index);
                if (success) {
                    RefreshStatements();
                    UpdateRules();
                } else {
                    MessageBox.Show("Failed to apply rule.");
                }
            };

            ruleControl.Top = rulePanel.Controls.Count > 0 ? rulePanel.Controls[rulePanel.Controls.Count - 1].Bottom : 0;
            ruleControl.Width = rulePanel.Width - SystemInformation.VerticalScrollBarWidth;
            rulePanel.Controls.Add(ruleControl);
        }

        void RefreshStatements() {
            foreach (StatementControl stmtControl in statementPanel.Controls) {
                stmtControl.GotFocus -= StmtControl_GotFocus;
            }
            statementPanel.Controls.Clear();

            foreach (var stmt in scope.Statements) {
                StatementControl stmtControl = new StatementControl();
                stmtControl.Statement = stmt;
                stmtControl.Top = statementPanel.Controls.Count > 0 ? statementPanel.Controls[statementPanel.Controls.Count - 1].Bottom : 0;
                stmtControl.Width = statementPanel.Width - SystemInformation.VerticalScrollBarWidth;
                stmtControl.GotFocus += StmtControl_GotFocus;
                statementPanel.Controls.Add(stmtControl);
            }

            if (scope.Solved) {
                MessageBox.Show("Solved!");
                rulePanel.Enabled = false;
            }
        }
    }
}
