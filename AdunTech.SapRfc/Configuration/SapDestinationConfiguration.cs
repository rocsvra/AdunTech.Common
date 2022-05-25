using SAP.Middleware.Connector;
using System;

namespace AdunTech.SapRfc
{
    class SapDestinationConfiguration : IDestinationConfiguration
    {
        private SapConnectionOptions options;

        private SapDestinationConfiguration() { }

        public SapDestinationConfiguration(SapConnectionOptions sapOptions)
        {
            options = sapOptions;
        }

        public RfcConfigParameters GetParameters(String destinationName)
        {
            RfcConfigParameters parms = new RfcConfigParameters();
            parms.Add(RfcConfigParameters.AppServerHost, options.AppServerHost);
            parms.Add(RfcConfigParameters.SystemNumber, options.SystemNumber);
            parms.Add(RfcConfigParameters.User, options.User);
            parms.Add(RfcConfigParameters.Password, options.Password);
            parms.Add(RfcConfigParameters.Client, options.Client);
            parms.Add(RfcConfigParameters.Language, options.Language);
            parms.Add(RfcConfigParameters.PoolSize, options.PoolSize);
            parms.Add(RfcConfigParameters.PeakConnectionsLimit, options.MaxPoolSize);
            parms.Add(RfcConfigParameters.ConnectionIdleTimeout, options.IdleTimeout);
            return parms;
        }

        public bool ChangeEventsSupported() => false;

        public event RfcDestinationManager.ConfigurationChangeHandler ConfigurationChanged;
    }
}
