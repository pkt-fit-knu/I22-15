using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DivAndConc
{

    class Node
    {
        public string name;
        public List<Node> children = new List<Node>();

        public Node(string name) { this.name = name; }
    }

    class Tree
    {
        Node root;
        Node current;
        Node father;

        public void Add(string name, List<string> chnames)
        {
            if (root == null)
            {
                root = new Node(name);
                foreach (string a in chnames)
                {
                    root.children.Add(new Node(a));
                }
                current = root.children[0];
                father = root;
            }
            else if ((name == "yes") || (name == "no"))
            {
                current.children.Add(new Node(name));
                current.children[0].children = null;
                foreach (Node a in father.children)
                {
                    if (a.children.Count == 0)
                    {
                        current = a;
                        break;
                    }
                }
            }
            else
            {
                current.children.Add(new Node(name));
                father = current;
                current = father.children[0];
                foreach (string a in chnames)
                    current.children.Add(new Node(a));
                father = current;
                current = current.children[0];
            }
        }


        public void Show()
        {
            Node cur = root;
            if (cur == null) Console.WriteLine("error");
            else
            {
                Round(cur);
                Console.WriteLine();
            }
        }

        private void Round(Node current)
        {

            Console.WriteLine(current.name);
            if (current.children != null)
                foreach (Node a in current.children.Where(a => a != null))
                {
                    Round(a);
                }
        }

    }
    class Program
    {
        public static double[] CalcGini(int st, string[][] mas, int count)
        {
            List<string> names = new List<string>();
            for (int i = 1; i < mas.Length; i++)
            {
                if (!names.Contains(mas[i][st]))
                {
                    names.Add(mas[i][st]);
                }
            }

            double[,] m = new double[names.Count, count];

            foreach (string s in names)
                for (int j = 1; j < mas.Length; j++)
                {
                    if (mas[j][st] == s)
                    {
                        switch (mas[j][count])
                        {
                            case "yes":
                                m[names.IndexOf(s), 0]++;
                                m[names.IndexOf(s), 2]++;
                                break;
                            case "no":
                                m[names.IndexOf(s), 1]++;
                                m[names.IndexOf(s), 2]++;
                                break;
                        }
                    }

                }

            
            double all = 0;
            for (int i = 0; i < m.GetLength(0); i++)
            {
                m[i, 3] = 1 - (Math.Pow(m[i, 0] / m[i, 2], 2) + Math.Pow(m[i, 1] / m[i, 2], 2));
                all += m[i, 2];
            }

            double gini = 0;

            for (int i = 0; i < m.GetLength(0); i++)
            {
                gini += (m[i, 2] / all) * m[i, 3];
            }

            double[] ginimasiv = new double[m.GetLength(0) + 1];
            for (int i = 0; i < m.GetLength(0); i++)
            {
                ginimasiv[i] = m[i, count - 1];
            }
            ginimasiv[ginimasiv.Length - 1] = gini;

            return ginimasiv;

        }
        public static void RecursivFunc(string[][] mas, Tree tree, int count)
        {
            //choosing best brunch
            List<double> ginilist = new List<double>();
            double[] ginigini = null;
            for (int i = 0; i < count; i++)
            {
                ginigini = CalcGini(i, mas, count);
                ginilist.Add(ginigini[ginigini.Length - 1]);
            }

            int index = ginilist.IndexOf(ginilist.Min());
            double[] ginimasiv = CalcGini(index, mas, count);

            //Adding to the tree
            List<string> children = new List<string>();
            for (int i = 1; i < mas.Length; i++)
            {
                if (!children.Contains(mas[i][index]))
                    children.Add(mas[i][index]);
            }

            tree.Add(mas[0][index], children);

            //if one child is perfect add it's result to the tree
            //else make recursion
            for (int i = 0; i < ginimasiv.Length - 1; i++)
            {
                if (ginimasiv[i] == 0)
                {
                    //you may know yes or no value
                    string yesno = null;
                    for (int j = 1; j < mas.Length; j++)
                        if (mas[j][index] == children[i])
                        {
                            yesno = mas[j][count];
                            break;
                        }

                    tree.Add(yesno, null);

                }
                else
                {
                    //need to cut your masiv
                    int lich = 0;
                    for (int j = 1; j < mas.Length; j++)
                        if (mas[j][index] == children[i]) lich++;

                    string[][] newmas = new string[lich + 1][];

                    newmas[0] = mas[0];
                    int ii = 1;
                    for (int j = 1; j < mas.Length; j++)
                    {
                        if (mas[j][index] == children[i])
                            newmas[ii++] = mas[j];
                    }

                    RecursivFunc(newmas, tree, count);
                }
            }
        }
        static void Main(string[] args)
        {

            StreamReader streamReader = new StreamReader(@"E:\data.txt");
            {
                string readToEnd = streamReader.ReadToEnd();

                string[] readStrings = readToEnd.Split('*');

                int count = readStrings[0].Count(a => a == ',');

                string[][] mass = new string[readStrings.Length][];
                for (int i = 0; i < readStrings.Length; i++)
                    mass[i] = readStrings[i].Split(',');

                Tree tree = new Tree();
                RecursivFunc(mass, tree, count);

                tree.Show();
            }
            Console.ReadKey();
        }
    }
}
