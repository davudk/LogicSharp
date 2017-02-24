namespace LogicSharp.Rules {
    public class HypotheticalSyllogismRule : LogicRule {
        public static readonly HypotheticalSyllogismRule Instance = new HypotheticalSyllogismRule();
        public override string Name => "Hypothetical Syllogism";
        public override string Abbreviation => "HS";

        private HypotheticalSyllogismRule() { }

        public override LogicNode Do(params LogicNode[] args) {
            if (args.Length != 2) return null;
            return Perform(args[0] as Implication, args[1] as Implication);
        }

        public static Implication Perform(Implication leftImpl, Implication rightImpl) {
            if (leftImpl == null || rightImpl == null || leftImpl.Negated || rightImpl.Negated) return null;

            if (leftImpl.Consequent.Equals(rightImpl.Antecedent))
                return new Implication(leftImpl.Antecedent.Clone(), rightImpl.Consequent.Clone());
            return null;
        }
    }
}
