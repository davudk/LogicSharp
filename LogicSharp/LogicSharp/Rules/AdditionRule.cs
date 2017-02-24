namespace LogicSharp.Rules {
    public class AdditionRule : LogicRule {
        public static readonly AdditionRule Instance = new AdditionRule();
        public override string Name => "Addition";
        public override string Abbreviation => "Add";

        private AdditionRule() { }

        public override LogicNode Do(params LogicNode[] args) {
            if (args.Length != 2) return null;
            return Perform(args[0], args[1]);
        }

        public static Disjunction Perform(LogicNode leftNode, LogicNode rightNode) {
            if (leftNode == null || rightNode == null) return null;

            return new Disjunction(leftNode.Clone(), rightNode.Clone());
        }
    }
}
