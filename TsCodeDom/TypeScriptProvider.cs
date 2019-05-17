using System;
using System.IO;
using System.Text;
using TsCodeDom.Entities;

namespace TsCodeDom
{
    /// <summary>
    /// Typescript Provider
    /// </summary>
    public class TypeScriptProvider
    {
        #region public Methods

        /// <summary>
        /// Generate Code From Namespace
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <param name="writer"></param>
        /// <param name="options"></param>
        public void GenerateCodeFromNamespace(TsCodeNamespace nameSpace, StreamWriter writer)
        {
            GenerateCodeFromNamespace(nameSpace, writer, new TsGeneratorOptions());
        }
        public void GenerateCodeFromNamespace(TsCodeNamespace nameSpace, StreamWriter writer, TsGeneratorOptions options)
        {
            //sec check
            if (nameSpace == null)
            {
                throw new ArgumentNullException("nameSpace");
            }
            if (writer == null)
            {
                throw new ArgumentNullException("writer");   
            }
            if (options == null)
            {
                throw new ArgumentNullException("options");
            }
            //write whole namespace
            nameSpace.WriteSource(writer, options, new TsWriteInformation(0));
        }
        public string GenerateCodeFromNamespace(TsCodeNamespace nameSpace)
        {
            return GenerateCodeFromNamespace(nameSpace, new TsGeneratorOptions());
        }
        public string GenerateCodeFromNamespace(TsCodeNamespace nameSpace, TsGeneratorOptions options)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (StreamWriter stream = new StreamWriter(memoryStream))
                {
                    GenerateCodeFromNamespace(nameSpace, stream, options);
                    stream.Flush();
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }              
            }
        }
        #endregion
    }
}
