namespace DB1.Core.DomainObjects;

public interface IEntity { }

public interface IEntity<T> : IEntity
{
    T Id { get; set; }
}
