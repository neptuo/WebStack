using Neptuo.FileSystems;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.WebStack.Http
{
    /// <summary>
    /// Extensions for <see cref="IHttpFile"/>.
    /// </summary>
    public static class _HttpFileExtensions
    {
        /// <summary>
        /// Saves <paramref name="httpFile"/> into <paramref name="targetDirectory"/> with name <see cref="IHttpFile.FileName"/>.
        /// </summary>
        /// <param name="httpFile">Posted file to save.</param>
        /// <param name="targetDirectory">Target directory.</param>
        /// <returns>Task.</returns>
        public static async Task SaveAsync(this IHttpFile httpFile, IDirectory targetDirectory)
        {
            Guard.NotNull(httpFile, "httpFile");
            Guard.NotNull(targetDirectory, "targetDictionary");

            IFile file = await targetDirectory.CreateFile(httpFile.FileName);
            await file.SetContentFromStreamAsync(httpFile.Content);
        }
    }
}
