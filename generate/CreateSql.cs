using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace createSQLLIB.generate
{
    internal interface CreateSql
    {
        /* Todo:
         * 1. 返回一个建好的sql语句
         * 2. 解析写好的sql语句，获取里面的字段名和字段值
         * 3. 生成一个插入sql语句
         * 4. 模拟各类数据
        */

        public string GenerateInsertSQL();
        public string GenerateUpdateSQL();
        public string SimulateData()
        { 
            // 再createData.cs中实现
            return "";
        }
        public string GenerateDeleteSQL();

    }
}
