namespace TsCodeDom.Entities
{
    /// <summary>
    /// TsCodeExpression
    /// </summary>
    public abstract class TsCodeExpression : TsCodeBase
    {
        internal abstract string GetSource(TsGeneratorOptions options, TsWriteInformation info);
    }
}
