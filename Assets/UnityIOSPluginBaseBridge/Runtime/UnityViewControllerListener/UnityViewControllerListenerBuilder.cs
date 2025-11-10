using System;
using UnityIOSPluginBaseBridge;

namespace LifecycleHandler
{
    public static class UnityViewControllerListenerBuilder
    {
        public static IDisposable Build(IUnityViewControllerListener listener)
        {
#if UNITY_IOS && !UNITY_EDITOR
            var instance = new UnityViewControllerListenerBridge(listener);
#else
            var instance = new DummyBridge();
#endif
            return instance;
        }
    }

    internal sealed class DummyBridge : IDisposable
    {
        public void Dispose()
        {
        }
    }
}
