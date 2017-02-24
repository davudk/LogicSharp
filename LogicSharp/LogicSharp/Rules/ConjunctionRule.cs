namespace LogicSharp.Rules {
    public class ConjunctionRule : LogicRule {
        public static readonly ConjunctionRule Instance = new ConjunctionRule();
        public override string Name => "Conjunction";
        public override string Abbreviation => "Conj";

        private ConjunctionRule() { }

        public override LogicNode Do(params LogicNode[] args) {
            if (args.Length != 2) return null;
            return Perform(args[0], args[1]);
        }

        public static Conjunction Perform(LogicNode leftNode, LogicNode rightNode) {
            if (leftNode == null || rightNode == null) return null;

            return new Conjunction(leftNode.Clone(), rightNode.Clone());
        }
    }
}
