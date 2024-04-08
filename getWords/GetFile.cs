using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace createSQLLIB.getWords
{
    internal class GetFile
    {
        private readonly string _path;
        private readonly string _fileName;
        private readonly string _fileExtension;
        private string _fileContent;

        public GetFile(string fileExtension, string fileName, string path)
        {
            _path = path;
            _fileName = fileName;
            _fileExtension = fileExtension;
            _fileContent = File.ReadAllText($"{_path}\\{_fileName}.{_fileExtension}");
        }

        public string GetContent()
        {
            return _fileContent;
        }

        public void SetContent(string content)
        {
            _fileContent = content;
        }

        public void SaveFile()
        {
            File.WriteAllText($"{_path}\\{_fileName}.{_fileExtension}", _fileContent);
        }

        /*
         * 
         */
        public void SaveFile(string path, string fileName, string fileExtension)
        {
            File.WriteAllText($"{path}\\{fileName}.{fileExtension}", _fileContent);
        }
    }
}
