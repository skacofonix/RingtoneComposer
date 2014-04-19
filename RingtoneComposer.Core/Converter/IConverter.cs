namespace RingtoneComposer.Core.Converter
{
    interface IConverter<T>
        where T : new()
    {
        string ToString(T t);

        T Parse(string s);
    }
}
