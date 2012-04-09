using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace CompileSystem.Compilation_Part
{
    public class Compilers
    {
        private readonly string _compilersDirectoryPath;
        private List<Compiler> _compilers;

        public Compilers(string compilersDirectory)
        {
            _compilersDirectoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, compilersDirectory);
            _compilers = new List<Compiler>();
        }

        public void Load()
        {
            //TODO: maybe move it to another class
            if (!Directory.Exists(_compilersDirectoryPath))
            {
                throw new FileNotFoundException("Can't find file", _compilersDirectoryPath);
            }

            string[] compileDirectoriesNames = Directory.GetDirectories(_compilersDirectoryPath);

            foreach (var compileDirectoriesName in compileDirectoriesNames)
            {
                string[] xmlFile = Directory.GetFiles(compileDirectoriesName, "*.xml");

                if (xmlFile.Count() == 0)
                    throw new FileNotFoundException("Can't find any xml file");
                if (xmlFile.Count() > 1)
                    throw new Exception("Can't choose xml file");

                var compiler = Parse(xmlFile[0]);
                _compilers.Add(compiler);
            }
        }

        public Compiler GetCompiler(string compilerName)
        {
            if (_compilers.Count == 0)
                return null;

            //TODO:unique compilers
            var compiler = _compilers.SingleOrDefault(item => item.Name == compilerName);

            return compiler;
        }

        //TODO:make correct parse function
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
                            compiler.Location = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                                             xmlReader.ReadElementContentAsString());
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
                }
            }

            if (compiler.Extension == "" || compiler.Name == "" || compiler.Location == "")
                throw new Exception("Bad xml information about compiler");

            return compiler;
        }

        public int Count
        {
            get { return _compilers.Count; }
        }

        public void AddCompiler(Compiler newCompiler)
        {

        }

        public List<Compiler> GetCompilers()
        {
            return _compilers;
        }

        public void Clear()
        {
            _compilers.Clear();
        }

        //TODO: does it need
        public bool Contains(string compilerName)
        {
            var result = GetCompiler(compilerName);
            if (result != null)
                return true;

            return false;
        }
    }
}