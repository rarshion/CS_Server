using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MultiSpel.DataBaseModule.Model;
using EasyDBUtility;

namespace MultiSpel.DataBaseModule.SqlServerDAL
{
    public class ImageDAL
    {
        private const string Insert_SQL = "insert into [Image] ([nodeid],[datetime],[status],[path],[fullpath],[filename]) values (@nodeid,@datetime,@status,@path,@fullpath,@filename)";
        private const string Select_By_LastTime_SQL = "select top 1 * from [Image] where nodeid = @nodeid order by [datetime] desc";

        public bool Insert(ImageData data)
        {
            SqlHelper helper = new SqlHelper();
            helper.CreateCommand(Insert_SQL);
            helper.AddParameter(data);
            return helper.ExecuteNonQuery() > 0 ? true : false;
        }

        public ImageData GetImageByTime(int cond)
        {
            SqlHelper helper = new SqlHelper();
            helper.CreateCommand(Select_By_LastTime_SQL);
            helper.AddParameter("@nodeid",cond);
            ImageData imageData = helper.ExecuteReaderSingle<ImageData>();
            return imageData;
        }

    }
}
