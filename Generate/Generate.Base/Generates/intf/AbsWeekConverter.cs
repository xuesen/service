namespace IIMes.Services.Generate.intf
{
    public abstract class AbsWeekConverter : AbsTimeConverter
    {
        protected string _weekRule = null;

        public AbsWeekConverter(int bit, char padChar, string fieldKey, string weekRule)
            : base(bit, padChar, fieldKey)
        {
            _weekRule = weekRule;
        }
    }
}
