using Neptuo.ComponentModel;
using Neptuo.Linq.Expressions;
using Neptuo.Templates.Compilation.CodeGenerators;
using Neptuo.Templates.Compilation.CodeObjects;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Templates.Compilation.CodeGenerators
{
    /// <summary>
    /// Implementation of object generator for <see cref="IObserverCodeObject"/> using component wrap
    /// and delegation to <see cref="CodeDomComponentObjectGenerator"/>.
    /// Takes instance of <see cref="IObserverCodeObject"/>, wraps it to <see cref="ComponentCodeObject"/>
    /// and generates code using <see cref="CodeDomComponentObjectGenerator"/>.
    /// </summary>
    public class CodeDomObserverObjectGenerator : CodeDomControlObjectGenerator
    {
        public CodeDomObserverObjectGenerator(IUniqueNameProvider nameProvider)
            : base(nameProvider)
        { }

        public override ICodeDomObjectResult Generate(ICodeDomObjectContext context, ICodeObject codeObject)
        {
            ITypeCodeObject typeCodeObject = codeObject as ITypeCodeObject;
            if (typeCodeObject == null)
            {
                context.AddError("Unnable to generate code for observer, which is not ITypeCodeObject.");
                return null;
            }

            IPropertiesCodeObject propertiesCodeObject = codeObject as IPropertiesCodeObject;
            if (propertiesCodeObject == null)
            {
                context.AddError("Unnable to generate code for observer, which is not IPropertiesCodeObject.");
                return null;
            }

            ComponentCodeObject component = new ComponentCodeObject(typeCodeObject.Type);
            component.Properties.AddRange(propertiesCodeObject.Properties);
            
            return base.Generate(context, component);
        }

        protected override ICodeDomObjectResult Generate(ICodeDomObjectContext context, ComponentCodeObject codeObject, string fieldName)
        {
            ICodeDomObjectResult result = base.Generate(context, codeObject, fieldName);
            if (result == null)
                return result;

            string variableName;
            if (!context.TryGetObserverTarget(out variableName))
            {
                context.AddError("Unnable to process observer without target variable name.");
                return null;
            }

            if (!result.HasExpression())
            {
                context.AddError("Unnable to process observer without expression from generated component.");
                return null;
            }

            return new CodeDomDefaultObjectResult(
                new CodeExpressionStatement(
                    new CodeMethodInvokeExpression(
                        new CodeFieldReferenceExpression(
                            new CodeThisReferenceExpression(),
                            "componentManager"
                        ),
                        TypeHelper.MethodName<IComponentManager, IControl, IControlObserver, Action<IControlObserver>>(m => m.AddObserver),
                        new CodeVariableReferenceExpression(variableName),
                        result.Expression,
                        new CodeMethodReferenceExpression(
                            new CodeThisReferenceExpression(),
                            FormatUniqueName(fieldName, BindMethodSuffix)
                        )
                    )
                )
            );
        }
    }
}
