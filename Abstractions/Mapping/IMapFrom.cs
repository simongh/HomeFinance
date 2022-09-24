using AutoMapper;

namespace HomeFinance.Mapping

{
	public interface IMapFrom<T>
	{
		public void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
	}
}