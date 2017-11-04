using System;
using System.IO;
using System.Text;
using System.CodeDom;
using System.Diagnostics;
using System.CodeDom.Compiler;
namespace MetaProgramLearn1
{
    public class CodeDomTest
    {
        public static void ShowLangs()
        {
            foreach (System.CodeDom.Compiler.CompilerInfo ci in System.CodeDom.Compiler.CodeDomProvider.GetAllCompilerInfo())
            {
                foreach (string lang in ci.GetLanguages())
                {
                    System.Console.Write(string.Format("{0} ", lang));
                }
                System.Console.WriteLine("");
            }
        }

        public static void GenerateCodes(string lang = "c#")
        {
            var nsProgram = BuildProgram();
            var compilerOpts = new CodeGeneratorOptions { IndentString = "\t", BracingStyle = "C", BlankLinesBetweenMembers = false };
            var codeTxtBuilder = new StringBuilder();
            using (var codeWriter = new StringWriter(codeTxtBuilder))
            {
                CodeDomProvider.CreateProvider(lang)
                    .GenerateCodeFromNamespace(nsProgram, codeWriter, compilerOpts);
            }
            var script = codeTxtBuilder.ToString();
            Debug.WriteLine(script);
        }

        public static CodeNamespace BuildProgram()
        {
            var ns = new CodeNamespace("MetaWorld");
            var nsImport = new CodeNamespaceImport("System");
            ns.Imports.Add(nsImport);

            var programClass = new CodeTypeDeclaration("Program");
            ns.Types.Add(programClass);

            var methodMain = new CodeMemberMethod { Attributes = MemberAttributes.Static, Name = "Main" };
            methodMain.Statements.Add(
                    new CodeMethodInvokeExpression(
                        new CodeSnippetExpression("Console"),
                        "WriteLine",
                        new CodePrimitiveExpression("Hello world!")
                        )
                    );
            programClass.Members.Add(methodMain);

            return ns;
        }
    }
}
