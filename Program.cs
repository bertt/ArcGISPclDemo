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
}
