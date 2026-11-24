# LogicSharp

A C# library that parses propositional logic statements and applies rules of inference to them.
Given a set of premises and a conclusion, you build a formal proof step by step, and the library
checks each step and tells you when the conclusion has been reached.

Written in 2017. It targets .NET Framework 4.5.2 and opens in Visual Studio (`LogicSharp/LogicSharp.sln`).

## Syntax

Statements are made of single-word labels (`p`, `q`, `raining`), parentheses and these operators,
from lowest to highest precedence:

| Operator | Accepted forms |
|---|---|
| Equivalence | `=` `<->` `≡` |
| Implication | `->` `⊃` |
| Disjunction | `v` `V` `\|` `∨` |
| Conjunction | `^` `&` `∧` |
| Negation | `!` `~` `¬` |

The full grammar is in [`Grammar.txt`](LogicSharp/LogicSharp/Grammar.txt):

```
Stmt        ::= Equivalence
Equivalence ::= Implication { EQUIV Equivalence }
Implication ::= Disjunction { IMPL Implication }
Disjunction ::= Conjunction { DISJ Disjunction }
Conjunction ::= Atom { CONJ Conjunction }
Atom        ::= NEG Atom | ( Stmt ) | LABEL
```

## Rules

| Abbreviation | Rule | Abbreviation | Rule |
|---|---|---|---|
| `MP` | Modus Ponens | `DMO` | De Morgan's Rule (Out) |
| `MT` | Modus Tollens | `Assoc` | Association |
| `HS` | Hypothetical Syllogism | `Dist` | Distribution |
| `DS` | Disjunctive Syllogism | `DNA` | Double Negation (Add) |
| `CD` | Constructive Dilemma | `DNR` | Double Negation (Remove) |
| `Abs` | Absorption | `Trans` | Transposition |
| `Simp` | Simplification | `MImp` | Material Implication |
| `Conj` | Conjunction | `MEquiv` | Material Equivalence |
| `Add` | Addition | `Exp` | Exportation |
| `DMI` | De Morgan's Rule (In) | `Taut` | Tautology |

## Usage

```csharp
using LogicSharp;
using LogicSharp.Rules;

var premises = new[] { LogicNode.Parse("p -> q"), LogicNode.Parse("p") };
var proof = new Scope(premises, LogicNode.Parse("q"));

// Statement 1 is "p -> q" and statement 2 is "p" (indices start at 0).
proof.TryRule(LogicRule.TryGetRule("MP"), 0, 1);   // adds "q"

Console.WriteLine(proof.Solved);                   // True
```

`TryRule` returns `false` when a rule doesn't apply to the statements you give it, so an invalid step
is never added to the proof.

## Demos

- **`Demo`**: a console app. Enter the premises one per line, then apply rules by typing their
  abbreviation and the statement numbers, e.g. `MP, 1, 2`. Type `.commands` for help. The
  demo's goal statement is fixed to `a`.
- **`WinformsDemo`**: the same idea with a Windows Forms interface.

## Licence

[MIT](LICENSE)
