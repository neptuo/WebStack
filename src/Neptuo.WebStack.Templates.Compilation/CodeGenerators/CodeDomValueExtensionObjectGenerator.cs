using Neptuo.ComponentModel;
using Neptuo.Linq.Expressions;
using Neptuo.Templates.Compilation.CodeGenerators;
using Neptuo.Templates.Compilation.CodeObjects;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Templates.Compilation.CodeGenerators
{
    /// <summary>
    /// Component generator for value extensions.
    /// </summary>
    public class CodeDomValueExtensionObjectGenerator : CodeDomComponentObjectGenerator
    {
        public CodeDomValueExtensionObjectGenerator(IUniqueNameProvider nameProvider)
            : base(nameProvider)
        { }

        protected override ICodeDomObjectResult Generate(ICodeDomObjectContext context, ComponentCodeObject codeObject, string fieldName)
        {
            ICodeDomObjectResult result = base.Generate(context, codeObject, fieldName);

            if (result != null && result.HasExpression())
            {
                Type returnType = GetExtensionProvideValueReturnType(codeObject.Type);
                CodeExpression expression = new CodeMethodInvokeExpression(
                    result.Expression,
                    TypeHelper.MethodName<IValueExtension, IValueExtensionContext, object>(e => e.ProvideValue),
                    GenerateValueExtensionContext(context, codeObject, fieldName)
                );

                if(returnType != typeof(object))
                {
                    expression = new CodeCastExpression(
                        returnType,
                        expression
                    );
                }

                return new CodeDomDefaultObjectResult(expression, returnType);
            }

            return result;
        }

        protected Type GetExtensionProvideValueReturnType(Type extensionType)
        {
            ReturnTypeAttribute attribute = extensionType.GetCustomAttribute<ReturnTypeAttribute>();
            if (attribute != null)
                return attribute.Type;

            return typeof(object);
        }

        protected CodeExpression GenerateValueExtensionContext(ICodeDomObjectContext context, ComponentCodeObject codeObject, string fieldName)
        {
            CodeExpression propertyTarget = null;
            context.TryGetPropertyTarget(out propertyTarget);
            
            CodeExpression propertyInfo = new CodePrimitiveExpression(null);
            ICodeProperty codeProperty;
            if(context.TryGetCodeProperty(out codeProperty)) 
            {
                if (propertyTarget != null)
                {
                    propertyInfo = new CodeMethodInvokeExpression(
                        new CodeMethodInvokeExpression(
                            propertyTarget,
                            TypeHelper.MethodName<Type, Type>(t => t.GetType)
                        ),
                        TypeHelper.MethodName<Type, string, PropertyInfo>(t => t.GetProperty),
                        new CodePrimitiveExpression(codeProperty.Property.Name)
                    );
                }
                else
                {
                    propertyTarget = new CodePrimitiveExpression(null);
                }
            }

            return new CodeObjectCreateExpression(
                new CodeTypeReference(typeof(DefaultValueExtensionContext)),
                propertyTarget,
                propertyInfo,
                new CodeFieldReferenceExpression(
                    new CodeThisReferenceExpression(),
                    "dependencyProvider"
                )
            );
        }
    }
}
