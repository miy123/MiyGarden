using Sprache;
using System;
using System.Linq;

namespace MiyGarden.Service.Others
{
    public abstract record Expr;
    public record BinaryExpr(string Field, string Op, string Value) : Expr;
    public record AndExpr(Expr Left, Expr Right) : Expr;
    public record User(string Country, int Age);

    public static class MiniQueryParser
    {
        // 解析識別字 (欄位名)
        private static readonly Parser<string> Identifier =
            Parse.Letter.AtLeastOnce().Text().Token();
        // 解析運算子
        private static readonly Parser<string> Operator =
            Parse.String("=").Text()
            .Or(Parse.String(">").Text())
            .Or(Parse.String("<").Text())
            .Token();
        // 解析字串常值 'xxx'
        private static readonly Parser<string> QuotedString =
            from open in Parse.Char('\'')
            from content in Parse.CharExcept('\'').Many().Text()
            from close in Parse.Char('\'')
            select content;
        // 解析數字
        private static readonly Parser<string> Number =
            Parse.Number.Token();
        // 值可以是數字或字串
        private static readonly Parser<string> Value =
            QuotedString.Or(Number);
        // 單一條件：Field Op Value
        private static readonly Parser<Expr> Condition =
            from field in Identifier
            from op in Operator
            from val in Value
            select new BinaryExpr(field, op, val);
        // 支援 AND 運算
        private static readonly Parser<Expr> AndCondition =
            Parse.ChainOperator(
                Parse.IgnoreCase("and").Token(),
                Condition,
                (op, left, right) => new AndExpr(left, right)
            );
        public static Expr ParseQuery(string input) => AndCondition.End().Parse(input);
    }

    public class SpracheTest
    {
        public static void Main()
        {
            var input = "age > 18 and country = 'TW'";
            var ast = MiniQueryParser.ParseQuery(input);
            Console.WriteLine(ast);
            // 輸出：AndExpr { Left = BinaryExpr { Field = age, Op = >, Value = 18 },
            //                Right = BinaryExpr { Field = country, Op = =, Value = TW } }

            var sql = ToSql(ast);
            Console.WriteLine(sql);

            var filter = Compile(ast);
            var users = new User[] { new("CN", 100), new("TW", 100) };
            var result = users.Where(filter).ToList();
            Console.WriteLine(result[0].Country + result[0].Age);
        }

        private static string ToSql(Expr expr) =>
            expr switch
            {
                BinaryExpr b => $"{b.Field} {b.Op} '{b.Value}'",
                AndExpr a => $"({ToSql(a.Left)} AND {ToSql(a.Right)})",
                _ => throw new NotImplementedException()
            };

        private static Func<User, bool> Compile(Expr expr) =>
            expr switch
            {
                BinaryExpr { Field: "age", Op: ">", Value: var v }
                => u => u.Age > int.Parse(v),
                BinaryExpr { Field: "country", Op: "=", Value: var v }
                => u => u.Country == v,
                AndExpr a => u => Compile(a.Left)(u) && Compile(a.Right)(u),
                _ => throw new NotSupportedException()
            };
    }
}
