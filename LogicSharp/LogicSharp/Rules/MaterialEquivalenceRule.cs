namespace LogicSharp.Rules {
    public enum MaterialEquivalenceExpandMode {
        ToConjunction,
        ToDisjunction
    }
    public class MaterialEquivalenceRule : LogicRule {
        public static readonly MaterialEquivalenceRule Instance = new MaterialEquivalenceRule();
        public override string Name => "Material Equivalence";
        public override string Abbreviation => "MEquiv";

        private MaterialEquivalenceRule() { }

        public override LogicNode Do(params LogicNode[] args) {
            if (args.Length != 1) return null;
            if (args[0].Type == NodeType.Conjunction) return Perform(args[0] as Conjunction);
            else if (args[0].Type == NodeType.Disjunction) return Perform(args[0] as Disjunction);
            else if (args[0].Type == NodeType.Equivalence)
                return Perform(args[0] as Equivalence, MaterialEquivalenceExpandMode.ToConjunction) ??
                    Perform(args[0] as Equivalence, MaterialEquivalenceExpandMode.ToDisjunction);
            return null;
        }

        public static LogicNode Perform(Equivalence equiv, MaterialEquivalenceExpandMode mode) {
            if (equiv == null) return null;

            if (mode == MaterialEquivalenceExpandMode.ToConjunction) {
                return new Conjunction(new Implication(equiv.Left.Clone(), equiv.Right.Clone()),
                    new Implication(equiv.Right.Clone(), equiv.Left.Clone()), equiv.Negations);
            } else if (mode == MaterialEquivalenceExpandMode.ToDisjunction) {
                return new Disjunction(new Conjunction(equiv.Left.Clone(), equiv.Right.Clone()),
                    new Conjunction(equiv.Right.Negate(), equiv.Left.Negate()), equiv.Negations);
            }

            return null;
        }

        public static Equivalence Perform(Conjunction conj) {
            if (conj == null) return null;

            Implication left = conj.Left as Implication;
            Implication right = conj.Right as Implication;
            if (left == null || right == null || left.Negated || right.Negated) return null;

            if (left.Antecedent.Equals(right.Consequent) && left.Consequent.Equals(right.Antecedent))
                return new Equivalence(left.Antecedent.Clone(), right.Consequent.Clone());

            return null;
        }

        public static Equivalence Perform(Disjunction disj) {
            if (disj == null) return null;

            Conjunction left = disj.Left as Conjunction;
            Conjunction right = disj.Right as Conjunction;
            if (left == null || right == null || left.Negated || right.Negated) return null;

            if ((left.Left.Equals(right.Left.Negate()) && left.Right.Equals(right.Right.Negate())) ||
                (left.Left.Equals(right.Right.Negate()) && left.Right.Equals(right.Left.Negate())))
                return new Equivalence(left.Left.Clone(), left.Right.Clone());

            return null;
        }
    }
}
