using System;
using System.Collections.Generic;
using ClassID;

namespace LexicalAnalyzer
{
    internal class LexicalBlock
    {
        private string[] keywords = {"if", "else", "repeat", "until"};
        private static char[] separators = {';', '+', '*', '(', ')', ':',' ', '=', '!','\n'};
        private static string[] separatorStrings = { ":=", "!=" };
        public Stack<TreeNode> tree1 = new Stack<TreeNode>();
        public string allTextProgram = null;
        private void LexicalAnalyzer(char code, ref string temp, ref string type)
        {
            if ((code >= 'a' && code <= 'z') || (code >= 'A' && code <= 'Z') || code == '_') //если символ - латиница
            {
                type += '1';
                temp += code;
                return;    
            }

            if (code >= '0' && code <= '9') //если символ - цифра
            {
                type += '2';
                temp += code;
                return;
            }

            foreach (var item in separators) //если разделитель
            {
                if (code == item)
                {
                    type += '3';
                    temp += code;
                    return;
                }         
            }
        }

        public bool AllTextAnalyser()
        {
            string temp = null;
            string type = null;
            string str = null;
            
            for (int c = 0; c < allTextProgram.Length; c++)
                LexicalAnalyzer(allTextProgram[c],ref temp,ref type);

            string allTextProgram1 = allTextProgram.Replace("\r\n", "");
            char[] chr = allTextProgram1.ToCharArray();
            int i = 0;
            while (i < type.Length - 1)
            {
                if (type[i] == '2') //обработка констант (тип 2)
                {
                    while (type[i] != '3')
                    {
                        if (type[i] == '1')
                        {
                            Console.WriteLine("Error");
                            return false;
                        }
                        str += temp[i];
                        i++;
                    }

                    tree1.Push(new TreeNode(str, 2));   
                    str = null;
                }

                if (type[i] == '1') //обработка переменных (тип 1)
                {
                    while (type[i] != '3')
                    {
                        str += temp[i];
                        i++;
                    }

                    bool flag = false;

                        for (int j = 0; j < keywords.Length; j++)
                        {
                            if (str == keywords[j])
                            {
                                tree1.Push(new TreeNode(str, 3 + j)); //обработка ключевых слов (3-6)
                                flag = true;
                            }
                        }

                        if (flag == false)
                            tree1.Push(new TreeNode(str, 1));
                        str = null;
                        flag = false;
                }
                if (type[i] == '3')
                {
                    while ((i < type.Length) && (type[i] == '3'))
                    {
                        if (temp[i] != ' ' && temp[i] != '\n')
                            str += temp[i];
                        i++;
                    }
                    if (str != null)
                    {
                        if (str.Length == 1)
                            for (int j = 0; j < separators.Length; j++)
                                if (str == separators[j].ToString())
                                {
                                    tree1.Push(new TreeNode(str, 7 + j)); //7 - 15
                                    break;
                                }

                        if (str.Length == 2)
                            for (int j = 0; j < separatorStrings.Length; j++)
                                if (str == separatorStrings[j])
                                {
                                    tree1.Push(new TreeNode(str, 16 + j)); //16-17
                                    break;
                                }
                    }
                    str = null;
                }
            }

            return true;
        }
    }
}
