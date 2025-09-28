using System;
using LifecycleHandler;
using UnityEngine;

namespace _Example
{
    internal sealed class ExampleApplication : MonoBehaviour, IViewControllerLifecycleListener
    {
        private IDisposable _lifecycleHandler;

        private void Awake()
        {
            _lifecycleHandler = ViewControllerLifecycleHandlerFactory.Create(this);
        }

        private void OnDestroy()
        {
            _lifecycleHandler?.Dispose();
        }

        public void OnViewWillLayoutSubviewsCallbacks()
        {
        }

        public void OnViewDidLayoutSubviewsCallbacks()
        {
        }

        public void OnViewWillDisappearCallbacks(bool animated)
        {
        }

        public void OnViewDidDisappearCallbacks(bool animated)
        {
        }

        public void OnViewWillAppearCallbacks(bool animated)
        {
        }

        public void OnViewDidAppearCallbacks(bool animated)
        {
        }

        public void OnInterfaceWillChangeOrientationCallbacks()
        {
        }

        public void OnInterfaceDidChangeOrientationCallbacks()
        {
        }
    }
}
