namespace LogicSharp.Rules {
    public class SimplificationRule : LogicRule {
        public static readonly SimplificationRule Instance = new SimplificationRule();
        public override string Name => "Simplification";
        public override string Abbreviation => "Simp";

        private SimplificationRule() { }

        public override LogicNode Do(params LogicNode[] args) {
            if (args.Length != 1) return null;
            return Perform(args[0] as Conjunction);
        }

        public static LogicNode Perform(Conjunction conj) {
            if (conj == null || conj.Negated) return null;

            return conj.Left.Clone();
        }
    }
}
