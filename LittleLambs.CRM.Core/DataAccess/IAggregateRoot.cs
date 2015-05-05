namespace LittleLambs.CRM.Core.Entities
{
	public interface IAggregateRoot<out T>
	{
		T Id { get; }
		bool CanBeSaved { get; }
		bool CanBeDeleted { get; }
	}
}
