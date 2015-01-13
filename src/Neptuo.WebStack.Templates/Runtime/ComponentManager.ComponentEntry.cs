using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neptuo.WebStack.Templates.Runtime
{
    partial class ComponentManager
    {
        /// <summary>
        /// Helper class for representing registered control.
        /// </summary>
        internal abstract class ComponentEntryBase
        {
            /// <summary>
            /// Target control.
            /// </summary>
            public virtual object Control { get; set; }

            /// <summary>
            /// Method that binds property values.
            /// </summary>
            public virtual Delegate PropertyBinder { get; set; }

            /// <summary>
            /// List of registered observers.
            /// </summary>
            public List<ObserverInfo> Observers { get; private set; }

            /// <summary>
            /// List of registered listeners for completion of init phase of <see cref="Control"/>.
            /// </summary>
            public List<Action<IControl>> InitComplete { get; private set; }

            /// <summary>
            /// Flag to see if properties where bound.
            /// </summary>
            public bool ArePropertiesBound { get; set; }

            /// <summary>
            /// Flag to see if init phase was processed.
            /// </summary>
            public bool IsInited { get; set; }

            /// <summary>
            /// Flag to see if <see cref="Control"/> is disposed.
            /// </summary>
            public bool IsDisposed { get; set; }

            public ComponentEntryBase()
            {
                Observers = new List<ObserverInfo>();
                InitComplete = new List<Action<IControl>>();
            }

            /// <summary>
            /// Calls property binder.
            /// </summary>
            public abstract void BindProperties();
        }

        /// <summary>
        /// Typed version of <see cref="ComponentEntryBase"/>.
        /// </summary>
        /// <typeparam name="T">Target control type.</typeparam>
        internal class ComponentEntry<T> : ComponentEntryBase
        {
            private T control;
            private Action<T> propertyBinder;

            public override object Control
            {
                get { return control; }
                set { control = (T)value; }
            }

            public override Delegate PropertyBinder
            {
                get { return propertyBinder; }
                set { propertyBinder = (Action<T>)value; }
            }

            public ComponentEntry()
            {

            }

            public override void BindProperties()
            {
                if (propertyBinder != null)
                {
                    propertyBinder(control);
                    ArePropertiesBound = true;
                }
            }
        }
    }
}
