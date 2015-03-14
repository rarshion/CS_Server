using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MultiSpel.DataBaseModule.SqlServerDAL;
using MultiSpel.DataBaseModule.Model;

namespace MultiSpel.DataBaseModule.BLL
{
    public class NodeBLL
    {
        private static readonly NodeDAL dal = new NodeDAL();

        public bool Insert(NodeData data)
        {
            return dal.Insert(data);
        }

        //public bool DeleteByPK(int id)
        //{
        //    return dal.DeleteByPK(id);
        //}

        //public bool DeleteByPK(int[] id)
        //{
        //    return dal.DeleteByPK(id);
        //}

        public bool UpdateByPK(NodeData data)
        {
            return dal.UpdateByPK(data);
        }

        //public bool UpdateByPK(int id)
        //{
        //    return 
        //}

        public NodeData GetDetailByPK(int id)
        {
            return dal.GetDetailByPK(id);
        }

        public NodeData GetDetailByPoint(double longtitude, double lantitude)
        {
            return dal.GetDetailByPoint(longtitude, lantitude);
        }

        public NodeData GetDetailByLocation(string location)
        {
            return dal.GetDetailByLocation(location);
        }

        public List<NodeData> GetAllNodes()
        {
            return dal.GetAll();
        }

        public int GetAllCount()
        {
            return dal.GetCount();
        }
    }
}
