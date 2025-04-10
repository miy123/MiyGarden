using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Sprache;
using System.Text;

namespace SourceGenerator
{
    [Generator]
    public class RecordSourceGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            // 過濾出副檔名為 .udt 的額外檔案
            var dslFiles = context.AdditionalTextsProvider
                .Where(file => file.Path.EndsWith(".txt"))
                .Select((file, cancellationToken) => file.GetText(cancellationToken)?.ToString())
                .Where(text => !string.IsNullOrWhiteSpace(text));

            // 解析 DSL 並生成對應的類別
            context.RegisterSourceOutput(dslFiles, (spc, text) =>
            {
                var records = DslGrammar.Records.Parse(text);
                foreach (var rec in records)
                {
                    var source = GenerateClassSource(rec);
                    spc.AddSource($"{rec.Name}.g.cs", SourceText.From(source, Encoding.UTF8));
                }
            });
        }

        private string GenerateClassSource(RecordDef rec)
        {
            var sb = new StringBuilder();
            sb.AppendLine("// Auto-generated class");
            sb.AppendLine($"public class {rec.Name}");
            sb.AppendLine("{");
            foreach (var field in rec.Fields)
            {
                sb.AppendLine($"    public {field.Type} {field.Name} {{ get; set; }}");
            }
            sb.AppendLine("}");
            return sb.ToString();
        }
    }

    public class FieldDef
    {
        public string? Type { get; set; }
        public string? Name { get; set; }
    }

    public class RecordDef
    {
        public string? Name { get; set; }
        public List<FieldDef> Fields { get; set; } = new();
    }

    public static class DslGrammar
    {
        private static readonly Parser<string> Identifier =
            from leading in Parse.WhiteSpace.Many()
            from first in Parse.Letter.Once()
            from rest in Parse.LetterOrDigit.Many()
            from trailing in Parse.WhiteSpace.Many()
            select new string(first.Concat(rest).ToArray());

        private static readonly Parser<FieldDef> Field =
            from type in Identifier
            from name in Identifier
            select new FieldDef { Type = type, Name = name };

        private static readonly Parser<IEnumerable<FieldDef>> Fields =
            from open in Parse.Char('[').Token()
            from fields in Field.DelimitedBy(Parse.Char(',').Token())
            from close in Parse.Char(']').Token()
            select fields;

        public static readonly Parser<RecordDef> Record =
            from _ in Parse.String("record").Token()
            from name in Identifier
            from fields in Fields
            select new RecordDef { Name = name, Fields = fields.ToList() };

        public static readonly Parser<IEnumerable<RecordDef>> Records =
            Record.Many();
    }
}
