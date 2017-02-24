namespace LogicSharp.Rules {
    public class ModusPonensRule : LogicRule {
        public static readonly ModusPonensRule Instance = new ModusPonensRule();
        public override string Name => "Modus Ponens";
        public override string Abbreviation => "MP";

        private ModusPonensRule() { }

        public override LogicNode Do(params LogicNode[] args) {
            if (args.Length != 2) return null;
            return Perform(args[0] as Implication, args[1]);
        }

        public static LogicNode Perform(Implication impl, LogicNode ante) {
            if (impl == null || ante == null || impl.Negated) return null;
            if (impl.Antecedent.Equals(ante)) {
                return impl.Consequent.Clone();
            }
            return null;
        }
    }
}
