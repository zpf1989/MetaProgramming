using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaProgramLearn1
{
    public class DynamicObjectTest
    {
        public static void Test()
        {
            dynamic dynObj = new MyExpandoObject();
            dynObj.Name = "zpf";
            dynObj.Age = 29;

            Console.WriteLine("My name is {0} and i'm {1} years old.", dynObj.Name, dynObj.age);
        }
    }

    public class MyExpandoObject : DynamicObject
    {
        private Dictionary<string, object> _dict = new Dictionary<string, object>();

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = null;
            if (_dict.ContainsKey(binder.Name.ToUpper()))
            {
                result = _dict[binder.Name.ToUpper()];
                return true;
            }
            return false;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (_dict.ContainsKey(binder.Name.ToUpper()))
            {
                _dict[binder.Name.ToUpper()] = value;
            }
            else
            {
                _dict.Add(binder.Name.ToUpper(), value);
            }
            return true;
        }
    }
}
