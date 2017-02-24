namespace LogicSharp.Rules {
    public class MaterialImplicationRule : LogicRule {
        public static readonly MaterialImplicationRule Instance = new MaterialImplicationRule();
        public override string Name => "Material Implication";
        public override string Abbreviation => "MImp";

        private MaterialImplicationRule() { }

        public override LogicNode Do(params LogicNode[] args) {
            if (args.Length != 1) return null;
            if (args[0]. Type == NodeType.Implication) return Perform(args[0] as Implication);
            else if (args[0].Type == NodeType.Disjunction) return Perform(args[0] as Disjunction);
            return null;
        }

        public static Disjunction Perform(Implication impl) {
            if (impl == null || impl.Negated) return null;

            return new Disjunction(impl.Antecedent.Negate(), impl.Consequent.Clone(), impl.Negations);
        }

        public static Implication Perform(Disjunction disj) {
            if (disj == null || disj.Negated) return null;

            return new Implication(disj.Left.Negate(), disj.Right.Clone(), disj.Negations);
        }
    }
}
