namespace Prodromoi.DbManager.Features;

public class Guard<T> where T : class
{
    public T Obj { get; }

    internal Guard(T obj)
    {
        Obj = obj;
    }
}

public static class GuardExtensions
{
    public static Guard<T> Guard<T>(this T obj) where T : class
    {
        return new Guard<T>(obj);
    }

}