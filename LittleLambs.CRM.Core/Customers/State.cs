using Value;

namespace LittleLambs.CRM.Core.Entities
{
	public sealed class State : ValueObject
	{
		private readonly string _abbreviation;
		private readonly string _name;

		public State(string abbreviation, string name)
		{
			_abbreviation = abbreviation;
			_name = name;
		}

		public string Abbreviation
		{
			get { return _abbreviation; }
		}

		public string Name
		{
			get { return _name; }
		}
	}
}