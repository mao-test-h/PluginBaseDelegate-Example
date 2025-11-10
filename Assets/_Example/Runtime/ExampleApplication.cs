using System;
using LifecycleHandler;
using UnityEngine;
using UnityEngine.UI;
using UnityIOSPluginBaseBridge;

namespace _Example
{
    internal sealed class ExampleApplication : MonoBehaviour, IUnityViewControllerListener, ILifeCycleListener
    {
        [SerializeField] private Button playMovieButton;

        private static string Tag => $"[{nameof(ExampleApplication)}]";
        private IDisposable _unityViewControllerListenerBridge;
        private IDisposable _lifeCycleListenerBridge;

        private void Awake()
        {
            _unityViewControllerListenerBridge = UnityViewControllerListenerBuilder.Build(this);
            _lifeCycleListenerBridge = LifeCycleListenerBuilder.Build(this);

            // イベント確認用にフルスクリーン動画を再生
            playMovieButton.onClick.AddListener(() =>
            {
                const string url = "https://devstreaming-cdn.apple.com/videos/streaming/examples/bipbop_16x9/bipbop_16x9_variant.m3u8";
                var ret = Handheld.PlayFullScreenMovie(url);
                Debug.Log($"{Tag} Handheld.PlayFullScreenMovie returned {ret}");
            });
        }

        private void OnDestroy()
        {
            _unityViewControllerListenerBridge?.Dispose();
            _lifeCycleListenerBridge?.Dispose();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            Debug.Log($"{Tag} >> OnApplicationFocus: " + hasFocus);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            Debug.Log($"{Tag} >> OnApplicationPause: " + pauseStatus);
        }

        public void OnViewWillLayoutSubviewsCallbacks()
        {
            Debug.Log($"{Tag} [IUnityViewControllerListener] OnViewWillLayoutSubviews");
        }

        public void OnViewDidLayoutSubviewsCallbacks()
        {
            Debug.Log($"{Tag} [IUnityViewControllerListener] OnViewDidLayoutSubviews");
        }

        public void OnViewWillDisappearCallbacks(bool animated)
        {
            Debug.Log($"{Tag} [IUnityViewControllerListener] OnViewWillDisappear (animated: {animated})");
        }

        public void OnViewDidDisappearCallbacks(bool animated)
        {
            Debug.Log($"{Tag} [IUnityViewControllerListener] OnViewDidDisappear (animated: {animated})");
        }

        public void OnViewWillAppearCallbacks(bool animated)
        {
            Debug.Log($"{Tag} [IUnityViewControllerListener] OnViewWillAppear (animated: {animated})");
        }

        public void OnViewDidAppearCallbacks(bool animated)
        {
            Debug.Log($"{Tag} [IUnityViewControllerListener] OnViewDidAppear (animated: {animated})");
        }

        public void OnInterfaceWillChangeOrientationCallbacks()
        {
            Debug.Log($"{Tag} [IUnityViewControllerListener] OnInterfaceWillChangeOrientation");
        }

        public void OnInterfaceDidChangeOrientationCallbacks()
        {
            Debug.Log($"{Tag} [IUnityViewControllerListener] OnInterfaceDidChangeOrientation");
        }

        public void OnDidFinishLaunchingCallbacks()
        {
            Debug.Log($"{Tag} [ILifeCycleListener] OnDidFinishLaunching");
        }

        public void OnDidBecomeActiveCallbacks()
        {
            Debug.Log($"{Tag} [ILifeCycleListener] OnDidBecomeActive");
        }

        public void OnWillResignActiveCallbacks()
        {
            Debug.Log($"{Tag} [ILifeCycleListener] OnWillResignActive");
        }

        public void OnDidEnterBackgroundCallbacks()
        {
            Debug.Log($"{Tag} [ILifeCycleListener] OnDidEnterBackground");
        }

        public void OnWillEnterForegroundCallbacks()
        {
            Debug.Log($"{Tag} [ILifeCycleListener] OnWillEnterForeground");
        }

        public void OnWillTerminateCallbacks()
        {
            Debug.Log($"{Tag} [ILifeCycleListener] OnWillTerminate");
        }

        public void OnUnityDidUnloadCallbacks()
        {
            Debug.Log($"{Tag} [ILifeCycleListener] OnUnityDidUnload");
        }

        public void OnUnityDidQuitCallbacks()
        {
            Debug.Log($"{Tag} [ILifeCycleListener] OnUnityDidQuit");
        }
    }
}
