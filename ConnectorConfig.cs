using System;
using Microsoft.Extensions.Configuration;

namespace com.businesscentral
{

    public partial class ConnectorConfig
    {
        public ConnectorConfig(IConfigurationRoot config)
        {
            if (config != null)
            {
                tenant = config["tenant"];
                companyID = config["companyID"];
                apiVersion1 = config["apiVersion1"];
                apiVersion2 = config["apiVersion2"];
                authInfo = config["authInfo"];

            }
            // If you are customizing here it means you
            //  should give a look on how use
            //  azure configuration file
            if (String.IsNullOrEmpty(tenant))
                tenant = "xxxxxxxx-xxxx-xxxx-xxxx-xxxxdxxxxxxx/Sandbox";
            if (String.IsNullOrEmpty(companyID))
                companyID = "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx";
            if (String.IsNullOrEmpty(apiVersion1))
                apiVersion1 = "v2.0";
            if (String.IsNullOrEmpty(apiVersion2))
                apiVersion2 = "v1.0";
            if (String.IsNullOrEmpty(authInfo))
                authInfo = "your_username:yout_web_service_access_key";
        }

        public String tenant;
        public String companyID;
        public String apiVersion1;
        public String apiVersion2;
        public String authInfo;

    }
}
