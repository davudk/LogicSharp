using System;

namespace LogicSharp {
    public enum NodeType {
        Equivalence,
        Implication,
        Conjunction,
        Disjunction,
        //Negation,
        Label
    }
    public abstract class LogicNode {
        public const char NegationCharacter = '¬';
        public NodeType Type { get; private set; }
        protected LogicNode left, right;
        protected string name;
        int _negations;
        public int Negations {
            get { return _negations; }
            set { _negations = Math.Max(0, value); }
        }
        public bool Negated => Negations % 2 != 0;

        protected LogicNode(NodeType type, int negations, LogicNode lhs = null, LogicNode rhs = null, string name = null) {
            Type = type;
            Negations = negations;
            left = lhs;
            right = rhs;
            this.name = name;
        }

        public LogicNode Negate(bool reduceNegations = true) {
            LogicNode clone = this.Clone();
            if (reduceNegations) clone.Negations = (clone.Negations + 1) % 2;
            else clone.Negations++;
            return clone;
        }

        public LogicNode DeNegate() {
            LogicNode clone = this.Clone();
            if (clone.Negations > 0) clone.Negations--;
            return clone;
        }

        public override bool Equals(object obj) {
            LogicNode rhs = obj as LogicNode;
            if (rhs == null) return false;
            if (Type == rhs.Type) {
                if (left != null && left.Equals(rhs.left) == false) return false;
                if (right != null && right.Equals(rhs.right) == false) return false;
                return Negated == rhs.Negated && name == rhs.name;
            }
            return false;
        }

        public override int GetHashCode() {
            return ((int)Type * 13 + name.GetHashCode()) * 7 +
                left.GetHashCode() * 29 + right.GetHashCode() * 31;
        }
        
        //public string ToStringWrapped() {
        //    bool wrap = Negations > 0 && Type != NodeType.Label;
        //    return wrap ? "(" + ToString() + ")" : ToString();
        //}

        public override string ToString() {
            return ToString(false, false);
        }

        public string ToString(bool reduceNegations, bool parens = true) {
            int neg = reduceNegations ? (Negated ? 1 : 0) : Negations;
            if (neg > 0) {
                if (IsAtomic()) return new string(NegationCharacter, neg) + ToStringInner(reduceNegations);
                else return new string(NegationCharacter, neg) + "(" + ToStringInner(reduceNegations, true) + ")";
            } else {
                string str = ToStringInner(reduceNegations, false);
                return (parens && !IsAtomic()) ? WrapInParens(str) : str;
            }
        }

        protected string WrapInParens(string s) => "(" + s + ")";
        protected abstract string ToStringInner(bool reduceNegations, bool preferParens = false);
        protected virtual bool IsAtomic() => false;
        
        public abstract LogicNode Clone();

        public static LogicNode Parse(string raw) {
            return Parser.ParseLogicNode(raw);
        }
    }

    public class Equivalence : LogicNode {
        public const char Character = '≡';
        public LogicNode Left { get { return left; } set { left = value; } }
        public LogicNode Right { get { return right; } set { right = value; } }

        public Equivalence(LogicNode lhs, LogicNode rhs, int negations = 0)
            : base(NodeType.Equivalence, negations, lhs, rhs) { }

        protected override string ToStringInner(bool reduceNegations, bool forceParens) => left.ToString(reduceNegations) + " " + Character.ToString() + " " + right.ToString(reduceNegations);
        public override LogicNode Clone() => new Equivalence(Left.Clone(), right.Clone(), Negations);
    }
    public class Implication : LogicNode {
        public const char Character = '⊃';
        public LogicNode Antecedent { get { return left; } set { left = value; } }
        public LogicNode Consequent { get { return right; } set { right = value; } }

        public Implication(LogicNode antecedent, LogicNode consequent, int negations = 0)
            : base(NodeType.Implication, negations, antecedent, consequent) { }

        protected override string ToStringInner(bool reduceNegations, bool forceParens) => left.ToString(reduceNegations) + " " + Character.ToString() + " " + right.ToString(reduceNegations);
        public override LogicNode Clone() => new Implication(Antecedent.Clone(), Consequent.Clone(), Negations);
    }
    public class Conjunction : LogicNode {
        public const char Character = '∧';
        public LogicNode Left { get { return left; } set { left = value; } }
        public LogicNode Right { get { return right; } set { right = value; } }

        public Conjunction(LogicNode lhs, LogicNode rhs, int negations = 0)
            : base(NodeType.Conjunction, negations, lhs, rhs) { }

        protected override string ToStringInner(bool reduceNegations, bool forceParens) => left.ToString(reduceNegations) + " " + Character.ToString() + " " + right.ToString(reduceNegations);
        public override LogicNode Clone() => new Conjunction(Left.Clone(), Right.Clone(), Negations);
    }
    public class Disjunction : LogicNode {
        public const char Character = '∨';
        public LogicNode Left { get { return left; } set { left = value; } }
        public LogicNode Right { get { return right; } set { right = value; } }

        public Disjunction(LogicNode lhs, LogicNode rhs, int negations = 0)
            : base(NodeType.Disjunction, negations, lhs, rhs) { }

        protected override string ToStringInner(bool reduceNegations, bool forceParens) => left.ToString(reduceNegations) + " " + Character.ToString() + " " + right.ToString(reduceNegations);
        public override LogicNode Clone() => new Disjunction(Left.Clone(), Right.Clone(), Negations);
    }
    public class Label : LogicNode {
        public string Name {
            get { return name; }
            set {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidOperationException("Name cannot be null, empty, or whitespace.");
                name = value;
            }
        }

        public Label(string name, int negations = 0)
            : base(NodeType.Label, negations, null, null, name) { }

        protected override string ToStringInner(bool reduceNegations, bool forceParens) => name;
        protected override bool IsAtomic() => true;
        public override LogicNode Clone() => new Label(Name, Negations);
    }
}
