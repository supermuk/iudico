namespace IUDICO.UnitTests.CompileService
{
    internal class CompileServiceLanguageSourceCode
    {
        //--correct source code

        //CPP correct source code
        public static string CPPCorrectSourceCode =
            "#include<iostream>\n#include<string>\nusing namespace std;\nvoid main(){\nstring a,b;\ncin>>a>>b;\ncout<<a<<b;}";

        //CS correct cource code
        public static string CSCorrectSourceCode =
            "using System;namespace MyProg{internal class Program{private static void Main(string[] args){string ar = Console.ReadLine();string result;string first, second;first = ar.Split(' ')[0];second = ar.Split(' ')[1];result = first.Insert(1,second);Console.Write(result);}}}";

        //Java correct source code
        public static string JavaCorrectSourceCode =
            "import java.io.BufferedReader;\nimport java.io.IOException;\nimport java.io.InputStreamReader;\npublic class Program \n{\npublic static void main(String[] args) throws IOException \n{\nStringBuilder builder = new StringBuilder();\nInputStreamReader input = new InputStreamReader(System.in);\nBufferedReader reader = new BufferedReader(input);\nString inputLine = reader.readLine();\nString first = inputLine.substring(0,1);\n String second = inputLine.substring(2,3);\n String result = first.concat(second);\n System.out.println(result);\n}\n}";

        //Delphi correct source code
        public static string DelphiCorrectSourceCode =
            "program Helloworld; \n{$APPTYPE CONSOLE}\n begin\n writeln('Hello, world!');\nend.";

        //--incorrect source code

        //CPP incorrect source code
        //+#
        public static string CPPIncorrectSourceCode =
            "include<iostream>\n#include<string>\nusing namespace std;\nvoid main(){\nstring a,b;\ncin>>a>>b;\ncout<<a<<b;}";

        //CS incorrect source code
        //+using
        public static string CSIncorrectSourceCode =
            "System;namespace MyProg{internal class Program{private static void Main(string[] args){string ar = Console.ReadLine();string result;string first, second;first = ar.Split(' ')[0];second = ar.Split(' ')[1];result = first.Insert(1,second);Console.Write(result);}}}";

        //Java incorrect source code
        //+package
        public static string JavaIncorrectSourceCode =
            "import java.io.BufferedReader;\nimport java.io.IOException;\nimport java.io.InputStreamReader;\npublic class Program \n{\npublic static void main(String[] args) throws IOException \n{\nStringBuilder builder = new StringBuilder();\nInputStreamReader input = new InputStreamReader(System.in);\nBufferedReader reader = new BufferedReader(input);\nString inputLine = reader.readLine();\nString first = inputLine.substring(0,1);\n String second = inputLine.substring(2,3);\n String result = first.concat(second);\n System.out.println(result);\n}\n}";

        //Delphi incorrect source code
        //+program
        public static string DelphiIncorrectSourceCode =
            "Helloworld; \n{$APPTYPE CONSOLE}\n begin\n writeln('Hello, world!');\nend.";

        //--incorrect timelimit source code

        //CPP timeLimit bad code
        public static string CPPTimelimitCorrectSourceCode =
            "#include<iostream>\n#include<string>\nusing namespace std;\nvoid main(){\nstring a,b;\nwhile(true){}\ncin>>a>>b;\ncout<<a<<b;}";

        //CS timelimit bad code
        public static string CSTimelimitCorrectSourceCode =
            "using System;namespace MyProg{internal class Program{private static void Main(string[] args){while(true){}string ar = Console.ReadLine();string result;string first, second;first = ar.Split(' ')[0];second = ar.Split(' ')[1];result = first.Insert(1,second);Console.Write(result);}}}";

        //Java timelimit bad code
        public static string JavaTimelimitCorrectSourceCode =
            "import java.io.BufferedReader;\nimport java.io.IOException;\nimport java.io.InputStreamReader;\npublic class Program \n{\npublic static void main(String[] args) throws IOException \n{\nwhile(true){}\n}\n}";

        //Delphi timelimit bad code
        public static string DelphiTimelimitCorrectSourceCode =
            "program Helloworld; \n{$APPTYPE CONSOLE}\nvar\nnum : Integer;\n begin\nnum := 1;\nWhile num <= 100 do \nbegin\nend; \nwriteln('Hello, world!');\nend.";

        //--incorrect memorylimit source code

        //CPP memorylimit bad code
        public static string CPPMemorylimitCorrectSourceCode =
            "#include<iostream>\n#include<string>\nusing namespace std;\nvoid main(){\nstring a,b;\nint myArray[10000];\nfor(int i = 1;i < 10000; i++){\nmyArray[i] = i;\n}\ncin>>a>>b;\ncout<<a<<b;}";

        //CS memorylimit bad code
        public static string CSMemorylimitCorrectSourceCode =
            "using System;namespace MyProg{internal class Program{private static void Main(string[] args){int[] myArray = new int[1000000];for(int i = 0; i < 1000000; i++){myArray[i] = i;}string ar = Console.ReadLine();string result;string first, second;first = ar.Split(' ')[0];second = ar.Split(' ')[1];result = first.Insert(1,second);Console.Write(result);}}}";

        //Java memorylimit bad code
        public static string JavaMemorylimitCorrectSourceCode =
            "import java.io.BufferedReader;\nimport java.io.IOException;\nimport java.io.InputStreamReader;\npublic class Program \n{\npublic static void main(String[] args) throws IOException \n{\nint myArray = new int[1000000];\nfor(int i = 0; i < 1000000; i++){\nmyArray[i] = i;\n}\nStringBuilder builder = new StringBuilder();\nInputStreamReader input = new InputStreamReader(System.in);\nBufferedReader reader = new BufferedReader(input);\nString inputLine = reader.readLine();\nString first = inputLine.substring(0,1);\n String second = inputLine.substring(2,3);\n String result = first.concat(second);\n System.out.println(result);\n}\n}";

        //Delphi memorylimit bad code
        public static string DelphiMemorylimitCorrectSourceCode =
            "program Helloworld; \n{$APPTYPE CONSOLE}\nvar\n X : array of Integer;\ni: Integer;\n begin\ni := 0;\nSetLength(X,1000000);\nWhile i <= 1000000 do \nbegin\n X[i] := i;\ni := i + 1; \nend; \nwriteln('Hello, world!');\nend.";
    }
}
