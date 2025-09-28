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
            Debug.Log("[ExampleApplication] OnViewWillLayoutSubviews");
        }

        public void OnViewDidLayoutSubviewsCallbacks()
        {
            Debug.Log("[ExampleApplication] OnViewDidLayoutSubviews");
        }

        public void OnViewWillDisappearCallbacks(bool animated)
        {
            Debug.Log($"[ExampleApplication] OnViewWillDisappear (animated: {animated})");
        }

        public void OnViewDidDisappearCallbacks(bool animated)
        {
            Debug.Log($"[ExampleApplication] OnViewDidDisappear (animated: {animated})");
        }

        public void OnViewWillAppearCallbacks(bool animated)
        {
            Debug.Log($"[ExampleApplication] OnViewWillAppear (animated: {animated})");
        }

        public void OnViewDidAppearCallbacks(bool animated)
        {
            Debug.Log($"[ExampleApplication] OnViewDidAppear (animated: {animated})");
        }

        public void OnInterfaceWillChangeOrientationCallbacks()
        {
            Debug.Log("[ExampleApplication] OnInterfaceWillChangeOrientation");
        }

        public void OnInterfaceDidChangeOrientationCallbacks()
        {
            Debug.Log("[ExampleApplication] OnInterfaceDidChangeOrientation");
        }
    }
}
