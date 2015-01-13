using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neptuo.WebStack.Templates.Runtime
{
    /// <summary>
    /// Standart implementation of <see cref="IComponentManager"/>.
    /// </summary>
    public partial class ComponentManager : IComponentManager
    {
        /// <summary>
        /// List of register objects.
        /// Key is target object, value is registration structure, <see cref="ComponentEntryBase"/>.
        /// </summary>
        private Dictionary<object, ComponentEntryBase> entries = new Dictionary<object, ComponentEntryBase>();

        public virtual void AddComponent<T>(T component, Action<T> propertyBinder)
        {
            Guard.NotNull(component, "component");

            ComponentEntryBase entry = new ComponentEntry<T>
            {
                Control = component,
                ArePropertiesBound = propertyBinder == null,
                PropertyBinder = propertyBinder
            };
            entries.Add(component, entry);
        }

        public virtual void AttachObserver<T>(IControl control, T observer, Action<T> propertyBinder)
            where T : IControlObserver
        {
            Guard.NotNull(control, "control");
            Guard.NotNull(observer, "observer");

            if (!entries.ContainsKey(control))
                return;

            entries[control].Observers.Add(new ObserverInfo<T>(observer, propertyBinder));
        }

        public void AttachInitComplete(IControl control, Action<IControl> handler)
        {
            Guard.NotNull(control, "control");
            Guard.NotNull(handler, "handler");

            if (entries.ContainsKey(control))
                entries[control].InitComplete.Add(handler);
        }

        public void OnInit(object control)
        {
            // control is null, continue on processing view.
            if (control == null)
                return;

            if (!entries.ContainsKey(control))
            {
                // Try init on not registered control
                BeforeInitComponent(control);

                IControl targetControl = control as IControl;
                if (targetControl != null)
                {
                    BeforeInitControl(targetControl);
                    targetControl.OnInit(this);
                }
                return;
            }

            // if control is already inited, continue on processing view.
            ComponentEntryBase entry = entries[control];
            if (entry.IsInited)
                return;

            // if control is already disposed, continue on processing view.
            if (entry.IsDisposed)
                return;

            BeforeInitComponent(entry.Control);
            
            IControl target = entry.Control as IControl;
            if (target != null)
                BeforeInitControl(target);

            if (!entry.ArePropertiesBound)
                entry.BindProperties();

            entry.IsInited = true;

            if (target == null)
                return;

            if (ExecuteObservers(entry))
                target.OnInit(this);

            if (entry.InitComplete.Count > 0)
            {
                foreach (Action<IControl> handler in entry.InitComplete)
                    handler(target);
            }

            AfterInitControl(target);
        }

        /// <summary>
        /// Executes all registered observer on <paramref name="entry"/>.
        /// </summary>
        /// <param name="entry">Registration entry.</param>
        /// <returns>Whether execution on <paramref name="entry"/> should be canceled.</returns>
        private bool ExecuteObservers(ComponentEntryBase entry)
        {
            bool canInit = true;
            if (entry.Observers.Count > 0)
            {
                ControlObserverEventArgs args = new ControlObserverEventArgs(entry.Control as IControl);
                foreach (ObserverInfo info in entry.Observers)
                {
                    if (!info.ArePropertiesBound)
                    {
                        info.BindProperties();
                        info.ArePropertiesBound = true;
                    }
                    info.Observer.OnInit(args);

                    if (args.Cancel)
                        canInit = false;
                }
            }

            return canInit;
        }

        /// <summary>
        /// Called before initing any component.
        /// </summary>
        /// <param name="component">Target component.</param>
        protected virtual void BeforeInitComponent(object component)
        { }

        /// <summary>
        /// Called before initing control (<see cref="IControl"/>).
        /// </summary>
        /// <param name="control">Target control.</param>
        protected virtual void BeforeInitControl(IControl control)
        { }

        /// <summary>
        /// Called after (whole) init phase was completed on <paramref name="control"/>.
        /// </summary>
        /// <param name="control">Target control.</param>
        protected virtual void AfterInitControl(IControl control)
        { }

        public void Render(object control, IRenderContext context)
        {
            if(control == null)
                return;

            // if control is string, simply write to writer
            if (control.GetType().FullName == typeof(String).FullName)
            {
                BeforeRenderComponent(control, context);
                context.Writer.Content(control);
                return;
            }

            if (!entries.ContainsKey(control))
            {
                // Try render not registered control.
                IControl targetControl = control as IControl;
                if (targetControl != null)
                {
                    BeforeRenderControl(targetControl, context);
                    DoRenderControl(targetControl, context);
                    AfterRenderControl(targetControl, context);
                }
                return;
            }

            ComponentEntryBase entry = entries[control];

            // if not inited, init it.
            if (!entry.IsInited)
                OnInit(control);

            // if dispose, continue processing view.
            if (entry.IsDisposed)
                return;

            BeforeRenderComponent(entry.Control, context);

            IControl target = entry.Control as IControl;
            if (target == null)
                return;

            BeforeRenderControl(target, context);

            bool canRender = true;
            if (entry.Observers.Count > 0)
            {
                ControlObserverEventArgs args = new ControlObserverEventArgs(target);
                foreach (ObserverInfo info in entry.Observers)
                {
                    if (!info.ArePropertiesBound)
                    {
                        info.BindProperties();
                        info.ArePropertiesBound = true;
                    }
                    info.Observer.Render(args, context);

                    if (args.Cancel)
                        canRender = false;
                }
            }

            if (canRender)
                DoRenderControl(target, context);

            AfterRenderControl(target, context);
        }

        /// <summary>
        /// Called before rendering any component.
        /// </summary>
        /// <param name="component">Target component.</param>
        /// <param name="context">Output rendering context.</param>
        protected virtual void BeforeRenderComponent(object component, IRenderContext context)
        { }

        /// <summary>
        /// Called before redering control (<see cref="IControl"/>).
        /// </summary>
        /// <param name="control">Target control.</param>
        /// <param name="context">Output rendering context.</param>
        protected virtual void BeforeRenderControl(IControl control, IRenderContext context)
        { }

        /// <summary>
        /// Do render phase on <paramref name="control"/>.
        /// </summary>
        /// <param name="control">Target control.</param>
        /// <param name="context">Output rendering context.</param>
        protected virtual void DoRenderControl(IControl control, IRenderContext context)
        {
            control.Render(context);
        }

        /// <summary>
        /// Called after (whole) render phase was completed on <paramref name="control"/>.
        /// </summary>
        /// <param name="control">Target control.</param>
        /// <param name="context">Output rendering context.</param>
        protected virtual void AfterRenderControl(IControl control, IRenderContext context)
        { }

        public void DisposeAll()
        {
            foreach (object entry in entries.Keys)
                Dispose(entry);
        }

        public void Dispose(object component)
        {
            // if not registered, continue on processing view.
            if (!entries.ContainsKey(component))
                return;

            ComponentEntryBase entry = entries[component];

            // if not inited, init it.
            if (!entry.IsInited)
                OnInit(component);

            // if already disposed, continue on processing view.
            if (entry.IsDisposed)
                return;

            IDisposable target = entry.Control as IDisposable;
            if (target != null)
            {
                entry.IsDisposed = true;
                target.Dispose();
            }
        }

        /// <summary>
        /// Called before disposing any component.
        /// </summary>
        /// <param name="component">Target component.</param>
        protected virtual void BeforeDisposeComponent(object component)
        { }

        /// <summary>
        /// Called before disposing control (<see cref="IControl"/>).
        /// </summary>
        /// <param name="component">Target component.</param>
        protected virtual void BeforeDisposeControl(IControl control)
        { }
    }
}
