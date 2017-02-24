namespace LogicSharp.Rules {
    public enum AssociationSidePreference {
        Left, Right
    }
    public class AssociationRule : LogicRule {
        public static readonly AssociationRule Instance = new AssociationRule();
        public override string Name => "Association";
        public override string Abbreviation => "Assoc";

        private AssociationRule() { }

        public override LogicNode Do(params LogicNode[] args) {
            if (args.Length != 1) return null;
            if (args[0].Type == NodeType.Conjunction) {
                return Perform(args[0] as Conjunction, AssociationSidePreference.Left) ?? Perform(args[0] as Conjunction, AssociationSidePreference.Right);
            } else if (args[0].Type == NodeType.Disjunction) {
                return Perform(args[0] as Disjunction, AssociationSidePreference.Left) ?? Perform(args[0] as Disjunction, AssociationSidePreference.Right);
            }
            return null;
        }

        public static Conjunction Perform(Conjunction conj, AssociationSidePreference pref) {
            if (conj == null || conj.Negated) return null;

            Conjunction left = conj.Left as Conjunction;
            Conjunction right = conj.Right as Conjunction;

            if (left == null && right == null) return null;
            else if (left != null && right == null) {
                pref = AssociationSidePreference.Right;
            } else if (left == null && right != null) {
                pref = AssociationSidePreference.Left;
            }

            if (pref == AssociationSidePreference.Left) {
                return new Conjunction(new Conjunction(conj.Left.Clone(), right.Left.Clone()), right.Right.Clone());
            } else if (pref == AssociationSidePreference.Right) {
                return new Conjunction(left.Left.Clone(), new Conjunction(left.Right.Clone(), conj.Right.Clone()));
            }

            return null;
        }

        public static Disjunction Perform(Disjunction conj, AssociationSidePreference pref) {
            if (conj == null || conj.Negated) return null;

            Disjunction left = conj.Left as Disjunction;
            Disjunction right = conj.Right as Disjunction;

            if (left == null && right == null) return null;
            else if (left != null && right == null) {
                if (left.Negations > 0) return null;
                pref = AssociationSidePreference.Right;
            } else if (left == null && right != null) {
                if (right.Negations > 0) return null;
                pref = AssociationSidePreference.Left;
            } else if (left.Negations > 0 || right.Negations > 0) return null;

            if (pref == AssociationSidePreference.Left) {
                return new Disjunction(new Disjunction(conj.Left.Clone(), right.Left.Clone()), right.Right.Clone());
            } else if (pref == AssociationSidePreference.Right) {
                return new Disjunction(left.Left.Clone(), new Disjunction(right.Left.Clone(), right.Right.Clone()));
            }

            return null;
        }
    }
}
