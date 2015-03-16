using Neptuo.ComponentModel;
using Neptuo.Templates.Compilation.CodeGenerators;
using Neptuo.Templates.Compilation.CodeObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Templates.Compilation.CodeGenerators
{
    /// <summary>
    /// Component generator which delegates execution to sub generator.
    /// </summary>
    public class CodeDomDelegatingObjectGenerator : CodeDomObjectGeneratorBase<ITypeCodeObject>
    {
        private readonly ICodeDomObjectGenerator controlGenerator;
        private readonly ICodeDomObjectGenerator extensionGenerator;
        private readonly ICodeDomObjectGenerator objectGenerator;

        public CodeDomDelegatingObjectGenerator(IUniqueNameProvider nameProvider)
        {
            controlGenerator = new CodeDomControlObjectGenerator(nameProvider);
            extensionGenerator = new CodeDomValueExtensionObjectGenerator(nameProvider);
            objectGenerator = new CodeDomComponentObjectGenerator(nameProvider);
        }

        protected override ICodeDomObjectResult Generate(ICodeDomObjectContext context, ITypeCodeObject codeObject)
        {
            if (typeof(IValueExtension).IsAssignableFrom(codeObject.Type))
                return extensionGenerator.Generate(context, codeObject);
            else if (typeof(IControl).IsAssignableFrom(codeObject.Type))
                return controlGenerator.Generate(context, codeObject);
            else
                return objectGenerator.Generate(context, codeObject);
        }
    }
}
