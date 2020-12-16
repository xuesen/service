namespace IIMes.Services.Generate.intf
{
    public abstract class AbsCommonConverter : AbsConverter<string>
    {
        public AbsCommonConverter(int bit, char padChar, string fieldKey, int minBit = 0)
            : base(bit, padChar, fieldKey, minBit)
        {

        }

    }
}
