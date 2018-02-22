namespace Nett.Parser.Productions
{
    internal abstract class Parsed<T, I>
    {
        public Parsed(T value, I info)
        {
            this.Value = value;
            this.Info = info;
        }

        public T Value { get; }

        public I Info { get; }
    }
}
