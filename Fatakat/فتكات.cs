
using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;
using System.Windows.Forms;

namespace com.calitha.goldparser
{

    [Serializable()]
    public class SymbolException : System.Exception
    {
        public SymbolException(string message) : base(message)
        {
        }

        public SymbolException(string message,
            Exception inner) : base(message, inner)
        {
        }

        protected SymbolException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

    }

    [Serializable()]
    public class RuleException : System.Exception
    {

        public RuleException(string message) : base(message)
        {
        }

        public RuleException(string message,
                             Exception inner) : base(message, inner)
        {
        }

        protected RuleException(SerializationInfo info,
                                StreamingContext context) : base(info, context)
        {
        }

    }

    enum SymbolConstants : int
    {
        SYMBOL_EOF              =  0, // (EOF)
        SYMBOL_ERROR            =  1, // (Error)
        SYMBOL_WHITESPACE       =  2, // Whitespace
        SYMBOL_MINUS            =  3, // '-'
        SYMBOL_MINUSMINUS       =  4, // '--'
        SYMBOL_EXCLAMEQ         =  5, // '!='
        SYMBOL_PERCENT          =  6, // '%'
        SYMBOL_LPAREN           =  7, // '('
        SYMBOL_RPAREN           =  8, // ')'
        SYMBOL_TIMES            =  9, // '*'
        SYMBOL_TIMESTIMES       = 10, // '**'
        SYMBOL_DIV              = 11, // '/'
        SYMBOL_COLON            = 12, // ':'
        SYMBOL_SEMI             = 13, // ';'
        SYMBOL_LBRACE           = 14, // '{'
        SYMBOL_RBRACE           = 15, // '}'
        SYMBOL_PLUS             = 16, // '+'
        SYMBOL_PLUSPLUS         = 17, // '++'
        SYMBOL_LT               = 18, // '<'
        SYMBOL_EQ               = 19, // '='
        SYMBOL_EQEQ             = 20, // '=='
        SYMBOL_GT               = 21, // '>'
        SYMBOL_BRAEK            = 22, // braek
        SYMBOL_CASE             = 23, // case
        SYMBOL_DIGIT            = 24, // Digit
        SYMBOL_DOUBLE           = 25, // double
        SYMBOL_ELSE             = 26, // else
        SYMBOL_END              = 27, // End
        SYMBOL_FLOAT            = 28, // float
        SYMBOL_FOR              = 29, // for
        SYMBOL_ID               = 30, // Id
        SYMBOL_IF               = 31, // if
        SYMBOL_INT              = 32, // int
        SYMBOL_START            = 33, // Start
        SYMBOL_STRING           = 34, // string
        SYMBOL_SWITCH           = 35, // switch
        SYMBOL_ASSIGN           = 36, // <assign>
        SYMBOL_CASE2            = 37, // <case>
        SYMBOL_CASE_LIST        = 38, // <case_list>
        SYMBOL_CONCEPT          = 39, // <concept>
        SYMBOL_COND             = 40, // <cond>
        SYMBOL_DATA             = 41, // <data>
        SYMBOL_DIGIT2           = 42, // <digit>
        SYMBOL_EXP              = 43, // <exp>
        SYMBOL_EXPR             = 44, // <expr>
        SYMBOL_FACTOR           = 45, // <factor>
        SYMBOL_FOR_STAT         = 46, // <for_stat>
        SYMBOL_ID2              = 47, // <id>
        SYMBOL_IF_STAT          = 48, // <if_stat>
        SYMBOL_OP               = 49, // <op>
        SYMBOL_PROGRAM          = 50, // <program>
        SYMBOL_STAT_LIST        = 51, // <stat_list>
        SYMBOL_STEP             = 52, // <step>
        SYMBOL_SWITCH_CASE_STAT = 53, // <switch_case_stat>
        SYMBOL_TERM             = 54  // <term>
    };

    enum RuleConstants : int
    {
        RULE_PROGRAM_START_END                                   =  0, // <program> ::= Start <stat_list> End
        RULE_STAT_LIST                                           =  1, // <stat_list> ::= <concept>
        RULE_STAT_LIST2                                          =  2, // <stat_list> ::= <concept> <stat_list>
        RULE_CONCEPT                                             =  3, // <concept> ::= <assign>
        RULE_CONCEPT2                                            =  4, // <concept> ::= <for_stat>
        RULE_CONCEPT3                                            =  5, // <concept> ::= <if_stat>
        RULE_CONCEPT4                                            =  6, // <concept> ::= <switch_case_stat>
        RULE_ASSIGN_EQ_SEMI                                      =  7, // <assign> ::= <id> '=' <expr> ';'
        RULE_ID_ID                                               =  8, // <id> ::= Id
        RULE_EXPR_PLUS                                           =  9, // <expr> ::= <expr> '+' <term>
        RULE_EXPR_MINUS                                          = 10, // <expr> ::= <expr> '-' <term>
        RULE_EXPR                                                = 11, // <expr> ::= <term>
        RULE_TERM_TIMES                                          = 12, // <term> ::= <term> '*' <factor>
        RULE_TERM_DIV                                            = 13, // <term> ::= <term> '/' <factor>
        RULE_TERM_PERCENT                                        = 14, // <term> ::= <term> '%' <factor>
        RULE_TERM                                                = 15, // <term> ::= <factor>
        RULE_FACTOR_TIMESTIMES                                   = 16, // <factor> ::= <factor> '**' <exp>
        RULE_FACTOR                                              = 17, // <factor> ::= <exp>
        RULE_EXP_LPAREN_RPAREN                                   = 18, // <exp> ::= '(' <expr> ')'
        RULE_EXP                                                 = 19, // <exp> ::= <id>
        RULE_EXP2                                                = 20, // <exp> ::= <digit>
        RULE_DIGIT_DIGIT                                         = 21, // <digit> ::= Digit
        RULE_IF_STAT_IF_LPAREN_RPAREN_START_END                  = 22, // <if_stat> ::= if '(' <cond> ')' Start <stat_list> End
        RULE_IF_STAT_IF_LPAREN_RPAREN_START_END_ELSE             = 23, // <if_stat> ::= if '(' <cond> ')' Start <stat_list> End else <stat_list>
        RULE_COND                                                = 24, // <cond> ::= <expr> <op> <expr>
        RULE_OP_LT                                               = 25, // <op> ::= '<'
        RULE_OP_GT                                               = 26, // <op> ::= '>'
        RULE_OP_EQEQ                                             = 27, // <op> ::= '=='
        RULE_OP_EXCLAMEQ                                         = 28, // <op> ::= '!='
        RULE_FOR_STAT_FOR_LPAREN_SEMI_SEMI_RPAREN_LBRACE_RBRACE  = 29, // <for_stat> ::= for '(' <data> <assign> ';' <cond> ';' <step> ')' '{' <stat_list> '}'
        RULE_DATA_INT                                            = 30, // <data> ::= int
        RULE_DATA_FLOAT                                          = 31, // <data> ::= float
        RULE_DATA_STRING                                         = 32, // <data> ::= string
        RULE_DATA_DOUBLE                                         = 33, // <data> ::= double
        RULE_STEP_MINUSMINUS                                     = 34, // <step> ::= '--' <id>
        RULE_STEP_MINUSMINUS2                                    = 35, // <step> ::= <id> '--'
        RULE_STEP_PLUSPLUS                                       = 36, // <step> ::= <id> '++'
        RULE_STEP_PLUSPLUS2                                      = 37, // <step> ::= '++' <id>
        RULE_STEP                                                = 38, // <step> ::= <assign>
        RULE_SWITCH_CASE_STAT_SWITCH_LPAREN_RPAREN_LBRACE_RBRACE = 39, // <switch_case_stat> ::= switch '(' <expr> ')' '{' <case_list> '}'
        RULE_CASE_CASE_COLON_BRAEK_SEMI                          = 40, // <case> ::= case <exp> ':' <stat_list> braek ';'
        RULE_CASE_LIST                                           = 41, // <case_list> ::= <case>
        RULE_CASE_LIST2                                          = 42  // <case_list> ::= <case> <case_list>
    };

    public class MyParser
    {
        private LALRParser parser;
        ListBox list,lis2;

        public MyParser(string filename,ListBox list,ListBox list2)
        {
            FileStream stream = new FileStream(filename,
                                               FileMode.Open, 
                                               FileAccess.Read, 
                                               FileShare.Read);
            this.list = list;
            this.lis2 = list2;
            Init(stream);
            stream.Close();
        }

        public MyParser(string baseName, string resourceName)
        {
            byte[] buffer = ResourceUtil.GetByteArrayResource(
                System.Reflection.Assembly.GetExecutingAssembly(),
                baseName,
                resourceName);
            MemoryStream stream = new MemoryStream(buffer);
            Init(stream);
            stream.Close();
        }

        public MyParser(Stream stream)
        {
            Init(stream);
        }

        private void Init(Stream stream)
        {
            CGTReader reader = new CGTReader(stream);
            parser = reader.CreateNewParser();
            parser.TrimReductions = false;
            parser.StoreTokens = LALRParser.StoreTokensMode.NoUserObject;

            parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
            parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);
            parser.OnTokenRead += new LALRParser.TokenReadHandler(TokenReadEvent);
        }

        public void Parse(string source)
        {
            this.list.Items.Clear();
            NonterminalToken token = parser.Parse(source);
            if (token != null)
            {
                Object obj = CreateObject(token);
                //todo: Use your object any way you like
            }
        }

        private Object CreateObject(Token token)
        {
            if (token is TerminalToken)
                return CreateObjectFromTerminal((TerminalToken)token);
            else
                return CreateObjectFromNonterminal((NonterminalToken)token);
        }

        private Object CreateObjectFromTerminal(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)SymbolConstants.SYMBOL_EOF :
                //(EOF)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ERROR :
                //(Error)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHITESPACE :
                //Whitespace
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUS :
                //'-'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUSMINUS :
                //'--'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXCLAMEQ :
                //'!='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PERCENT :
                //'%'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LPAREN :
                //'('
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RPAREN :
                //')'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMES :
                //'*'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMESTIMES :
                //'**'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIV :
                //'/'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COLON :
                //':'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SEMI :
                //';'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LBRACE :
                //'{'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RBRACE :
                //'}'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUS :
                //'+'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUSPLUS :
                //'++'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LT :
                //'<'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQ :
                //'='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQEQ :
                //'=='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GT :
                //'>'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_BRAEK :
                //braek
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CASE :
                //case
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIGIT :
                //Digit
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DOUBLE :
                //double
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ELSE :
                //else
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_END :
                //End
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FLOAT :
                //float
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR :
                //for
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ID :
                //Id
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF :
                //if
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INT :
                //int
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_START :
                //Start
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STRING :
                //string
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SWITCH :
                //switch
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ASSIGN :
                //<assign>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CASE2 :
                //<case>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CASE_LIST :
                //<case_list>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CONCEPT :
                //<concept>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COND :
                //<cond>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DATA :
                //<data>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIGIT2 :
                //<digit>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXP :
                //<exp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXPR :
                //<expr>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FACTOR :
                //<factor>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR_STAT :
                //<for_stat>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ID2 :
                //<id>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF_STAT :
                //<if_stat>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_OP :
                //<op>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PROGRAM :
                //<program>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STAT_LIST :
                //<stat_list>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STEP :
                //<step>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SWITCH_CASE_STAT :
                //<switch_case_stat>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TERM :
                //<term>
                //todo: Create a new object that corresponds to the symbol
                return null;

            }
            throw new SymbolException("Unknown symbol");
        }

        public Object CreateObjectFromNonterminal(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.RULE_PROGRAM_START_END :
                //<program> ::= Start <stat_list> End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STAT_LIST :
                //<stat_list> ::= <concept>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STAT_LIST2 :
                //<stat_list> ::= <concept> <stat_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT :
                //<concept> ::= <assign>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT2 :
                //<concept> ::= <for_stat>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT3 :
                //<concept> ::= <if_stat>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT4 :
                //<concept> ::= <switch_case_stat>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASSIGN_EQ_SEMI :
                //<assign> ::= <id> '=' <expr> ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ID_ID :
                //<id> ::= Id
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR_PLUS :
                //<expr> ::= <expr> '+' <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR_MINUS :
                //<expr> ::= <expr> '-' <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR :
                //<expr> ::= <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_TIMES :
                //<term> ::= <term> '*' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_DIV :
                //<term> ::= <term> '/' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_PERCENT :
                //<term> ::= <term> '%' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM :
                //<term> ::= <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR_TIMESTIMES :
                //<factor> ::= <factor> '**' <exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR :
                //<factor> ::= <exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP_LPAREN_RPAREN :
                //<exp> ::= '(' <expr> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP :
                //<exp> ::= <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP2 :
                //<exp> ::= <digit>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DIGIT_DIGIT :
                //<digit> ::= Digit
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IF_STAT_IF_LPAREN_RPAREN_START_END :
                //<if_stat> ::= if '(' <cond> ')' Start <stat_list> End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IF_STAT_IF_LPAREN_RPAREN_START_END_ELSE :
                //<if_stat> ::= if '(' <cond> ')' Start <stat_list> End else <stat_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COND :
                //<cond> ::= <expr> <op> <expr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_LT :
                //<op> ::= '<'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_GT :
                //<op> ::= '>'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_EQEQ :
                //<op> ::= '=='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_EXCLAMEQ :
                //<op> ::= '!='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FOR_STAT_FOR_LPAREN_SEMI_SEMI_RPAREN_LBRACE_RBRACE :
                //<for_stat> ::= for '(' <data> <assign> ';' <cond> ';' <step> ')' '{' <stat_list> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DATA_INT :
                //<data> ::= int
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DATA_FLOAT :
                //<data> ::= float
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DATA_STRING :
                //<data> ::= string
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DATA_DOUBLE :
                //<data> ::= double
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_MINUSMINUS :
                //<step> ::= '--' <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_MINUSMINUS2 :
                //<step> ::= <id> '--'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_PLUSPLUS :
                //<step> ::= <id> '++'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_PLUSPLUS2 :
                //<step> ::= '++' <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP :
                //<step> ::= <assign>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_SWITCH_CASE_STAT_SWITCH_LPAREN_RPAREN_LBRACE_RBRACE :
                //<switch_case_stat> ::= switch '(' <expr> ')' '{' <case_list> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CASE_CASE_COLON_BRAEK_SEMI :
                //<case> ::= case <exp> ':' <stat_list> braek ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CASE_LIST :
                //<case_list> ::= <case>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CASE_LIST2 :
                //<case_list> ::= <case> <case_list>
                //todo: Create a new object using the stored tokens.
                return null;

            }
            throw new RuleException("Unknown rule");
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            string message = "Token error with input: '"+args.Token.ToString()+"'";
            //todo: Report message to UI?
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = "Error: "+args.UnexpectedToken.ToString()+"in line : "+args.UnexpectedToken.Location.LineNr;
            list.Items.Add(message);
            String message2 = "Expected: "+args.ExpectedTokens.ToString();
            list.Items.Add(message2);
            //todo: Report message to UI?
        }

        private void TokenReadEvent(LALRParser parser,TokenReadEventArgs args)
        {
            string info = args.Token.Text + "  \t\t" + (SymbolConstants) args.Token.Symbol.Id;
            this.lis2.Items.Add(info);
        }
    }
}
