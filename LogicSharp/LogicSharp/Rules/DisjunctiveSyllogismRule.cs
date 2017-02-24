namespace LogicSharp.Rules {
    public class DisjunctiveSyllogismRule : LogicRule {
        public static readonly DisjunctiveSyllogismRule Instance = new DisjunctiveSyllogismRule();
        public override string Name => "Disjunctive Syllogism";
        public override string Abbreviation => "DS";

        private DisjunctiveSyllogismRule() { }

        public override LogicNode Do(params LogicNode[] args) {
            if (args.Length != 2) return null;
            return Perform(args[0] as Disjunction, args[1]);
        }

        public static LogicNode Perform(Disjunction disj, LogicNode negRhs) {
            if (disj == null || negRhs == null || disj.Negated) return null;

            if (disj.Left.Negate().Equals(negRhs))
                return disj.Right.Clone();
            return null;
        }
    }
}
