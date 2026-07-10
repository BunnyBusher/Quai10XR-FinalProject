using Foundation.Runtime;

namespace Debug.Runtime
{
    public class DebugConsole : FBehaviour
    {
        private void Start()
        {
            SetFact("TestDebug","This a fact set at Start");
        }
    }
}
