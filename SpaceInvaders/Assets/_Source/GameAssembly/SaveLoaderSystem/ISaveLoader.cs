namespace SaveLoaderSystem
{
	public interface ISaveLoader
	{
		void Save();
		bool TryLoad();
	}
}