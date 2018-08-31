/*
 * 程序中文名称: 将博内容管理系统企业版
 * 
 * 程序英文名称: JumboECMS
 * 
 * 程序版本: 1.4.x
 * 
 * 程序作者: 将博
 * 
 * 官方网站: http://www.jumboecms.net/
 * 
 */

using System;
using System.Data;

namespace JumboECMS.DBUtility
{
    /// <summary>
    /// 本对象用与提供对OLEDB数据库的常用访问操作。
    /// </summary>
    public class OleDbOperHandler : DbOperHandler
    {
        /// <summary>
        /// 构造函数，接收一个OLEDB数据库连接对象OleDbConnection
        /// </summary>
        /// <param name="_conn"></param>
        public OleDbOperHandler(System.Data.OleDb.OleDbConnection _conn)
        {
            conn = _conn;
            dbType = DatabaseType.OleDb;
            conn.Open();
            cmd = conn.CreateCommand();
            da = new System.Data.OleDb.OleDbDataAdapter();
        }

        /// <summary>
        /// 构造函数，接收一个mdb文件
        /// </summary>
        /// <param name="path"></param>
        public OleDbOperHandler(string path)
        {
            conn = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path);
            dbType = DatabaseType.OleDb;
            conn.Open();
            cmd = conn.CreateCommand();
            da = new System.Data.OleDb.OleDbDataAdapter();
        }
        /// <summary>
        /// 产生OleDbCommand对象所需的查询参数。
        /// </summary>
        protected override void GenParameters()
        {
            System.Data.OleDb.OleDbCommand oleCmd = (System.Data.OleDb.OleDbCommand)cmd;
            if (this.alFieldItems.Count > 0)
            {
                for (int i = 0; i < alFieldItems.Count; i++)
                {
                    oleCmd.Parameters.AddWithValue("@para" + i.ToString(), ((DbKeyItem)alFieldItems[i]).fieldValue.ToString());
                }
            }

            if (this.alSqlCmdParameters.Count > 0)
            {
                for (int i = 0; i < this.alSqlCmdParameters.Count; i++)
                {
                    oleCmd.Parameters.AddWithValue("@spara" + i.ToString(), ((DbKeyItem)alSqlCmdParameters[i]).fieldValue.ToString());
                }
            }
            if (this.alConditionParameters.Count > 0)
            {
                for (int i = 0; i < this.alConditionParameters.Count; i++)
                {
                    oleCmd.Parameters.AddWithValue("@cpara" + i.ToString(), ((DbKeyItem)alConditionParameters[i]).fieldValue.ToString());
                }
            }
        }

    }
}
