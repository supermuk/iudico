namespace IUDICO.UnitTests.CompileService
{
    class CompileServiceLanguageSourceCode
    {
        //--correct source code

        //CPP correct source code
        public static string CPPCorrectSourceCode = "#include<iostream>\n#include<string>\nusing namespace std;\nvoid main(){\nstring a,b;\ncin>>a>>b;\ncout<<a<<b;}";
        
        //CS correct cource code
        public static string CSCorrectSourceCode = "using System;namespace MyProg{internal class Program{private static void Main(string[] args){string ar = Console.ReadLine();string result;string first, second;first = ar.Split(' ')[0];second = ar.Split(' ')[1];result = first.Insert(1,second);Console.Write(result);}}}";
        
        //Java correct source code
        public static string JavaCorrectSourceCode = "import java.io.BufferedReader;\nimport java.io.IOException;\nimport java.io.InputStreamReader;\npublic class Main \n{\npublic static void main(String[] args) throws IOException \n{\nStringBuilder builder = new StringBuilder();\nInputStreamReader input = new InputStreamReader(System.in);\nBufferedReader reader = new BufferedReader(input);\nString inputLine = reader.readLine();\nString first = inputLine.substring(0,1);\n String second = inputLine.substring(2,3);\n String result = first.concat(second);\n System.out.println(result);\n}\n}";
        
        //Delphi correct source code
        public static string DelphiCorrectSourceCode = "program Helloworld; \n{$APPTYPE CONSOLE}\n begin\n writeln('Hello, world!');\nend.";

        //--incorrect source code

        //CPP incorrect source code
        //+#
        public static string CPPIncorrectSourceCode = "include<iostream>\n#include<string>\nusing namespace std;\nvoid main(){\nstring a,b;\ncin>>a>>b;\ncout<<a<<b;}";
        
        //CS incorrect source code
        //+using
        public static string CSIncorrectSourceCode = "System;namespace MyProg{internal class Program{private static void Main(string[] args){string ar = Console.ReadLine();string result;string first, second;first = ar.Split(' ')[0];second = ar.Split(' ')[1];result = first.Insert(1,second);Console.Write(result);}}}";
        
        //Java incorrect source code
        //+package
        public static string JavaIncorrectSourceCode = "import java.io.BufferedReader;\nimport java.io.IOException;\nimport java.io.InputStreamReader;\npublic class Main \n{\npublic static void main(String[] args) throws IOException \n{\nStringBuilder builder = new StringBuilder();\nInputStreamReader input = new InputStreamReader(System.in);\nBufferedReader reader = new BufferedReader(input);\nString inputLine = reader.readLine();\nString first = inputLine.substring(0,1);\n String second = inputLine.substring(2,3);\n String result = first.concat(second);\n System.out.println(result);\n}\n}";
        
        //Delphi incorrect source code
        //+program
        public static string DelphiIncorrectSourceCode = "Helloworld; \n{$APPTYPE CONSOLE}\n begin\n writeln('Hello, world!');\nend.";
    }
}
