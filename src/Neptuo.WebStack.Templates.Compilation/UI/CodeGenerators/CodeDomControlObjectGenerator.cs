using Neptuo.ComponentModel;
using Neptuo.Linq.Expressions;
using Neptuo.Templates.Compilation.CodeGenerators;
using Neptuo.Templates.Compilation.CodeObjects;
using Neptuo.WebStack.Templates.UI;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Templates.Compilation.UI.CodeGenerators
{
    /// <summary>
    /// Component generator for controls.
    /// </summary>
    public class CodeDomControlObjectGenerator : CodeDomComponentObjectGenerator
    {
        /// <summary>
        /// Suffix of 'bind' method.
        /// </summary>
        protected const string BindMethodSuffix = "Bind";

        public CodeDomControlObjectGenerator(IUniqueNameProvider nameProvider)
            : base(nameProvider)
        {
            IsPropertyAssignmentInCreateMethod = false;
        }

        protected override ICodeDomObjectResult Generate(ICodeDomObjectContext context, ComponentCodeObject codeObject, string fieldName)
        {
            ICodeDomObjectResult result = base.Generate(context, codeObject, fieldName);
            CodeMemberMethod bindMethod = GenerateBindMethod(context, codeObject, fieldName);
            if (bindMethod == null)
                return null;

            // Append bind method right after create method for this field.
            string createMethodName = FormatUniqueName(fieldName, CreateMethodSuffix);
            int createMethodIndex = context.Structure.Class.Members.IndexOf(context.Structure.Class.Members.OfType<CodeMemberMethod>().First(m => m.Name == createMethodName));
            context.Structure.Class.Members.Insert(createMethodIndex + 1, bindMethod);

            return result;
        }

        protected override IEnumerable<CodeStatement> GenerateCreateMethodAdditionalStatements(ICodeDomObjectContext context, ComponentCodeObject codeObject, string fieldName)
        {
            List<CodeStatement> statements = new List<CodeStatement>(base.GenerateCreateMethodAdditionalStatements(context, codeObject, fieldName));

            // Register control to component manager.
            statements.Add(new CodeExpressionStatement(
                new CodeMethodInvokeExpression(
                    new CodeFieldReferenceExpression(
                        new CodeThisReferenceExpression(),
                        "componentManager"
                    ),
                    TypeHelper.MethodName<IComponentManager, IControl, Action<IControl>>(m => m.AddComponent),
                    new CodeVariableReferenceExpression(fieldName),
                    new CodeMethodReferenceExpression(
                        new CodeThisReferenceExpression(),
                        FormatUniqueName(fieldName, BindMethodSuffix)
                    )
                )
            ));

            // Generate and attach observers.
            CodeDomAstObserverFeature generator = new CodeDomAstObserverFeature();
            IEnumerable<CodeStatement> result = generator.Generate(context, codeObject, fieldName);
            if (result == null)
                return null;

            statements.AddRange(result);
            return statements;
        }

        protected virtual CodeMemberMethod GenerateBindMethod(ICodeDomObjectContext context, ComponentCodeObject codeObject, string fieldName)
        {
            // Bind method.
            CodeMemberMethod bindMethod = new CodeMemberMethod()
            {
                Name = FormatUniqueName(fieldName, BindMethodSuffix),
                Attributes = MemberAttributes.Private
            };

            // Bind method parameter.
            bindMethod.Parameters.Add(new CodeParameterDeclarationExpression(
                new CodeTypeReference(codeObject.Type),
                fieldName
            ));

            // Bind AST properties.
            IEnumerable<CodeStatement> statements = GenerateAstPropertyAssignment(context, codeObject, fieldName);
            if (statements == null)
                return null;

            bindMethod.Statements.AddRange(statements);
            return bindMethod;
        }
    }
}
