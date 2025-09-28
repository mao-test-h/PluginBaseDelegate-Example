using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine.Assertions;

namespace LifecycleHandler
{
    internal sealed class UnityViewControllerLifecycleHandler : IDisposable
    {
        private static readonly Dictionary<IntPtr, Action> ViewWillLayoutSubviewsCallbacks = new();
        private static readonly Dictionary<IntPtr, Action> ViewDidLayoutSubviewsCallbacks = new();
        private static readonly Dictionary<IntPtr, Action> ViewWillDisappearCallbacks = new();
        private static readonly Dictionary<IntPtr, Action> ViewDidDisappearCallbacks = new();
        private static readonly Dictionary<IntPtr, Action> ViewWillAppearCallbacks = new();
        private static readonly Dictionary<IntPtr, Action> ViewDidAppearCallbacks = new();
        private static readonly Dictionary<IntPtr, Action> InterfaceWillChangeOrientationCallbacks = new();
        private static readonly Dictionary<IntPtr, Action> InterfaceDidChangeOrientationCallbacks = new();
        private bool _disposed;
        private IntPtr _ptr;

        private UnityViewControllerLifecycleHandler(IntPtr ptr) => _ptr = ptr;

        internal static UnityViewControllerLifecycleHandler Create(
            Action onViewWillLayoutSubviews = null,
            Action onViewDidLayoutSubviews = null,
            Action onViewWillDisappear = null,
            Action onViewDidDisappear = null,
            Action onViewWillAppear = null,
            Action onViewDidAppear = null,
            Action onInterfaceWillChangeOrientation = null,
            Action onInterfaceDidChangeOrientation = null)
        {
            var ptr = CreateUnityViewControllerLifecycleHandler(
                ViewWillLayoutSubviewsCallbackStatic,
                ViewDidLayoutSubviewsCallbackStatic,
                ViewWillDisappearCallbackStatic,
                ViewDidDisappearCallbackStatic,
                ViewWillAppearCallbackStatic,
                ViewDidAppearCallbackStatic,
                InterfaceWillChangeOrientationCallbackStatic,
                InterfaceDidChangeOrientationCallbackStatic);

            if (onViewWillLayoutSubviews != null) ViewWillLayoutSubviewsCallbacks[ptr] = onViewWillLayoutSubviews;
            if (onViewDidLayoutSubviews != null) ViewDidLayoutSubviewsCallbacks[ptr] = onViewDidLayoutSubviews;
            if (onViewWillDisappear != null) ViewWillDisappearCallbacks[ptr] = onViewWillDisappear;
            if (onViewDidDisappear != null) ViewDidDisappearCallbacks[ptr] = onViewDidDisappear;
            if (onViewWillAppear != null) ViewWillAppearCallbacks[ptr] = onViewWillAppear;
            if (onViewDidAppear != null) ViewDidAppearCallbacks[ptr] = onViewDidAppear;
            if (onInterfaceWillChangeOrientation != null) InterfaceWillChangeOrientationCallbacks[ptr] = onInterfaceWillChangeOrientation;
            if (onInterfaceDidChangeOrientation != null) InterfaceDidChangeOrientationCallbacks[ptr] = onInterfaceDidChangeOrientation;

            UnityRegisterViewControllerListener(ptr);
            return new UnityViewControllerLifecycleHandler(ptr);
        }

        ~UnityViewControllerLifecycleHandler()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                Assert.IsTrue(_ptr != IntPtr.Zero);

                if (disposing)
                {
                    // release managed resources
                }

                ViewWillLayoutSubviewsCallbacks.Remove(_ptr);
                ViewDidLayoutSubviewsCallbacks.Remove(_ptr);
                ViewWillDisappearCallbacks.Remove(_ptr);
                ViewDidDisappearCallbacks.Remove(_ptr);
                ViewWillAppearCallbacks.Remove(_ptr);
                ViewDidAppearCallbacks.Remove(_ptr);
                InterfaceWillChangeOrientationCallbacks.Remove(_ptr);
                InterfaceDidChangeOrientationCallbacks.Remove(_ptr);

                UnityUnregisterViewControllerListener(_ptr);
                ReleaseUnityViewControllerLifecycleHandler(_ptr);
                _ptr = IntPtr.Zero;
                _disposed = true;
            }

            return;
        }


        [DllImport("__Internal", EntryPoint = "LifecycleHandler_CreateUnityViewControllerLifecycleHandler")]
        private static extern IntPtr CreateUnityViewControllerLifecycleHandler(
            ViewWillLayoutSubviewsCallback viewWillLayoutSubviewsCallback,
            ViewDidLayoutSubviewsCallback viewDidLayoutSubviewsCallback,
            ViewWillDisappearCallback viewWillDisappearCallback,
            ViewDidDisappearCallback viewDidDisappearCallback,
            ViewWillAppearCallback viewWillAppearCallback,
            ViewDidAppearCallback viewDidAppearCallback,
            InterfaceWillChangeOrientationCallback interfaceWillChangeOrientationCallback,
            InterfaceDidChangeOrientationCallback interfaceDidChangeOrientationCallback);

        [DllImport("__Internal", EntryPoint = "LifecycleHandler_ReleaseUnityViewControllerLifecycleHandler")]
        private static extern void ReleaseUnityViewControllerLifecycleHandler(IntPtr ptr);

        [DllImport("__Internal", EntryPoint = "LifecycleHandler_UnityRegisterViewControllerListener")]
        private static extern void UnityRegisterViewControllerListener(IntPtr ptr);

        [DllImport("__Internal", EntryPoint = "LifecycleHandler_UnityUnregisterViewControllerListener")]
        private static extern void UnityUnregisterViewControllerListener(IntPtr ptr);


        private delegate void ViewWillLayoutSubviewsCallback(IntPtr context);

        private delegate void ViewDidLayoutSubviewsCallback(IntPtr context);

        private delegate void ViewWillDisappearCallback(IntPtr context);

        private delegate void ViewDidDisappearCallback(IntPtr context);

        private delegate void ViewWillAppearCallback(IntPtr context);

        private delegate void ViewDidAppearCallback(IntPtr context);

        private delegate void InterfaceWillChangeOrientationCallback(IntPtr context);

        private delegate void InterfaceDidChangeOrientationCallback(IntPtr context);

        [MonoPInvokeCallback(typeof(ViewWillLayoutSubviewsCallback))]
        private static void ViewWillLayoutSubviewsCallbackStatic(IntPtr context)
        {
            if (ViewWillLayoutSubviewsCallbacks.TryGetValue(context, out var callback))
            {
                callback.Invoke();
            }
        }

        [MonoPInvokeCallback(typeof(ViewDidLayoutSubviewsCallback))]
        private static void ViewDidLayoutSubviewsCallbackStatic(IntPtr context)
        {
            if (ViewDidLayoutSubviewsCallbacks.TryGetValue(context, out var callback))
            {
                callback.Invoke();
            }
        }

        [MonoPInvokeCallback(typeof(ViewWillDisappearCallback))]
        private static void ViewWillDisappearCallbackStatic(IntPtr context)
        {
            if (ViewWillDisappearCallbacks.TryGetValue(context, out var callback))
            {
                callback.Invoke();
            }
        }

        [MonoPInvokeCallback(typeof(ViewDidDisappearCallback))]
        private static void ViewDidDisappearCallbackStatic(IntPtr context)
        {
            if (ViewDidDisappearCallbacks.TryGetValue(context, out var callback))
            {
                callback.Invoke();
            }
        }

        [MonoPInvokeCallback(typeof(ViewWillAppearCallback))]
        private static void ViewWillAppearCallbackStatic(IntPtr context)
        {
            if (ViewWillAppearCallbacks.TryGetValue(context, out var callback))
            {
                callback.Invoke();
            }
        }

        [MonoPInvokeCallback(typeof(ViewDidAppearCallback))]
        private static void ViewDidAppearCallbackStatic(IntPtr context)
        {
            if (ViewDidAppearCallbacks.TryGetValue(context, out var callback))
            {
                callback.Invoke();
            }
        }

        [MonoPInvokeCallback(typeof(InterfaceWillChangeOrientationCallback))]
        private static void InterfaceWillChangeOrientationCallbackStatic(IntPtr context)
        {
            if (InterfaceWillChangeOrientationCallbacks.TryGetValue(context, out var callback))
            {
                callback.Invoke();
            }
        }

        [MonoPInvokeCallback(typeof(InterfaceDidChangeOrientationCallback))]
        private static void InterfaceDidChangeOrientationCallbackStatic(IntPtr context)
        {
            if (InterfaceDidChangeOrientationCallbacks.TryGetValue(context, out var callback))
            {
                callback.Invoke();
            }
        }
    }
}
