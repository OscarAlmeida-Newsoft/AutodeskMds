using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SharedEntities;

namespace WCFSharedServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMeasureMyPlataformServices" in both code and config file together.
    [ServiceContract]
    public interface IMeasureMyPlataformServices
    {
        [OperationContract]
        TokenResponse GetUrlRedirectMeasureMyPlataform(UserSAMLive user);
    }
}
