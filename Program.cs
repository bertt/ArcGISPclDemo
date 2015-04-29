using System;
using System.Threading.Tasks;
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
            //var baseUrl = "http://sampleserver3.arcgisonline.com/ArcGIS/";
            //var arcGisServerEndPoint = "/Earthquakes/EarthquakesFromLastSevenDays/FeatureServer/0";
            var baseUrl = "http://arcgis.geodan.nl:6080/arcgis/";
            var arcGISServerEndPoint = "/public/test_AGF/FeatureServer/4";

            // initialize
            ArcGIS.ServiceModel.Serializers.JsonDotNetSerializer.Init();

            // get something from featureserver
            var resFeatureServer = PclTester.GetFeaturesFromFeatureServer(baseUrl, arcGISServerEndPoint);
            Console.WriteLine("Number of features from Esri featureserver: {0}", resFeatureServer.Result);
            addPointSample(baseUrl,arcGISServerEndPoint);

            Console.ReadKey();
        }


        private static async void addPointSample(string baseUrl, string arcGISServerEndPoint)
        {
            // add a point feature
            var feature = new Feature<Point>();
            feature.Attributes.Add("objectid_1", 0);
            feature.Attributes.Add("objectid", 0);
            feature.Attributes.Add("o_watergan", " ");
            feature.Attributes.Add("symbologie", " ");
            // oops we miss something here to raise error...
            feature.Attributes.Add("angle", 0);
            // feature.Attributes.Add("sysangle",0);

            feature.Geometry = new Point { X = 165282.05719999969, Y = 501225.37609999999, SpatialReference = new SpatialReference { Wkid = 28992 } };

            try
            {
                var result = await PclTester.AddPoint(baseUrl, arcGISServerEndPoint, feature);
                Console.WriteLine("Added feature to Esri featureserver: {0}", result);
            }
            catch (ArcGISServerException arcGISServerException)
            {
                Console.Write("Error message " + arcGISServerException.Message);
                Console.Write("Error code: " + arcGISServerException.ArcGISError.Code);
            }
            
        }

    }
}


