namespace Prodromoi.Core.Interfaces
{
    public interface IEntity<out TId>
    {
        TId Id { get; }
    }
}