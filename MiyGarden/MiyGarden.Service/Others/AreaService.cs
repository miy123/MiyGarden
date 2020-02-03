using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Data.Entity.Spatial;
//using Microsoft.SqlServer.Types;
using System.Data.SqlTypes;

namespace MiyGarden.Service.Others
{
    //public class AreaService
    //{
    //    public List<T> GetAppAreaInfo<T>()
    //        where T : class
    //    {
    //        var result = new List<T>();
    //        var listArea = GetMainAreaData().Where(x => x.Status == "Y").ToList();
    //        if (typeof(T) == typeof(AreaLayerInfoModel))
    //        {
    //            var dbResult = GetAreaData();
    //            listArea.ForEach(area =>
    //            {
    //                var groupData = dbResult.Where(ar => ar.AreaGroupID == area.MainID && ar.Status == "Y");
    //                AreaLayerInfoModel a = new AreaLayerInfoModel
    //                {
    //                    ID = area.MainID,
    //                    Organ = area.Organ,
    //                    AreaLevel = area.AreaLevel,
    //                    //AreaStatus = areaLimitList.Where(d => d.Id == areaLimitList.Where(ar => ar.Value == area.AreaStatus).FirstOrDefault().ParentId).FirstOrDefault().Value,
    //                    Description = area.Description,
    //                    ListArea = GetAreaBoundary(groupData),
    //                    Affiliation = area.Affiliation,
    //                    VersionNo = area.VersionNo.Value,
    //                    AreaTitle = area.AreaTitle,
    //                    CenterPoint = "121.21,24.66",
    //                    Remark = ""
    //                };
    //                result.Add(a as T);
    //            });
    //        }
    //        return result;
    //    }

    //    private List<AreaBoundary> GetAreaBoundary(IEnumerable<DAM_Area> groupData)
    //    {
    //        var result = new List<AreaBoundary>();
    //        //找出外框
    //        var outer = groupData.Where(o => o.SourceID == 0);
    //        //計數器
    //        int count = 1;
    //        //跑過所有的外框
    //        foreach (var o in outer)
    //        {
    //            AreaBoundary outerBound = new AreaBoundary
    //            {
    //                No = count,
    //                OuterID = 0,
    //                Area = o.Area.AsText().Replace("))", "").Replace(", ", "*").Replace(" ", ",").Replace("*", " ").Replace(",((", "*").Replace("(", "").Split('*')[1]
    //            };
    //            //先加入外框
    //            result.Add(outerBound);
    //            //記得計數器要加
    //            count++;
    //            //找出此外框是否有內框
    //            var inner = groupData.Where(d => d.SourceID == o.ID).ToList();
    //            //先確定有資料
    //            if (inner.Count() > 0)
    //            {
    //                //總輸出inner結果
    //                List<DbGeometry> iResultList = new List<DbGeometry>();
    //                //當原本的inner裡面全remove掉才出去
    //                while (inner.Count() > 0)
    //                {
    //                    //是否要繼續往下執行（因為假如 1 3 重疊  2 3 重疊 => 要全放在一起，可是執行時 1 和 2 沒重疊，所以就忽略了）
    //                    bool isContinue = true;
    //                    for (int r = 0; r < iResultList.Count(); r++)
    //                    {
    //                        try
    //                        {
    //                            //有重疊的話
    //                            if (iResultList[r].Intersects(inner[0].Area))
    //                            {
    //                                iResultList[r] = iResultList[r].Union(inner[0].Area);
    //                                inner.Remove(inner[0]);
    //                                isContinue = false;
    //                                break;
    //                            }
    //                        }
    //                        catch
    //                        {
    //                            continue;
    //                        }
    //                    }
    //                    if (isContinue)
    //                    {
    //                        //要被remove掉的元素編號
    //                        List<DAM_Area> removeList = new List<DAM_Area>();
    //                        //inner聯集後的結果（都從第一個拿）
    //                        DbGeometry iResult = inner[0].Area;
    //                        //把第一個加入remove陣列中
    //                        removeList.Add(inner[0]);
    //                        if (inner.Count() > 1)
    //                        {
    //                            //跑過所有的內框
    //                            for (var k = 1; k < inner.Count(); k++)
    //                            {
    //                                try
    //                                {
    //                                    //如果有重疊才進行聯集
    //                                    if (iResult.Intersects(inner[k].Area))
    //                                    {
    //                                        iResult = iResult.Union(inner[k].Area);
    //                                        removeList.Add(inner[k]);
    //                                    }
    //                                }
    //                                catch
    //                                {
    //                                    continue;
    //                                }
    //                            }
    //                        }
    //                        //將結果加入總輸出結果中
    //                        iResultList.Add(iResult);
    //                        //把removelist的全部remove掉
    //                        foreach (var rl in removeList)
    //                        {
    //                            inner.Remove(rl);
    //                        }
    //                    }
    //                }
    //                //將所有各自重疊的部份聯集起來，剩下的就是沒交集的部份
    //                foreach (var d in iResultList)
    //                {
    //                    AreaBoundary innerBound = new AreaBoundary
    //                    {
    //                        No = count,
    //                        OuterID = outerBound.No,  //這裡拿的是外框的編號
    //                        Area = d.AsText().Replace("))", "").Replace(", ", "*").Replace(" ", ",").Replace("*", " ").Replace(",((", "*").Replace("(", "").Split('*')[1]
    //                    };
    //                    //加入至資料
    //                    result.Add(innerBound);
    //                    //記得要加計數器
    //                    count++;
    //                }
    //            }
    //        }
    //        return result;
    //    }

    //    public List<DAM_Area> GetAreaData()
    //    {
    //        var result = new List<DAM_Area>();
    //        var odata = ExcelToDS("C:/Users/Miy/Desktop/民航局/地圖限飛.xlsx");
    //        foreach (DataRow item in odata.Tables[0].Rows)
    //        {
    //            if (string.IsNullOrEmpty(item["ID"].ToString())) continue;
    //            var hex = item["Area"];
    //            ////byte[] bytes = Enumerable.Range(0, hex.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(hex.Substring(x, 2), 16)).ToArray();
    //            ////SqlGeometry geo = SqlGeometry.Deserialize(new System.Data.SqlTypes.SqlBytes(bytes));
    //            //var geo = DbGeometry.FromBinary((byte[])hex);
    //            //var ss = DbGeometry.FromBinary(Encoding.ASCII.GetBytes(hex.ToString()), 4326);
    //            var ss = SqlGeography.STGeomFromWKB(new SqlBytes(Encoding.ASCII.GetBytes(hex.ToString().Substring(2, hex.ToString().Length - 2))), 4326);
    //            var dd = DbGeometry.FromText("GEOMETRYCOLLECTION (POINT (4 0), LINESTRING (4 2, 5 3), POLYGON ((0 0, 3 0, 3 3, 0 3, 0 0), (1 1, 1 2, 2 2, 2 1, 1 1)))").AsText();
    //            result.Add(new DAM_Area
    //            {
    //                //Area = geo,
    //                ID = int.Parse(item["ID"].ToString()),
    //                AreaLevel = item["AreaLevel"] as string,
    //                AreaTitle = item["AreaTitle"] as string,
    //                Affiliation = item["Affiliation"] as string,
    //                SourceID = int.Parse(item["SourceID"].ToString()),
    //                AreaGroupID = int.Parse(item["AreaGroupID"].ToString())
    //            });
    //        }
    //        return result;
    //    }
    //    public List<DAM_MainArea> GetMainAreaData()
    //    {
    //        var result = new List<DAM_MainArea>();
    //        var odata = ExcelToDS("C:/Users/Miy/Desktop/民航局/DAM_MainArea.xlsx");
    //        foreach (DataRow item in odata.Tables[0].Rows)
    //        {
    //            if (string.IsNullOrEmpty(item["MainID"].ToString())) continue;
    //            result.Add(new DAM_MainArea
    //            {
    //                MainID = int.Parse(item["MainID"].ToString()),
    //                AreaLevel = item["AreaLevel"] as string,
    //                AreaTitle = item["AreaTitle"] as string,
    //                Affiliation = item["Affiliation"] as string,
    //                Organ = item["Organ"] as string,
    //                Prior = int.Parse(item["Prior"].ToString()),
    //                Status = item["Status"] as string,
    //                VersionNo = int.Parse(item["VersionNo"].ToString())
    //            });
    //        }
    //        return result;
    //    }

    //    public DataSet ExcelToDS(string Path)
    //    {
    //        string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + Path + ";" + "Extended Properties=Excel 8.0;";
    //        OleDbConnection conn = new OleDbConnection(strConn);
    //        conn.Open();
    //        DataTable schemaTable = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, null);
    //        var tableName = schemaTable.Rows[0][2].ToString().Trim();
    //        var strExcel = "select * from [" + tableName + "]";
    //        OleDbDataAdapter myCommand = new OleDbDataAdapter(strExcel, strConn);
    //        var ds = new DataSet();
    //        myCommand.Fill(ds, "table1");
    //        conn.Close();
    //        return ds;
    //    }
    //}
    public class DAM_Area
    {
        public int ID { get; set; }
        public string Organ { get; set; }
        public string AreaLevel { get; set; }
        public string AreaTitle { get; set; }
        public string AreaStatus { get; set; }
        //public DbGeometry Area { get; set; }
        public string Description { get; set; }
        public string Affiliation { get; set; }
        public string Status { get; set; }
        public int? VersionNo { get; set; }
        public DateTime? CreationTime { get; set; }
        public string CreatorUserId { get; set; }
        public DateTime? ModifictionTime { get; set; }
        public string LastModifiedUserId { get; set; }
        public int? AreaGroupID { get; set; }
        public int? SourceID { get; set; }
    }
    public class DAM_MainArea
    {
        public int MainID { get; set; }
        public string Organ { get; set; }
        public string AreaLevel { get; set; }
        public string AreaTitle { get; set; }
        public string AreaStatus { get; set; }
        public string Description { get; set; }
        public string Affiliation { get; set; }
        public Nullable<int> Prior { get; set; }
        public string Status { get; set; }
        public Nullable<int> VersionNo { get; set; }
        public Nullable<System.DateTime> CreationTime { get; set; }
        public string CreatorUserId { get; set; }
        public Nullable<System.DateTime> ModifictionTime { get; set; }
        public string LastModifiedUserId { get; set; }
        public Nullable<int> VersionNo1 { get; set; }
    }
    public class AreaLayerInfoModel
    {

        //建構子
        public AreaLayerInfoModel()
        {
            ListArea = new List<AreaBoundary>();
        }

        //id
        public int ID { get; set; }

        //機關
        public string Organ { get; set; }

        //層級
        public string AreaLevel { get; set; }

        //標題
        public string AreaTitle { get; set; }

        //狀態
        public string AreaStatus { get; set; }

        //區域座標（外框）
        public List<AreaBoundary> ListArea { get; set; }

        //描述
        public string Description { get; set; }

        //所屬縣市
        public string Affiliation { get; set; }

        //版本號
        public int VersionNo { get; set; }

        //罰款
        public string Fine { get; set; }

        //中心點
        public string CenterPoint { get; set; }

        //備註
        public string Remark { get; set; }
    }
    public class AreaBoundary
    {
        //編號（為了讓內框好查詢到外框）（從1號開始算）
        public int No { get; set; }

        //外框編號（如果是0代表自己是外框）
        public int OuterID { get; set; }

        //座標資料
        public string Area { get; set; }
    }
}
