namespace LogicSharp.Rules {
    public enum ExportationMode {
        Expand, Reduce
    }
    public class ExportationRule : LogicRule {
        public static readonly ExportationRule Instance = new ExportationRule();
        public override string Name => "Exportation";
        public override string Abbreviation => "Exp";

        private ExportationRule() { }

        public override LogicNode Do(params LogicNode[] args) {
            if (args.Length != 1) return null;
            return Perform(args[0] as Implication, ExportationMode.Expand) ?? Perform(args[0] as Implication, ExportationMode.Reduce);
        }

        public static Implication Perform(Implication impl, ExportationMode mode) {
            if (impl == null || impl.Negated) return null;

            if (mode == ExportationMode.Expand) {
                Conjunction conj = impl.Antecedent as Conjunction;
                if (conj != null && conj.Negations == 0) {
                    return new Implication(conj.Left.Clone(), new Implication(conj.Right.Clone(), impl.Consequent.Clone()), impl.Negations);
                }
            } else if (mode == ExportationMode.Reduce) {
                Implication rightImpl = impl.Consequent as Implication;
                if (rightImpl != null && rightImpl.Negations == 0) {
                    return new Implication(new Conjunction(impl.Antecedent.Clone(), rightImpl.Antecedent.Clone()), rightImpl.Consequent.Clone(), impl.Negations);
                }
            }

            return null;
        }
    }
}
