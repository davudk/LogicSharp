namespace LogicSharp.Rules {
    public class ConstructiveDilemmaRule : LogicRule {
        public static readonly ConstructiveDilemmaRule Instance = new ConstructiveDilemmaRule();
        public override string Name => "Constructive Dilemma";
        public override string Abbreviation => "CD";

        private ConstructiveDilemmaRule() { }

        public override LogicNode Do(params LogicNode[] args) {
            if (args.Length != 2) return null;
            return Perform(args[0] as Conjunction, args[1] as Disjunction);
        }

        public static Disjunction Perform(Conjunction conj, Disjunction disj) {
            if (conj == null || disj == null || conj.Negated || disj.Negated) return null;

            Implication left = conj.Left as Implication;
            Implication right = conj.Right as Implication;

            if (left == null || right == null || left.Negated || right.Negated) return null;

            if (left.Antecedent.Equals(disj.Left) && right.Antecedent.Equals(disj.Right))
                return new Disjunction(left.Consequent.Clone(), right.Consequent.Clone());

            return null;
        }
    }
}
