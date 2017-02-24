using LogicSharp;
using LogicSharp.Rules;
using System;
using System.Collections.Generic;

namespace Demo {
    class Program {
        const int IndexSize = 4;
        const int BodySize = 58;
        const int SourceSize = 15;

        static void Main(string[] args) {
            Console.WriteLine();
            Console.WriteLine("════ Welcome to LogicSharp Demo ════".PadRight(80, '═'));

            Proof();
        }

        static void Proof() {
            Console.WriteLine("> new proof");
            Console.WriteLine("Enter given statements (no input to END):");

            List<LogicNode> givens = new List<LogicNode>();
            string input;
            do {
                Console.Write("> ");
                input = Console.ReadLine().Trim();
                LogicNode givenNode = LogicNode.Parse(input);
                if (givenNode != null) {
                    Console.WriteLine("Received: " + givenNode);
                    givens.Add(givenNode);
                } else if (input.Length > 0) {
                    Console.WriteLine("Error: parse failed");
                } else if (givens.Count == 0) {
                    Console.WriteLine("You must enter at least one given statement.");
                }
            } while (input.Length > 0 || givens.Count == 0);

            Console.WriteLine("Let the proof begin!");
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);

            Scope scope = new Scope(givens, LogicNode.Parse("a"));

            do {
                Console.Clear();
                PrintScope(scope);
                ListOptions();

                if (scope.Solved) {
                    Console.WriteLine("Solved!");
                    Console.WriteLine("Press any key to exit...");
                    Console.ReadKey(true);
                    break;
                }

                Console.Write("> ");
                input = Console.ReadLine().Trim();

                if (input.Length == 0) {
                    Console.WriteLine("Press enter again to exit this proof.");
                    if (Console.ReadKey(true).Key == ConsoleKey.Enter) {
                        break;
                    }
                } else if (input.StartsWith(".")) {
                    Command(input);
                } else {
                    string[] parts = input.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length < 2) {
                        Console.WriteLine("Invalid rule.");
                        Console.ReadKey(true);
                        continue;
                    } else if (parts.Length >= 4) {
                        Console.WriteLine("Too many source indices.");
                        Console.ReadKey(true);
                        continue;
                    }

                    string ruleAbbr = parts[0];
                    int index0 = -1, index1 = -1;
                    LogicNode node = null;
                    
                    if (!int.TryParse(parts[1], out index0)) {
                        Console.WriteLine("Invalid index: " + parts[1]);
                        Console.ReadKey(true);
                        continue;
                    }

                    if (parts.Length == 3) {
                        if (!int.TryParse(parts[2], out index1) && (node = LogicNode.Parse(parts[2])) == null) {
                            Console.WriteLine("Invalid index: " + parts[2]);
                            Console.ReadKey(true);
                            continue;
                        };
                    }

                    index0--;
                    index1--;

                    LogicRule rule = LogicRule.TryGetRule(ruleAbbr);
                    if (rule == null) {
                        Console.WriteLine("Unknown rule abbreviation: " + ruleAbbr);
                        Console.ReadKey(true);
                        continue;
                    }

                    bool result = false;
                    if (parts.Length == 2) result = scope.TryRule(rule, index0);
                    else if (parts.Length == 3 && index1 >= 0) result = scope.TryRule(rule, index0, index1);
                    else if (parts.Length == 3) result = scope.TryRule(rule, index0, node);

                    if (!result) {
                        Console.WriteLine("Rule failed.");
                        Console.ReadKey(true);
                        continue;
                    }
                }
            } while (true);
        }

        static void PrintScope(Scope scope) {
            Console.Write("╔");
            Console.Write(new string('═', IndexSize));
            Console.Write("╦");
            Console.Write(new string('═', BodySize));
            Console.Write("╤");
            Console.Write(new string('═', SourceSize));
                        
            // ╤ ╠ ┼ ├
            foreach (var stmt in scope.Statements) {
                PrintStmt(stmt);
            }
            PrintStmt(null);
        }
        
        static void PrintStmt(Statement stmt) {
            if (stmt == null) {
                Console.Write("╠" + new string(' ', IndexSize) + "╠ ");
                Console.WriteLine(new string(' ', BodySize - 2) + " ├");
                return;
            }

            string indexStr = (stmt.Index + 1) + ".";
            if (indexStr.Length < IndexSize) indexStr = " " + indexStr;
            indexStr = indexStr.PadRight(IndexSize);

            Console.Write("╠" + indexStr + "╠ ");
            Console.Write(stmt.Node.ToString().PadRight(BodySize - 2) + " ├ ");
            if (stmt is GivenStmt) Console.WriteLine("Given");
            else {
                ResultStmt result = stmt as ResultStmt;
                if (result == null) {
                    Console.WriteLine();
                    return;
                }

                Console.Write(result.RuleUsed.Abbreviation);
                Console.Write(", " + (result.SourceIndex0 + 1));
                if (result.SourceIndex1 >= 0) Console.Write(", " + (result.SourceIndex1 + 1));
                else if (result.SourceNode != null) Console.Write(", " + result.SourceNode);
                Console.WriteLine();
            }
        }

        static void ListOptions() {
            Console.Write("╠" + new string('═', IndexSize) + "╩");
            Console.Write("══ Rules of Inference ════".PadRight(BodySize, '═') + "╧" + new string ('═', SourceSize));
            Console.WriteLine("║");
            foreach (var rule in LogicRule.GetRules()) {
                Console.Write(("╠ " + rule.Abbreviation + ":").PadRight(9));
                Console.WriteLine(rule.Name);
            }
            Console.WriteLine("║");
            Console.Write("╚═══════ Please type your rule: (e.g. MP, 1, 2) or type .commands ════".PadRight(80, '═'));
        }

        static void Command(string command) {
            Console.Clear();

            string lower = command.ToLower();
            if (lower == ".commands") {
                Console.WriteLine();
                Console.WriteLine("════  Commands ════".PadRight(80, '═'));

                DisplayCommand(".commands", "Displays this page.");
                DisplayCommand(".exit", "Quits the application.");
                DisplayCommand(".help rule", "Displays the rule (in notation).");
            } else if (lower == ".exit") {
                Environment.Exit(0);
            } else if (lower.StartsWith(".help")) {
                ;
                string ruleAbbr = command.Remove(0, 5).Trim();
                Console.Clear();

                switch (ruleAbbr.ToUpper()) {
                case "MP":
                    DisplayRule("Modus Ponens", "MP", " p -> q\n p\n--------\n q");
                    break;
                case "MT":
                    DisplayRule("Modus Tolens", "MT", " p -> q\n -q\n--------\n -p");
                    break;
                case "HS":
                    DisplayRule("Hypothetical Syllogism", "HS", " p -> q\n q -> r\n--------\n p -> r");
                    break;
                case "DS":
                    DisplayRule("Disjunctive Syllogism", "DS", " p v q\n -p\n--------\n q");
                    break;
                case "CD":
                    DisplayRule("Constructive Dilemma", "CD", " p v q\n -p\n--------\n q");
                    break;
                case "ABS":
                    DisplayRule("Absorption", "ABS", " p -> q\n--------\n p -> (p ^ q)");
                    break;
                case "SIMP":
                    DisplayRule("Simplification", "SIMP", " p ^ q\n--------\n p");
                    break;
                case "CONJ":
                    DisplayRule("Conjunction", "CONJ", " p\n q\n--------\n p ^ q");
                    break;
                case "ADD":
                    DisplayRule("Addition", "ADD", " p v (any)\n -p\n--------\n p v (any)");
                    break;
                }
            } else {
                Console.WriteLine("Unknown command.");
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }

        static void DisplayCommand(string name, string desc) {
            if (name == null) return;
            if (desc == null) desc = string.Empty;
            Console.Write("    ");
            Console.Write(name.PadRight(16));
            Console.Write("    ");

            while (desc.Length > 0) {
                int len = Math.Min(desc.Length, 56);
                string part = desc.Substring(0, len);

                if (len != 56) desc = string.Empty;
                else desc = desc.Substring(len);

                Console.Write(part);
                if (desc.Length > 0) Console.Write(new string(' ', 24));
            }

            Console.WriteLine();
        }

        static void DisplayRule(string name, string abbr, string text) {
            const string Indent = "    ";
            Console.WriteLine();
            Console.WriteLine(("════  " + name + "  (" + abbr + ") ════").PadRight(80, '═'));
            string[] lines = text.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines) {
                Console.Write(Indent);
                Console.WriteLine(line);
            }
        }
    }
}
