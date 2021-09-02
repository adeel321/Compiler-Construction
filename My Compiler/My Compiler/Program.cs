using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;

namespace My_Compiler
{
    public class token
    {
        public string classPart;
        public string valuePart;
        public int lineNumber;
        public token(string classPart, string valuePart, int lineNumber)
        {
            this.classPart = classPart;
            this.valuePart = valuePart;
            this.lineNumber = lineNumber;
        }
    }
    class classification 
    {
        public string isKey(string lexene)
        {
            string[] key = { "int", "float", "string", "bool", "void", "for", "while", "if", "else", "break", "continue", "return", "null", "true", "false", "public", "static", "extends", "protected", "private", "class", "final", "abstract", "new", "main" };
            string[] value = { "Data Type", "Data Type", "Data Type", "Data Type", "Void", "For", "While", "If", "Else", "Break", "Continue", "Return", "Null Constant", "Bool Constant", "Bool Constant", "Public", "Static", "Extends", "Protected", "Private", "Class", "Final", "Abstract", "New", "Main" };
            for (int i = 0; i < 25; i++)
            {
                if (lexene == key[i])
                {
                    return value[i];
                }
            }
            return "Invalid Lexene";
        }
        public bool isPun(string lexene)
        {
            string[] pun = { ".", ",", ";", "(", ")", "{", "}", "[", "]" , "~"};
            for (int i = 0; i < 10; i++)
            {
                if (lexene == pun[i])
                {
                    return true;
                }
            }
            return false;
        }
        public string isOpe(string lexene)
        {
            string[] key = { "+", "-", "*", "/", "%", "&&", "||", "!", "<", ">", "<=", ">=", "!=", "==", "=" };
            string[] value = { "PM", "PM", "MDM", "MDM", "MDM", "&&", "||", "!", "ROP", "ROP", "ROP", "ROP", "ROP", "ROP", "=" };
            for (int i = 0; i < 15; i++)
            {
                if (lexene == key[i])
                {
                    return value[i];
                }
            }
            return "Invalid Lexene";
        }
        public bool isIde(string lexene)
        {
            Regex rx = new Regex(@"^[_A-Za-z][_A-Za-z0-9]*$");
            MatchCollection matches = rx.Matches(lexene);
            if (matches.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool isStr_Constant(string lexene)
        {
            if (lexene.StartsWith("\""))
            {
                if (lexene.EndsWith("\""))
                {
                    return true;
                }
            }
            return false;
        }
        public bool isFlt_Constant(string lexene)
        {
            Regex rx = new Regex(@"^[0-9]+[.][0-9]+$");
            MatchCollection matches = rx.Matches(lexene);
            if (matches.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool isInt_Constant(string lexene)
        {
            Regex rx = new Regex(@"^[0-9]+$");
            MatchCollection matches = rx.Matches(lexene);
            if (matches.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    class wordBreaker
    {
        int lineNumber = 0;
        ArrayList codeArrayList = new ArrayList();
        public void loadArrayList(string source)
        {
            char codeChar;
            StreamReader sr = new StreamReader(source);
            do
            {
                codeChar = (char)sr.Read();
                codeArrayList.Add(codeChar);
            } while (!sr.EndOfStream);
        }
        public string wordSplit()
        {
            string temp = codeArrayList[0].ToString();
            codeArrayList.RemoveAt(0);
            if (temp == "\r")
            {
                lineNumber++;
                codeArrayList.RemoveAt(0);
                return "Invalid Lexene";
            }
            else if (temp == "\n")
            {
                return "Invalid lexene";
            }
            else if (temp == " ")
            {
                return "Invalid Lexene";
            }
            else if (isAlphabetic(char.Parse(temp)) == true)
            {
                string str = temp;
                while (getArrayListCount() != 0 && isBreaker(char.Parse(codeArrayList[0].ToString())) == false && isPoint(char.Parse(codeArrayList[0].ToString())) == false)
                {
                    temp = codeArrayList[0].ToString();
                    codeArrayList.RemoveAt(0);
                    str += temp;
                }
                //if (getArrayListCount() != 0)
                //{
                //    if (codeArrayList[0].ToString() == " " || codeArrayList[0].ToString() == "\n")
                //    {
                //        codeArrayList.RemoveAt(0);
                //    }
                //}
                return str;
            }
            else if (isNumeric(char.Parse(temp)) == true)
            {
                int counter = 0;
                bool isAlphanumeric = false;
                string str = temp;
                while (getArrayListCount() != 0 && isBreaker(char.Parse(codeArrayList[0].ToString())) == false && counter < 2)
                {
                    if (isAlphabetic(char.Parse(codeArrayList[0].ToString())))
                    {
                        isAlphanumeric = true;
                    }
                    if (codeArrayList[0].ToString() == ".")
                    { 
                        int i = 1;
                        while (isBreaker(char.Parse(codeArrayList[i].ToString())) == false && isPoint(char.Parse(codeArrayList[i].ToString())) == false)
                        {
                            if (isAlphabetic(char.Parse(codeArrayList[i].ToString())) == true)
                            {
                                isAlphanumeric = true;
                            }
                            if (isAlphanumeric == true)
                            {
                                return str;
                            }
                            i++;
                        }
                        if (isAlphanumeric == true)
                        {
                            return str;
                        }
                        counter++;
                        
                        if (counter == 2)
                        {
                            return str;
                        }
                    }
                    temp = codeArrayList[0].ToString();
                    codeArrayList.RemoveAt(0);
                    str += temp;
                }
                //if (getArrayListCount() != 0)
                //{
                //    if (codeArrayList[0].ToString() == " " || codeArrayList[0].ToString() == "\n")
                //    {
                //        codeArrayList.RemoveAt(0);
                //    }
                //}
                return str;
            }
            else if (temp == "#" || temp == "$")
            {
                if (temp == "#")
                {
                    while (getArrayListCount() != 0 && temp != "\n")
                    {
                        temp = codeArrayList[0].ToString();
                        codeArrayList.RemoveAt(0);
                    }
                    return "Invalid Lexene";
                }
                else
                {
                    temp = codeArrayList[0].ToString();
                    codeArrayList.RemoveAt(0);
                    while (getArrayListCount() != 0 && temp != "$")
                    {
                        if (temp == "\r")
                        {
                            lineNumber++;
                        }
                        temp = codeArrayList[0].ToString();
                        codeArrayList.RemoveAt(0);
                    }
                    return "Invalid Lexene";
                }
            }
            //else if (temp == "\"")
            //{
            //    temp = codeArrayList[0].ToString();
            //    codeArrayList.RemoveAt(0);
            //    string iterator = temp;
            //    while (iterator != "\"" || iterator != "\n")
            //    {
            //        iterator = codeArrayList[1].ToString();
            //        temp += codeArrayList[0].ToString();
            //        codeArrayList.RemoveAt(0);
            //    }
            //    return temp;
            //}
            else if (temp == "\"")
            {
                string str = temp;
                temp = codeArrayList[0].ToString();
                codeArrayList.RemoveAt(0);
                while (temp != "\"" && temp != "\r")
                {
                    str += temp;
                    temp = codeArrayList[0].ToString();
                    codeArrayList.RemoveAt(0);
                    //if (temp == "\\")
                    //{
                    //    temp = codeArrayList[0].ToString();
                    //    codeArrayList.RemoveAt(0);
                    //    str += temp;
                    //    temp = codeArrayList[0].ToString();
                    //    codeArrayList.RemoveAt(0);
                    //}
                    //else
                    //{
                    //if (getArrayListCount() != 0 && codeArrayList[0].ToString() != "\r")
                    //{
                    //    temp = codeArrayList[0].ToString();
                    //    codeArrayList.RemoveAt(0);
                    //}
                    //else
                    //{
                    //   break;
                    //}
                    //}
                }
                if (temp == "\"")
                {
                    str += temp;
                }
                return str;
            }
            else if (temp == "+" || temp == "-" || temp == "*" || temp == "/" || temp == "%" || temp == "&" || temp == "|" || temp == "!" || temp == "<" || temp == ">" || temp == "=" || temp == ";")
            {
                if (temp == "&" || temp == "|")
                {
                    if (getArrayListCount() != 0 && temp == "&" && codeArrayList[0].ToString() == "&")
                    {
                        temp += codeArrayList[0];
                        codeArrayList.RemoveAt(0);
                        return temp;
                    }
                    else if (getArrayListCount() != 0 && temp == "|" && codeArrayList[0].ToString() == "|")
                    {
                        temp += codeArrayList[0];
                        codeArrayList.RemoveAt(0);
                        return temp;
                    }
                    else
                    {
                        return temp;
                    }
                }
                else if (temp == ">")
                {
                    if (getArrayListCount() != 0 && codeArrayList[0].ToString() == "=")
                    {
                        temp += codeArrayList[0];
                        codeArrayList.RemoveAt(0);
                        return temp;
                    }
                    else
                    {
                        return temp;
                    }
                }
                else if (temp == "<")
                {
                    if (getArrayListCount() != 0 && codeArrayList[0].ToString() == "=")
                    {
                        temp += codeArrayList[0];
                        codeArrayList.RemoveAt(0);
                        return temp;
                    }
                    else
                    {
                        return temp;
                    }
                }
                else if (temp == "!")
                {
                    if (getArrayListCount() != 0 && codeArrayList[0].ToString() == "=")
                    {
                        temp += codeArrayList[0];
                        codeArrayList.RemoveAt(0);
                        return temp;
                    }
                    else
                    {
                        return temp;
                    }
                }
                else if (temp == "=")
                {
                    if (getArrayListCount() != 0 && codeArrayList[0].ToString() == "=")
                    {
                        temp += codeArrayList[0];
                        codeArrayList.RemoveAt(0);
                        return temp;
                    }
                    else
                    {
                        return temp;
                    }
                }
                else
                {
                    return temp;
                }
            }
            else if (temp == "." || temp == "," || temp == "(" || temp == ")" || temp == "{" || temp == "}" || temp == "[" || temp == "]" || temp == ";" || temp == "~")
            {
                return temp;
            }
            else
            {
                return null;
            }
        }
        public bool isAlphabetic(char letter)
        {
            string words = "@:abcdefghijklmnopqrstuvwxyz'ABCDEFGHIJKLMNOPQRSTUVWXYZ_";
            for (int i = 0; i < 56; i++)
            {
                if (letter == words[i])
                {
                    return true;
                }
            }
            return false;
        }
        public bool isNumeric(char letter)
        {
            string words = "0123456789";
            for (int i = 0; i < 10; i++)
            {
                if (letter == words[i])
                {
                    return true;
                }
            }
            return false;
        }
        public bool isBreaker(char letter)
        {
            string words = "~;,()[]{}+-*/%&|!=<>#$" + "\n" + " " + "\r";
            for (int i = 0; i < 25; i++)
            {
                if (letter == words[i])
                {
                    return true;
                }
            }
            return false;
        }
        public bool isPoint(char letter)
        {
            if (letter == '.')
            {
                return true;
            }
            return false;
        }
        public int getArrayListCount()
        {
            return codeArrayList.Count;
        }
        public int getLineNumber()
        {
            return lineNumber;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //string temp;
            //StreamWriter sw = new StreamWriter(@"C:\\Users\\muham\\OneDrive\\Desktop\\lexene.txt");
            List<token> tokenList = new List<token>();
            wordBreaker wb = new wordBreaker();
            classification cl = new classification();
            string source = "C:\\Users\\muham\\OneDrive\\Desktop\\lexical_sample_file.txt";
            string destination = "C:\\Users\\muham\\OneDrive\\Desktop\\lexical_analyzer.txt";
            wb.loadArrayList(source);
            do
            {
                string temp = wb.wordSplit();
                if (temp == "Invalid Lexene")
                { }
                else
                {
                    string isKeyWord = cl.isKey(temp);
                    string isOperator = cl.isOpe(temp);
                    if (isKeyWord != "Invalid Lexene")
                    {
                        File.AppendAllText(destination, "( " + isKeyWord + ", " + temp + ", " + wb.getLineNumber().ToString() + " )" + "\n");
                        tokenList.Add(new token(isKeyWord, temp, wb.getLineNumber()));
                    }
                    else if (cl.isIde(temp))
                    {
                        File.AppendAllText(destination, "( " + "Identifier" + ", " + temp + ", " + wb.getLineNumber().ToString() + " )" + "\n");
                        tokenList.Add(new token("Identifier", temp, wb.getLineNumber()));
                    }
                    else if (cl.isPun(temp))
                    {
                        File.AppendAllText(destination, "( " + temp + ", " + temp + ", " + wb.getLineNumber().ToString() + " )" + "\n");
                        tokenList.Add(new token(temp, temp, wb.getLineNumber()));
                    }
                    else if (isOperator != "Invalid Lexene")
                    {
                        File.AppendAllText(destination, "( " + isOperator + ", " + temp + ", " + wb.getLineNumber().ToString() + " )" + "\n");
                        tokenList.Add(new token(isOperator, temp, wb.getLineNumber()));
                    }
                    else if (cl.isStr_Constant(temp))
                    {
                        temp = temp.Trim('"');
                        File.AppendAllText(destination, "( " + "String Constant" + ", " + temp + ", " + wb.getLineNumber().ToString() + " )" + "\n");
                        tokenList.Add(new token("String Constant", temp, wb.getLineNumber()));
                    }
                    else if (cl.isFlt_Constant(temp))
                    {
                        File.AppendAllText(destination, "( " + "Float Constant" + ", " + temp + ", " + wb.getLineNumber().ToString() + " )" + "\n");
                        tokenList.Add(new token("Float Constant", temp, wb.getLineNumber()));
                    }
                    else if (cl.isInt_Constant(temp))
                    {
                        File.AppendAllText(destination, "( " + "Integer Constant" + ", " + temp + ", " + wb.getLineNumber().ToString() + " )" + "\n");
                        tokenList.Add(new token("Integer Constant", temp, wb.getLineNumber()));
                    }
                    else
                    {
                        File.AppendAllText(destination, "( " + "Invalid Lexene" + ", " + temp + ", " + wb.getLineNumber().ToString() + " )" + "\n");
                        //tokenList.Add(new token("Invalid Lexene", temp, wb.getLineNumber()));
                    }
                }
            } while (wb.getArrayListCount() > 0);
            tokenList.Add(new token("$", "$", 786));
            syntaxAnalyzer sa = new syntaxAnalyzer();
            sa.checkSyntactically(tokenList);
            Console.ReadKey();
            //codeArrayList.RemoveAt(3);
            //temp = codeArrayList[0].ToString();
            //codeArrayList.RemoveAt(0);
            //if (temp == "+" || temp == "-" || temp == "*" || temp == "/" || temp == "%" || temp == "&" || temp == "|" || temp == "!" || temp == "<" || temp == ">" || temp == "=")
            //{
            //    if (temp == "&" && codeArrayList[0].ToString() == "&")
            //    {
            //        temp = temp + codeArrayList[0];
            //        codeArrayList.RemoveAt(0);
            //        File.AppendAllText(@"C:\\Users\\muham\\OneDrive\\Desktop\\newlexene.txt", '(' + "Arithmetic Operators, " + temp + ')' + '\n');
            //    }
            //    else if (temp == "|" && codeArrayList[0].ToString() == "|")
            //    {
            //        temp = temp + codeArrayList[0];
            //        codeArrayList.RemoveAt(0);
            //        File.AppendAllText(@"C:\\Users\\muham\\OneDrive\\Desktop\\newlexene.txt", '(' + "Arithmetic Operators, " + temp + ')' + '\n');
            //    }
            //    else if (temp == "<" && codeArrayList[0].ToString() == "=")
            //    {
            //        temp = temp + codeArrayList[0];
            //        codeArrayList.RemoveAt(0);
            //        File.AppendAllText(@"C:\\Users\\muham\\OneDrive\\Desktop\\newlexene.txt", '(' + "Arithmetic Operators, " + temp + ')' + '\n');
            //    }
            //    else if (temp == ">" && codeArrayList[0].ToString() == "=")
            //    {
            //        temp = temp + codeArrayList[0];
            //        codeArrayList.RemoveAt(0);
            //        File.AppendAllText(@"C:\\Users\\muham\\OneDrive\\Desktop\\newlexene.txt", '(' + "Arithmetic Operators, " + temp + ')' + '\n');
            //    }
            //    else if (temp == "!" && codeArrayList[0].ToString() == "=")
            //    {
            //        temp = temp + codeArrayList[0];
            //        codeArrayList.RemoveAt(0);
            //        File.AppendAllText(@"C:\\Users\\muham\\OneDrive\\Desktop\\newlexene.txt", '(' + "Arithmetic Operators, " + temp + ')' + '\n');
            //    }
            //    else if (temp == "=" && codeArrayList[0].ToString() == "=")
            //    {
            //        temp = temp + codeArrayList[0];
            //        codeArrayList.RemoveAt(0);
            //        File.AppendAllText(@"C:\\Users\\muham\\OneDrive\\Desktop\\newlexene.txt", '(' + "Arithmetic Operators, " + temp + ')' + '\n');
            //    }
            //    else
            //    {

            //    }
            //    //sw.WriteLine("+");
            //    //File.AppendAllText(@"C:\\Users\\muham\\OneDrive\\Desktop\\newlexene.txt", '(' + "Arithmetic Operators, " + temp + ')' + '\n');
            //}            


            //for (int i = 0; i < codeArrayList.Count; i++)
            //{
            //    Console.WriteLine(codeArrayList[i]);
            //}
            //Console.ReadKey();
        }
    }
}
