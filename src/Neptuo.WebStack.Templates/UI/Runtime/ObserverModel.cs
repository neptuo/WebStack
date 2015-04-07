using Neptuo.WebStack.Templates.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Templates.UI.Runtime
{
    /// <summary>
    /// Helper class for representing registered observer.
    /// </summary>
    internal abstract class ObserverModelBase
    {
        /// <summary>
        /// Target observer.
        /// </summary>
        public virtual IControlObserver Observer { get; set; }

        /// <summary>
        /// Method that binds property values.
        /// </summary>
        public virtual Delegate PropertyBinder { get; set; }

        /// <summary>
        /// Flag to see if properties where bound.
        /// </summary>
        public bool ArePropertiesBound { get; set; }

        public ObserverModelBase(IControlObserver observer, Delegate propertyBinder)
        {
            Ensure.NotNull(observer, "observer");
            Observer = observer;
            PropertyBinder = propertyBinder;
        }

        /// <summary>
        /// Calls property binder.
        /// </summary>
        public abstract void BindProperties();
    }

    /// <summary>
    /// Typed version of <see cref="ObserverModelBase"/>.
    /// </summary>
    /// <typeparam name="T">Target observer type.</typeparam>
    internal class ObserverModel<T> : ObserverModelBase
        where T : IControlObserver
    {
        private T observer;
        private Action<T> propertyBinder;

        public override IControlObserver Observer
        {
            get { return observer; }
            set { observer = (T)value; }
        }

        public override Delegate PropertyBinder
        {
            get { return propertyBinder; }
            set { propertyBinder = (Action<T>)value; }
        }

        public ObserverModel(T observer, Action<T> propertyBinder)
            : base(observer, propertyBinder)
        { }

        public override void BindProperties()
        {
            if (propertyBinder != null)
            {
                propertyBinder(observer);
                ArePropertiesBound = true;
            }
        }
    }
}
