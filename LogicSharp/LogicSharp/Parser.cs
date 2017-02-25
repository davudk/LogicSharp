namespace LogicSharp {
    class Parser {
        private Parser() { }

        public static LogicNode ParseLogicNode(string s) {
            Tokenizer tknr = new Tokenizer(s);
            LogicNode result = ParseStmt(tknr);
            if (result == null) return null;
            return tknr.PeekToken() == null ? result : null;
        }
        
        static LogicNode ParseStmt(Tokenizer tknr) {
            return ParseEquivalence(tknr);
        }

        static LogicNode ParseEquivalence(Tokenizer tknr) {
            LogicNode impl = ParseImplication(tknr);
            Token tok;
            if ((tok = tknr.PeekToken()) != null && tok.Type == TokenType.Equivalence) {
                tknr.GetToken();

                LogicNode right = ParseEquivalence(tknr);
                if (right == null) return null;
                return new Equivalence(impl, right);
            }
            return impl;
        }

        static LogicNode ParseImplication(Tokenizer tknr) {
            LogicNode disj = ParseDisjunction(tknr);
            Token tok;
            if ((tok = tknr.PeekToken()) != null && tok.Type == TokenType.Implication) {
                tknr.GetToken();

                LogicNode right = ParseImplication(tknr);
                if (right == null) return null;
                return new Implication(disj, right);
            }
            return disj;
        }

        static LogicNode ParseDisjunction(Tokenizer tknr) {
            LogicNode conj = ParseConjunction(tknr);
            Token tok;
            if ((tok = tknr.PeekToken()) != null && tok.Type == TokenType.Disjunction) {
                tknr.GetToken();

                LogicNode right = ParseDisjunction(tknr);
                if (right == null) return null;
                return new Disjunction(conj, right);
            }
            return conj;
        }

        static LogicNode ParseConjunction(Tokenizer tknr) {
            LogicNode atom = ParseAtom(tknr);
            Token tok;
            if ((tok = tknr.PeekToken()) != null && tok.Type == TokenType.Conjunction) {
                tknr.GetToken();

                LogicNode right = ParseConjunction(tknr);
                if (right == null) return null;
                return new Conjunction(atom, right);
            }
            return atom;
        }

        static LogicNode ParseAtom(Tokenizer tknr) {
            Token tok = tknr.PeekToken();
            if (tok == null) return null;

            if (tok.Type == TokenType.Negation) {
                tknr.GetToken();
                LogicNode atom = ParseAtom(tknr);
                atom.Negations++;
                return atom;
                //return new Negation(ParseAtom(tknr));
            } else if (tok.Type == TokenType.LParen) {
                tknr.GetToken();
                LogicNode stmt = ParseStmt(tknr);
                if ((tok = tknr.PeekToken()) != null && tok.Type == TokenType.RParen) {
                    tknr.GetToken();
                    return stmt;
                } else {
                    //throw new NotImplementedException("Expected RParen.");
                    return null;
                }
            } else if (tok.Type == TokenType.Label) {
                return new Label(tknr.GetToken().Lexeme);
            }

            return null;
        }
    }
}
