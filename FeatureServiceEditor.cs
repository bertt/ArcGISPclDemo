﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ArcGIS.ServiceModel;
using ArcGIS.ServiceModel.Common;
using ArcGIS.ServiceModel.Operation;

namespace ArcGISPclDemo
{
    public class FeatureServiceEditor
    {
        public static async Task<IEnumerable<Feature<T>>> GetFeatures<T>(string baseUrl, string arcGISServerEndPoint) where T : IGeometry
        {
            var gateway = new PortalGateway(baseUrl);
            var queryPoint = new Query(arcGISServerEndPoint.AsEndpoint());
            var resultPoints = await gateway.Query<T>(queryPoint);
            return resultPoints.Features;
        }

        public static async Task<ApplyEditResponse> AddFeature<T>(string baseUrl, string arcGISServerEndPoint,Feature<T> feature) where T : IGeometry
        {
            var gateway = new PortalGateway(baseUrl);
            var adds = new ApplyEdits<T>(arcGISServerEndPoint.AsEndpoint())
            {
                Adds = new List<Feature<T>> { feature }
            };

            var resultAdd = await gateway.ApplyEdits(adds);

            if (!resultAdd.Adds[0].Success)
            {
                // there was an error, throw exception
                throw new ArcGISServerException("ArcGIS server returns error.", resultAdd.Adds[0].Error);
            }

            return resultAdd.Adds[0];
        }

        public static async Task<ApplyEditResponse> UpdateFeature<T>(string baseUrl, string arcGISServerEndPoint, Feature<T> feature) where T : IGeometry
        {
            var gateway = new PortalGateway(baseUrl);
            var updates = new ApplyEdits<T>(arcGISServerEndPoint.AsEndpoint())
            {
                Updates = new List<Feature<T>> { feature }
            };

            var resultUpdates = await gateway.ApplyEdits(updates);

            if (!resultUpdates.Updates[0].Success)
            {
                // there was an error, throw exception
                throw new ArcGISServerException("ArcGIS server returns error.", resultUpdates.Updates[0].Error);
            }

            return resultUpdates.Updates[0];
        }

        public static async Task<ApplyEditResponse> DeleteFeature<T>(string baseUrl, string arcGISServerEndPoint,
            long featureId) where T : IGeometry
        {
            var gateway = new PortalGateway(baseUrl);

            var deletes = new ApplyEdits<T>(arcGISServerEndPoint.AsEndpoint())
            {
                Deletes = new List<long> {featureId}
            };

            var resultUpdates = await gateway.ApplyEdits(deletes);

            if (!resultUpdates.Deletes[0].Success)
            {
                // there was an error, throw exception
                throw new ArcGISServerException("ArcGIS server returns error.", resultUpdates.Deletes[0].Error);
            }

            return resultUpdates.Deletes[0];
        }


    }
}
