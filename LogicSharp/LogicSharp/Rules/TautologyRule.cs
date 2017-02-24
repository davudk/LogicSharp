namespace LogicSharp.Rules {
    public enum TautologyExpandMode {
        ToConjunction,
        ToDisjunction
    }
    public class TautologyRule : LogicRule {
        public static readonly TautologyRule Instance = new TautologyRule();
        public override string Name => "Tautology";
        public override string Abbreviation => "Taut";

        private TautologyRule() { }

        public override LogicNode Do(params LogicNode[] args) {
            if (args.Length != 1) return null;
            if (args[0].Type == NodeType.Conjunction) return Perform(args[0] as Conjunction);
            else if (args[0].Type == NodeType.Disjunction) return Perform(args[0] as Disjunction);
            return Perform(args[0] as Implication, TautologyExpandMode.ToConjunction);
        }

        public static LogicNode Perform(LogicNode node, TautologyExpandMode mode) {
            if (node == null) return null;

            if (mode == TautologyExpandMode.ToConjunction) {
                return new Conjunction(node.Clone(), node.Clone());
            } else if (mode == TautologyExpandMode.ToDisjunction) {
                return new Disjunction(node.Clone(), node.Clone());
            }

            return null;
        }

        public static LogicNode Perform(Conjunction conj) {
            if (conj == null || conj.Negations > 0) return null;

            if (conj.Left.Equals(conj.Right))
                return conj.Left.Clone();

            return null;
        }

        public static LogicNode Perform(Disjunction disj) {
            if (disj == null || disj.Negations > 0) return null;

            if (disj.Left.Equals(disj.Right))
                return disj.Left.Clone();

            return null;
        }
    }
}
