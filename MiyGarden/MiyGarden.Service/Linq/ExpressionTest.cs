using MiyGarden.Service.Others;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MiyGarden.Service.Linq
{
    public class ExpressionTest
    {
        public void LambdaExpressionTestExecute()
        {
            var extensions = new ExpressionExtensions();
            Console.WriteLine(extensions.LambdaExpressionTest1(x => x + 1, 1));
            Console.WriteLine(extensions.LambdaExpressionTest1((SampleClass x) => x.Name, new SampleClass()));

            Console.WriteLine(extensions.LambdaExpressionTest2(x => x + 1, 1));
            Console.WriteLine(extensions.LambdaExpressionTest2((SampleClass x) => x.Name, new SampleClass()));
        }

        public void Excute()
        {
            int p1 = 10;
            int p2 = 8;

            // -a
            Func<int, int> a1 = n => -n;
            var param1 = Expression.Parameter(typeof(int), "n");
            // -1*a
            Expression expr1 = Expression.Multiply(Expression.Constant(-1), param1);
            Console.WriteLine("-a ：" + a1.Invoke(p1) + "///////" + Expression.Lambda<Func<int, int>>(expr1, new ParameterExpression[] { param1 }).Compile()(p1));

            // a + b * 2
            Func<int, int, int> a2 = (n1, n2) => n1 + n2 * 2;
            Expression expr2 = Expression.Add(Expression.Constant(p1), Expression.Multiply(Expression.Constant(p2), Expression.Constant(2)));
            Console.WriteLine("a + b * 2 ：" + a2.Invoke(p1, p2) + "///////" + Expression.Lambda<Func<int>>(expr2).Compile()());

            //Math.Sin(x) + Math.Cos(y)
            var pparam1 = Expression.Parameter(typeof(double), "x");
            var pparam2 = Expression.Parameter(typeof(double), "y");
            // Math.Sin(x)
            var eexpr1 = Expression.Call(null, new Func<double, double>(Math.Sin).GetMethodInfo(), pparam1);
            // Math.Cos(y)
            var eexpr2 = Expression.Call(null, typeof(Math).GetMethod("Cos"), pparam2);
            Expression expr3 = Expression.Add(eexpr1, eexpr2);
            var lambdaExpr = Expression.Lambda<Func<double, double, double>>(expr3, new ParameterExpression[] { pparam1, pparam2 }).Compile();
            Console.WriteLine("Math.Sin(x) + Math.Cos(y)：" + lambdaExpr(p1, p2));
            var callExpr = new ExpressionCallMethod().Mytry<SampleClass>();

            Console.WriteLine(callExpr.ToString());
            Console.WriteLine(Expression.Lambda<Func<int>>(callExpr).Compile()());

            var callExpr2 = new ExpressionCallMethod().Hello<SampleClass>();

            Expression.Lambda<Action>(callExpr2).Compile()();
        }

        public Expression<Func<string, string, SampleClass>> BuildLambda()
        {
            var createdType = typeof(SampleClass);
            var displayValueParam = Expression.Parameter(typeof(string), "lalala");
            var displayValueParam2 = Expression.Parameter(typeof(string), "lalala2");
            var ctor = Expression.New(createdType);
            var displayValueProperty = createdType.GetProperty("Name");
            var displayValueProperty2 = createdType.GetProperty("Name2");
            var displayValueAssignment = Expression.Bind(
                displayValueProperty, displayValueParam);
            var displayValueAssignment2 = Expression.Bind(
                displayValueProperty2, displayValueParam2);
            var memberInit = Expression.MemberInit(ctor, displayValueAssignment, displayValueAssignment2);

            return
                Expression.Lambda<Func<string, string, SampleClass>>(memberInit, displayValueParam, displayValueParam2);
        }

        public class SampleClass
        {
            public int AddIntegers(int arg1, int arg2)
            {
                return arg1 + arg2;
            }

            public string Name { get; set; }

            public string Name2 { get; set; }
        }

        public class ExpressionCallMethod
        {
            public Expression Mytry<T>()
            {
                Expression callExpr = Expression.Call(
                Expression.New(typeof(T)),
                   new Func<int, int, int>(new SampleClass().AddIntegers).GetMethodInfo()
                //   .GetGenericMethodDefinition()
                //.MakeGenericMethod(typeof(T))
                ,
                //typeof(T).GetMethod("AddIntegers" /*new Type[] { typeof(int), typeof(int) }*/),
                Expression.Constant(1),
                Expression.Constant(2)
                );
                return callExpr;
            }
            public Expression Hello<T>()
            {
                Expression callExpr = Expression.Call(
                   null,
                   new Action<IMiyAble<object>>(MiyAble.SayHello).GetMethodInfo()
                   .GetGenericMethodDefinition()
                   .MakeGenericMethod(typeof(T))
                ,
                Expression.Constant(new T[] { }.AsMiyAble(), typeof(IMiyAble<T>))
                );
                return callExpr;
            }
        }

        public class ExpressionExtensions
        {
            public U LambdaExpressionTest1<T, U>(Func<T, U> func, T parms)
            {
                Console.WriteLine(func.GetMethodInfo());
                return func(parms);
            }

            public U LambdaExpressionTest2<T, U>(Expression<Func<T, U>> func, T parms)
            {
                Console.WriteLine(func.Body);
                if (func.Body is MemberExpression member)
                {
                    Console.WriteLine(member.Member);
                }
                return func.Compile()(parms);
            }

            public Expression<Func<T, bool>> ChangeFuncToExpress<T>(Func<T, bool> boolMethod)
            {
                return x => boolMethod(x);
            }
        }
    }
}
