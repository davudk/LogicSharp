namespace LogicSharp.Rules {
    public class CommutationRule : LogicRule {
        public static readonly CommutationRule Instance = new CommutationRule();
        public override string Name => "Commutation";
        public override string Abbreviation => "COM";

        private CommutationRule() { }

        public override LogicNode Do(params LogicNode[] args) {
            if (args.Length != 1) return null;
            if (args[0].Type == NodeType.Conjunction) {
                return Perform(args[0] as Conjunction);
            } else if (args[0].Type == NodeType.Disjunction) {
                return Perform(args[0] as Disjunction);
            }
            return null;
        }

        public static Conjunction Perform(Conjunction conj) {
            if (conj == null) return null;

            return new Conjunction(conj.Right.Clone(), conj.Left.Clone(), conj.Negations);
        }

        public static Disjunction Perform(Disjunction disj) {
            if (disj == null) return null;

            return new Disjunction(disj.Right.Clone(), disj.Left.Clone(), disj.Negations);
        }
    }
}
