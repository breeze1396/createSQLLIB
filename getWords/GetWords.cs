using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace createSQLLIB.getWords
{
    public class GetWords
    {
        public GetWords(string wordsName)
        {
            GetFile getFile = new("txt", wordsName, "H:\\0CSHARP\\createSql\\createSQLLIB\\words");
            _strings = GetStringArrayg(getFile.GetContent());
        }

        public string[] GetStrings()
        {
            return _strings;
        }

        private string[] GetStringArrayg(string multilineString)
        {
            multilineString = multilineString.Replace("\r", "").Replace("\n", "");
            return multilineString.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        }

        private string[] _strings;
    }
}
