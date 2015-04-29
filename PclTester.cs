using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArcGIS.ServiceModel;
using ArcGIS.ServiceModel.Common;
using ArcGIS.ServiceModel.Operation;

namespace ArcGISPclDemo
{
    public class PclTester
    {
        public static async Task<int> GetFeaturesFromMapServer()
        {
            var gateway = new PortalGateway("http://sampleserver3.arcgisonline.com/ArcGIS/");
            var queryPoint = new Query("Earthquakes/EarthquakesFromLastSevenDays/MapServer/0".AsEndpoint());
            var resultPoints = await gateway.Query<Point>(queryPoint);
            return resultPoints.Features.Count();
        }


        public static async Task<int> GetFeaturesFromFeatureServer(string baseUrl, string arcGISServerEndPoint)
        {
            var gateway = new PortalGateway(baseUrl);
            var queryPoint = new Query(arcGISServerEndPoint.AsEndpoint());
            var resultPoints = await gateway.Query<Point>(queryPoint);
            return resultPoints.Features.Count();
        }

        public static async Task<int> AddPoint(string baseUrl, string arcGISServerEndPoint,Feature<Point> feature )
        {
            var gateway = new PortalGateway(baseUrl);
            var adds = new ApplyEdits<Point>(arcGISServerEndPoint.AsEndpoint())
            {
                Adds = new List<Feature<Point>> { feature }
            };
            var resultAdd = await gateway.ApplyEdits(adds);

            if (!resultAdd.Adds[0].Success)
            {
                // there was an error, throw exception
                throw new ArcGISServerException("ArcGIS server returns error.", resultAdd.Adds[0].Error);
            }

            return resultAdd.Adds.Count;
        }
    }
}
