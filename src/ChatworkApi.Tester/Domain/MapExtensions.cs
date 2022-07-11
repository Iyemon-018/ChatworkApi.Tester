namespace ChatworkApi.Tester.Domain
{
    using AutoMapper;

    public static class MapExtensions
    {
        private static IMapper _mapper;

        public static void SetMapper(IMapper mapper) => _mapper = mapper;

        public static TDestination Map<TDestination>(this object self)
        {
            return _mapper.Map<TDestination>(self);
        }

        public static void Map(this object self
                             , object      destination)
        {
            _mapper.Map(self, destination);
        }
    }
}