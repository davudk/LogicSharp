namespace LogicSharp.Rules {
    public class ModusTolensRule : LogicRule {
        public static readonly ModusTolensRule Instance = new ModusTolensRule();
        public override string Name => "Modus Tolens";
        public override string Abbreviation => "MT";

        private ModusTolensRule() { }

        public override LogicNode Do(params LogicNode[] args) {
            if (args.Length != 2) return null;
            return Perform(args[0] as Implication, args[1]);
        }

        public static LogicNode Perform(Implication impl, LogicNode negCons) {
            if (impl == null || negCons == null || impl.Negated) return null;
            if (impl.Consequent.Negate().Equals(negCons)) {
                return impl.Antecedent.Negate();
            }
            return null;
        }
    }
}
