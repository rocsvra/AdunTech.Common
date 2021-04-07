using AutoMapper;
using System.Collections.Generic;

namespace AdunTech.AutoMapperExtension
{
    public static class Ext4AutoMapper
    {
        internal static Mapper Mapper<TSource, TDestination>()
        {
            MapperConfiguration cfg = new MapperConfiguration(o => o.CreateMap(typeof(TSource), typeof(TDestination)));
            return new Mapper(cfg);
        }

        /// <summary>
        ///  类型映射
        /// </summary>
        public static TDestination Map<TSource, TDestination>(this TSource source)
        {
            if (source == null)
            {
                return default;
            }
            return Mapper<TSource, TDestination>().Map<TDestination>(source);
        }

        /// <summary>
        /// 集合列表类型映射
        /// </summary>
        public static IEnumerable<TDestination> Map<TSource, TDestination>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                return default;
            }
            return Mapper<TSource, TDestination>().Map<IEnumerable<TDestination>>(source);
        }
    }
}
