using System;
using System.Numerics;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Microsoft.Xaml.Interactivity;

namespace GeekyTool.UI.Behaviors
{
    // This implementation is based on what you can found in https://english.r2d2rigo.es/2016/01/26/implementing-a-tilt-effect-behavior-for-uwp-with-windows-composition/
    public class TiltBehavior : Behavior
    {
        private Visual elementVisual;

        private Compositor compositor;

        private UIElement uiElement;

        private ScalarKeyFrameAnimation touchInAnimation;
        private ScalarKeyFrameAnimation touchOutAnimation;

        protected override void OnAttached()
        {
            base.OnAttached();

            uiElement = AssociatedObject as UIElement;
            if (uiElement == null)
                throw new InvalidOperationException("TiltBehavior can only be attached to types inherit from UIElement.");

            elementVisual = ElementCompositionPreview.GetElementVisual(uiElement);

            compositor = elementVisual.Compositor;

            touchInAnimation = compositor.CreateScalarKeyFrameAnimation();
            touchInAnimation.InsertKeyFrame(0.00f, 0.0f);
            touchInAnimation.InsertKeyFrame(1.00f, 20.0f);
            touchInAnimation.Duration = TimeSpan.FromMilliseconds(200);

            touchOutAnimation = compositor.CreateScalarKeyFrameAnimation();
            touchOutAnimation.InsertKeyFrame(0.00f, 20.0f);
            touchOutAnimation.InsertKeyFrame(1.00f, 0.00f);
            touchOutAnimation.Duration = TimeSpan.FromMilliseconds(200);

            uiElement.PointerPressed += UiElementOnPointerPressed;
            uiElement.PointerMoved += UiElementOnPointerMoved;
            uiElement.PointerCanceled += UiElementOnPointerCanceled;
            uiElement.PointerReleased += UiElementOnPointerReleased;
            uiElement.PointerExited += UiElementOnPointerExited;
        }
        
        protected override void OnDetaching()
        {
            base.OnDetaching();

            if (elementVisual != null)
            {
                ResetTiltEffect();
                
                elementVisual.Dispose();
                elementVisual = null;
                compositor = null;
            }

            uiElement.PointerPressed -= UiElementOnPointerPressed;
            uiElement.PointerMoved -= UiElementOnPointerMoved;
            uiElement.PointerCanceled -= UiElementOnPointerCanceled;
            uiElement.PointerReleased -= UiElementOnPointerReleased;
            uiElement.PointerExited -= UiElementOnPointerExited;
            uiElement = null;
        }


        private void UiElementOnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            ApplyTiltEffect(e, withAnimation: true);
        }

        private void UiElementOnPointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.IsInContact)
            {
                ApplyTiltEffect(e);
            }
            else
            {
                ResetTiltEffect();
            }
        }

        private void UiElementOnPointerCanceled(object sender, PointerRoutedEventArgs e)
        {
            ResetTiltEffect(withAnimation: true);
        }

        private void UiElementOnPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            ResetTiltEffect(withAnimation: true);
        }

        private void UiElementOnPointerExited(object sender, PointerRoutedEventArgs e)
        {
            ResetTiltEffect();
        }

        private void ApplyTiltEffect(PointerRoutedEventArgs e, bool withAnimation = false)
        {
            elementVisual.CenterPoint = 
                new Vector3((float)uiElement.RenderSize.Width * 0.5f, (float)uiElement.RenderSize.Height * 0.5f, -10.0f);

            var contactPoint = e.GetCurrentPoint(uiElement).Position;

            var contactVector = 
                new Vector3((float)contactPoint.X, (float)contactPoint.Y, 0.0f) - elementVisual.CenterPoint;
            contactVector = Vector3.Normalize(contactVector);

            // Apply transform matrix to contact vector
            elementVisual.RotationAxis = new Vector3(-contactVector.Y, contactVector.X, 0.0f);

            if (withAnimation)
            {
                elementVisual.StartAnimation(nameof(elementVisual.RotationAngleInDegrees), touchInAnimation);
            }
            else
            {
                elementVisual.RotationAngleInDegrees = 20.0f;
            }
        }

        private void ResetTiltEffect(bool withAnimation = false)
        {
            if (withAnimation)
            {
                elementVisual.StartAnimation(nameof(elementVisual.RotationAngleInDegrees), touchOutAnimation);
            }
            else
            {
                elementVisual.RotationAngleInDegrees = 0.0f;
            }
        }
    }
}