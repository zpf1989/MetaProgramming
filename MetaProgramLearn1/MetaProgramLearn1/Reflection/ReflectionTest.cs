using System;
using System.Diagnostics;
using System.Reflection;
using System.Linq;

namespace MetaProgramLearn1.Reflection
{
    public class ReflectionTest
    {
        public static void TypeTest()
        {
            var type = Type.GetType("System.Random");
            type = typeof(Random);
            Random rd = new Random();
            type = rd.GetType();
        }

        public static void AssemblyTest()
        {
            //If the assembly is already loaded into the current AppDomain, it’ll return the existing reference. 
            var assembly = Assembly.Load(new AssemblyName() { Name = "mscorlib", Version = new Version(4, 0, 0, 0) });
            var assembly2 = Assembly.Load("mscorlib, Version=4.0.0.0");
            var assembly3 = Assembly.LoadFrom(@"file:///C:/Windows/Microsoft.NET/Framework/v4.0.30319/mscorlib.dll");
            var assembly4 = Assembly.GetExecutingAssembly();
            var ass5 = Assembly.GetEntryAssembly();
        }

        public static void GetGenericType()
        {
            var ass = typeof(Assembly).Assembly;
            var lazyType1 = ass.GetType("System.Lazy`1");
            Debug.WriteLine("lazyType1:" + lazyType1.FullName);
            var lazyType2 = typeof(Lazy<>);//System.Lazy`1
            Debug.WriteLine("lazyType2:" + lazyType2.FullName);
            var lazyType3 = typeof(Lazy<int>);//System.Lazy`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]
            Debug.WriteLine("lazyType3:" + lazyType3.FullName);
            var threeTupleType = typeof(Tuple<,,>);
            Debug.WriteLine("threeTupleType:" + threeTupleType.FullName);//System.Tuple`3
        }

        public static void MakeGenericType()
        {
            var openLazyType = typeof(Lazy<>);
            var closedLazyType = openLazyType.MakeGenericType(typeof(int));
            Debug.WriteLine("closedLazyType:" + closedLazyType.FullName);//System.Lazy`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]
        }

        public static void GetMethod()
        {
            //需要指定详细条件，避免方法重载导致的多个匹配
            var rdType = new Random().GetType();
            var nextMethod1 = rdType.GetMethod("Next", new Type[] { typeof(int), typeof(int) });
            var nextMethod2 = rdType.GetMethod("Next", BindingFlags.Instance | BindingFlags.Public, null, new Type[] { typeof(int), typeof(int) }, null);
            var paras2 = nextMethod2.GetParameters();

            Console.WriteLine("paras2:" + string.Join("|", paras2.Select(p => p.Name + "," + p.Position)));
        }

        public static void GetFieldsAndProperties()
        {
            DateTime dt = DateTime.Now;
            var fields = typeof(DateTime).GetFields();//BindingFlags.Instance | BindingFlags.Public
            Console.WriteLine("fields:" + string.Join("|", fields.Select(f => f.Name)));

            var properties = typeof(DateTime).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            Console.WriteLine("props:" + string.Join("|", properties.Select(p => p.Name)));
        }

    }
}
