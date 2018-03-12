// Copyright (c) Ivan Bondarev, Stanislav Mihalkovich (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.IO;
using PascalSharp.Compiler;
using PascalSharp.Internal.Errors;
using PascalSharp.Internal.Localization;

namespace PascalSharp.Console
{
	class ConsoleCompiler
	{        
        private static string StringsPrefix = "PABCNETC_";

        private static string StringResourcesGet(string Key)
        {
            return StringResources.Get(StringsPrefix + Key);
        }

        public static int Main(string[] args)
		{
            DateTime ldt = DateTime.Now;
            StringResourcesLanguage.LoadDefaultConfig();

            var Compiler = new Compiler.Compiler(null, null);
            StringResourcesLanguage.CurrentLanguageName = StringResourcesLanguage.AccessibleLanguages[0];
            ldt = DateTime.Now;

            if (args.Length == 0)
            {
                System.Console.WriteLine(StringResourcesGet("COMMANDLINEISABSENT"));
                return 2;
            }

            var FileName = args[0];
            if (!File.Exists(FileName))
            {
                System.Console.WriteLine(StringResourcesGet("FILEISABSENT{0}"),FileName);
                return 3;
            }
            var outputType = CompilerOptions.OutputType.ConsoleApplicaton;

            CompilerOptions co = new CompilerOptions(FileName, outputType);
            if (FileName.ToLower().EndsWith(".pabcproj"))
                co.ProjectCompiled = true;
            if (args.Length==1)
                co.OutputDirectory = "";
            else co.OutputDirectory = args[1];
            co.Rebuild = false;
            co.Debug = true;
            co.UseDllForSystemUnits = false;

            bool success = true;
            if (Compiler.Compile(co) != null)
                System.Console.WriteLine("OK");
            else
            {
                System.Console.WriteLine(StringResourcesGet("COMPILEERRORS"));
                success = false;
            }
            // Console.WriteLine("OK {0}ms", (DateTime.Now - ldt).TotalMilliseconds); /////

            for (int i = 0; i < Compiler.ErrorsList.Count; i++)
            {
                if (Compiler.ErrorsList[i] is LocatedError)
                {
                    SourceLocation sl;
                    if ((sl = (Compiler.ErrorsList[i] as LocatedError).SourceLocation) != null)
                        System.Console.WriteLine(string.Format("[{0},{1}] {2}: {3}", sl.BeginPosition.Line, sl.BeginPosition.Column, Path.GetFileName(sl.FileName), Compiler.ErrorsList[i].Message));
                    else
                        System.Console.WriteLine(Compiler.ErrorsList[i]);
                }
                break; // выйти после первой же ошибки
            }
            if (success)
                return 0;
            else return 1;
		}
	}
}
