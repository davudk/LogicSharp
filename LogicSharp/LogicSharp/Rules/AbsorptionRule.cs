namespace LogicSharp.Rules {
    public class AbsorptionRule : LogicRule {
        public static readonly AbsorptionRule Instance = new AbsorptionRule();
        public override string Name => "Absorption";
        public override string Abbreviation => "Abs";

        private AbsorptionRule() { }

        public override LogicNode Do(params LogicNode[] args) {
            if (args.Length != 1) return null;
            return Perform(args[0] as Implication);
        }

        public static Implication Perform(Implication impl) {
            if (impl == null || impl.Negated) return null;

            return new Implication(impl.Antecedent.Clone(), new Conjunction(impl.Antecedent.Clone(), impl.Consequent.Clone()), impl.Negations);
        }
    }
}
