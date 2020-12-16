namespace IIMes.Services.Generate.intf
{
    public abstract class AbsYearConverter : AbsTimeConverter
    {
        public AbsYearConverter(int bit, char padChar, string fieldKey)
            : base(bit, padChar, fieldKey)
        {

        }
    }
}
