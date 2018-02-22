namespace Nett.Parser.Info
{
    internal sealed class KeyParseInfo
    {
        public KeyParseInfo(ParseInfo nameInfo, ParseInfo assignmentInfo)
        {
            this.NameInfo = nameInfo;
            this.AssignmentInfo = assignmentInfo;
        }

        public ParseInfo NameInfo { get; }

        public ParseInfo AssignmentInfo { get; }
    }
}
