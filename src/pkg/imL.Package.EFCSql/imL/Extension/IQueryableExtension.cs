using Microsoft.EntityFrameworkCore;

namespace imL.Package.EFCSql
{
    public static class IQueryableExtension
    {
        public static IQueryable<G> AsDefault<G>(this IQueryable<G> _this, bool _ant = true) where G : class
        {
            IQueryable<G> _return = _this.AsQueryable();

            if (_ant)
                _return = _return.AsNoTracking();

            return _return;
        }
        public static IQueryable<G> AsEmpty<G>(this IQueryable<G> _this) where G : class
        {
            return _this.AsDefault().Take(0);
        }
        public static IQueryable<G> UseTableHint<G>(this IQueryable<G> _this, params string[] _hint)
        {
            if (_hint == null || _hint.Length == 0)
                return _this;

            string _join = string.Join(",", _hint);

            return _this.TagWith("TABLE_HINT: " + _join);
        }
        public static IQueryable<G> UseNoTable<G>(this IQueryable<G> _this)
        {
            return _this.TagWith("NO_TABLE");
        }
        public static IQueryable<G> Pagination<G>(this IQueryable<G> _this, int _page = 1, int _take = 32) where G : class
        {
            if (_page <= 0)
                _page = 1;

            if (_take <= 0)
                _take = 1;

            return _this
                .Skip(_take * (_page - 1))
                .Take(_take);
        }
        
    }
}
