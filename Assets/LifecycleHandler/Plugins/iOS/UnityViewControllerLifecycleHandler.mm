#include "PluginBase/UnityViewControllerListener.h"
#include <stdint.h>

// Callback types for all lifecycle events
typedef void (*ViewWillLayoutSubviewsCallback)(void* context);
typedef void (*ViewDidLayoutSubviewsCallback)(void* context);
typedef void (*ViewWillDisappearCallback)(void* context, uint8_t animated);
typedef void (*ViewDidDisappearCallback)(void* context, uint8_t animated);
typedef void (*ViewWillAppearCallback)(void* context, uint8_t animated);
typedef void (*ViewDidAppearCallback)(void* context, uint8_t animated);
typedef void (*InterfaceWillChangeOrientationCallback)(void* context);
typedef void (*InterfaceDidChangeOrientationCallback)(void* context);

@interface UnityViewControllerLifecycleHandler : NSObject<UnityViewControllerListener>
@property (nonatomic, assign) ViewWillLayoutSubviewsCallback viewWillLayoutSubviewsCallback;
@property (nonatomic, assign) ViewDidLayoutSubviewsCallback viewDidLayoutSubviewsCallback;
@property (nonatomic, assign) ViewWillDisappearCallback viewWillDisappearCallback;
@property (nonatomic, assign) ViewDidDisappearCallback viewDidDisappearCallback;
@property (nonatomic, assign) ViewWillAppearCallback viewWillAppearCallback;
@property (nonatomic, assign) ViewDidAppearCallback viewDidAppearCallback;
@property (nonatomic, assign) InterfaceWillChangeOrientationCallback interfaceWillChangeOrientationCallback;
@property (nonatomic, assign) InterfaceDidChangeOrientationCallback interfaceDidChangeOrientationCallback;
@end

@implementation UnityViewControllerLifecycleHandler

- (void)viewWillLayoutSubviews:(NSNotification*)notification
{
    if (self.viewWillLayoutSubviewsCallback) {
        self.viewWillLayoutSubviewsCallback((__bridge void*)self);
    }
}

- (void)viewDidLayoutSubviews:(NSNotification*)notification
{
    if (self.viewDidLayoutSubviewsCallback) {
        self.viewDidLayoutSubviewsCallback((__bridge void*)self);
    }
}

- (void)viewWillDisappear:(NSNotification*)notification
{
    if (self.viewWillDisappearCallback) {
        NSNumber* animatedNumber = notification.userInfo[@"animated"];
        uint8_t animated = animatedNumber ? [animatedNumber boolValue] : 0;
        self.viewWillDisappearCallback((__bridge void*)self, animated);
    }
}

- (void)viewDidDisappear:(NSNotification*)notification
{
    if (self.viewDidDisappearCallback) {
        NSNumber* animatedNumber = notification.userInfo[@"animated"];
        uint8_t animated = animatedNumber ? [animatedNumber boolValue] : 0;
        self.viewDidDisappearCallback((__bridge void*)self, animated);
    }
}

- (void)viewWillAppear:(NSNotification*)notification
{
    if (self.viewWillAppearCallback) {
        NSNumber* animatedNumber = notification.userInfo[@"animated"];
        uint8_t animated = animatedNumber ? [animatedNumber boolValue] : 0;
        self.viewWillAppearCallback((__bridge void*)self, animated);
    }
}

- (void)viewDidAppear:(NSNotification*)notification
{
    if (self.viewDidAppearCallback) {
        NSNumber* animatedNumber = notification.userInfo[@"animated"];
        uint8_t animated = animatedNumber ? [animatedNumber boolValue] : 0;
        self.viewDidAppearCallback((__bridge void*)self, animated);
    }
}

- (void)interfaceWillChangeOrientation:(NSNotification*)notification
{
    if (self.interfaceWillChangeOrientationCallback) {
        self.interfaceWillChangeOrientationCallback((__bridge void*)self);
    }
}

- (void)interfaceDidChangeOrientation:(NSNotification*)notification
{
    if (self.interfaceDidChangeOrientationCallback) {
        self.interfaceDidChangeOrientationCallback((__bridge void*)self);
    }
}

@end

#ifdef __cplusplus
extern "C" {
#endif

void* LifecycleHandler_CreateUnityViewControllerLifecycleHandler(
                                                                 ViewWillLayoutSubviewsCallback viewWillLayoutSubviewsCallback,
                                                                 ViewDidLayoutSubviewsCallback viewDidLayoutSubviewsCallback,
                                                                 ViewWillDisappearCallback viewWillDisappearCallback,
                                                                 ViewDidDisappearCallback viewDidDisappearCallback,
                                                                 ViewWillAppearCallback viewWillAppearCallback,
                                                                 ViewDidAppearCallback viewDidAppearCallback,
                                                                 InterfaceWillChangeOrientationCallback interfaceWillChangeOrientationCallback,
                                                                 InterfaceDidChangeOrientationCallback interfaceDidChangeOrientationCallback)
{
    UnityViewControllerLifecycleHandler* handler = [[UnityViewControllerLifecycleHandler alloc] init];
    handler.viewWillLayoutSubviewsCallback = viewWillLayoutSubviewsCallback;
    handler.viewDidLayoutSubviewsCallback = viewDidLayoutSubviewsCallback;
    handler.viewWillDisappearCallback = viewWillDisappearCallback;
    handler.viewDidDisappearCallback = viewDidDisappearCallback;
    handler.viewWillAppearCallback = viewWillAppearCallback;
    handler.viewDidAppearCallback = viewDidAppearCallback;
    handler.interfaceWillChangeOrientationCallback = interfaceWillChangeOrientationCallback;
    handler.interfaceDidChangeOrientationCallback = interfaceDidChangeOrientationCallback;
    return (__bridge_retained void*)handler;
}

void LifecycleHandler_ReleaseUnityViewControllerLifecycleHandler(void* ptr)
{
    UnityViewControllerLifecycleHandler* handler = (__bridge_transfer UnityViewControllerLifecycleHandler*)ptr;
    handler.viewWillLayoutSubviewsCallback = nil;
    handler.viewDidLayoutSubviewsCallback = nil;
    handler.viewWillDisappearCallback = nil;
    handler.viewDidDisappearCallback = nil;
    handler.viewWillAppearCallback = nil;
    handler.viewDidAppearCallback = nil;
    handler.interfaceWillChangeOrientationCallback = nil;
    handler.interfaceDidChangeOrientationCallback = nil;
}

void LifecycleHandler_UnityRegisterViewControllerListener(void* ptr)
{
    UnityViewControllerLifecycleHandler* handler = (__bridge UnityViewControllerLifecycleHandler*)ptr;
    UnityRegisterViewControllerListener(handler);
}

void LifecycleHandler_UnityUnregisterViewControllerListener(void* ptr)
{
    UnityViewControllerLifecycleHandler* handler = (__bridge UnityViewControllerLifecycleHandler*)ptr;
    UnityUnregisterViewControllerListener(handler);
}

#ifdef __cplusplus
}
#endif
