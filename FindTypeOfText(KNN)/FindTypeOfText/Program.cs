using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FindTypeOfText
{
    public struct tfCurrDocument
    {
        public string Key { get; set; }
        public int Value { get; set; }
    }
    class Program
    {

        public static string[] GetArrayOfWords(string text)
        {
            string newString = text.Where(str => str != '”' && str != '“' && str != ',' && str != '.' && str != ':' && str != '"' && str != '"').Aggregate("", (current, str) => current + str);
            string[] ArrayOfWords = newString.Split(' ');
            return ArrayOfWords;
        }

        static string[] InitProgram(string pathFile)
        {
            StreamReader sr = new StreamReader(pathFile);
            string s = sr.ReadToEnd();
            return GetArrayOfWords(s);
        }

        static List<double> GetTfOfDoc(string[] document)
        {
            var tfCurrDocument = new List<tfCurrDocument>();
            
            foreach (string item in document)
            {
                var tf = tfCurrDocument.FirstOrDefault(x => x.Key == item).Key;

                if (tf == null)
                {
                    tfCurrDocument.Add(new tfCurrDocument() {Key = item, Value = 1});
                }
                else
                {


                    int count = 0;
                    for (int i = 0; i < tfCurrDocument.Count; i++)
                    {
                        if (tfCurrDocument[i].Key == item)
                        {
                            var tempObj = tfCurrDocument[i];
                            tempObj.Value++;
                            count = tempObj.Value;
                            tfCurrDocument[i] = tempObj;
                        }
                    }

                    tfCurrDocument.Add(new tfCurrDocument() { Key = item, Value = count });
                }
                
            }

            return tfCurrDocument.Select(item => (double) item.Value/document.Length).ToList();
        }

        static List<double> GetIdfOfCurrBlock(string[] firstDoc, string[] secondtDoc, string[] thirdDoc, string[] fourthDoc)
        {
            List<double> forReturn = new List<double>();
            double countOfDocument = 4;
            for (int i = 0; i < firstDoc.Length; i++)
            {
                double count = 0;
                if (secondtDoc.Contains(firstDoc[i]))
                {
                    count++;
                }
                if (thirdDoc.Contains(firstDoc[i]))
                {
                    count++;
                }
                if (fourthDoc.Contains(firstDoc[i]))
                {
                    count++;
                }
                double idf = Math.Log(countOfDocument/count);
                if (!double.IsInfinity(idf))
                {
                    forReturn.Add(idf);
                }
                else
                {
                    forReturn.Add(0.0);
                }
                
            }
            return forReturn;
        }

        static List<double> GetIdfOfCurrBlock(string[] firstDoc, string[] secondtDoc, string[] thirdDoc,
            string[] fourthDoc, string[] fifthDoc, string[] sixthDoc, string[] seventhDoc, string[] eightDoc, string[] ninthDoc)
        {
            List<double> forReturn = new List<double>();
            double countOfDocument = 8;
            for (int i = 0; i < firstDoc.Length; i++)
            {
                double count = 0;
                if (secondtDoc.Contains(firstDoc[i]))
                {
                    count++;
                }
                if (thirdDoc.Contains(firstDoc[i]))
                {
                    count++;
                }
                if (fourthDoc.Contains(firstDoc[i]))
                {
                    count++;
                }
                if (fifthDoc.Contains(firstDoc[i]))
                {
                    count++;
                }
                if (sixthDoc.Contains(firstDoc[i]))
                {
                    count++;
                }
                if (seventhDoc.Contains(firstDoc[i]))
                {
                    count++;
                }
                if (eightDoc.Contains(firstDoc[i]))
                {
                    count++;
                }
                if (ninthDoc.Contains(firstDoc[i]))
                {
                    count++;
                }
                double idf = Math.Log(countOfDocument / count);
                if (!double.IsInfinity(idf))
                {
                    forReturn.Add(idf);
                }
                else
                {
                    forReturn.Add(0.0);
                }

            }
            return forReturn;
        }

        static List<double> CalcDofDoc(List<double> tf, List<double> idf)
        {
            List<double> forReturn = new List<double>();
            for (int i = 0; i < tf.Count; i++)
            {
                double mult = tf[i]*idf[i];
                forReturn.Add(mult);
            }
            return forReturn;
        }


        static double Tomimoto(List<double> firstDoc, List<double> SecondDoc)
        {
            double result=0;
            var count = firstDoc.Count < SecondDoc.Count ? firstDoc.Count : SecondDoc.Count;
            double scalarDob = 0;
            for (int i = 0; i < count; i++)
            {
                double mult = firstDoc[i]*SecondDoc[i];
                scalarDob += mult;
            }

            double ASquare = firstDoc.Sum(t => t*t);

            double BSquare = SecondDoc.Sum(t => t*t);
            result = scalarDob/(ASquare + BSquare - scalarDob);
            return result;
        }

        static void GetResult(double x1, double x2, double x3, double x4, double x5, double x6, double x7, double x8)
        {
            double sumforFirstBlock = x1 + x2 + x3 + x4;
            double sumforSecondBlock = x5 + x6 + x7 + x8;
            if (sumforFirstBlock < sumforSecondBlock)
            {
                Console.WriteLine("Its Sport Document");
            }
            else
            {
                Console.WriteLine("Its Politics Document");    
            }
        }
        static void Main(string[] args)
        {
            string[] sport1 = InitProgram("E:/Sport/1Sport.txt");
            string[] sport2 = InitProgram("E:/Sport/2Sport.txt");
            string[] sport3 = InitProgram("E:/Sport/3Sport.txt");
            string[] sport4 = InitProgram("E:/Sport/4Sport.txt");

            string[] x = InitProgram("E:/x.txt");

            string[] politics1 = InitProgram("E:/Politicans/1Politic.txt");
            string[] politics2 = InitProgram("E:/Politicans/2Politic.txt");
            string[] politics3 = InitProgram("E:/Politicans/3Politic.txt");
            string[] politics4 = InitProgram("E:/Politicans/4Politic.txt");

            

            List<double> tfsport1 = GetTfOfDoc(sport1);
            List<double> tfsport2 = GetTfOfDoc(sport2);
            List<double> tfsport3 = GetTfOfDoc(sport3);
            List<double> tfsport4 = GetTfOfDoc(sport4);

            List<double> tfx = GetTfOfDoc(x);

            List<double> tfpolitics1 = GetTfOfDoc(politics1);
            List<double> tfpolitics2 = GetTfOfDoc(politics2);
            List<double> tfpolitics3 = GetTfOfDoc(politics3);
            List<double> tfpolitics4 = GetTfOfDoc(politics4);



            List<double> idfsport1 = GetIdfOfCurrBlock(sport1, sport2, sport3, sport4);
            List<double> idfsport2 = GetIdfOfCurrBlock(sport2, sport1, sport3, sport4);
            List<double> idfsport3 = GetIdfOfCurrBlock(sport3, sport1, sport2, sport4);
            List<double> idfsport4 = GetIdfOfCurrBlock(sport4, sport1, sport2, sport3);

            List<double> idfX = GetIdfOfCurrBlock(x, sport1, sport2, sport3, sport4, politics1, politics2, politics3,
                politics4);

            List<double> idfpolitics1 = GetIdfOfCurrBlock(politics1, politics2, politics3, politics4);
            List<double> idfpolitics2 = GetIdfOfCurrBlock(politics2, politics1, politics3, politics4);
            List<double> idfpolitics3 = GetIdfOfCurrBlock(politics3, politics1, politics2, politics4);
            List<double> idfpolitics4 = GetIdfOfCurrBlock(politics4, politics1, politics2, politics3);



            List<double> dsport1 = CalcDofDoc(tfsport1, idfsport1);
            List<double> dsport2 = CalcDofDoc(tfsport2, idfsport2);
            List<double> dsport3 = CalcDofDoc(tfsport3, idfsport3);
            List<double> dsport4 = CalcDofDoc(tfsport4, idfsport4);

            List<double> dx = CalcDofDoc(tfx, idfX);

            List<double> dpolitics1 = CalcDofDoc(tfpolitics1, idfpolitics1);
            List<double> dpolitics2 = CalcDofDoc(tfpolitics2, idfpolitics2);
            List<double> dpolitics3 = CalcDofDoc(tfpolitics3, idfpolitics3);
            List<double> dpolitics4 = CalcDofDoc(tfpolitics4, idfpolitics4);


            double tomimoto1 = Tomimoto(dx, dsport1);
            double tomimoto2 = Tomimoto(dx, dsport2);
            double tomimoto3 = Tomimoto(dx, dsport3);
            double tomimoto4 = Tomimoto(dx, dsport4);
            double tomimoto5 = Tomimoto(dx, dpolitics1);
            double tomimoto6 = Tomimoto(dx, dpolitics2);
            double tomimoto7 = Tomimoto(dx, dpolitics3);
            double tomimoto8 = Tomimoto(dx, dpolitics4);

            GetResult(tomimoto1, tomimoto2, tomimoto3, tomimoto4, tomimoto5, tomimoto6, tomimoto7, tomimoto8);

            Console.ReadKey();
        }
    }
}
