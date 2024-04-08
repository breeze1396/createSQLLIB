using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;


namespace createSQLLIB.generate
{
    public class FieldInfo
    {
        public required string FieldName { get; set; }
        public required string FieldType { get; set; }
        public string? DefaultValue { get; set; }
        public bool IsPrimaryKey { get; set; }
        public string? Comment { get; set; }
        public bool IsNullable { get; set; }
        public bool IsAutoIncrement { get; set; }
        public required SimulateType SimuType { get; set; }
    }
    /*todo：解决comment中带有','的问题
     * 对于解析字段，加上字段的约束
     * 表名自动解析
     * 表的注释自动解析
        */
    public class PraseSQL
    {
        public PraseSQL(string createTableSql)
        {
            _createTableSql = createTableSql;
            ShrinkSql();
            TableName = PraseTableName(_createTableSql);
            TableComment = PraseTableComment(_createTableSql);
        }

        

        public List<FieldInfo> ExtractFields()
        {
            List<FieldInfo> fields = [];
            var sql = SplitSQL(_createTableSql);

            for (int i = 0; i < sql.Length; i++)
            {
                if (sql[i].Length == 0) continue;
                var sqlArray = sql[i].Split(" ");
                FieldInfo field = new()
                {
                    FieldName = sqlArray[0],
                    FieldType = sqlArray[1],
                    DefaultValue = PraseDefaultValue(sql[i]),
                    Comment = PraseComment(sql[i]),
                    IsPrimaryKey = PraseIsPrimaryKey(sql[i]),
                    IsNullable = PraseIsNullable(sql[i]),
                    IsAutoIncrement = PraseIsAutoIncrement(sql[i]),
                    SimuType = new NoSimulate()
                };

                fields.Add(field);
            }
            return fields;
        }

        private static string[] SplitSQL(string sql)
        {
            sql = sql.Substring(sql.IndexOf("(") + 1, sql.LastIndexOf(")") - sql.IndexOf("(") - 1);
            return sql.Split(",");
        }
        private static string PraseTableName(string sql)
        {
            var regex = new Regex(@"\s*(\S+)\s*\(", RegexOptions.IgnoreCase);
            Match match = regex.Match(sql);

            if (match.Success && match.Groups.Count > 1)
            {
                return match.Groups[1].Value;
            }

            return "UserDB";
        }
        private static string PraseTableComment(string sql)
        {
            var regex = new Regex(@"\)\s*COMMENT\s*['|""](.+?)['|""]", RegexOptions.IgnoreCase);
            Match match = regex.Match(sql);

            if (match.Success && match.Groups.Count > 1)
            {
                return match.Groups[1].Value;
            }

            return "";
        }
        private static string PraseType(string sql)
        {
            return "";
        }
        private static string? PraseDefaultValue(string sql)
        {
            var regex = new Regex(@"DEFAULT\s*(\S+)(?=[ ,)])", RegexOptions.IgnoreCase);
            Match match = regex.Match(sql);

            if (match.Success && match.Groups.Count > 1)
            {
                return match.Groups[1].Value;
            }

            return null;
        }
        private static string? PraseComment(string sql)
        {
            var regex = new Regex(@"[COMMENT]\s*['|""](.+?)['|""]", RegexOptions.IgnoreCase);
            Match match = regex.Match(sql);
            if (match.Success && match.Groups.Count > 1)
            {
                return match.Groups[1].Value;
            }

            return null;
        }
        private static bool PraseIsPrimaryKey(string sql)
        {
            return sql.Contains("PRIMARY KEY") || sql.Contains("primary key");
        }
        private static bool PraseIsNullable(string sql)
        {
            return sql.Contains("NOT NULL") || sql.Contains("not null");
        }
        private static bool PraseIsAutoIncrement(string sql)
        {
            return sql.Contains("auto_increment") || sql.Contains("AUTO_INCREASE");
        }
        private void ShrinkSql()
        {
            _createTableSql = _createTableSql.Replace("\r", "").Replace("\n", "");
            _createTableSql = Regex.Replace(_createTableSql, @"\s+", " ");
        }
        
        
        public string TableName { get; }
        public string TableComment { get; }


        private string _createTableSql = "";
    }
}