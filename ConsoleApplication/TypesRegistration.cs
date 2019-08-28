using Autofac;
using ConsoleApplication.Operations;

namespace ConsoleApplication
{
    /// <summary>
    /// Класс для настройки DI-контейнера Autofac.
    /// </summary>
    internal static class TypesRegistration
    {
        private const string _calcDivCountOperationName = "calcDivCount";
        private const string _calcPunctuationMarkCountOperationName = "calcPunctuationMarkCount";
        private const string _checkBracesOperationName = "checkBraces";

        private const string cssFileTypeName = "cssFile";
        private const string htmlFileTypeName = "htmlFile";
        private const string otherFileTypeName = "otherFile";

        /// <summary>
        /// Построение DI-контейнера.
        /// </summary>
        /// <returns>DI-контейнер.</returns>
        public static IContainer BuildContainer()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<CalcDivCount>().Named<IOperation>(_calcDivCountOperationName);
            containerBuilder.RegisterType<CalcPunctuationMarkCount>().Named<IOperation>(_calcPunctuationMarkCountOperationName);
            containerBuilder.RegisterType<CheckBraces>().Named<IOperation>(_checkBracesOperationName);

            RegisterFileTypes(containerBuilder);

            containerBuilder.RegisterType<FilesProcessor>()
              .As<IFilesProcessor>()
              .WithParameter(
                (p, c) => true,
                (p, c) => new[]
                {
                  c.ResolveNamed<IFileType>(cssFileTypeName),
                  c.ResolveNamed<IFileType>(htmlFileTypeName),
                  c.ResolveNamed<IFileType>(otherFileTypeName)
                });

            return containerBuilder.Build();
        }

        /// <summary>
        /// Регистрация обрабатываемых типов файлов.
        /// </summary>
        private static void RegisterFileTypes(ContainerBuilder containerBuilder)
        {
            const string extensionParameterName = "extension";
            const string operationsParameterName = "operations";

            containerBuilder.RegisterType<FileType>().Named<IFileType>(cssFileTypeName)
                .WithParameter(
                    (p, c) => p.Name == extensionParameterName,
                    (p, c) => ".css")
                .WithParameter(
                    (p, c) => p.Name == operationsParameterName,
                    (p, c) => new[]
                    {
                      c.ResolveNamed<IOperation>(_checkBracesOperationName)
                    });

            containerBuilder.RegisterType<FileType>().Named<IFileType>(htmlFileTypeName)
                .WithParameter(
                    (p, c) => p.Name == extensionParameterName,
                    (p, c) => ".html")
                .WithParameter(
                    (p, c) => p.Name == operationsParameterName,
                    (p, c) => new[]
                    {
                      c.ResolveNamed<IOperation>(_calcDivCountOperationName)
                    });

            containerBuilder.RegisterType<FileType>().Named<IFileType>(otherFileTypeName)
                .WithParameter(
                    (p, c) => p.Name == extensionParameterName,
                    (p, c) => null)
                .WithParameter(
                    (p, c) => p.Name == operationsParameterName,
                    (p, c) => new[]
                    {
                      c.ResolveNamed<IOperation>(_calcPunctuationMarkCountOperationName)
                    });
        }
    }
}