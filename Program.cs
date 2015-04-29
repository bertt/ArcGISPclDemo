using System;
using ArcGIS.ServiceModel.Common;

namespace ArcGISPclDemo
{
    class Program
    {
        static void Main()
        {

            //var baseUrl = "http://sampleserver3.arcgisonline.com/ArcGIS/";
            //var arcGisServerEndPoint = "/Earthquakes/EarthquakesFromLastSevenDays/FeatureServer/0";
            var baseUrl = "http://arcgis.geodan.nl:6080/arcgis/";
            var arcGisServerEndPoint = "/public/test_AGF/FeatureServer/4";

            // initialize
            ArcGIS.ServiceModel.Serializers.JsonDotNetSerializer.Init();

            // get something from featureserver
            var resFeatureServer = PclTester.GetFeaturesFromFeatureServer(baseUrl, arcGisServerEndPoint);
            Console.WriteLine("Number of features from Esri featureserver: {0}", resFeatureServer.Result);

            // add a feature
            var feature = new Feature<Point>();
            feature.Attributes.Add("objectid_1",0);
            feature.Attributes.Add("objectid", 0);
            feature.Attributes.Add("o_watergan"," ");
            feature.Attributes.Add("symbologie"," ");
            // oops we miss something here to raise error...
            // feature.Attributes.Add("angle",0);
            feature.Attributes.Add("sysangle",0);

            feature.Geometry = new Point { X = 165282.05719999969, Y = 501225.37609999999, SpatialReference = new SpatialReference { Wkid = 28992 } };

            try
            {
                var res = PclTester.AddPoint(baseUrl, arcGisServerEndPoint, feature).Result;
                Console.WriteLine("Added feature to Esri featureserver: {0}", res);
            }
            catch (AggregateException ag)
            {
                var inner = ag.InnerException;
                Console.Write("Error message " + inner.Message);
                Console.Write("Error code: " + ((ArcGISServerException)inner).ArcGISError.Code);
            }

            Console.ReadKey();
        }

    }
}


