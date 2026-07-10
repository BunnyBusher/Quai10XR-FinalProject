using Foundation.Runtime;
using UnityEngine;

namespace Debug.Runtime
{
    public class DebugConsoleGet : FBehaviour
    {
        [ContextMenu("Write in Console")]
        public void GetTheFact()
        {
            string value = GetFact<string>("TestDebug");
            UnityEngine.Debug.Log(value);
        }
    }
}
