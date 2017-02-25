using System.Linq;

namespace LogicSharp {
    enum TokenType {
        //Error,
        Equivalence,
        Implication,
        Conjunction,
        Disjunction,
        Negation,
        Label,
        LParen, RParen
    }
    class Token {
        public TokenType Type { get; private set; }
        public string Lexeme { get; private set; }

        public Token(TokenType type, string lexeme) {
            Type = type;
            Lexeme = lexeme;
        }
    }
    class Tokenizer {
        static readonly string NonLabelChars = "=<->^vV()";
        string s;
        int i;
        Token peekedToken;
        public string Text => s;

        public Tokenizer(string s) {
            this.s = s;
        }

        public Token GetToken() {
            if (peekedToken != null) {
                Token tok = peekedToken;
                peekedToken = null;
                return tok;
            }

            while (!EOF()) {
                if (char.IsWhiteSpace(s, i)) i++;
                else break;
            }
            if (EOF()) return null;

            if (HasString("=")) {
                i += 1;
                return new Token(TokenType.Equivalence, "=");
            } else if (HasString("<->")) {
                i += 3;
                return new Token(TokenType.Equivalence, "<->");
            } else if (HasString("≡")) {
                i += 1;
                return new Token(TokenType.Equivalence, "≡");
            } else if (HasString("->")) {
                i += 2;
                return new Token(TokenType.Implication, "->");
            } else if (HasString("⊃")) {
                i += 1;
                return new Token(TokenType.Implication, "⊃");
            } else if (HasString("^")) {
                i += 1;
                return new Token(TokenType.Conjunction, "^");
            } else if (HasString("&")) {
                i += 1;
                return new Token(TokenType.Conjunction, "&");
            } else if (HasString("∧")) {
                i += 1;
                return new Token(TokenType.Conjunction, "∧");
            } else if (HasString("v")) {
                i += 1;
                return new Token(TokenType.Disjunction, "v");
            } else if (HasString("V")) {
                i += 1;
                return new Token(TokenType.Disjunction, "V");
            } else if (HasString("|")) {
                i += 1;
                return new Token(TokenType.Disjunction, "|");
            } else if (HasString("∨")) {
                i += 1;
                return new Token(TokenType.Disjunction, "∨");
            } else if (HasString("!")) {
                i += 1;
                return new Token(TokenType.Negation, "!");
            } else if (HasString("~")) {
                i += 1;
                return new Token(TokenType.Negation, "~");
            } else if (HasString("¬")) {
                i += 1;
                return new Token(TokenType.Negation, "¬");
            } else if (HasString("(")) {
                i += 1;
                return new Token(TokenType.LParen, "(");
            } else if (HasString(")")) {
                i += 1;
                return new Token(TokenType.RParen, ")");
            }

            if (!char.IsLetter(s, i)) {
                return null;
                //return new Token(TokenType.Error, string.Empty);
            }

            int start = i;
            do {
                i++;
            } while (!EOF() && !NonLabelChars.Contains(s[i]) && !char.IsWhiteSpace(s, i) && char.IsLetterOrDigit(s, i));
            int len = i - start;
            return new Token(TokenType.Label, s.Substring(start, len));
        }

        public Token PeekToken() {
            if (peekedToken == null) return peekedToken = GetToken();
            else return peekedToken;
        }

        bool HasString(string str) {
            if (str.Length == 1) return EOF() ? false : s[i] == str[0];

            if (i + str.Length >= s.Length) return false;
            string sub = s.Substring(i, str.Length);
            return sub == str;
        }

        bool EOF() => i >= s.Length;
    }
}
