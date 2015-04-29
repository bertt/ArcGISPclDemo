using System;
using ArcGIS.ServiceModel.Operation;

namespace ArcGISPclDemo
{
    public class ArcGISServerException:ApplicationException
    {
        private ArcGISError _error;
        public ArcGISServerException(string message,ArcGISError error): base(message)
        {
            _error = error;
        }

        public ArcGISError ArcGISError
        {
            get { return _error; }
        }
    }
}
