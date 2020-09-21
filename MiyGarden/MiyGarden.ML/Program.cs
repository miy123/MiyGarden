using MiyGarden.Service.ML;

namespace MiyGarden.ML
{
    static class Program
    {
        static void Main(string[] args)
        {
            var mlTest = new MLTest();

            //mlTest.InitIris();
            mlTest.BinaryClassification();
        }
    }
}
