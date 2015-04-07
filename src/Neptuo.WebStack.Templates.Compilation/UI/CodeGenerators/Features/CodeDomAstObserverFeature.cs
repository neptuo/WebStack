using Neptuo.Templates.Compilation.CodeGenerators;
using Neptuo.Templates.Compilation.CodeObjects;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Templates.Compilation.UI.CodeGenerators
{
    /// <summary>
    /// Feature generator for generating and registering control observers.
    /// </summary>
    public class CodeDomAstObserverFeature
    {
        /// <summary>
        /// Generates code for attached observers on <paramref name="codeObject"/> and
        /// returns statements for registering whese on <paramref name="fieldName"/>.
        /// </summary>
        /// <param name="context">Generator context.</param>
        /// <param name="codeObject">Code object with control observers.</param>
        /// <param name="fieldName">Name of where observers should be attached.</param>
        /// <returns>Enumeration of statements for registering observers on <paramref name="fieldName"/>.</returns>
        public IEnumerable<CodeStatement> Generate(ICodeDomContext context, IObserversCodeObject codeObject, string fieldName)
        {
            List<CodeStatement> statements = new List<CodeStatement>();
            foreach (ICodeObject observer in codeObject.Observers)
            {
                ICodeDomObjectResult result = context.Registry.WithObjectGenerator().Generate(
                    context.CreateObjectContext().AddObserverTarget(fieldName), 
                    observer
                );
                if (result == null)
                    return null;

                if (result.HasExpression())
                    statements.Add(new CodeExpressionStatement(result.Expression));
                else if (result.HasStatement())
                    statements.Add(result.Statement);
            }

            return statements;
        }
    }
}
