using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MetaProgramLearn1
{
    public class ExpressionTreeTest
    {
        public static void CompileExpTree()
        {
            Expression<Func<int, int, bool>> expGreaterThan = (left, right) => left > right;
            var funcGreaterThan = expGreaterThan.Compile();
            var rst = funcGreaterThan(100, 23);
            Debug.WriteLine("100 greater than 23：" + rst);
        }

        public static Func<int, int, bool> CompileLambda()
        {
            ParameterExpression left = Expression.Parameter(typeof(int), "left");
            ParameterExpression right = Expression.Parameter(typeof(int), "right");
            Expression<Func<int, int, bool>> expGreaderThan = Expression.Lambda<Func<int, int, bool>>(Expression.GreaterThan(left, right), left, right);
            return expGreaderThan.Compile();
        }

        public static Func<T, T, bool> Generate<T>(string op)
        {
            ParameterExpression left = Expression.Parameter(typeof(T), "x");
            ParameterExpression right = Expression.Parameter(typeof(T), "y");
            return Expression.Lambda<Func<T, T, bool>>(
                (
                    op.Equals(">") ? Expression.GreaterThan(left, right) :
                    op.Equals("<") ? Expression.LessThan(left, right) :
                    op.Equals(">=") ? Expression.GreaterThanOrEqual(left, right) :
                    op.Equals("<=") ? Expression.LessThanOrEqual(left, right) :
                    op.Equals("!=") ? Expression.NotEqual(left, right) :
                    Expression.Equal(left, right)
                )
                , left, right)
                .Compile();
        }
    }
}
