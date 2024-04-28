using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LexicalAnalyzer;
using ClassID;
using Struct;
namespace SyntacsycalAnalyzer
{
    internal class SyntacticalBlock
    {
        public Stack<string> AnalyzerStack = new Stack<string>(); //правая часть
        public List<TreeNode> lexemList = new List<TreeNode>();
        String[] rules = {";",":=","(",")","<Арифм.выражение>","<Логическое выражение>","<Оператор>","<Конец if>",
            "<Совокупность операторов>","until","<T>","<E-список>","<F>","<Т-список>","<Логическая операция>"};
        public List<TreeNode> nodeList = new List<TreeNode>();
        public int lastCell = 0;
        public List<Structures> structList = new List<Structures>();
        public int k = 0;
        public int mark = -1;
        public bool jumpFlag = false;
        public int elseFlag = 0;

        public void RefreshTree(Stack<TreeNode> tree1)
        {
            while (tree1.Count != 0)
            {
                TreeNode node =  tree1.Pop();
                lexemList.Insert(0, node);
            }
        }
        public bool SyntacticalAnalyzer()
        {
            AnalyzerStack.Push("The End");
            AnalyzerStack.Push("Программа");

            while (lexemList.Count > k)
            {
                TreeNode node = lexemList[k];
                string magazine = AnalyzerStack.Pop();
                int data = node.Data;

                switch (magazine)
                {
                    case ("Программа"):
                        {
                           switch (data)
                            {
                                case 1:
                                    {
                                        Rule13();
                                        break;
                                    }
                                case 3:
                                    {
                                        Rule13();
                                        break;
                                    }
                                case 5:
                                    {
                                        Rule13();
                                        break;
                                    }
                                    default: return false;
                            }
                            break;
                        }
                    case ("<Оператор>"):
                        {
                            switch (data)
                            {
                                case 1:
                                    {
                                        Rule1();
                                        if (elseFlag != 1 && !jumpFlag)
                                        {
                                            if (nodeList.FindIndex(i => i.Name == node.Name) == -1)
                                            {
                                                nodeList.Add(new TreeNode(node.Name));
                                                lastCell = nodeList.Count - 1;
                                            }
                                            else
                                            {
                                                lastCell = nodeList.FindIndex(i => i.Name == node.Name);
                                            }
                                        }
                                        k++;
                                        break;
                                    }
                                case 3:
                                    {
                                        Rule2();
                                        k++;
                                        break;
                                    }
                                case 5:
                                    {
                                        Rule3();
                                        k++;
                                        break;
                                    }
                                default: return false;
                            }
                            break;
                        }
                    case ("<Совокупность операторов>"):
                        {
                            switch (data)
                            {
                                case 1:
                                    {
                                        Rule13();
                                        break;
                                    }
                                case 3:
                                    {
                                        Rule13();
                                        break;
                                    }
                                case 4:
                                    {
                                        Rule5();
                                        k++;
                                        break;
                                    }
                                case 5:
                                    {
                                        Rule13();
                                        break;
                                    }
                                case 6:
                                    {
                                        Rule6();
                                        break;
                                    }
                                default: return false;

                            }
                            break;
                        }
                    case ("<Конец if>"):
                        {
                            switch (data)
                            {
                                case 1:
                                    {
                                        Rule6();
                                        break;
                                    }
                                case 3:
                                    {
                                        Rule6();
                                        break;
                                    }
                                case 4:
                                    {
                                        Rule5();
                                        if (elseFlag == 2)
                                        {
                                            elseFlag--;
                                        }
                                        jumpFlag = false;
                                        k++;
                                        break;
                                    }
                                case 5:
                                    {
                                        Rule6();
                                        break;
                                    }
                                case 6:
                                    {
                                        Rule6();
                                        break;
                                    }
                                default: return false;
                            }
                            break;
                        }
                    case ("<Арифм.выражение>"):
                        {
                            switch (data)
                            {
                                case 1:
                                    {
                                        Rule7();
                                        break;
                                    }
                                case 2:
                                    {
                                        Rule7();
                                        break;
                                    }
                                default: return false;
                            }
                            break;
                        }
                    case ("<E-список>"):
                        {
                            switch(data)
                            {
                                case 7:
                                    {
                                        Rule6();
                                        break;
                                    }
                                case 8:
                                    {
                                        Rule8();
                                        if (elseFlag != 1 && !jumpFlag)
                                        {
                                            nodeList.Add(new TreeNode("+"));
                                            structList.Add(new Structures());
                                            structList[structList.Count - 1].r = nodeList.Count - 1;
                                            structList[structList.Count - 1].p = lastCell;
                                            structList[structList.Count - 1].name = "+";
                                        }
                                        k++;
                                        break;
                                    }
                                default: return false;
                            }
                            break;
                        }
                    case ("<T>"):
                        {
                            switch(data)
                            {
                                case 1:
                                    {
                                        Rule9();
                                        break;
                                    }
                                case 2:
                                    {
                                        Rule9();
                                        break;
                                    }
                                default: return false;
                            }
                            break;
                        }
                    case ("<Т-список>"):
                        {
                            switch (data)
                            {
                                case 7:
                                    {
                                        Rule6();
                                        break;
                                    }
                                case 8:
                                    {
                                        Rule6();
                                        break;
                                    }
                                case 9:
                                    {
                                        Rule10();
                                        if (elseFlag != 1 && !jumpFlag)
                                        {
                                            nodeList.Add(new TreeNode("*"));
                                            structList.Add(new Structures());
                                            structList[structList.Count - 1].r = nodeList.Count - 1;
                                            structList[structList.Count - 1].p = lastCell;
                                            structList[structList.Count - 1].name = "*";
                                        }
                                        k++;
                                        break;
                                    }
                                default: return false;
                            }
                            break;
                        }
                    case ("<F>"):
                        {
                            switch(data)
                            {
                                case 1:
                                    {
                                        Rule11();
                                        if (elseFlag != 1 && !jumpFlag)
                                        {
                                            lastCell = nodeList.FindIndex(i => i.Name == node.Name);
                                        }
                                        k++;
                                        break;
                                    }
                                case 2:
                                    {
                                        if (elseFlag != 1 && !jumpFlag)
                                        {
                                            if (nodeList.FindIndex(i => i.Name == node.Name) == -1)
                                            {
                                                nodeList.Add(new TreeNode(node.Name, int.Parse(node.Name)));
                                                lastCell = nodeList.Count - 1;
                                            }
                                            else
                                            {
                                                lastCell = nodeList.FindIndex(i => i.Name == node.Name);
                                            }
                                        }
                                        Rule11();
                                        k++;
                                        break;
                                    }
                                default: return false;
                            }
                            break;
                        }
                    case ("<Логическое выражение>"):
                        {
                            switch (data)
                            {
                                case 1:
                                    {
                                        Rule12();
                                        break;
                                    }
                                case 2:
                                    {
                                        Rule12();
                                        break;
                                    }
                                default: return false;
                            }
                            break;
                        }
                    case ("<Логическая операция>"):
                        {
                            switch (data)
                            {
                                case 14:
                                 
                                case 17:
                                    {
                                        Rule11();
                                        if (elseFlag != 1 && !jumpFlag)
                                        {
                                            nodeList.Add(new TreeNode("!"));
                                            structList.Add(new Structures());
                                            structList[structList.Count - 1].p = lastCell;
                                            if (data == 14)
                                            {
                                                structList[structList.Count - 1].name = "=";
                                                nodeList[nodeList.Count - 1].Name = "=";
                                            }
                                            else
                                            {
                                                structList[structList.Count - 1].name = "!=";
                                                nodeList[nodeList.Count - 1].Name = "!=";
                                            }
                                            structList[structList.Count - 1].r = nodeList.Count - 1;
                                        }
                                        k++;
                                        break;
                                    }
                                default: return false;
                            }
                            break;
                        }
                    case (":="):
                        {
                            switch (data)
                            {
                                case 16:
                                    {
                                        Rule11();
                                        if (elseFlag != 1 && !jumpFlag)
                                        {
                                            structList.Add(new Structures());
                                            structList[structList.Count - 1].p = lastCell;
                                            structList[structList.Count - 1].name = ":=";
                                        }
                                        k++;
                                        break;
                                    }
                                default: return false;
                            }
                            break;
                        }
                    case (";"):
                        {
                            switch (data)
                            {
                                case 7:
                                    {
                                        Rule11();
                                        if (elseFlag == 1)
                                            elseFlag--;
                                        jumpFlag = false;
                                        k++;
                                        break;
                                    }
                                default: return false;
                            }
                            break;
                        }
                    case ("("):
                        {
                            switch (data)
                            {
                                case 10:
                                    {
                                        Rule11();
                                        k++;
                                        break;
                                    }
                                default: return false;
                            }
                            break;
                        }
                    case (")"):
                        {
                            switch (data)
                            {
                                case 11:
                                    {
                                        Rule11();
                                        k++;
                                        break;
                                    }
                                default: return false;
                            }
                            break;
                        }
                    case ("until"):
                        {
                            switch (data)
                            {
                                case 6:
                                    {
                                        Rule11();
                                        k++;
                                        break;
                                    }
                                default: return false;
                            }
                            break;
                        }
                    case ("Сложить"):
                        {
                            if (elseFlag != 1 && !jumpFlag)
                            {
                                int target = -1;
                                for (int i = structList.Count - 1; i >= 0; i--)
                                {
                                    if (structList[i].name == "+")
                                    {
                                        target = i;
                                        break;
                                    }
                                }
                                structList[target].q = lastCell;
                                nodeList[structList[target].r].Data = nodeList[structList[target].p].Data + nodeList[structList[target].q].Data;
                                lastCell = structList[target].r;
                            }
                            break;
                        }
                    case ("Умножить"):
                        {
                            if (elseFlag != 1 && !jumpFlag)
                            {
                                structList[structList.Count - 1].q = lastCell;
                                nodeList[structList[structList.Count - 1].r].Data = nodeList[structList[structList.Count - 1].p].Data * nodeList[structList[structList.Count - 1].q].Data;
                                lastCell = structList[structList.Count - 1].r;
                            }
                            break;
                            
                        }
                    case ("Присвоить"):
                        {
                            if (elseFlag != 1 && !jumpFlag)
                            {
                                for (int i = structList.Count - 1; i >= 0; i--)
                                {
                                    if (structList[i].name == ":=")
                                    {
                                        structList[i].q = lastCell;
                                        nodeList[structList[i].p].Data = nodeList[structList[i].q].Data;
                                        break;
                                    }
                                }
                            }
                            break;
                        }
                    case ("Сравнить"):
                        {
                            if (elseFlag != 1 && !jumpFlag)
                            {
                                structList[structList.Count - 1].q = lastCell;
                                if (structList[structList.Count - 1].name == "=")
                                {
                                    if (nodeList[structList[structList.Count - 1].p].Data == nodeList[structList[structList.Count - 1].q].Data)
                                        nodeList[structList[structList.Count - 1].r].Data = 1;
                                    else
                                        nodeList[structList[structList.Count - 1].r].Data = 0;
                                }
                                else
                                {
                                    if (nodeList[structList[structList.Count - 1].p].Data == nodeList[structList[structList.Count - 1].q].Data)
                                        nodeList[structList[structList.Count - 1].r].Data = 0;
                                    else
                                        nodeList[structList[structList.Count - 1].r].Data = 1;
                                }
                                lastCell = structList[structList.Count - 1].r;
                            }
                            break;
                        }
                    case ("Условный переход"):
                        {
                            if (elseFlag != 1 && !jumpFlag)
                                if (nodeList[lastCell].Data == 0)
                                {
                                    k = mark;
                                    AnalyzerStack.Pop();
                                }
                            break;
                        }
                    case ("Метка"):
                        {
                            if (elseFlag != 1 && !jumpFlag)
                            {
                                mark = k - 1;
                            }
                            break;
                        }
                    case ("Условный переход по 0"):
                        {
                            
                            if (elseFlag != 1 && !jumpFlag)
                            {
                                if (nodeList[lastCell].Data == 1)
                                    elseFlag = 2;
                                else
                                    jumpFlag = true;
                            } 
                            break;
                        }
                }
            }

            string str1 = AnalyzerStack.Pop();
            string str2 = AnalyzerStack.Pop();
            if (str1 == "<Совокупность операторов>" && str2 == "The End")
                return true;
            else
                return false;
        }

        private void Rule1()
        { 
            AnalyzerStack.Push(";");
            AnalyzerStack.Push("Присвоить");
            AnalyzerStack.Push("<Арифм.выражение>");
            AnalyzerStack.Push(":=");
        }

        private void Rule2()
        {
            AnalyzerStack.Push("<Конец if>");
            AnalyzerStack.Push("<Оператор>");
            AnalyzerStack.Push("Условный переход по 0");
            AnalyzerStack.Push(")");
            AnalyzerStack.Push("<Логическое выражение>");
            AnalyzerStack.Push("(");
        }

        private void Rule3()
        {
            AnalyzerStack.Push(";");
            AnalyzerStack.Push("Условный переход");
            AnalyzerStack.Push("<Логическое выражение>");
            AnalyzerStack.Push("until");
            AnalyzerStack.Push("<Совокупность операторов>");
            AnalyzerStack.Push("Метка");
        }

        private void Rule4()
        {
            AnalyzerStack.Push(";");
            AnalyzerStack.Push("<Логическое выражение>");
        }

        private void Rule5()
        {
            AnalyzerStack.Push("<Оператор>");
        }

        private void Rule6()
        {
        }

        private void Rule7()
        {
            AnalyzerStack.Push("<E-список>");
            AnalyzerStack.Push("<T>");
        }

        private void Rule8()
        {
            AnalyzerStack.Push("<E-список>");
            AnalyzerStack.Push("Сложить");
            AnalyzerStack.Push("<T>");
        }

        private void Rule9()
        {
            AnalyzerStack.Push("<Т-список>");
            AnalyzerStack.Push("<F>");
        }

        private void Rule10()
        {
            AnalyzerStack.Push("<Т-список>");
            AnalyzerStack.Push("Умножить");
            AnalyzerStack.Push("<F>");
        }
        private void Rule11()
        {
        }

        private void Rule12()
        {
            AnalyzerStack.Push("Сравнить");
            AnalyzerStack.Push("<F>");
            AnalyzerStack.Push("<Логическая операция>");
            AnalyzerStack.Push("<F>");
        }

        private void Rule13()
        {
            AnalyzerStack.Push("<Совокупность операторов>");
            AnalyzerStack.Push("<Оператор>");
        }
    }
}
