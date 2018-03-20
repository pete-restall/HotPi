namespace Restall.HotPi
{
	public interface IMapper<in TFrom, out TTo>
	{
		TTo Map(TFrom obj);
	}
}
