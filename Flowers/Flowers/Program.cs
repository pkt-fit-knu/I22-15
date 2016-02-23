using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Flowers
{
    class Setosa
    {
       List<string> list = new List<string>();
       public double count {get;set;}

        public void method(double Sepal_length, double Sepal_width, double Petal_length, double Petal_width,string name)
        {
            if (Petal_width <= 0.6 && (Petal_length<1.9 && Petal_length>1.1))
            {
                string a = "Iris-setosa";
                if(name==a)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Iris-setosa - {0},{1},{2},{3},{4}", Sepal_length, Sepal_width, Petal_length, Petal_width, name);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Iris-setosa - {0},{1},{2},{3},{4}", Sepal_length, Sepal_width, Petal_length, Petal_width, name);
                    Console.ForegroundColor = ConsoleColor.White;
                    count++;
                }
                
            }
            if((Petal_width>1.0&&Petal_width<1.8)&&(Sepal_width<=3.2&&Sepal_width>2.0))
            {
                string a = "Iris-versicolor";
                if(name==a)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Iris-versicolor - {0},{1},{2},{3},{4}", Sepal_length, Sepal_width, Petal_length, Petal_width, name);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Iris-versicolor - {0},{1},{2},{3},{4}", Sepal_length, Sepal_width, Petal_length, Petal_width, name);
                    Console.ForegroundColor = ConsoleColor.White;
                    count++;
                }

                
            }
            if((Petal_width>1.4&&Petal_width<2.5)&&(Petal_length>4.9&&Petal_length<6.7))
            {
               string a = "Iris-virginica";
                if(name==a)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Iris-virginica - {0},{1},{2},{3},{4}", Sepal_length, Sepal_width, Petal_length, Petal_width, name);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Iris-virginica - {0},{1},{2},{3},{4}", Sepal_length, Sepal_width, Petal_length, Petal_width, name);
                    Console.ForegroundColor = ConsoleColor.White;
                    count++;
                }

            }
        }
        public void reader()
        {
            if (File.Exists(@"E:\12.txt"))
            {
                StreamReader sr = new StreamReader(@"E:\12.txt");
                string Input = null;
                while ((Input = sr.ReadLine()) != null)
                {                   
                    list.Add(Input);
                }
               
            }
        }
        public void proverka()
        {
           for(int i=0;i<list.Count;i++)
           {
               if(list[i]!="")
               {
                   string[] s = list[i].Split(',');

                   while(true)
                   {
                       int j = 0;
                       double x1, x2, x3, x4;
                       string a;
                       double.TryParse(s[j], out x1);
                       j++;
                       double.TryParse(s[j], out x2);
                       j++;
                       double.TryParse(s[j], out x3);
                       j++;
                       double.TryParse(s[j], out x4);
                       j++;
                       a = s[j];

                       method(x1, x2, x3, x4,a);
                       break;
                   }
               }                                 
           }
        }


    }
 
    class Program
    {
        static void Main(string[] args)
        {
            Setosa setosa = new Setosa();

            setosa.reader();
            setosa.proverka();
            Console.WriteLine("150/{0}={1}%",setosa.count,(setosa.count/150)*100);
            Console.ReadKey();
        }
    }
}
