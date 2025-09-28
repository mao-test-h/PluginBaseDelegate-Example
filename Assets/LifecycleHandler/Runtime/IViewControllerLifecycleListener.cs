namespace LifecycleHandler
{
    public interface IViewControllerLifecycleListener
    {
        void OnViewWillLayoutSubviewsCallbacks();
        void OnViewDidLayoutSubviewsCallbacks();
        void OnViewWillDisappearCallbacks(bool animated);
        void OnViewDidDisappearCallbacks(bool animated);
        void OnViewWillAppearCallbacks(bool animated);
        void OnViewDidAppearCallbacks(bool animated);
        void OnInterfaceWillChangeOrientationCallbacks();
        void OnInterfaceDidChangeOrientationCallbacks();
    }
}
