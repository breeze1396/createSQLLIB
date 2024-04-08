using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;


namespace createSQLLIB.generate
{
    //todo：解决comment中带有','的问题
    public class PraseSQL
    {
       public enum SimulateType
       {
            noSimulate,
            fixedValue,
            randomValue,
            incrementalValue,
            wordsValues
       }
        public class FieldInfo
        {
            public required string FieldName { get; set; }
            public required string FieldType { get; set; }
            public string? DefaultValue { get; set; }
            public bool IsPrimaryKey { get; set; }
            public string? Comment { get; set; }
            public bool IsNullable { get; set; }
            public bool IsAutoIncrement { get; set; }
            public SimulateType SimuType { get; set; }
        }

        public List<FieldInfo> ExtractFields(string createTableSql)
        {
            createTableSql = ShrinkSql(createTableSql);
            List<FieldInfo> fields = [];
            var sql = SplitSQL(createTableSql);

            for (int i = 0; i < sql.Length; i++)
            {
                if (sql[i].Length == 0) continue;
                var sqlArray = sql[i].Split(" ");
                Console.WriteLine(sqlArray.Length);
                FieldInfo field = new()
                {
                    FieldName = sqlArray[0],
                    FieldType = sqlArray[1],
                    DefaultValue = PraseDefaultValue(sql[i]),
                    Comment = PraseComment(sql[i]),
                    IsPrimaryKey = PraseIsPrimaryKey(sql[i]),
                    IsNullable = PraseIsNullable(sql[i]),
                    IsAutoIncrement = PraseIsAutoIncrement(sql[i])
                };

                fields.Add(field);
            }
            return fields;
        }

        private static string ShrinkSql(string sql)
        {
            sql = sql.Replace("\r", "").Replace("\n", "");
            return Regex.Replace(sql, @"\s+", " ");
        }
        private static string[] SplitSQL(string sql)
        {
            sql = sql.Substring(sql.IndexOf("(") + 1, sql.LastIndexOf(")") - sql.IndexOf("(") - 1);

            return sql.Split(",");
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
    }
}