using createSQLLIB.getWords;

namespace createSQLLIB
{
    public class GenerateSQL
    {
        public GenerateSQL()
        {
            
        }

        public string GenerateInsertSQL()
        {
            GetFile getFile = new("txt", "province", "H:\\0CSHARP\\createSql\\createSQLLIB\\words");
            var content = getFile.GetContent();
            return content;
        }

    }
}
