using LogicSharp.Rules;
using System.Collections.Generic;

namespace LogicSharp {
    public abstract class Statement {
        public Scope Scope { get; private set; }
        public LogicNode Node { get; private set; }
        public int Index { get; private set; }

        protected Statement(Scope scope, LogicNode node, int index) {
            Scope = scope;
            Node = node;
            Index = index;
        }

        public abstract string GetSourceString();
    }

    public class GivenStmt : Statement {
        public bool Given => true;

        public GivenStmt(Scope scope, LogicNode node, int index)
            : base(scope, node, index) { }

        public override string GetSourceString() => "Given";
    }

    public class ResultStmt : Statement {
        public LogicRule RuleUsed { get; private set; }
        public int SourceIndex0 { get; private set; }
        public int SourceIndex1 { get; private set; }
        public LogicNode SourceNode { get; private set; }
        public IEnumerable<int> SourceIndices {
            get {
                List<int> list = new List<int>();
                if (SourceIndex0 > 0) list.Add(SourceIndex0);
                if (SourceIndex1 > 0) list.Add(SourceIndex1);
                return list;
            }
        }
        public IEnumerable<Statement> SourceStmts => Scope.GetStatements(SourceIndices);

        public ResultStmt(Scope scope, LogicNode node, int index, LogicRule ruleUsed, int sourceIndex)
            : base(scope, node, index) {
            RuleUsed = ruleUsed;
            SourceIndex0 = sourceIndex;
            SourceIndex1 = -1;
        }

        public ResultStmt(Scope scope, LogicNode node, int index, LogicRule ruleUsed, int sourceIndex, LogicNode sourceNode)
            : base(scope, node, index) {
            RuleUsed = ruleUsed;
            SourceIndex0 = sourceIndex;
            SourceIndex1 = -1;
            SourceNode = sourceNode;
        }

        public ResultStmt(Scope scope, LogicNode node, int index, LogicRule ruleUsed, int sourceIndex0, int sourceIndex1)
            : base(scope, node, index) {
            RuleUsed = ruleUsed;
            SourceIndex0 = sourceIndex0;
            SourceIndex1 = sourceIndex1;
        }

        public override string GetSourceString() {
            string str = RuleUsed.Abbreviation + ", " + (SourceIndex0 + 1);

            if (SourceIndex1 >= 0) str += ", " + (SourceIndex1 + 1);
            else if (SourceNode != null) str += ", " + SourceNode;

            return str;
        }
    }
}
