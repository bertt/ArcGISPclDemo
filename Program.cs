using System;
using System.Linq;
using System.Threading.Tasks;
using ArcGIS.ServiceModel;
using ArcGIS.ServiceModel.Common;

namespace ArcGISPclDemo
{
    class Program
    {
        static void Main()
        {
            MainAsync().Wait();
        }
        
        static async Task MainAsync()
        {
            var baseUrl = "http://arcgis.geodan.nl:6080/arcgis/";
            var arcGISServerEndPoint = "/public/test_AGF/FeatureServer/4";

            // initialize
            ArcGIS.ServiceModel.Serializers.JsonDotNetSerializer.Init();

            // get something from featureserver
            var resFeatureServer = await FeatureServiceEditor.GetFeatures<Point>(baseUrl, arcGISServerEndPoint);
            Console.WriteLine("Number of features from Esri featureserver: {0}", resFeatureServer.ToList().Count);
            var addedFeatureId = addPointSample(baseUrl,arcGISServerEndPoint).Result;
            Console.WriteLine("Added point: " + addedFeatureId);
            // now delete point
            var res = await FeatureServiceEditor.DeleteFeature<Point>(baseUrl, arcGISServerEndPoint, addedFeatureId);
            Console.WriteLine("Feature deleted: " + res.ObjectId);
            Console.ReadLine();
        }

        private static async Task<long> addPointSample(string baseUrl, string arcGISServerEndPoint)
        {
            // add a point feature
            var feature = new Feature<Point>();
            feature.Attributes.Add("objectid_1", 0);
            feature.Attributes.Add("objectid", 0);
            feature.Attributes.Add("o_watergan", " ");
            feature.Attributes.Add("symbologie", " ");
            feature.Attributes.Add("angle", 0);
            feature.Attributes.Add("sysangle",0);

            feature.Geometry = new Point { X = 165282.05719999969, Y = 501225.37609999999, SpatialReference = new SpatialReference { Wkid = 28992 } };

            try
            {
                var result = await FeatureServiceEditor.AddFeature(baseUrl, arcGISServerEndPoint, feature);
                var objectid = result.ObjectId;
                return objectid;
            }
            catch (ArcGISServerException arcGISServerException)
            {
                Console.Write("Error message " + arcGISServerException.Message);
                Console.Write("Error code: " + arcGISServerException.ArcGISError.Code);
            }
            return 0;

        }

    }
}


