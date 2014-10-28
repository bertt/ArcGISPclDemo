using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArcGIS.ServiceModel;
using ArcGIS.ServiceModel.Common;
using ArcGIS.ServiceModel.Operation;

namespace ArcGISPclDemo
{
    class Program
    {
        static void Main()
        {
            // initialize
            ArcGIS.ServiceModel.Serializers.JsonDotNetSerializer.Init();

            // get something from mapserver
            var resMapServer = PclTester.GetEarthQuakesFromMapServer();
            Console.WriteLine("Earthquakes from Esri mapserver: {0}", resMapServer.Result);

            // get something from featureserver
            var resFeatureServer = PclTester.GetEarthQuakesFromFeatureServer();
            Console.WriteLine("Earthquakes from Esri featureserver: {0}", resFeatureServer.Result);

            // add an earthquake
            var res = PclTester.AddEarthquake();
            Console.WriteLine("Added earthquakes to Esri featureserver: {0}", res.Result);


            Console.ReadKey();
        }

    }

    public class PclTester
    {

        public static async Task<int> GetEarthQuakesFromMapServer()
        {
            var gateway = new PortalGateway("http://sampleserver3.arcgisonline.com/ArcGIS/");
            var queryPoint = new Query("Earthquakes/EarthquakesFromLastSevenDays/MapServer/0".AsEndpoint());
            var resultPoints = await gateway.Query<Point>(queryPoint);
            return resultPoints.Features.Count();
        }


        public static async Task<int> GetEarthQuakesFromFeatureServer()
        {
            var gateway = new PortalGateway("http://sampleserver3.arcgisonline.com/ArcGIS/");
            var queryPoint = new Query("/Earthquakes/EarthquakesFromLastSevenDays/FeatureServer/0".AsEndpoint());
            var resultPoints = await gateway.Query<Point>(queryPoint);
            return resultPoints.Features.Count();
        }



        public static async Task<int> AddEarthquake()
        {
            var gateway = new PortalGateway("http://sampleserver3.arcgisonline.com/ArcGIS/");
            var feature = new Feature<Point>();
            feature.Attributes.Add("type", 0);
            feature.Geometry = new Point { SpatialReference = new SpatialReference { Wkid = SpatialReference.WGS84.Wkid }, X = 5, Y = 53 };

            var adds = new ApplyEdits<Point>(@"/Earthquakes/EarthquakesFromLastSevenDays/FeatureServer/0".AsEndpoint())
            {
                Adds = new List<Feature<Point>> { feature }
            };
            var resultAdd = await gateway.ApplyEdits(adds);

            return resultAdd.Adds.Count;
        }
    }
}
