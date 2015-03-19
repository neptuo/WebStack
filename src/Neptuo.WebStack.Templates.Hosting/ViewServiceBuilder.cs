using Neptuo.ComponentModel;
using Neptuo.Compilers;
using Neptuo.Templates.Compilation;
using Neptuo.Templates.Compilation.AssemblyScanning;
using Neptuo.Templates.Compilation.CodeCompilers;
using Neptuo.Templates.Compilation.CodeGenerators;
using Neptuo.Templates.Compilation.CodeObjects;
using Neptuo.Templates.Compilation.Parsers;
using Neptuo.Templates.Compilation.Parsers.Normalization;
using Neptuo.Templates.Compilation.ViewActivators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neptuo.WebStack.Templates.Compilation.CodeGenerators;
using Neptuo.WebStack.Templates.UI;

namespace Neptuo.WebStack.Templates.Hosting
{
    public static class ViewServiceFactory
    {
        public static IViewService BuildViewService(string tempDirectory, string binDirectory)
        {
            // Name normalizer for components/controls.
            INameNormalizer componentNormalizer = new CompositeNameNormalizer(
                new SuffixNameNormalizer("Control"),
                new LowerInvariantNameNormalizer()
            );

            INameNormalizer observerNormalizer = new CompositeNameNormalizer(
                new SuffixNameNormalizer("Observer"),
                new LowerInvariantNameNormalizer()
            );

            // Name normalizer for tokens.
            INameNormalizer tokenNormalizer = new CompositeNameNormalizer(
                new SuffixNameNormalizer("Extension"),
                new LowerInvariantNameNormalizer()
            );

            // Create extensible parser registry.
            IParserRegistry parserRegistry = new DefaultParserRegistry()
                .AddPropertyNormalizer(new LowerInvariantNameNormalizer())
                .AddTypeScanner(
                    new TypeScanner()
                        .AddTypeFilterNotAbstract()
                        .AddTypeFilterNotInterface()
                        .AddAssembly("ui", "Neptuo.WebStack.Templates.UI", "Neptuo.WebStack.Templates")
                        .AddEmptyPrefix("data", "Observers")
                )
                .AddContentBuilderRegistry(
                    new ContentBuilderRegistry(componentNormalizer)
                        .AddGenericControlSearchHandler<GenericContentControl>(c => c.TagName)
                        .AddRootBuilder<GeneratedView>(v => v.Content)
                )
                .AddObserverBuilder(
                    new ObserverBuilderRegistry(observerNormalizer)
                        .AddHtmlAttributeBuilder<IHtmlAttributeCollectionAware>(c => c.HtmlAttributes)
                )
                .AddPropertyBuilder(
                    new ContentPropertyBuilderRegistry()
                        .AddSearchHandler(propertyInfo => new TypeDefaultPropertyBuilder())
                )
                .AddLiteralBuilder(new LiteralBuilder())
                .AddTokenBuilder(new TokenBuilderRegistry(tokenNormalizer))
                .RunTypeScanner();


            // Create code generator.
            IUniqueNameProvider nameProvider = new SequenceUniqueNameProvider("field", 1);
            CodeDomGenerator codeGenerator = new CodeDomGenerator(
                new CodeDomDefaultRegistry()
                    .AddObjectGenerator(
                        new CodeDomObjectGeneratorRegistry()
                            .AddGenerator<CommentCodeObject>(new CodeDomCommentObjectGenerator())
                            .AddGenerator<ComponentCodeObject>(new CodeDomDelegatingObjectGenerator(nameProvider))
                            .AddGenerator<ObserverCodeObject>(new CodeDomObserverObjectGenerator(nameProvider))
                            .AddGenerator<RootCodeObject>(new CodeDomRootObjectGenerator(CodeDomStructureGenerator.Names.EntryPointFieldName))
                            .AddGenerator<PlainValueCodeObject>(new CodeDomLiteralObjectGenerator())
                    )
                    .AddPropertyGenerator(
                        new CodeDomPropertyGeneratorRegistry()
                            .AddGenerator<SetCodeProperty>(new CodeDomSetPropertyGenerator())
                            .AddGenerator<ListAddCodeProperty>(new CodeDomListAddPropertyGenerator())
                            .AddGenerator<DictionaryAddCodeProperty>(new CodeDomDictionaryAddPropertyGenerator())
                    )
                    .AddStructureGenerator(new CodeDomDefaultStructureGenerator()
                        .SetBaseType<GeneratedView>()
                        .AddInterface<IDisposable>()
                        .SetEntryPointName(CodeDomStructureGenerator.Names.CreateViewPageControlsMethod)
                        .AddEntryPointParameter<GeneratedView>(CodeDomStructureGenerator.Names.EntryPointFieldName)
                    )
                    .AddAttributeGenerator(new CodeDomAttributeGeneratorRegistry()
                        .AddDefaultValueGenerator()
                    )
                    .AddTypeConversionGenerator(new CodeDomDefaultTypeConvertionGenerator())
                    .AddVisitor(new CodeDomVisitorRegistry())
                    .AddDependencyGenerator(new CodeDomDependencyProviderGenerator())
                ,
                new CodeDomDefaultConfiguration()
                    .IsDirectObjectResolve(false)
                    .IsAttributeDefaultEnabled(false)
                    .IsPropertyTypeDefaultEnabled(false)
            );

            CodeCompiler codeCompiler = new CodeCompiler();
            codeCompiler.TempDirectory(tempDirectory);
            codeCompiler.IsDebugMode(true);
            codeCompiler.References().AddDirectory(binDirectory);

            DefaultViewService viewService = new DefaultViewService();
            viewService.ParserService
                .AddContentParser("Default", new XmlContentParser(parserRegistry, true))
                .AddValueParser("Default", new PlainValueParser())
                .AddValueParser("Default", new TokenValueParser(parserRegistry));

            viewService.GeneratorService.AddGenerator("CodeDom", codeGenerator);
            viewService.ActivatorService.AddActivator("CodeDom", codeCompiler);
            viewService.CompilerService.AddCompiler("CodeDom", codeCompiler);

            viewService.Pipeline.AddCodeGeneratorService("Default", "CodeDom");
            viewService.Pipeline.AddViewActivatorService("Default", "CodeDom");
            viewService.Pipeline.AddCodeCompilerService("Default", "CodeDom");

            return viewService;
        }
    }
}
