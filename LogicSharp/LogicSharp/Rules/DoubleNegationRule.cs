namespace LogicSharp.Rules {
    public class DoubleNegationAddRule : LogicRule {
        public static readonly DoubleNegationAddRule Instance = new DoubleNegationAddRule();
        public override string Name => "Double Negation (Add)";
        public override string Abbreviation => "DNA";

        private DoubleNegationAddRule() { }

        public override LogicNode Do(params LogicNode[] args) {
            if (args.Length != 1) return null;
            return Perform(args[0]);
        }

        public static LogicNode Perform(LogicNode node) {
            if (node == null) return null;

            LogicNode clone = node.Clone();
            clone.Negations += 2;
            return clone;
        }
    }
    public class DoubleNegationRemoveRule : LogicRule {
        public static readonly DoubleNegationRemoveRule Instance = new DoubleNegationRemoveRule();
        public override string Name => "Double Negation (Remove)";
        public override string Abbreviation => "DNR";

        private DoubleNegationRemoveRule() { }

        public override LogicNode Do(params LogicNode[] args) {
            if (args.Length != 1) return null;
            return Perform(args[0]);
        }

        public static LogicNode Perform(LogicNode node) {
            if (node == null || node.Negations < 2) return null;

            LogicNode clone = node.Clone();
            clone.Negations -= 2;
            return clone;
        }
    }
}
