namespace HealthSystem
{
	public interface IHealth
	{
		public int Health { get;}
		public void DecreaseHealth(int amount = 1);
	}
}