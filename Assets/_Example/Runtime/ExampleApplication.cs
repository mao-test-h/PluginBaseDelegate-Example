using System;
using LifecycleHandler;
using UnityEngine;
using UnityEngine.UI;

namespace _Example
{
    internal sealed class ExampleApplication : MonoBehaviour, IViewControllerLifecycleListener
    {
        [SerializeField] private Button playMovieButton;

        private string Tag => $"[{nameof(ExampleApplication)}]";
        private IDisposable _lifecycleHandler;

        private void Awake()
        {
            _lifecycleHandler = ViewControllerLifecycleHandlerFactory.Create(this);

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
            _lifecycleHandler.Dispose();
        }

        void OnApplicationFocus(bool hasFocus)
        {
            Debug.Log($"{Tag} >> OnApplicationFocus: " + hasFocus);
        }

        void OnApplicationPause(bool pauseStatus)
        {
            Debug.Log($"{Tag} >> OnApplicationPause: " + pauseStatus);
        }

        public void OnViewWillLayoutSubviewsCallbacks()
        {
            Debug.Log($"{Tag} OnViewWillLayoutSubviews");
        }

        public void OnViewDidLayoutSubviewsCallbacks()
        {
            Debug.Log($"{Tag} OnViewDidLayoutSubviews");
        }

        public void OnViewWillDisappearCallbacks(bool animated)
        {
            Debug.Log($"{Tag} OnViewWillDisappear (animated: {animated})");
        }

        public void OnViewDidDisappearCallbacks(bool animated)
        {
            Debug.Log($"{Tag} OnViewDidDisappear (animated: {animated})");
        }

        public void OnViewWillAppearCallbacks(bool animated)
        {
            Debug.Log($"{Tag} OnViewWillAppear (animated: {animated})");
        }

        public void OnViewDidAppearCallbacks(bool animated)
        {
            Debug.Log($"{Tag} OnViewDidAppear (animated: {animated})");
        }

        public void OnInterfaceWillChangeOrientationCallbacks()
        {
            Debug.Log($"{Tag} OnInterfaceWillChangeOrientation");
        }

        public void OnInterfaceDidChangeOrientationCallbacks()
        {
            Debug.Log($"{Tag} OnInterfaceDidChangeOrientation");
        }
    }
}
