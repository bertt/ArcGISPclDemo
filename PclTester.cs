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


        public static async Task<int> GetFeaturesFromFeatureServer(string BaseUrl, string ArcGISServerEndPoint)
        {
            var gateway = new PortalGateway(BaseUrl);
            var queryPoint = new Query(ArcGISServerEndPoint.AsEndpoint());
            var resultPoints = await gateway.Query<Point>(queryPoint);
            return resultPoints.Features.Count();
        }

        public static async Task<int> AddFeature(string BaseUrl, string ArcGISServerEndPoint,Feature<Point> feature )
        {
            var gateway = new PortalGateway(BaseUrl);
            var adds = new ApplyEdits<Point>(ArcGISServerEndPoint.AsEndpoint())
            {
                Adds = new List<Feature<Point>> { feature }
            };
            var resultAdd = await gateway.ApplyEdits(adds);

            return resultAdd.Adds.Count;
        }
    }
}
