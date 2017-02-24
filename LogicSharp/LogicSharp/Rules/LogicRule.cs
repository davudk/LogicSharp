using System.Collections.Generic;

namespace LogicSharp.Rules {
    public abstract class LogicRule {
        public abstract string Name { get; }
        public abstract string Abbreviation { get; }

        public abstract LogicNode Do(params LogicNode[] args);

        public static LogicRule TryGetRule(string abbreviation) {
            string lower = abbreviation.ToLower();
            foreach (var rule in GetRules())
                if (rule.Abbreviation.ToLower() == lower) return rule;
            return null;
        }

        public static IEnumerable<LogicRule> GetRules() {
            return new List<LogicRule>() {
                ModusPonensRule.Instance,
                ModusTolensRule.Instance,
                HypotheticalSyllogismRule.Instance,
                DisjunctiveSyllogismRule.Instance,
                ConstructiveDilemmaRule.Instance,
                AbsorptionRule.Instance,
                SimplificationRule.Instance,
                ConjunctionRule.Instance,
                AdditionRule.Instance,
                DeMorgansInRule.Instance,
                DeMorgansOutRule.Instance,
                AssociationRule.Instance,
                DistributionRule.Instance,
                DoubleNegationAddRule.Instance,
                DoubleNegationRemoveRule.Instance,
                TranspositionRule.Instance,
                MaterialImplicationRule.Instance,
                MaterialEquivalenceRule.Instance,
                ExportationRule.Instance,
                TautologyRule.Instance
            };
        }
    }
}
