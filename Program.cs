using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partitioner
{
    class Program
    {
        public static List<string> OneDimAddListAtoListB(List<string> listA, List<string> listB)
        {
            List<string> listResult = new List<string>() { };
            for (int i = 0; i < listA.Count; ++i)
            {
                listResult.Add(listA[i]);
            }
            for (int i = 0; i < listB.Count; ++i)
            {
                listResult.Add(listB[i]);
            }
            return listResult;
        }//adding lists together
        public static List<List<string>> TwoDimAddListAtoListB(List<List<string>> listA, List<List<string>> listB)
        {
            List<List<string>> ResultTwoDimList = new List<List<string>>() { };
            for (int i = 0; i < listA.Count; ++i)
            {
                ResultTwoDimList.Add(listA[i]);
            }
            for (int i = 0; i < listB.Count; ++i)
            {
                ResultTwoDimList.Add(listB[i]);
            }
            return ResultTwoDimList;
        }
        public static void OutputList(List<string> list)
        {
            Console.Write("[");
            for (int i = 0; i < list.Count; ++i)
            {
                if (i == list.Count - 1)
                    Console.Write("{0}", list[i]);
                else
                    Console.Write("{0},", list[i]);
                
            }
            Console.WriteLine("]");
        }//output 1D list
        public static List<string> SliceList(List<string> list, int start, int end)
        {
            
            end = (end == -1) ? list.Count : end;
            start = (start == -1) ? 0 : start;
            List<string> sublist = new List<string>() { };
            for (int i = start; i < end; ++i)
            {
                sublist.Add(list[i]);
            }
            return sublist;
        } //my c# version of slicing
        public static void OutputList(List<List<string>> twodimlist)
        {
            Console.Write("[");
            for (int i = 0; i < twodimlist.Count; ++i)
            {
                Console.Write("[");
                for (int j = 0; j < twodimlist[i].Count; ++j)
                {
                    if (j == twodimlist[i].Count - 1)
                        Console.Write("{0}", twodimlist[i][j]);
                    else
                        Console.Write("{0},", twodimlist[i][j]);
                }
                if (i == twodimlist.Count - 1)
                    Console.Write("]");
                else
                    Console.Write("],");
            }
            Console.WriteLine("]");
        } //output 2D list
        public static void OutputList(List<List<List<string>>> threedimlist)//output 3D list
        {
            Console.Write("[");
            for (int i = 0; i < threedimlist.Count; ++i)
            {
                Console.Write("[");
                for (int j = 0; j < threedimlist[i].Count; ++j)
                {
                    Console.Write("[");
                    for (int k = 0; k < threedimlist[i][j].Count; ++k)
                    {
                        if (k == threedimlist[i][j].Count - 1)
                            Console.Write("{0}", threedimlist[i][j][k]);
                        else
                            Console.Write("{0},", threedimlist[i][j][k]);
                    }
                    if (j == threedimlist[i].Count - 1)
                        Console.WriteLine("]");
                    else
                        Console.Write("],");
                }
                if (i == threedimlist.Count - 1)
                    Console.Write("]");
                else
                    Console.Write("],");                
            }
            Console.WriteLine("]");
        }
        public static List<List<List<string>>> PairPartitioner(List<string> a)
        {
            if (a.Count == 2)
            {
                List<List<List<string>>> temp = new List<List<List<string>>>() { new List<List<string>>() { new List<string>() { a[0], a[1] } } };
                return temp;
            }
            List<List<List<string>>> ret = new List<List<List<string>>>() { };
            for (int i = 1; i < a.Count; ++i)
            {
                List<List<string>> p1 = new List<List<string>>() { new List<string>() { a[0], a[i] } };
                List<string> rem = OneDimAddListAtoListB(SliceList(a, 1, i), SliceList(a, i + 1, -1));
                List<List<List<string>>> res = PairPartitioner(rem);
                foreach (var ri in res)
                {
                    ret.Add(TwoDimAddListAtoListB(p1, ri));
                }
            }
            return ret;
        }
        public static void RunFunctionTests()
        {
            //test OneDimAddListAtoListB function
            List<string> onedimlistone = new List<string>() { "A", "B" };
            List<string> onedimlisttwo = new List<string>() { "C", "D" };
            List<string> onedimlistoneaddtwo = OneDimAddListAtoListB(onedimlistone, onedimlisttwo);
            Console.WriteLine("Expected: \t[A,B,C,D]");
            Console.Write("Actual: \t");
            OutputList(onedimlistoneaddtwo); //expected: [A,B,C,D]

            //test TwoDimAddListAtoListB function
            List<List<string>> twodimlistone = new List<List<string>>() { new List<string>() { "A", "B" }, new List<string>() { "C", "D" } };
            List<List<string>> twodimlisttwo = new List<List<string>>() { new List<string>() { "E", "F" }, new List<string>() { "G", "H" } };
            List<List<string>> twodimlistoneaddtwo = TwoDimAddListAtoListB(twodimlistone, twodimlisttwo);
            Console.WriteLine("Expected: \t[[A,B],[C,D],[E,F],[G,H]]");
            Console.Write("Actual: \t");
            OutputList(twodimlistoneaddtwo); //expected [[A,B],[C,D],[E,F],[G,H]]

            //test Output for three-dimensional lists
            List<List<List<string>>> threedimlistone = new List<List<List<string>>>() { twodimlistone, twodimlisttwo };
            Console.WriteLine("Expected: \t[[[A,B],[C,D]],[[E,F],[G,H]]]");
            Console.Write("Actual: \t");
            OutputList(threedimlistone);

            //test SliceList
            List<string> SliceTester = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H" };
            Console.Write("Slice [1:] \t");
            OutputList(SliceList(SliceTester, 1, -1));
            Console.Write("Slice[1:3]\t");
            OutputList(SliceList(SliceTester, 1, 3));
            Console.Write("Slice[1:7]\t");
            OutputList(SliceList(SliceTester, 1, 7));
            Console.Write("Slice[0:7]\t");
            OutputList(SliceList(SliceTester, 0, 7));
            Console.Write("Slice[5:3]\t");
            OutputList(SliceList(SliceTester, 5, 3));
            Console.Write("Slice[:3]\t");
            OutputList(SliceList(SliceTester, -1, 3));
            Console.Write("Slice[:1]\t");
            OutputList(SliceList(SliceTester, -1, 1));
            Console.Write("Slice[:0]\t");
            OutputList(SliceList(SliceTester, -1, 0));
            Console.Write("Slice[:7]\t");
            OutputList(SliceList(SliceTester, -1, 7));
        }
        static void Main(string[] args)
        {
            List<string> testrun = new List<string>() { "A", "B", "C", "D", "E", "F"};
            List<List<List<string>>> Combinations = PairPartitioner(testrun);
            OutputList(Combinations);
            Console.WriteLine(Combinations.Count);
            
        }
    }
}
