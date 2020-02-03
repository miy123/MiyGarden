//using MiyGarden.Models.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace MiyGarden.Service.Linq
//{
//    public class EFTest
//    {
//        private CommonDbContext _db;
//        public EFTest()
//        {
//            _db = CommonDbContext.Create();
//        }

//        public Result CreateTest(int count)
//        {
//            Result result = new Result(false);
//            //_db.Configuration.AutoDetectChangesEnabled = false;
//            const string characters = "0123456789qazwsxedcrfvtgbyhnujmikolp";
//            var random = new Random();
//            List<Performance> performanceList = new List<Performance>();
//            for (int i = 0; i < count; i++)
//            {
//                performanceList.Add(new Performance
//                {
//                    Name = i.ToString(),
//                    NickName = new string(Enumerable.Repeat(characters, 10).Select(x => x[random.Next(x.Length)]).ToArray())
//                });
//            }
//            try
//            {
//                this._db.Performance.AddRange(performanceList);
//                this._db.SaveChanges();
//                result.Success = true;
//            }
//            catch (Exception ex)
//            {
//                result.Exception = ex;
//                result.Message = ex.Message;
//            }
//            finally
//            {
//                //this._db.Configuration.AutoDetectChangesEnabled = true;
//            }
//            return result;
//        }
//    }
//}
