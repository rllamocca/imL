using System;
using System.Collections.Generic;
using System.Linq;

namespace imL.Struct
{
    public struct OneToMany<G1, G2>
    {
        public G1 One { set; get; }
        public IEnumerable<G2> Many { set; get; }

        public OneToMany(G1 _one, IEnumerable<G2> _many)
        {
            One = _one;
            Many = _many;
        }

        public static IEnumerable<OneToMany<G1, G2>> FromLot(IEnumerable<Lot<G1, G2>> _from, Func<G1, IComparable> _key)
        {
            IEnumerable<G1> _ones =
                from _a in _from
                group _a by _a.Lot1 into _group
                select _group.Key;

            IEnumerable<OneToMany<G1, G2>> _return =
                from _a in _ones
                select new OneToMany<G1, G2>()
                {
                    One = _a,
                    Many = from _b in _from
                           where _key(_b.Lot1) == _key(_a)
                           select _b.Lot2
                };

            return _return;
        }

        ////void ReadAndWriteProperty(Func<Class1, T> getProp, Action<Class1, T> setProp)
        //static List<G> SortBy<G>(List<G> toSort, Func<G, IComparable> getProp)
        //{
        //    if (toSort != null && toSort.Count > 0)
        //    {
        //        return toSort
        //            .OrderBy(x => getProp(x))
        //            .ToList();
        //    }
        //    return null;
        //}
        //static List<TSource> OrderByAsListOrNull<TSource, TKey>(this ICollection<TSource> collection, Func<TSource, TKey> keySelector)
        //{
        //    if (collection != null && collection.Count > 0)
        //    {
        //        return collection
        //            .OrderBy(x => keySelector(x))
        //            .ToList();
        //    }

        //    return null;
        //}

        //void DoSomething<T>(Expression<Func<T>> property)
        //{
        //    var propertyInfo = ((MemberExpression)property.Body).Member as PropertyInfo;
        //    if (propertyInfo == null)
        //    {
        //        throw new ArgumentException("The lambda expression 'property' should point to a valid Property");
        //    }
        //}
        //static string Meth<T>(Expression<Func<T>> expression)
        //{
        //    var name = ((MemberExpression)expression.Body).Member.Name;
        //    var value = expression.Compile()();
        //    return string.Format("{0} - {1}", name, value);
        //}
    }
}
