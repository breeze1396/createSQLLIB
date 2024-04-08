using createSQLLIB.getWords;
using static createSQLLIB.generate.PraseSQL;

namespace createSQLLIB.generate
{
    public enum Type
    {
        INT,
        VARCHAR,
        DATETIME,
        TINYINT,
        BIGINT,
        TEXT,
        FLOAT,
        DOUBLE,
        DECIMAL,
        DATE,
        TIME,
        TIMESTAMP,
        CHAR,
        BLOB,
        ENUM
    }
    /*
     * 对于一个表，生成数据
     */
    public class CreateData
    {
        public CreateData(string createTableSql, int times = 10)
        {
            // 初始化
            _praseSQL = new PraseSQL(createTableSql);

            _fields = _praseSQL.ExtractFields();
            // 对于每个字段，设置模拟数据选项

            _tableName = _praseSQL.TableName;
            _tableCom = _praseSQL.TableComment;
            _times = times;


            // 生成数据
            SimulateData();
        }

        public void SimulateData()
        {
            _dataCreateTable = $"CREATE TABLE IF NOT EXISTS {_tableName} (\n";

            _dataInsert = $"INSERT INTO {_tableName} (";

            // 生成createTables 和 insertData
            for (int i = 0; i < _fields.Count; i++)
            {
                string fieldName = _fields[i].FieldName;
                string fieldType = _fields[i].FieldType;
                string? defaultValue = _fields[i].DefaultValue;
                string? comment = _fields[i].Comment;
                bool isPrimaryKey = _fields[i].IsPrimaryKey;
                bool isNullable = _fields[i].IsNullable;
                bool isAutoIncrement = _fields[i].IsAutoIncrement;

                _dataInsert += $"{fieldName},";
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

                Simulate(_fields[i]);   // 模拟数据

            }
            
            _dataCreateTable += ")";
            if(_tableCom.Length != 0) _dataCreateTable += $" COMMENT '{_tableCom}';";
            _dataInsert += ") VALUES\n";

            // 插入insertData的数据
            for(int i = 0; i < _data[0].Count; i++)
            {
                _dataInsert += "(";
                for(int j = 0; j < _data.Count; j++)
                {
                    _dataInsert += $"'{_data[j][i]}',";
                }
                _dataInsert += "),\n";
            }

            Console.WriteLine(_dataCreateTable + "\n\n\n");
            Console.WriteLine(_dataInsert);
        }


        public string GetDataCreateTable()
        {
            return _dataCreateTable;
        }
        public string GetDataInsert()
        {
            return _dataInsert;
        }

        private void Simulate(FieldInfo fieldInfo)
        {
            Type type = whatType(fieldInfo.FieldType);
            SimulateType simulateType = fieldInfo.SimuType;
            var simulateData = new List<string>();
            switch (type)
            {
                case Type.INT:
                    
                    break;
                case Type.VARCHAR:
                    break;
                case Type.DATETIME:
                    break;
                case Type.TINYINT:
                    break;
                case Type.BIGINT:
                    break;
                case Type.TEXT:
                    break;
                case Type.FLOAT:
                    break;
                case Type.DOUBLE:
                    break;
                case Type.DECIMAL:
                    break;
                case Type.DATE:
                    break;
                case Type.TIME:
                    break;
                case Type.TIMESTAMP:
                    break;
                case Type.CHAR:
                    break;
                case Type.BLOB:
                    break;
                case Type.ENUM:
                    break;
            }

            _data.Add(simulateData);
        }

        private Type whatType(string fieldType)
        {
            if (fieldType.Contains("int"))
            {
                return Type.INT;
            }
            else if (fieldType.Contains("varchar"))
            {
                return Type.VARCHAR;
            }
            else if (fieldType.Contains("datetime"))
            {
                return Type.DATETIME;
            }
            else if (fieldType.Contains("tinyint"))
            {
                return Type.TINYINT;
            }
            else if (fieldType.Contains("bigint"))
            {
                return Type.BIGINT;
            }
            else if (fieldType.Contains("text"))
            {
                return Type.TEXT;
            }
            else if (fieldType.Contains("float"))
            {
                return Type.FLOAT;
            }
            else if (fieldType.Contains("double"))
            {
                return Type.DOUBLE;
            }
            else if (fieldType.Contains("decimal"))
            {
                return Type.DECIMAL;
            }
            else if (fieldType.Contains("date"))
            {
                return Type.DATE;
            }
            else if (fieldType.Contains("time"))
            {
                return Type.TIME;
            }
            else if (fieldType.Contains("timestamp"))
            {
                return Type.TIMESTAMP;
            }
            else if (fieldType.Contains("char"))
            {
                return Type.CHAR;
            }
            else if (fieldType.Contains("blob"))
            {
                return Type.BLOB;
            }
            else if (fieldType.Contains("enum"))
            {
                return Type.ENUM;
            }
            return Type.VARCHAR;
        }

        private void simuINTref(ref List<string> strings, SimulateType simulateType)
        {
            switch(simulateType.Type)
            {
                case SimulateTypeEum.FixedValue:
                    for(int i = 0; i < _times; i++)
                    {
                        strings.Add("");
                    }
                    break;
                default:

                    break;
            }
        }

        private readonly List<FieldInfo> _fields;   // 字段信息
        private readonly PraseSQL _praseSQL;        // 解析sql


        private readonly string _tableName = "";         // tableName
        private readonly string _tableCom = "";          // tableComment
        private string _dataCreateTable = "";            // createTable
        private string _dataInsert = "";                 // insertData
        private int _times;                              // 生成数据的次数

        private List<List<string>> _data = [];            // 数据
    } // class CreateData

} // namespace createSQLLIB.generate
