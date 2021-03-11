using System;
using System.Collections.Generic;
using System.Windows;

namespace MRD.UserRoleSeparationHelper
{
    public class PermissionBehavior : IDisposable
    {
        // Static behaviour parameters
        public static readonly Dictionary<FrameworkElement, PermissionBehavior> Instances = new Dictionary<FrameworkElement, PermissionBehavior>();

        public static readonly DependencyProperty LoggedInUserTypeProperty =
            DependencyProperty.RegisterAttached("LoggedInUserType", typeof(UserType), typeof(PermissionBehavior),
                                                new PropertyMetadata(OnLoggedInUserTypePropertyChanged));

        public static readonly DependencyProperty RequiredUserTypeProperty =
            DependencyProperty.RegisterAttached("RequiredUserType", typeof(UserType), typeof(PermissionBehavior),
                                                new PropertyMetadata(OnRequiredUserTypePropertyChanged));
        private bool disposedValue;

        private static void OnLoggedInUserTypePropertyChanged(DependencyObject dependencyObject,
                                                      DependencyPropertyChangedEventArgs e)
        {
            SetLoggedInUserType(dependencyObject, (UserType)e.NewValue);
        }

        public static void SetLoggedInUserType(DependencyObject obj, UserType value)
        {
            var behavior = GetAttachedBehavior(obj as FrameworkElement);
            behavior.AssociatedObject = obj as FrameworkElement;
            obj.SetValue(LoggedInUserTypeProperty, value);

            behavior.LoggedInUserType = value;
            behavior.UpdateVisibility();
            behavior.Dispose();
        }

        public static object GetLoggedInUserType(DependencyObject obj)
        {
            return obj.GetValue(LoggedInUserTypeProperty);
        }

        private static void OnRequiredUserTypePropertyChanged(DependencyObject dependencyObject,
                                                      DependencyPropertyChangedEventArgs e)
        {
            SetRequiredUserType(dependencyObject, (UserType)e.NewValue);
        }

        public static void SetRequiredUserType(DependencyObject obj, UserType value)
        {
            var behavior = GetAttachedBehavior(obj as FrameworkElement);
            behavior.AssociatedObject = obj as FrameworkElement;
            obj.SetValue(RequiredUserTypeProperty, value);

            behavior.RequiredUserType = value;
            behavior.UpdateVisibility();
            behavior.Dispose();
        }

        public static object GetRequiredUserType(DependencyObject obj)
        {
            return obj.GetValue(RequiredUserTypeProperty);
        }

        private static PermissionBehavior GetAttachedBehavior(FrameworkElement obj)
        {
            if (!Instances.ContainsKey(obj))
            {
                Instances[obj] = new PermissionBehavior { AssociatedObject = obj };
            }

            return Instances[obj];
        }

        static PermissionBehavior()
        {

        }

        // Class instance parameters
        private FrameworkElement AssociatedObject { get; set; }
        private UserType LoggedInUserType { get; set; }
        private UserType RequiredUserType { get; set; }

        private void UpdateVisibility()
        {
            if (LoggedInUserType.HasFlag(RequiredUserType))
            {
                AssociatedObject.Visibility = Visibility.Visible;
                AssociatedObject.IsEnabled = true;
            }
            else
            {
                AssociatedObject.Visibility = Visibility.Collapsed;
                AssociatedObject.IsEnabled = false;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    AssociatedObject = null;
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~PermissionBehavior()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
