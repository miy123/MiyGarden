using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiyGarden.Service.Async
{
    public class YieldTest
    {
        public void Test1()
        {
            foreach (var model in GetModels())
            {
                DataModelHelper.ProcessPhase1(model);
                DataModelHelper.ProcessPhase2(model);
                DataModelHelper.ProcessPhase3(model);
            }
        }

        public void Test2()
        {
            foreach (var model in this.StreamProcessPhase3(this.StreamProcessPhase2(this.StreamProcessPhase1(GetModels())))) ;
        }

        private IEnumerable<DataModel> StreamProcessPhase1(IEnumerable<DataModel> models)
        {
            foreach (var model in models)
            {
                DataModelHelper.ProcessPhase1(model);
                yield return model;
            }
        }
        private IEnumerable<DataModel> StreamProcessPhase2(IEnumerable<DataModel> models)
        {
            foreach (var model in models)
            {
                DataModelHelper.ProcessPhase2(model);
                yield return model;
            }
        }
        private IEnumerable<DataModel> StreamProcessPhase3(IEnumerable<DataModel> models)
        {
            foreach (var model in models)
            {
                DataModelHelper.ProcessPhase3(model);
                yield return model;
            }
        }

        public void Test3()
        {
            foreach (var m in StreamAsyncProcessPhase3(StreamAsyncProcessPhase2(StreamAsyncProcessPhase1(GetModels())))) ;
        }

        private IEnumerable<DataModel> StreamAsyncProcessPhase1(IEnumerable<DataModel> models)
        {
            Task<DataModel> previous_result = null;
            foreach (var model in models)
            {
                if (previous_result != null) yield return previous_result.Result;
                previous_result = Task.Run<DataModel>(() => { DataModelHelper.ProcessPhase1(model); return model; });
            }
            if (previous_result != null) yield return previous_result.GetAwaiter().GetResult();
        }

        private IEnumerable<DataModel> StreamAsyncProcessPhase2(IEnumerable<DataModel> models)
        {
            Task<DataModel> previous_result = null;
            foreach (var model in models)
            {
                if (previous_result != null) yield return previous_result.Result;
                previous_result = Task.Run<DataModel>(() => { DataModelHelper.ProcessPhase2(model); return model; });
            }
            if (previous_result != null) yield return previous_result.GetAwaiter().GetResult();
        }

        private IEnumerable<DataModel> StreamAsyncProcessPhase3(IEnumerable<DataModel> models)
        {
            Task<DataModel> previous_result = null;
            foreach (var model in models)
            {
                if (previous_result != null) yield return previous_result.Result;
                previous_result = Task.Run<DataModel>(() => { DataModelHelper.ProcessPhase3(model); return model; });
            }
            if (previous_result != null) yield return previous_result.GetAwaiter().GetResult();
        }

        private IEnumerable<DataModel> GetModels()
        {
            Random rnd = new Random();
            byte[] allocate(int size)
            {
                byte[] buf = new byte[size];
                rnd.NextBytes(buf);
                return buf;
            }

            for (int seed = 1; seed <= 5; seed++)
            {
                yield return new DataModel()
                {
                    ID = $"DATA-{Guid.NewGuid():N}",
                    SerialNO = seed,
                    Buffer = allocate(1024)
                };
            }
        }

        public class DataModel
        {
            public string ID { get; set; }
            public int SerialNO { get; set; }
            public DataModelStageEnum Stage { get; set; } = DataModelStageEnum.INIT;
            public byte[] Buffer = null;
        }

        public enum DataModelStageEnum
        {
            INIT,
            PHASE1_COMPLETE,
            PHASE2_COMPLETE,
            PHASE3_COMPLETE
        }

        public static class DataModelHelper
        {
            public static bool ProcessPhase1(DataModel data)
            {
                if (data == null || data.Stage != DataModelStageEnum.INIT) return false;

                Console.Error.WriteLine($"[P1][{DateTime.Now}] data({data.SerialNO}) start...");
                Thread.Sleep(1000);
                data.Stage = DataModelStageEnum.PHASE1_COMPLETE;
                Console.Error.WriteLine($"[P1][{DateTime.Now}] data({data.SerialNO}) end...");

                return true;
            }
            public static bool ProcessPhase2(DataModel data)
            {
                if (data == null || data.Stage != DataModelStageEnum.PHASE1_COMPLETE) return false;

                Console.Error.WriteLine($"[P2][{DateTime.Now}] data({data.SerialNO}) start...");
                Thread.Sleep(1500);
                data.Stage = DataModelStageEnum.PHASE2_COMPLETE;
                Console.Error.WriteLine($"[P2][{DateTime.Now}] data({data.SerialNO}) end...");

                return true;
            }
            public static bool ProcessPhase3(DataModel data)
            {
                if (data == null || data.Stage != DataModelStageEnum.PHASE2_COMPLETE) return false;

                Console.Error.WriteLine($"[P3][{DateTime.Now}] data({data.SerialNO}) start...");
                Thread.Sleep(2000);
                data.Stage = DataModelStageEnum.PHASE3_COMPLETE;
                Console.Error.WriteLine($"[P3][{DateTime.Now}] data({data.SerialNO}) end...");

                return true;
            }
        }
    }
}
