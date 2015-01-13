using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neptuo.WebStack.Templates
{
    /// <summary>
    /// Component manager.
    /// 
    /// </summary>
    public interface IComponentManager
    {
        /// <summary>
        /// Adds component.
        /// </summary>
        /// <typeparam name="T">Component type.</typeparam>
        /// <param name="component">Target component.</param>
        /// <param name="propertyBinder">Property binder for <paramref name="component"/>.</param>
        void AddComponent<T>(T component, Action<T> propertyBinder);

        /// <summary>
        /// Attaches observer to <paramref name="control"/>.
        /// </summary>
        /// <typeparam name="T">Observer type.</typeparam>
        /// <param name="control">Target control.</param>
        /// <param name="observer">Observer to attach.</param>
        /// <param name="propertyBinder">Observer property binder.</param>
        void AttachObserver<T>(IControl control, T observer, Action<T> propertyBinder)
            where T : IControlObserver;

        /// <summary>
        /// Attaches init complete hander of <paramref name="control"/> init phase.
        /// </summary>
        /// <param name="control">Target control.</param>
        /// <param name="handler">Handler for init complete.</param>
        void AttachInitComplete(IControl control, Action<IControl> handler);

        /// <summary>
        /// Starts init phase on <paramref name="control"/>.
        /// </summary>
        /// <param name="control">Target control</param>
        void OnInit(object control);

        /// <summary>
        /// Renders <paramref name="control"/> into <paramref name="context"/>.
        /// </summary>
        /// <param name="control">Target control.</param>
        /// <param name="context">Output rendering context.</param>
        void Render(object control, IRenderContext context);

        /// <summary>
        /// Disposes <paramref name="component"/>.
        /// </summary>
        /// <param name="component">Target object to dispose.</param>
        void Dispose(object component);

        /// <summary>
        /// Disposes all registered and not already disposed components.
        /// </summary>
        void DisposeAll();
    }
}
