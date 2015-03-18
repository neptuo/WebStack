using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Exceptions
{
    public class ExceptionStack
    {
        private bool isEmpty = true;
        private readonly Stack<ExceptionModel> storage = new Stack<ExceptionModel>();

        private ExceptionModel PushModel(ExceptionModel model)
        {
            if (!isEmpty)
            {
                ExceptionModel previous = storage.Peek();
                previous.NextModel = model;
                model.PreviousModel = previous;
            }
            
            isEmpty = false;
            storage.Push(model);
            return model;
        }

        public ExceptionModel Push(Exception exception)
        {
            return PushModel(new ExceptionModel(exception));
        }

        public ExceptionModel PushHandled(Exception exception)
        {
            return PushModel(new ExceptionModel(true, exception));
        }

        public ExceptionStack MarkAsHandled(ExceptionModel model)
        {
            if (storage.Contains(model))
                model.IsHandled = true;
            else
                throw Ensure.Exception.ArgumentOutOfRange("model", "Current exception stack doesn't contain passed model.");

            return this;
        }

        public bool TryPeekTopUnhandled(out ExceptionModel model)
        {
            if (isEmpty)
            {
                model = null;
                return false;
            }

            model = storage.Peek();

            while (model != null && model.IsHandled)
                model = model.PreviousModel;

            return model != null;
        }
    }
}
