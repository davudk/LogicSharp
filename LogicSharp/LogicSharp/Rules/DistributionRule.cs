namespace LogicSharp.Rules {
    public class DistributionRule : LogicRule {
        public static readonly DistributionRule Instance = new DistributionRule();
        public override string Name => "Distribution";
        public override string Abbreviation => "Dist";

        private DistributionRule() { }

        public override LogicNode Do(params LogicNode[] args) {
            if (args.Length != 1) return null;
            if (args[0].Type == NodeType.Conjunction) {
                return Perform(args[0] as Conjunction);
            } else if (args[0].Type == NodeType.Disjunction) {
                return Perform(args[0] as Disjunction);
            }
            return null;
        }

        public static Disjunction Perform(Conjunction conj) {
            if (conj == null || conj.Negated) return null;

            Disjunction left = conj.Left as Disjunction;
            Disjunction right = conj.Right as Disjunction;

            if (left == null && right != null && right.Negations == 0) {
                return new Disjunction(new Conjunction(conj.Left.Clone(), right.Left.Clone()),
                    new Conjunction(conj.Left.Clone(), right.Right.Clone()), conj.Negations);
            } else if (left != null && right != null &&  left.Negations == 0 && right.Negations == 0) {
                if (left.Left.Equals(right.Left)) {
                    return new Disjunction(left.Left.Clone(), new Conjunction(left.Right.Clone(), right.Right.Clone()), conj.Negations);
                }
            }

            return null;
        }

        public static Conjunction Perform(Disjunction conj) {
            if (conj == null || conj.Negated) return null;

            Conjunction left = conj.Left as Conjunction;
            Conjunction right = conj.Right as Conjunction;

            if (left == null && right != null && right.Negations == 0) {
                return new Conjunction(new Disjunction(conj.Left.Clone(), right.Left.Clone()),
                    new Disjunction(conj.Left.Clone(), right.Right.Clone()), conj.Negations);
            } else if (left != null && right != null &&  left.Negations == 0 && right.Negations == 0) {
                if (left.Left.Equals(right.Left)) {
                    return new Conjunction(left.Left.Clone(), new Disjunction(left.Right.Clone(), right.Right.Clone()), conj.Negations);
                }
            }

            return null;
        }
    }
}
