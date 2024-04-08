using createSQLLIB.getWords;
using static createSQLLIB.generate.PraseSQL;

namespace createSQLLIB.generate
{
    // 一次解析一个文件
    public class CreateData
    {
        private readonly List<FieldInfo> _fields;   // 字段信息
        private readonly PraseSQL _praseSQL;        // 解析sql

        
        private readonly string _tableName = "";         // tableName
        private string _dataCreateTable = "";            // createTable
        private string _dataInsert = "";                 // insertData

        public CreateData(string createTableSql, string tableName="dbTable")
        {
            _praseSQL = new PraseSQL();

            _fields = _praseSQL.ExtractFields(createTableSql);
            _tableName = tableName;

            SimulateData();
        }

        public void SimulateData()
        {
            _dataCreateTable = $"CREATE TABLE {_tableName}(\n";

            _dataInsert = $"INSERT INTO {_tableName} VALUES(";

            for (int i = 0; i < _fields.Count; i++)
            {
                string fieldName = _fields[i].FieldName;
                string fieldType = _fields[i].FieldType;
                string? defaultValue = _fields[i].DefaultValue;
                string? comment = _fields[i].Comment;
                bool isPrimaryKey = _fields[i].IsPrimaryKey;
                bool isNullable = _fields[i].IsNullable;
                bool isAutoIncrement = _fields[i].IsAutoIncrement;

                _dataInsert += $"({fieldName})";
                _dataCreateTable += $"{fieldName} {fieldType}";
                if (!isNullable)
                {
                    _dataCreateTable += " NOT NULL";
                }
                if (defaultValue != null)
                {
                    _dataCreateTable += $" DEFAULT {defaultValue}";
                }
                if (comment != null)
                {
                    _dataCreateTable += $" COMMENT '{comment}'";
                }
                if (isAutoIncrement)
                {
                    _dataCreateTable += " AUTO_INCREMENT";
                }
                if (isPrimaryKey)
                {
                    _dataCreateTable += " PRIMARY KEY";
                }
                _dataCreateTable += ",\n";
            }
            _dataCreateTable += ");";
            _dataInsert += " values";

            for (int i = 0; i < _fields.Count; i++)
            {
                SimulateType simulateType = _fields[i].SimuType;
                string fieldType = _fields[i].FieldType;
                string? defaultValue = _fields[i].DefaultValue;
                bool isPrimaryKey = _fields[i].IsPrimaryKey;
                bool isNullable = _fields[i].IsNullable;
                bool isAutoIncrement = _fields[i].IsAutoIncrement;

                if (!isNullable)
                {
                    _dataCreateTable += " NOT NULL";
                }
                if (defaultValue != null)
                {
                    _dataCreateTable += $" DEFAULT {defaultValue}";
                }
                if (isAutoIncrement)
                {
                    _dataCreateTable += " AUTO_INCREMENT";
                }
                if (isPrimaryKey)
                {
                    _dataCreateTable += " PRIMARY KEY";
                }
                
            }

            // 对于不同的字段类型，生成不同的数据
            // 1. 字符串

            // 2. 数字
            // 3. 日期
            // 4. 布尔
            // 5. 其他
            Console.WriteLine(_dataCreateTable);
        }

        public string GetDataCreateTable()
        {
            return _dataCreateTable;
        }
        public string GetDataInsert()
        {
            return _dataInsert;
        }


    }
}
