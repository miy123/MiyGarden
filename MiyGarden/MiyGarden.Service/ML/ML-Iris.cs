using Microsoft.ML;
using MiyGarden.Service.ML.Models;
using System;
using System.IO;

namespace MiyGarden.Service.ML
{
    public partial class MLTest
    {
        /// <summary>
        /// 分類鳶尾花(K-means)
        /// </summary>
        public void InitIris()
        {
            IrisData Setosa = new IrisData
            {
                SepalLength = 5.1f,
                SepalWidth = 3.5f,
                PetalLength = 1.4f,
                PetalWidth = 0.2f
            };

            var mlContext = new MLContext(seed: 0);

            string dataPath = AppDomain.CurrentDomain.BaseDirectory + "/MLData/iris-data.txt";
            IDataView dataView = mlContext.Data.LoadFromTextFile<IrisData>(dataPath, hasHeader: false, separatorChar: ',');

            string featuresColumnName = "Features";
            var pipeline = mlContext.Transforms
                .Concatenate(featuresColumnName, "SepalLength", "SepalWidth", "PetalLength", "PetalWidth")
                .Append(mlContext.Clustering.Trainers.KMeans(featuresColumnName, numberOfClusters: 3));

            ITransformer model = pipeline.Fit(dataView);

            var modelZipUri = AppDomain.CurrentDomain.BaseDirectory + "/MLModel/iris.zip";
            using (var fileStream = new FileStream(modelZipUri, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                mlContext.Model.Save(model, dataView.Schema, fileStream);
            }

            ITransformer transformer;

            // Load model.
            using (var file = File.OpenRead(modelZipUri))
                transformer = mlContext.Model.Load(file, out DataViewSchema schema);

            // var result = model.Transform(mlContext.Data.LoadFromEnumerable(new List<IrisData>() { Setosa }));
            var predictor = mlContext.Model.CreatePredictionEngine<IrisData, ClusterPrediction>(transformer);
            var prediction = predictor.Predict(Setosa);
            Console.WriteLine($"Cluster: {prediction.PredictedClusterId}");
            Console.WriteLine($"Distances: {string.Join(" ", prediction.Distances)}");
        }
    }
}
