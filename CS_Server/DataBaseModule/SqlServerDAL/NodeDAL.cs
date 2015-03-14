using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MultiSpel.DataBaseModule.Model;
using EasyDBUtility;


namespace MultiSpel.DataBaseModule.SqlServerDAL
{
    public class NodeDAL
    {
        private const string Insert_SQL = "insert into [Node] ([name],[status],[longtitude],[lantitude],[location],[savepath]) values (@name,@status,@longtitude,@lantitude,@location,@savepath)";
        //private const string Delete_By_ID_SQL = "delete from [user] where id=@id";
        //private const string BatchDelete_By_ID_SQL = "delete from [user] where id in({0})";
        private const string Update_By_ID_SQL = "update [Node] set [name]=@name,[status]=@status,[longtitude]=@longtitude,[lantitude]=@lantitude,[location]=@location,[savepath]=@savepath where id=@id";
        private const string Select_Count_SQL = "select count(*) from [Node]";
        private const string Select_By_ID_SQL = "select * from [Node] where id=@id";
        private const string SelectAll_By_SQL = "select * from [Node]";
        private const string Select_By_Location_SQL = "select * from [Node] where location=@location";
        private const string Select_By_LongLan_SQL = "select * from [Node] where longtitude=@long and lantitude=@lan";
        //private const string Select_By_UN_SQL = "select * from [user] where username=@userName";


        /// <summary>
        /// 新增一条数据记录
        /// </summary>
        /// <param name="data">实体对象</param>
        /// <returns>是否添加成功</returns>
        public bool Insert(NodeData data)
        {
            SqlHelper helper = new SqlHelper();
            helper.CreateCommand(Insert_SQL);
            helper.AddParameter(data);
            return helper.ExecuteNonQuery() > 0 ? true : false;
        }

        ///// <summary>
        ///// 根据主键值删除一条数据记录
        ///// </summary>
        ///// <param name="id">主键值</param>
        ///// <returns>是否删除成功</returns>
        //public bool DeleteByPK(int id)
        //{
        //    SqlHelper helper = new SqlHelper();
        //    helper.CreateCommand(Delete_By_ID_SQL);
        //    helper.AddParameter("@id", id);
        //    return helper.ExecuteNonQuery() > 0 ? true : false;
        //}

        ///// <summary>
        ///// 根据主键值批量删除数据记录
        ///// </summary>
        ///// <param name="id">主键值</param>
        ///// <returns>是否删除成功</returns>
        //public bool DeleteByPK(int[] id)
        //{
        //    bool success = false;
        //    string strPKValue = string.Empty;
        //    foreach (int id0 in id)
        //    {
        //        strPKValue += id0 + ",";
        //    }
        //    SqlHelper helper = new SqlHelper();
        //    helper.BeginTransaction();
        //    try
        //    {
        //        helper.CreateCommand(string.Format(BatchDelete_By_ID_SQL, strPKValue.TrimEnd(',')));
        //        helper.ExecuteNonQuery();
        //        helper.Commit();
        //        success = true;
        //    }
        //    catch
        //    {
        //        helper.RollBack();
        //        success = false;
        //    }
        //    return success;
        //}

        /// <summary>
        /// 根据主键值修改一条数据记录
        /// </summary>
        /// <param name="data">实体对象</param>
        /// <returns>是否修改成功</returns>
        public bool UpdateByPK(NodeData data)
        {
            SqlHelper helper = new SqlHelper();
            helper.CreateCommand(Update_By_ID_SQL);
            helper.AddParameter(data);
            return helper.ExecuteNonQuery() > 0 ? true : false;
        }

        /// <summary>
        /// 根据主键值获取一条数据记录详细信息
        /// </summary>
        /// <param name="id">主键值</param>
        /// <returns>查询的实体对象</returns>
        public NodeData GetDetailByPK(int id)
        {
            SqlHelper helper = new SqlHelper();
            helper.CreateCommand(Select_By_ID_SQL);
            helper.AddParameter("@id", id);
            return helper.ExecuteReaderSingle<NodeData>();
        }

        public NodeData GetDetailByPoint(double longtitude, double lantitude)
        {
            SqlHelper helper = new SqlHelper();
            helper.CreateCommand(Select_By_LongLan_SQL);
            helper.AddParameter("@long", longtitude);
            helper.AddParameter("@lan", lantitude);
            return helper.ExecuteReaderSingle<NodeData>();
        }

        public NodeData GetDetailByLocation(string location)
        {
            SqlHelper helper = new SqlHelper();
            helper.CreateCommand(Select_By_Location_SQL);
            helper.AddParameter("@location", location);
            return helper.ExecuteReaderSingle<NodeData>();
        }
      
        public List<NodeData> GetAll()
        {
            SqlHelper helper = new SqlHelper();
            helper.CreateCommand(SelectAll_By_SQL);
            List<NodeData> list = helper.ExecuteReader<NodeData>();
            return list;
        }

        public int GetCount()
        {
            SqlHelper helper = new SqlHelper();
            helper.CreateCommand(Select_Count_SQL);
            return helper.ExecuteNonQuery();
        }

        //public NodeData GetDetailByUN(string username)
        //{
        //    SqlHelper helper = new SqlHelper();
        //    helper.CreateCommand(Select_By_UN_SQL);
        //    helper.AddParameter("@userName", username);
        //    return helper.ExecuteReaderSingle<NodeData>();
        //}
    }
}
