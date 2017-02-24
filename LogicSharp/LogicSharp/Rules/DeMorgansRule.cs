namespace LogicSharp.Rules {
    public class DeMorgansInRule : LogicRule {
        public static readonly DeMorgansInRule Instance = new DeMorgansInRule();
        public override string Name => "De Morgan's Rule (In)";
        public override string Abbreviation => "DMI";

        private DeMorgansInRule() { }

        public override LogicNode Do(params LogicNode[] args) {
            if (args.Length != 1) return null;
            if (args[0].Type == NodeType.Conjunction) {
                return Perform(args[0] as Conjunction);
            } else if (args[0].Type == NodeType.Disjunction) {
                return Perform(args[0] as Disjunction);
            }
            return null;
        }

        public static Disjunction Perform(Conjunction conj) {
            if (conj == null || conj.Negations == 0) return null;

            return new Disjunction(conj.Left.Negate(), conj.Right.Negate(), conj.Negations - 1);
        }

        public static Conjunction Perform(Disjunction disj) {
            if (disj == null || disj.Negations == 0) return null;

            return new Conjunction(disj.Left.Negate(), disj.Right.Negate(), disj.Negations - 1);
        }
    }
    public class DeMorgansOutRule : LogicRule {
        public static readonly DeMorgansOutRule Instance = new DeMorgansOutRule();
        public override string Name => "De Morgan's Rule (Out)";
        public override string Abbreviation => "DMO";

        private DeMorgansOutRule() { }
        
        public override LogicNode Do(params LogicNode[] args) {
            if (args.Length != 1) return null;
            if (args[0].Type == NodeType.Conjunction) {
                return Perform(args[0] as Conjunction);
            } else if (args[0].Type == NodeType.Disjunction) {
                return Perform(args[0] as Disjunction);
            }
            return null;
        }

        public static Disjunction Perform(Conjunction conj) {
            if (conj == null || conj.Left.Negations == 0 || conj.Right.Negations == 0) return null;

            return new Disjunction(conj.Left.DeNegate(), conj.Right.DeNegate(), conj.Negations + 1);
        }

        public static Conjunction Perform(Disjunction disj) {
            if (disj == null || disj.Left.Negations == 0 || disj.Right.Negations == 0) return null;

            return new Conjunction(disj.Left.DeNegate(), disj.Right.DeNegate(), disj.Negations + 1);
        }
    }
}
