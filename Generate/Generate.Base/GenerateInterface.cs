using System.Collections.Generic;

namespace IIMes.Services.Generate
{
    public interface IGenerating
    {
        AbsGeneratePrinciple Context { get; }

        IGeneratingChannel Channel { get; }

        void Generate();

        IGeneratingResult Result { get; }
    }

    public interface IGeneratingChannel
    {
        List<IGeneratingResult> Generate(AbsGeneratePrinciple context);
    }

    public interface ISubGeneratingChannel
    {
        string Type { get; }

        string TypeStrip { get; }

        IConverter[] Converters(AbsGeneratePrinciple context);

        string[] UnlikeSuffixArr { get; }

        string TypeInDB { get; }
    }
    
    public interface IGeneratingResult
    {
        string[] Result { get; }
    }

    public interface IConverter
    {
        string Convert(object obj);
        bool IsHaveDefautValue();
        object GetDefautValue();
    }

    public interface IValueProvider
    {
        object GetValue(string key);
        void AddValue(string key, object value);
        void RemoveValue(string key);
    }

    public interface IGeneratePrinciple
    {
        ISubGeneratingChannel Current { get; }
    }

    public interface IReferedBox
    {
        string GetMaxSN(string key);
        void SetMaxSN(string key, string sn);
    }
}
