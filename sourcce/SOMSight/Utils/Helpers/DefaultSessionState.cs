using ISOMSightServices;
using System.Web;

namespace SOMSight.Utils
{
    public class DefaultSessionState : ISessionState
    {

        private readonly HttpSessionStateBase _session;

        public DefaultSessionState(HttpSessionStateBase pSession)
        {
            _session = pSession;
        }

        public void Clear()
        {
            _session.RemoveAll();
        }

        public void Delete(string pKey)
        {
            _session.Remove(pKey);
        }

        public object Get(string pKey)
        {
            return _session[pKey];
        }

        public T Get<T>(string pKey) where T : class
        {
            return _session[pKey] as T;
        }

        public ISessionState Store(string pKey, object value)
        {
            _session[pKey] = value;

            return this;
        }
    }
}