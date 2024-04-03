
using System;



public class Parser {
	public const int _EOF = 0;
	public const int _operation = 1;
	public const int _comparison = 2;
	public const int _VarName = 3;
	public const int _IntNum = 4;
	public const int _FloatNum = 5;
	public const int _Semicol = 6;
	public const int _Equal = 7;
	public const int maxT = 17;

	const bool _T = true;
	const bool _x = false;
	const int minErrDist = 2;
	
	public Scanner scanner;
	public Errors  errors;

	public Token t;    // last recognized token
	public Token la;   // lookahead token
	int errDist = minErrDist;



	public Parser(Scanner scanner) {
		this.scanner = scanner;
		errors = new Errors();
	}

	void SynErr (int n) {
		if (errDist >= minErrDist) errors.SynErr(la.line, la.col, n);
		errDist = 0;
	}

	public void SemErr (string msg) {
		if (errDist >= minErrDist) errors.SemErr(t.line, t.col, msg);
		errDist = 0;
	}
	
	void Get () {
		for (;;) {
			t = la;
			la = scanner.Scan();
			if (la.kind <= maxT) { ++errDist; break; }

			la = t;
		}
	}
	
	void Expect (int n) {
		if (la.kind==n) Get(); else { SynErr(n); }
	}
	
	bool StartOf (int s) {
		return set[s, la.kind];
	}
	
	void ExpectWeak (int n, int follow) {
		if (la.kind == n) Get();
		else {
			SynErr(n);
			while (!StartOf(follow)) Get();
		}
	}


	bool WeakSeparator(int n, int syFol, int repFol) {
		int kind = la.kind;
		if (kind == n) {Get(); return true;}
		else if (StartOf(repFol)) {return false;}
		else {
			SynErr(n);
			while (!(set[syFol, kind] || set[repFol, kind] || set[0, kind])) {
				Get();
				kind = la.kind;
			}
			return StartOf(syFol);
		}
	}

	
	void LexicalAnalyzer() {
		if (la.kind == 10) {
			ForStatement();
		} else if (la.kind == 15) {
			IfElseStatement();
		} else SynErr(18);
	}

	void ForStatement() {
		Expect(10);
		Expect(11);
		Declaration();
		Expect(6);
		Condition();
		Expect(6);
		ForExpression();
		Expect(12);
		Expect(13);
		Body();
		Expect(14);
	}

	void IfElseStatement() {
		Expect(15);
		Expect(11);
		Condition();
		Expect(12);
		Expect(13);
		Body();
		Expect(14);
		Expect(16);
		Expect(13);
		Body();
		Expect(14);
	}

	void TypeSpecifier() {
		if (la.kind == 8) {
			Get();
		} else if (la.kind == 9) {
			Get();
		} else SynErr(19);
	}

	void Declaration() {
		TypeSpecifier();
		Expect(3);
		Expect(7);
		while (la.kind == 4 || la.kind == 5) {
			if (la.kind == 4) {
				Get();
			} else {
				Get();
			}
		}
	}

	void Condition() {
		if (la.kind == 4) {
			IntCondition();
		} else if (la.kind == 5) {
			FloatCondition();
		} else if (la.kind == 3) {
			VarCondition();
		} else SynErr(20);
	}

	void ForExpression() {
		Expect(3);
		Expect(1);
		if (la.kind == 3) {
			Get();
		} else if (la.kind == 4) {
			Get();
		} else if (la.kind == 5) {
			Get();
		} else SynErr(21);
	}

	void Body() {
		while (la.kind == 8 || la.kind == 9 || la.kind == 15) {
			if (la.kind == 8 || la.kind == 9) {
				Expression();
			} else {
				IfElseStatement();
			}
		}
	}

	void IntCondition() {
		Expect(4);
		while (la.kind == 1) {
			Get();
			Expect(4);
		}
		Expect(2);
		Expect(4);
		while (la.kind == 1) {
			Get();
			Expect(4);
		}
	}

	void FloatCondition() {
		Expect(5);
		while (la.kind == 1) {
			Get();
			Expect(5);
		}
		Expect(2);
		Expect(5);
		while (la.kind == 1) {
			Get();
			Expect(5);
		}
	}

	void VarCondition() {
		Expect(3);
		Expect(2);
		while (la.kind == 4 || la.kind == 5) {
			if (la.kind == 4) {
				Get();
			} else {
				Get();
			}
		}
	}

	void Expression() {
		if (la.kind == 8) {
			IntExpression();
		} else if (la.kind == 9) {
			FloatExpression();
		} else SynErr(22);
		Expect(6);
	}

	void IntExpression() {
		Expect(8);
		Expect(3);
		Expect(7);
		IntExprVar();
		while (la.kind == 1) {
			Get();
			IntExprVar();
		}
	}

	void FloatExpression() {
		Expect(9);
		Expect(3);
		Expect(7);
		FloatExprVar();
		while (la.kind == 1) {
			Get();
			FloatExprVar();
		}
	}

	void FloatExprVar() {
		if (la.kind == 5) {
			Get();
		} else if (la.kind == 3) {
			Get();
		} else SynErr(23);
	}

	void IntExprVar() {
		if (la.kind == 4) {
			Get();
		} else if (la.kind == 3) {
			Get();
		} else SynErr(24);
	}



	public void Parse() {
		la = new Token();
		la.val = "";		
		Get();
		LexicalAnalyzer();
		Expect(0);

	}
	
	static readonly bool[,] set = {
		{_T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x}

	};
} // end Parser


public class Errors {
	public int count = 0;                                    // number of errors detected
	public System.IO.TextWriter errorStream = Console.Out;   // error messages go to this stream
	public string errMsgFormat = "-- line {0} col {1}: {2}"; // 0=line, 1=column, 2=text

	public virtual void SynErr (int line, int col, int n) {
		string s;
		switch (n) {
			case 0: s = "EOF expected"; break;
			case 1: s = "operation expected"; break;
			case 2: s = "comparison expected"; break;
			case 3: s = "VarName expected"; break;
			case 4: s = "IntNum expected"; break;
			case 5: s = "FloatNum expected"; break;
			case 6: s = "Semicol expected"; break;
			case 7: s = "Equal expected"; break;
			case 8: s = "\"int\" expected"; break;
			case 9: s = "\"float\" expected"; break;
			case 10: s = "\"for\" expected"; break;
			case 11: s = "\"(\" expected"; break;
			case 12: s = "\")\" expected"; break;
			case 13: s = "\"{\" expected"; break;
			case 14: s = "\"}\" expected"; break;
			case 15: s = "\"if\" expected"; break;
			case 16: s = "\"else\" expected"; break;
			case 17: s = "??? expected"; break;
			case 18: s = "invalid LexicalAnalyzer"; break;
			case 19: s = "invalid TypeSpecifier"; break;
			case 20: s = "invalid Condition"; break;
			case 21: s = "invalid ForExpression"; break;
			case 22: s = "invalid Expression"; break;
			case 23: s = "invalid FloatExprVar"; break;
			case 24: s = "invalid IntExprVar"; break;

			default: s = "error " + n; break;
		}
		errorStream.WriteLine(errMsgFormat, line, col, s);
		count++;
	}

	public virtual void SemErr (int line, int col, string s) {
		errorStream.WriteLine(errMsgFormat, line, col, s);
		count++;
	}
	
	public virtual void SemErr (string s) {
		errorStream.WriteLine(s);
		count++;
	}
	
	public virtual void Warning (int line, int col, string s) {
		errorStream.WriteLine(errMsgFormat, line, col, s);
	}
	
	public virtual void Warning(string s) {
		errorStream.WriteLine(s);
	}
} // Errors


public class FatalError: Exception {
	public FatalError(string m): base(m) {}
}