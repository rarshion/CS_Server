using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MultiSpel.DataBaseModule.SqlServerDAL;
using MultiSpel.DataBaseModule.Model;

namespace MultiSpel.DataBaseModule.BLL
{
    public class ImageBLL
    {
        private static readonly ImageDAL dal = new ImageDAL();

        public bool Insert(ImageData data)
        {
            return dal.Insert(data);
        }

        //public ImageData GetDetailByPK(int id)
        //{
        //    return dal.GetDetailByPK(id);
        //}

        public ImageData GetDetailByLastTime(int nodeid)
        {
            return dal.GetImageByTime(nodeid);
        }

        //public List<ImageData> GetAllNodes()
        //{
        //    return dal.GetAll();
        //}

        //public int GetAllCount()
        //{
        //    return dal.GetCount();
        //}

    }
}
