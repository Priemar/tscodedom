using System.IO;
using System.Text;

namespace TsCodeDom.Entities
{
    public abstract class TsCodeWriteBase : TsCodeBase
    {
        internal abstract void WriteSource(StreamWriter writer, TsGeneratorOptions options, TsWriteInformation info);
        /// <summary>
        /// GetString
        /// </summary>
        /// <param name="options"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        internal string GetStringFromWriteSource(TsGeneratorOptions options, TsWriteInformation info)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new StreamWriter(memoryStream))
                {
                    //writesource
                    WriteSource(writer, options, info);
                    writer.Flush();
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    return Encoding.UTF8.GetString(memoryStream.ToArray());                   
                }
            }
        }
    }
}
