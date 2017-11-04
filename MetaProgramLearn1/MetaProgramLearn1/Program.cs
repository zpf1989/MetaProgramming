using MetaProgramLearn1.Reflection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaProgramLearn1
{
    class Program
    {
        static void Main(string[] args)
        {
            //CodeDomT();
            //ExpTreeT();
            //DynamicObjectTest.Test();
            ReflectionTest.GetGenericType();
            Console.ReadKey();
        }

        static void CodeDomT()
        {
            //CodeDomTest.ShowLangs();
            CodeDomTest.GenerateCodes("c#");
            CodeDomTest.GenerateCodes("c++");
            CodeDomTest.GenerateCodes("vb");
            //CodeDomTest.GenerateCodes("javascript");
        }

        static void ExpTreeT()
        {
            int left = 100, right = 1000;
            ExpressionTreeTest.CompileExpTree();
            var func = ExpressionTreeTest.CompileLambda();
            Debug.WriteLine("{0} > {1} is {2}", left, right, func(left, right));
            Debug.WriteLine("---------------CompileLambda Extend---------------");
            var dynamicFunc = ExpressionTreeTest.Generate<int>("<=");
            Debug.WriteLine("int：{0} <= {1} is {2}", left, right, dynamicFunc(left, right));
            double x = 100.89, y = 89.23;
            var decFunc = ExpressionTreeTest.Generate<double>("!=");
            Debug.WriteLine("double：{0} != {1} is {2}", left, right, decFunc(x, y));
        }
    }
}
