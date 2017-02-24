namespace LogicSharp.Rules {
    public class TranspositionRule : LogicRule {
        public static readonly TranspositionRule Instance = new TranspositionRule();
        public override string Name => "Transposition";
        public override string Abbreviation => "Trans";

        private TranspositionRule() { }

        public override LogicNode Do(params LogicNode[] args) {
            if (args.Length != 1) return null;
            return Perform(args[0] as Implication);
        }

        public static Implication Perform(Implication impl) {
            if (impl == null || impl.Negated) return null;

            return new Implication(impl.Consequent.Negate(), impl.Antecedent.Negate(), impl.Negations);
        }
    }
}
