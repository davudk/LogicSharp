using LogicSharp.Rules;
using System;
using System.Collections.Generic;

namespace LogicSharp {
    public class Scope {
        List<Statement> statements;
        public IEnumerable<Statement> Statements => new List<Statement>(statements);
        public int Count => statements.Count;
        public LogicNode Conclusion { get; private set; }
        public bool Solved { get; private set; }

        public Scope(IEnumerable<LogicNode> given, LogicNode conclusion) {
            if (given == null) throw new ArgumentNullException("given");
            else if (conclusion == null) throw new ArgumentNullException("conclusion");

            statements = new List<Statement>();
            foreach (LogicNode node in given) {
                statements.Add(new GivenStmt(this, node, statements.Count));
                if (node.Equals(conclusion))
                    throw new ArgumentException("conclusion", "Cannot be given.");
            }

            if (statements.Count == 0) throw new ArgumentException("given", "Cannot be empty.");

            Conclusion = conclusion;
        }

        public bool TryRule(LogicRule rule, int argIndex) {
            if (Solved) return true;
            if (rule == null || !IndexInBounds(argIndex)) return false;

            LogicNode result = rule.Do(statements[argIndex].Node);
            if (result == null) return false;
            AddResult(new ResultStmt(this, result, statements.Count, rule, argIndex));
            return true;
        }

        public bool TryRule(LogicRule rule, int argIndex, LogicNode argNode) {
            if (Solved) return true;
            if (rule == null || !IndexInBounds(argIndex)) return false;

            LogicNode result = rule.Do(statements[argIndex].Node, argNode);
            if (result == null) return false;
            AddResult(new ResultStmt(this, result, statements.Count, rule, argIndex, argNode));
            return true;
        }

        public bool TryRule(LogicRule rule, int argIndex0, int argIndex1) {
            if (Solved) return true;
            if (rule == null || !IndexInBounds(argIndex0) || !IndexInBounds(argIndex1)) return false;

            LogicNode result = rule.Do(statements[argIndex0].Node, statements[argIndex1].Node);
            if (result == null) return false;
            AddResult(new ResultStmt(this, result, statements.Count, rule, argIndex0, argIndex1));
            return true;
        }

        public IEnumerable<Statement> GetStatements(IEnumerable<int> indices) {
            foreach (int index in indices) {
                if (IndexInBounds(index)) yield return statements[index];
                else throw new ArgumentException("One or more indices were out of bounds.");
            }
        }

        void AddResult(ResultStmt result) {
            foreach (Statement stmt in statements) {
                if (stmt.Node.Equals(result.Node)) return;
            }
            statements.Add(result);
            if (result.Node.Equals(Conclusion)) Solved = true;
        }

        bool IndexInBounds(int index) => index >= 0 && index < statements.Count;
    }
}
