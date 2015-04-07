using Neptuo.ComponentModel;
using Neptuo.WebStack.Templates.UI.Runtime;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using Neptuo;
using Neptuo.Activators;

namespace Neptuo.WebStack.Templates.UI
{
    /// <summary>
    /// Base class for generated views.
    /// </summary>
    public abstract class GeneratedView : DisposableBase, IControl
    {
        protected IComponentManager componentManager;
        protected IDependencyProvider dependencyProvider;

        /// <summary>
        /// View content.
        /// </summary>
        public ICollection<object> Content { get; private set; }

        public GeneratedView()
        {
            Content = new List<object>();
        }

        /// <summary>
        /// Method where to setup root controls.
        /// </summary>
        /// <param name="view">Generated view instance to bind.</param>
        protected abstract void BindView(GeneratedView view);

        /// <summary>
        /// Starts init phase of this view.
        /// </summary>
        public void Init(IDependencyProvider dependencyProvider, IComponentManager componentManager)
        {
            Ensure.NotNull(dependencyProvider, "dependencyProvider");
            Ensure.NotNull(componentManager, "componentManager");
            this.dependencyProvider = dependencyProvider;
            this.componentManager = componentManager;
            componentManager.AddComponent(this, BindView);
            componentManager.OnInit(this);
        }

        public void OnInit(IComponentManager componentManager)
        {
            Ensure.NotNull(componentManager, "componentManager");

            foreach (object item in Content)
                componentManager.OnInit(item);
        }

        /// <summary>
        /// Renders view output to <paramref name="writer"/>.
        /// </summary>
        /// <param name="writer">Output writer.</param>
        public void Render(IComponentManager componentManager, IHtmlWriter writer)
        {
            Ensure.NotNull(writer, "writer");

            foreach (object item in Content)
                componentManager.Render(item, writer);
        }

        /// <summary>
        /// Disposes this view.
        /// </summary>
        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            componentManager.DisposeAll();
        }

        /// <summary>
        /// Utility method for creating <see cref="IValueExtensionContext"/> for <paramref name="targetObject"/> and <paramref name="targetProperty"/>.
        /// </summary>
        /// <param name="targetObject">Target object for <see cref="IValueExtensionContext"/>.</param>
        /// <param name="targetProperty">Target property for <see cref="IValueExtensionContext"/>.</param>
        /// <returns><see cref="IValueExtensionContext"/> for <paramref name="targetObject"/> and <paramref name="targetProperty"/>.</returns>
        protected IValueExtensionContext CreateValueExtensionContext(object targetObject, string targetProperty)
        {
            Ensure.NotNull(targetObject, "targetObject");

            PropertyInfo propertyInfo = null;
            if (!String.IsNullOrEmpty(targetProperty))
                propertyInfo = targetObject.GetType().GetProperty(targetProperty);

            return new DefaultValueExtensionContext(
                targetObject,
                propertyInfo
            );
        }

        /// <summary>
        /// Utility method for casting <paramref name="value"/> to <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Target type.</typeparam>
        /// <param name="value">Source value.</param>
        /// <returns><paramref name="value"/> as <typeparamref name="T"/>.</returns>
        protected virtual T CastValueTo<T>(object value)
        {
            if (value == null)
                return default(T);

            Type sourceType = value.GetType();
            Type targetType = typeof(T);

            if (sourceType == targetType)
                return (T)value;

            TypeConverter converter = TypeDescriptor.GetConverter(value);
            if (converter.CanConvertTo(targetType))
                return (T)converter.ConvertTo(value, targetType);

            throw new InvalidOperationException(String.Format("Unnable to convert to target type! Source type: {0}, target type: {1}", sourceType, targetType));
        }
    }
}
