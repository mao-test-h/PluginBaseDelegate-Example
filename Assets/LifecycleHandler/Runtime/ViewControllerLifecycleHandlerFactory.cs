using System;

namespace LifecycleHandler
{
    public static class ViewControllerLifecycleHandlerFactory
    {
        public static IDisposable Create(IViewControllerLifecycleListener lifecycleListener)
        {
#if UNITY_IOS && !UNITY_EDITOR
            var instance = new UnityViewControllerLifecycleHandler(lifecycleListener);
#else
            var instance = new DummyHandler();
#endif
            return instance;
        }
    }

    internal sealed class DummyHandler : IDisposable
    {
        public void Dispose()
        {
        }
    }
}
