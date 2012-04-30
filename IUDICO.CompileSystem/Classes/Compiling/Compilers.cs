using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace CompileSystem.Classes.Compiling
{
    public class Compilers
    {
        private readonly string compilersDirectoryPath;
        private readonly List<Compiler> compilers;

        public Compilers(string compilersDirectory)
        {
            if (compilersDirectory == null)
                throw new Exception("Directory path can't be null");

            this.compilersDirectoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, compilersDirectory);
            this.compilers = new List<Compiler>();
        }

        public void Load()
        {
            if (!Directory.Exists(this.compilersDirectoryPath))
            {
                return;
                // throw new FileNotFoundException("Can't find file", _compilersDirectoryPath);
            }

            string[] compileDirectoriesNames = Directory.GetDirectories(this.compilersDirectoryPath);

            foreach (var compileDirectoriesName in compileDirectoriesNames)
            {
                string[] xmlFile = Directory.GetFiles(compileDirectoriesName, "*.xml");
                /*
                if (xmlFile.Count() == 0)
                    throw new FileNotFoundException("Can't find any xml file");
                 * */
                if (xmlFile.Count() > 1)
                    throw new Exception("Can't choose xml file");

                if (xmlFile.Count() == 1)
                {
                    var compiler = this.Parse(xmlFile[0]);
                    this.compilers.Add(compiler);
                }
            }
        }

        public Compiler GetCompiler(string compilerName)
        {
            if (this.compilers.Count == 0)
                return null;

            // TODO: unique compilers
            var compiler = this.compilers.SingleOrDefault(item => item.Name == compilerName);

            return compiler;
        }

        private Compiler Parse(string xmlFilePath)
        {
            var compiler = new Compiler();
            var xmlReader = new XmlTextReader(xmlFilePath);
            xmlReader.Read();

            while (xmlReader.Read())
            {
                switch (xmlReader.Name)
                {
                    case "name":
                        {
                            compiler.Name = xmlReader.ReadElementContentAsString();
                        }
                        break;

                    case "location":
                        {
                            compiler.Location = Path.Combine(
                                AppDomain.CurrentDomain.BaseDirectory, xmlReader.ReadElementContentAsString());
                        }
                        break;

                    case "extension":
                        {
                            compiler.Extension = xmlReader.ReadElementContentAsString().TrimEnd().TrimStart();
                        }
                        break;
                    case "arguments":
                        {
                            compiler.Arguments = xmlReader.ReadElementContentAsString().TrimEnd().TrimStart();
                        }
                        break;
                    case "compiledExtension":
                        {
                            compiler.CompiledExtension = xmlReader.ReadElementContentAsString().TrimEnd().TrimStart();
                        }
                        break;
                    case "needShortName":
                        {
                            compiler.IsNeedShortPath = xmlReader.ReadElementContentAsBoolean();
                        }
                        break;
                }
            }

            if (compiler.Extension == string.Empty || compiler.Name == string.Empty || compiler.Location == string.Empty || compiler.CompiledExtension == string.Empty || compiler.Arguments == string.Empty)
                throw new Exception("Bad xml information about compiler");

            return compiler;
        }

        public int Count
        {
            get { return this.compilers.Count; }
        }

        public void AddCompiler(Compiler newCompiler)
        {
            if (newCompiler == null)
                throw new Exception("Compiler is null");

            this.compilers.Add(newCompiler);
        }

        public List<Compiler> GetCompilers()
        {
            return this.compilers;
        }

        public void Clear()
        {
            this.compilers.Clear();
        }

        public bool Contains(string compilerName)
        {
            var result = this.GetCompiler(compilerName);
            if (result != null)
                return true;

            return false;
        }
    }
}