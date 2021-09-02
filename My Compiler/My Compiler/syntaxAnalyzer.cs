using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Compiler
{
    class syntaxAnalyzer
    {
        static int i = 0;
        List<token> tokenList = new List<token>();
        public void checkSyntactically(List<token> tokenList)
        {
            this.tokenList = tokenList;
            if (s())
            {
                if (tokenList[i].classPart == "$")
                {
                    Console.WriteLine("No Syntax Error!");
                }
                else
                {
                    Console.WriteLine("Syntax Error At Line Number: " + tokenList[i].lineNumber);
                }
            }
            else 
            {
                Console.WriteLine("Syntax Error At Line Number: " + tokenList[i].lineNumber);
            }
        }
        public bool s()
        {
            if (tokenList[i].classPart == "Public" || tokenList[i].classPart == "Protected" || tokenList[i].classPart == "Private" || tokenList[i].classPart == "Static" || tokenList[i].classPart == "Abstract" || tokenList[i].classPart == "Final" || tokenList[i].classPart == "Class" || tokenList[i].classPart == "Main")
            {
                if (classRep())
                {
                    if (tokenList[i].classPart == "Main")
                    {
                        i++;
                        if (tokenList[i].classPart == "(")
                        {
                            i++;
                            if (tokenList[i].classPart == ")")
                            {
                                i++;
                                if (tokenList[i].classPart == "{")
                                {
                                    i++;
                                    if (fmst())
                                    {
                                        if (tokenList[i].classPart == "}")
                                        {
                                            i++;
                                            if (classRep())
                                            {
                                                return true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
        public bool classRep()
        {
            if (tokenList[i].classPart == "Public" || tokenList[i].classPart == "Protected" || tokenList[i].classPart == "Private" || tokenList[i].classPart == "Static" || tokenList[i].classPart == "Abstract" || tokenList[i].classPart == "Final" || tokenList[i].classPart == "Class")
            {
                if (klass())
                {
                    if (classRep())
                    {
                        return true;
                    }
                }
            }
            else if (tokenList[i].classPart == "Main" || tokenList[i].classPart == "$")
            {
                return true;
            }
            return false;
        }
        public bool fmst()
        {
            if (tokenList[i].classPart == "Data Type" || tokenList[i].classPart == "If" || tokenList[i].classPart == "For" || tokenList[i].classPart == "While" || tokenList[i].classPart == "Identifier" || tokenList[i].classPart == "Return")
            {
                if (fsst())
                {
                    if (fmst())
                    {
                        return true;
                    }
                }
            }
            else if (tokenList[i].classPart == "}")
            {
                return true;
            }
            return false;
        }
        public bool klass()
        {
            if (tokenList[i].classPart == "Public" || tokenList[i].classPart == "Protected" || tokenList[i].classPart == "Private" || tokenList[i].classPart == "Static" || tokenList[i].classPart == "Abstract" || tokenList[i].classPart == "Final" || tokenList[i].classPart == "Class")
            {
                if (am())
                {
                    if (ctm())
                    {
                        if (tokenList[i].classPart == "Class")
                        {
                            i++;
                            if (tokenList[i].classPart == "Identifier")
                            {
                                i++;
                                if (inh())
                                {
                                    if (tokenList[i].classPart == "{")
                                    {
                                        i++;
                                        if (cmst())
                                        {
                                            if (tokenList[i].classPart == "}")
                                            {
                                                i++;
                                                return true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
        public bool am()
        {
            if (tokenList[i].classPart == "Public")
            {
                i++;
                return true;
            }
            else if (tokenList[i].classPart == "Protected")
            {
                i++;
                return true;
            }
            else if (tokenList[i].classPart == "Private")
            {
                i++;
                return true;
            }
            else if (tokenList[i].classPart == "Static" || tokenList[i].classPart == "Abstract" || tokenList[i].classPart == "Final" || tokenList[i].classPart == "Class" || tokenList[i].classPart == "Data Type" || tokenList[i].classPart == "Identifier" || tokenList[i].classPart == "Void")
            {
                return true;
            }
            return false;
        }
        public bool ctm()
        {
            if (tokenList[i].classPart == "Static")
            {
                i++;
                return true;
            }
            else if (tokenList[i].classPart == "Abstract")
            {
                i++;
                return true;
            }
            else if (tokenList[i].classPart == "Final")
            {
                i++;
                return true;
            }
            else if (tokenList[i].classPart == "Class")
            {
                return true;
            }
            return false; 
        }
        public bool cmst()
        {
            if (tokenList[i].classPart == "Public" || tokenList[i].classPart == "Protected" || tokenList[i].classPart == "Private" || tokenList[i].classPart == "Static" || tokenList[i].classPart == "Data Type" || tokenList[i].classPart == "Identifier" || tokenList[i].classPart == "~")
            {
                if (csst())
                {
                    if (cmst())
                    {
                        return true;
                    }
                }
            }
            else if (tokenList[i].classPart == "}")
            {
                return true;
            }
            return false;
        }
        public bool fsst()
        {
            if (tokenList[i].classPart == "Data Type")
            {
                if (dec())
                {
                    return true;
                }
            }
            else if (tokenList[i].classPart == "If")
            {
                if (ifElse())
                {
                    return true;
                }
            }
            else if (tokenList[i].classPart == "For")
            {
                if (forSt())
                {
                    return true;
                }
            }
            else if (tokenList[i].classPart == "While")
            {
                if (whileSt())
                {
                    return true;
                }
            }
            else if (tokenList[i].classPart == "Identifier")
            {
                i++;
                if (a())
                {
                    return true;
                }
            }
            else if (tokenList[i].classPart == "Return")
            {
                i++;
                if (oe())
                {
                    if (tokenList[i].classPart == ";")
                    {
                        i++;
                        return true;
                    }
                }
            }
            return false;
        }
        public bool dec()
        {
            if (tokenList[i].classPart == "Data Type")
            {
                i++;
                if (dec2())
                {
                    return true;
                }
            }
            return false;
        }
        public bool ifElse()
        {
            if (tokenList[i].classPart == "If")
            {
                i++;
                if (tokenList[i].classPart == "(")
                {
                    i++;
                    if (oe())
                    {
                        if (tokenList[i].classPart == ")")
                        {
                            i++;
                            if (tokenList[i].classPart == "{")
                            {
                                i++;
                                if (lmst())
                                {
                                    if (tokenList[i].classPart == "}")
                                    {
                                        i++;
                                        if (oElse())
                                        {
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
        public bool forSt()
        {
            if (tokenList[i].classPart == "For")
            {
                i++;
                if (tokenList[i].classPart == "(")
                {
                    i++;
                    if (c1())
                    {
                        if (c2())
                        {
                            if (tokenList[i].classPart == ";")
                            {
                                i++;
                                if (c3())
                                {
                                    if (tokenList[i].classPart == ")")
                                    {
                                        i++;
                                        if (tokenList[i].classPart == "{")
                                        {
                                            i++;
                                            if (lmst())
                                            {
                                                if (tokenList[i].classPart == "}")
                                                {
                                                    i++;
                                                    return true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
        public bool whileSt()
        {
            if (tokenList[i].classPart == "While")
            {
                i++;
                if (tokenList[i].classPart == "(")
                {
                    i++;
                    if (oe())
                    {
                        if(tokenList[i].classPart == ")")
                        {
                            i++;
                            if (tokenList[i].classPart == "{")
                            {
                                i++;
                                if (lmst())
                                {
                                    if (tokenList[i].classPart == "}")
                                    {
                                        i++;
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
        public bool a()
        {
            if (tokenList[i].classPart == "Identifier")
            {
                i++;
                if (oInit())
                {
                    if (oList())
                    {
                        return true;
                    }
                }
            }
            else if (tokenList[i].classPart == "[")
            {
                i++;
                if (ab())
                {
                    return true;
                }
            }
            else if (tokenList[i].classPart == "." || tokenList[i].classPart == "=")
            {
                if (a1Sst())
                {
                    return true;
                }
            }
            else if (tokenList[i].classPart == "(")
            {
                i++;
                if (pl())
                {
                    if (tokenList[i].classPart == ")")
                    {
                        i++;
                        if (a2Sst())
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public bool oe()
        {
            if (tokenList[i].classPart == "Integer Constant" || tokenList[i].classPart == "String Constant" || tokenList[i].classPart == "Float Constant" || tokenList[i].classPart == "Bool Constant" || tokenList[i].classPart == "Null Constant" || tokenList[i].classPart == "(" || tokenList[i].classPart == "!" || tokenList[i].classPart == "Identifier")
            {
                if (ae())
                {
                    if (oe1())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool csst()
        {
            if (tokenList[i].classPart == "Public" || tokenList[i].classPart == "Protected" || tokenList[i].classPart == "Private" || tokenList[i].classPart == "Static" || tokenList[i].classPart == "Data Type" || tokenList[i].classPart == "Identifier")
            {
                if (am())
                {
                    if (ftm())
                    {
                        if (extra())
                        {
                            return true;
                        }
                    }
                }
            }
            else if (tokenList[i].classPart == "~")
            {
                i++;
                if (fdec())
                {
                    return true;
                }
            }
            return false;
        }
        public bool dec2()
        {
            if (tokenList[i].classPart == "Identifier")
            {
                i++;
                if (init())
                {
                    if (list())
                    {
                        return true;
                    }
                }
            }
            else if (tokenList[i].classPart == "[")
            {
                i++;
                if (tokenList[i].classPart == "]")
                {
                    i++;
                    if (tokenList[i].classPart == "Identifier")
                    {
                        i++;
                        if (init2())
                        {
                            if (list2())
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        public bool lmst()
        {
            if (tokenList[i].classPart == "Data Type" || tokenList[i].classPart == "If" || tokenList[i].classPart == "For" || tokenList[i].classPart == "While" || tokenList[i].classPart == "Identifier" || tokenList[i].classPart == "Continue" || tokenList[i].classPart == "Break")
            {
                if (lsst())
                {
                    if (lmst())
                    {
                        return true;
                    }
                }
            }
            else if (tokenList[i].classPart == "}")
            {
                return true;
            }
            return false;
        }
        public bool oElse()
        {
            if (tokenList[i].classPart == "Else")
            {
                i++;
                if (tokenList[i].classPart == "{")
                {
                    i++;
                    if (lmst())
                    {
                        if (tokenList[i].classPart == "}")
                        {
                            i++;
                            return true;
                        }
                    }
                }
            }
            else if (tokenList[i].classPart == "Data Type" || tokenList[i].classPart == "Identifier" || tokenList[i].classPart == "If" || tokenList[i].classPart == "For" || tokenList[i].classPart == "While" || tokenList[i].classPart == "Return" || tokenList[i].classPart == "Continue" || tokenList[i].classPart == "Break" || tokenList[i].classPart == "}")
            {
                return true;
            }
            return false;
        }
        public bool c1()
        {
            if (tokenList[i].classPart == "Data Type")
            {
                if (dec())
                {
                    return true;
                }
            }
            else if (tokenList[i].classPart == "Identifier")
            {
                i++;
                if (a())
                {
                    return true;
                }
            }
            else if (tokenList[i].classPart == ";")
            {
                i++;
                return true;
            }
            return false;
        }
        public bool c2()
        {
            if (tokenList[i].classPart == "Integer Constant" || tokenList[i].classPart == "String Constant" || tokenList[i].classPart == "Float Constant" || tokenList[i].classPart == "Bool Constant" || tokenList[i].classPart == "Null Constant" || tokenList[i].classPart == "(" || tokenList[i].classPart == "!" || tokenList[i].classPart == "Identifier")
            {
                if (oe())
                {
                    return true;
                }
            }
            else if (tokenList[i].classPart == ";")
            {
                return true;
            }
            return false;
        }
        public bool c3()
        {
            if (tokenList[i].classPart == "Identifier")
            {
                i++;
                if (x())
                {
                    if (tokenList[i].classPart == "=")
                    {
                        i++;
                        if (oe())
                        {
                            return true;
                        }
                    }
                }
            }
            else if (tokenList[i].classPart == ")")
            {
                return true;
            }
            return false;
        }
        public bool oNow()
        {
            if (tokenList[i].classPart == "New")
            {
                i++;
                if (tokenList[i].classPart == "Identifier")
                {
                    i++;
                    if (tokenList[i].classPart == "(")
                    {
                        i++;
                        if (pl())
                        {
                            if(tokenList[i].classPart == ")")
                            {
                                i++;
                                return true;
                            }
                        }
                    }
                }
            }
            else if (tokenList[i].classPart == "Identifier")
            {
                i++;
                return true;
            }
            return false;
        }
        public bool oList()
        {
            if (tokenList[i].classPart == ";")
            {
                i++;
                return true;
            }
            else if (tokenList[i].classPart == ",")
            {
                i++;
                if (tokenList[i].classPart == "Identifier")
                {
                    i++;
                    if (oInit())
                    {
                        if (oList())
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public bool ab()
        {
            if (tokenList[i].classPart == "Integer Constant" || tokenList[i].classPart == "String Constant" || tokenList[i].classPart == "Float Constant" || tokenList[i].classPart == "Bool Constant" || tokenList[i].classPart == "Null Constant" || tokenList[i].classPart == "(" || tokenList[i].classPart == "!" || tokenList[i].classPart == "Identifier")
            {
                if (oe())
                {
                    if (tokenList[i].classPart == "]")
                    {
                        i++;
                        if (a1Sst())
                        {
                            return true;
                        }
                    }
                }
            }
            else if (tokenList[i].classPart == "]")
            {
                i++;
                if (tokenList[i].classPart == "Identifier")
                {
                    i++;
                    if (oInit2())
                    {
                        if (oList2())
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public bool a1Sst()
        {
            if (tokenList[i].classPart == ".")
            {
                i++;
                if (tokenList[i].classPart == "Identifier")
                {
                    i++;
                    if (aSst())
                    {
                        return true;
                    }
                }
            }
            else if (tokenList[i].classPart == "=")
            {
                i++;
                if (oe())
                {
                    if (tokenList[i].classPart == ";")
                    {
                        i++;
                        return true;
                    }
                }
            }
            return false;
        }
        public bool pl()
        {
            if (tokenList[i].classPart == "Integer Constant" || tokenList[i].classPart == "String Constant" || tokenList[i].classPart == "Float Constant" || tokenList[i].classPart == "Bool Constant" || tokenList[i].classPart == "Null Constant" || tokenList[i].classPart == "(" || tokenList[i].classPart == "!" || tokenList[i].classPart == "Identifier")
            {
                if (oe())
                {
                    if (pl1())
                    {
                        return true;
                    }
                }
            }
            else if (tokenList[i].classPart == ")" || tokenList[i].classPart == "}")
            {
                return true;
            }
            return false;
        }
        public bool a2Sst()
        {
            if (tokenList[i].classPart == ".")
            {
                i++;
                if (tokenList[i].classPart == "Identifier")
                {
                    i++;
                    if (aSst())
                    {
                        return true;
                    }
                }
            }
            else if (tokenList[i].classPart == "[")
            {
                i++;
                if (oe())
                {
                    if (tokenList[i].classPart == "]")
                    {
                        i++;
                        if (a1Sst())
                        {
                            return true;
                        }
                    }
                }
            }
            else if (tokenList[i].classPart == ";")
            {
                i++;
                return true;
            }
            return false;
        }
        public bool ae()
        {
            if (tokenList[i].classPart == "Integer Constant" || tokenList[i].classPart == "String Constant" || tokenList[i].classPart == "Float Constant" || tokenList[i].classPart == "Bool Constant" || tokenList[i].classPart == "Null Constant" || tokenList[i].classPart == "(" || tokenList[i].classPart == "!" || tokenList[i].classPart == "Identifier")
            {
                if (re())
                {
                    if (ae1())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool oe1()
        {
            if (tokenList[i].classPart == "||")
            {
                i++;
                if (ae())
                {
                    if (oe1())
                    {
                        return true;
                    }
                }
            }
            else if (tokenList[i].classPart == ")" || tokenList[i].classPart == "]" || tokenList[i].classPart == "," || tokenList[i].classPart == "}" || tokenList[i].classPart == ";")
            {
                return true;
            }
            return false;
        }
        public bool ftm()
        {
            if (tokenList[i].classPart == "Static")
            {
                i++;
                return true;
            }
            else if (tokenList[i].classPart == "Data Type" || tokenList[i].classPart == "Identifier" || tokenList[i].classPart == "Void")
            {
                return true;
            }
            return false;
        }
        public bool extra()
        {
            if (tokenList[i].classPart == "Data Type")
            {
                if (dec())
                {
                    return true;
                }
            }
            else if (tokenList[i].classPart == "Identifier")
            {
                i++;
                if (extra2())
                {
                    return true;
                }
            }
            return false;
        }
        public bool fdec()
        {
            if (tokenList[i].classPart == "Public" || tokenList[i].classPart == "Protected" || tokenList[i].classPart == "Private" || tokenList[i].classPart == "Static" || tokenList[i].classPart == "Data Type" || tokenList[i].classPart == "Identifier" || tokenList[i].classPart == "Void")
            {
                if (am())
                {
                    if (ftm())
                    {
                        if (rt())
                        {
                            if (tokenList[i].classPart == "Identifier")
                            {
                                i++;
                                if (tokenList[i].classPart == "(")
                                {
                                    i++;
                                    if(dpl())
                                    {
                                        if (tokenList[i].classPart == ")")
                                        {
                                            i++;
                                            if(tokenList[i].classPart == "{")
                                            {
                                                i++;
                                                if (fmst())
                                                {
                                                    if (tokenList[i].classPart == "}")
                                                    {
                                                        i++;
                                                        return true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
        public bool init()
        {
            if (tokenList[i].classPart == "=")
            {
                i++;
                if (oe())
                {
                    return true;
                }
            }
            else if (tokenList[i].classPart == ";" || tokenList[i].classPart == ",")
            {
                return true;
            }
            return false;
        }
        public bool list()
        {
            if (tokenList[i].classPart == ";")
            {
                i++;
                return true;
            }
            else if (tokenList[i].classPart == ",")
            {
                i++;
                if (tokenList[i].classPart == "Identifier")
                {
                    i++;
                    if (init())
                    {
                        if (list())
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public bool init2()
        {
            if (tokenList[i].classPart == "=")
            {
                i++;
                if (now())
                {
                    return true;
                }
            }
            else if (tokenList[i].classPart == ";" || tokenList[i].classPart == ",")
            {
                return true;
            }
            return false;
        }
        public bool list2()
        {
            if (tokenList[i].classPart == ";")
            {
                i++;
                return true;
            }
            else if (tokenList[i].classPart == ",")
            {
                i++;
                if (tokenList[i].classPart == "Identifier")
                {
                    i++;
                    if (init2())
                    {
                        if (list2())
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public bool lsst()
        {
            if (tokenList[i].classPart == "Data Type")
            {
                if (dec())
                {
                    return true;
                }
            }
            else if (tokenList[i].classPart == "If")
            {
                if (ifElse())
                {
                    return true;
                }
            }
            else if (tokenList[i].classPart == "For")
            {
                if (forSt())
                {
                    return true;
                }
            }
            else if (tokenList[i].classPart == "While")
            {
                if(whileSt())
                {
                    return true;
                }
            }
            else if (tokenList[i].classPart == "Identifier")
            {
                i++;
                if (a())
                {
                    return true;
                }
            }
            else if (tokenList[i].classPart == "Continue")
            {
                i++;
                if (tokenList[i].classPart == ";")
                {
                    i++;
                    return true;
                }
            }
            else if (tokenList[i].classPart == "Break")
            {
                i++;
                if (tokenList[i].classPart == ";")
                {
                    i++;
                    return true;
                }
            }
            return false;
        }
        public bool x()
        {
            if (tokenList[i].classPart == "[")
            {
                i++;
                if (oe())
                {
                    if (tokenList[i].classPart == "]")
                    {
                        i++;
                        if (yx())
                        {
                            return true;
                        }
                    }
                }
            }
            else if (tokenList[i].classPart == "(")
            {
                i++;
                if (pl())
                {
                    if(tokenList[i].classPart == ")")
                    {
                        i++;
                        if (tokenList[i].classPart == ".")
                        {
                            i++;
                            if (tokenList[i].classPart == "Identifier")
                            {
                                i++;
                                if (x())
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            else if (tokenList[i].classPart == ".")
            {
                i++;
                if (tokenList[i].classPart == "Identifier")
                {
                    i++;
                    if (x())
                    {
                        return true;
                    }
                }
            }
            else if (tokenList[i].classPart == "=")
            {
                return true;
            }
            return false;
        }
        public bool oInit()
        {
            if (tokenList[i].classPart == "=")
            {
                i++;
                if (oNow())
                {
                    return true;
                }
            }
            else if (tokenList[i].classPart == ";" || tokenList[i].classPart == ",")
            {
                return true;
            }
            return false;
        }
        public bool oNow2()
        {
            if (tokenList[i].classPart == "New")
            {
                i++;
                if (tokenList[i].classPart == "Identifier")
                {
                    i++;
                    if (tokenList[i].classPart == "[")
                    {
                        i++;
                        if (oe())
                        {
                            if (tokenList[i].classPart == "]")
                            {
                                i++;
                                return true;
                            }
                        }
                    }
                }
            }
            else if (tokenList[i].classPart == "{")
            {
                i++;
                if (pl())
                {
                    if (tokenList[i].classPart == "}")
                    {
                        i++;
                        return true;
                    }
                }
            }
            else if (tokenList[i].classPart == "Identifier")
            {
                i++;
                return true;
            }
            return false;
        }
        public bool oList2()
        {
            if (tokenList[i].classPart == ";")
            {
                i++;
                return true;
            }
            else if(tokenList[i].classPart == ",")
            {
                i++;
                if (tokenList[i].classPart == "Identifier")
                {
                    i++;
                    if (oInit2())
                    {
                        if (oList2())
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public bool aSst()
        {
            if (tokenList[i].classPart == "(")
            {
                i++;
                if (pl())
                {
                    if (tokenList[i].classPart == ")")
                    {
                        i++;
                        if (a2Sst())
                        {
                            return true;
                        }
                    }
                }
            }
            else if (tokenList[i].classPart == ".")
            {
                i++;
                if (tokenList[i].classPart == "Identifier")
                {
                    i++;
                    if (aSst())
                    {
                        return true;
                    }
                }
            }
            else if (tokenList[i].classPart == "=")
            {
                i++;
                if (oe())
                {
                    if (tokenList[i].classPart == ";")
                    {
                        i++;
                        return true;
                    }
                }
            }
            else if (tokenList[i].classPart == "[")
            {
                i++;
                if (oe())
                {
                    if (tokenList[i].classPart == "]")
                    {
                        i++;
                        if (a1Sst())
                        {
                            return true;
                        }
                    }
                }
            }
            else if (tokenList[i].classPart == ";")
            {
                i++;
                return true;
            }
            return false;
        }
        public bool pl1()
        {
            if (tokenList[i].classPart == ",")
            {
                i++;
                if (oe())
                {
                    if (pl1())
                    {
                        return true;
                    }
                }
            }
            else if (tokenList[i].classPart == ")" || tokenList[i].classPart == "}")
            {
                return true;
            }
            return false;
        }
        public bool re()
        {
            if (tokenList[i].classPart == "Integer Constant" || tokenList[i].classPart == "String Constant" || tokenList[i].classPart == "Float Constant" || tokenList[i].classPart == "Bool Constant" || tokenList[i].classPart == "Null Constant" || tokenList[i].classPart == "(" || tokenList[i].classPart == "!" || tokenList[i].classPart == "Identifier")
            {
                if (e())
                {
                    if (re1())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool ae1()
        {
            if (tokenList[i].classPart == "&&")
            {
                i++;
                if (re())
                {
                    if(ae1())
                    {
                        return true;
                    }
                }
            }
            else if (tokenList[i].classPart == "||" || tokenList[i].classPart == ")" || tokenList[i].classPart == "]" || tokenList[i].classPart == "," || tokenList[i].classPart == "}" || tokenList[i].classPart == ";")
            {
                return true;
            }
            return false;
        }
        public bool extra2()
        {
            if (tokenList[i].classPart == "Identifier" || tokenList[i].classPart == "[")
            {
                if (oDec2())
                {
                    return true;
                }
            }
            else if (tokenList[i].classPart == "(")
            {
                i++;
                if (dpl())
                {
                    if (tokenList[i].classPart == ")")
                    {
                        i++;
                        if (tokenList[i].classPart == "{")
                        {
                            i++;
                            if (fmst())
                            {
                                if (tokenList[i].classPart == "}")
                                {
                                    i++;
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
        public bool rt()
        {
            if (tokenList[i].classPart == "Data Type")
            {
                i++;
                if (choice())
                {
                    return true;
                }
            }
            else if (tokenList[i].classPart == "Identifier")
            {
                i++;
                if (choice())
                {
                    return true;
                }
            }
            else if (tokenList[i].classPart == "Void")
            {
                i++;
                return true;
            }
            return false;
        }
        public bool dpl()
        {
            if (tokenList[i].classPart == "Data Type" || tokenList[i].classPart == "Identifier" || tokenList[i].classPart == "Void")
            {
                if (rt())
                {
                    if (tokenList[i].classPart == "Identifier")
                    {
                        i++;
                        if (dpll())
                        {
                            return true;
                        }
                    }
                }
            }
            else if (tokenList[i].classPart == ")")
            {
                return true;
            }
            return false;
        }
        public bool now()
        {
            if (tokenList[i].classPart == "New")
            {
                i++;
                if (tokenList[i].classPart == "Data Type")
                {
                    i++;
                    if(tokenList[i].classPart == "[")
                    {
                        i++;
                        if (oe())
                        {
                            if (tokenList[i].classPart == "]")
                            {
                                i++;
                                return true;
                            }
                        }
                    }
                }
            }
            else if (tokenList[i].classPart == "{")
            {
                i++;
                if (pl())
                {
                    if (tokenList[i].classPart == "}")
                    {
                        i++;
                        return true;
                    }
                }
            }
            else if (tokenList[i].classPart == "Identifier")
            {
                i++;
                return true;
            }
            return false;
        }
        public bool yx()
        {
            if (tokenList[i].classPart == ".")
            {
                i++;
                if (tokenList[i].classPart == "Identifier")
                {
                    i++;
                    if (x())
                    {
                        return true;
                    }
                }
            }
            else if (tokenList[i].classPart == "=")
            {
                return true;
            }
            return false;
        }
        public bool oInit2()
        {
            if (tokenList[i].classPart == "=")
            {
                i++;
                if (oNow2())
                {
                    return true;
                }
            }
            else if (tokenList[i].classPart == ";" || tokenList[i].classPart == ",")
            {
                return true;
            }
            return false;
        }
        public bool e()
        {
            if (tokenList[i].classPart == "Integer Constant" || tokenList[i].classPart == "String Constant" || tokenList[i].classPart == "Float Constant" || tokenList[i].classPart == "Bool Constant" || tokenList[i].classPart == "Null Constant" || tokenList[i].classPart == "(" || tokenList[i].classPart == "!" || tokenList[i].classPart == "Identifier")
            {
                if (t())
                {
                    if (e1())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool re1()
        {
            if (tokenList[i].classPart == "ROP")
            {
                i++;
                if (e())
                {
                    if (re1())
                    {
                        return true;
                    }
                }
            }
            else if (tokenList[i].classPart == "&&" || tokenList[i].classPart == "||" || tokenList[i].classPart == ")" || tokenList[i].classPart == "]" || tokenList[i].classPart == "," || tokenList[i].classPart == "}" || tokenList[i].classPart == ";")
            {
                return true;
            }
            return false;
        }
        public bool oDec2()
        {
            if (tokenList[i].classPart == "Identifier")
            {
                i++;
                if (oInit())
                {
                    if (oList())
                    {
                        return true;
                    }
                }
            }
            else if (tokenList[i].classPart == "[")
            {
                i++;
                if (tokenList[i].classPart == "]")
                {
                    i++;
                    if (tokenList[i].classPart == "Identifier")
                    {
                        i++;
                        if (oInit2())
                        {
                            if (oList2())
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        public bool choice()
        {
            if (tokenList[i].classPart == "[")
            {
                i++;
                if (tokenList[i].classPart == "]")
                {
                    i++;
                    return true;
                }
            }
            else if (tokenList[i].classPart == "Identifier")
            {
                return true;
            }
            return false;
        }
        public bool dpll()
        {
            if (tokenList[i].classPart == ",")
            {
                i++;
                if (rt())
                {
                    if (tokenList[i].classPart == "Identifier")
                    {
                        i++;
                        if (dpll())
                        {
                            return true;
                        }
                    }
                }
            }
            else if (tokenList[i].classPart == ")")
            {
                return true;
            }
            return false;
        }
        public bool t()
        {
            if (tokenList[i].classPart == "Integer Constant" || tokenList[i].classPart == "String Constant" || tokenList[i].classPart == "Float Constant" || tokenList[i].classPart == "Bool Constant" || tokenList[i].classPart == "Null Constant" || tokenList[i].classPart == "(" || tokenList[i].classPart == "!" || tokenList[i].classPart == "Identifier")
            {
                if (f())
                {
                    if (t1())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool e1()
        {
            if (tokenList[i].classPart == "PM")
            {
                i++;
                if (t())
                {
                    if (e1())
                    {
                        return true;
                    }
                }
            }
            else if (tokenList[i].classPart == "ROP" || tokenList[i].classPart == "&&" || tokenList[i].classPart == "||" || tokenList[i].classPart == ")" || tokenList[i].classPart == "]" || tokenList[i].classPart == "," || tokenList[i].classPart == "}" || tokenList[i].classPart == ";")
            {
                return true;
            }
            return false;
        }
        public bool f()
        {
            if (tokenList[i].classPart == "Integer Constant" || tokenList[i].classPart == "String Constant" || tokenList[i].classPart == "Float Constant" || tokenList[i].classPart == "Bool Constant" || tokenList[i].classPart == "Null Constant")
            {
                if (constant())
                {
                    return true;
                }
            }
            else if (tokenList[i].classPart == "(")
            {
                i++;
                if (oe())
                {
                    if (tokenList[i].classPart == ")")
                    {
                        i++;
                        return true;
                    }
                }
            }
            else if (tokenList[i].classPart == "!")
            {
                i++;
                if (f())
                {
                    return true;
                }
            }
            else if (tokenList[i].classPart == "Identifier")
            {
                i++;
                if (z())
                {
                    return true;
                }
            }
            return false;
        }
        public bool t1()
        {
            if (tokenList[i].classPart == "MDM")
            {
                i++;
                if (f())
                {
                    if (t1())
                    {
                        return true;
                    }
                }
            }
            else if (tokenList[i].classPart == "PM" || tokenList[i].classPart == "ROP" || tokenList[i].classPart == "&&" || tokenList[i].classPart == "||" || tokenList[i].classPart == ")" || tokenList[i].classPart == "]" || tokenList[i].classPart == "," || tokenList[i].classPart == "}" || tokenList[i].classPart == ";")
            {
                return true;
            }
            return false;
        }
        public bool constant()
        {
            if (tokenList[i].classPart == "Integer Constant")
            {
                i++;
                return true;
            }
            else if (tokenList[i].classPart == "String Constant")
            {
                i++;
                return true;
            }
            else if (tokenList[i].classPart == "Float Constant")
            {
                i++;
                return true;
            }
            else if (tokenList[i].classPart == "Bool Constant")
            {
                i++;
                return true;
            }
            else if (tokenList[i].classPart == "Null Constant")
            {
                i++;
                return true;
            }
            return false;
        }
        public bool z()
        {
            if (tokenList[i].classPart == ".")
            {
                i++;
                if (tokenList[i].classPart == "Identifier")
                {
                    i++;
                    if (z())
                    {
                        return true;
                    }
                }
            }
            else if (tokenList[i].classPart == "[")
            {
                i++;
                if (oe())
                {
                    if (tokenList[i].classPart == "]")
                    {
                        i++;
                        if (z1())
                        {
                            return true;
                        }
                    }
                }
            }
            else if (tokenList[i].classPart == "(")
            {
                i++;
                if (pl())
                {
                    if (tokenList[i].classPart == ")")
                    {
                        i++;
                        if (z())
                        {
                            return true;
                        }
                    }
                }
            }
            else if (tokenList[i].classPart == "MDM" || tokenList[i].classPart == "PM" || tokenList[i].classPart == "ROP" || tokenList[i].classPart == "&&" || tokenList[i].classPart == "||" || tokenList[i].classPart == ")" || tokenList[i].classPart == "]" || tokenList[i].classPart == "," || tokenList[i].classPart == "}" || tokenList[i].classPart == ";")
            {
                return true;
            }
            return false;
        }
        public bool z1()
        {
            if (tokenList[i].classPart == ".")
            {
                i++;
                if (tokenList[i].classPart == "Identifier")
                {
                    i++;
                    if (z())
                    {
                        return true;
                    }
                }
            }
            else if (tokenList[i].classPart == "MDM" || tokenList[i].classPart == "PM" || tokenList[i].classPart == "ROP" || tokenList[i].classPart == "&&" || tokenList[i].classPart == "||" || tokenList[i].classPart == ")" || tokenList[i].classPart == "]" || tokenList[i].classPart == "," || tokenList[i].classPart == "}" || tokenList[i].classPart == ";")
            {
                return true;
            }
            return false;
        }
        public bool inh()
        {
            if (tokenList[i].classPart == "Extends")
            {
                i++;
                if (tokenList[i].classPart == "Identifier")
                {
                    i++;
                    return true;
                }
            }
            else if (tokenList[i].classPart == "{")
            {
                return true;
            }
            return false;
        }
        public bool mmst()
        {
            if (tokenList[i].classPart == "Data Type" || tokenList[i].classPart == "If" || tokenList[i].classPart == "For" || tokenList[i].classPart == "While" || tokenList[i].classPart == "Identifier")
            {
                if (msst())
                {
                    if (mmst())
                    {
                        return true;
                    }
                }
            }
            else if (tokenList[i].classPart == "}")
            {
                return true;
            }
            return false;
        }
        public bool msst()
        {
            if (tokenList[i].classPart == "Data Type")
            {
                if (dec())
                {
                    return true;
                }
            }
            else if (tokenList[i].classPart == "If")
            {
                if (ifElse())
                {
                    return true;
                }
            }
            else if (tokenList[i].classPart == "For")
            {
                if (forSt())
                {
                    return true;
                }
            }
            else if (tokenList[i].classPart == "While")
            {
                if (whileSt())
                {
                    return true;
                }
            }
            else if (tokenList[i].classPart == "Identifier")
            {
                i++;
                if (a())
                {
                    return true;
                }
            }
            return false;
        }
    }
}
