using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace Parichay.Common
{
    public static class ExpressionBuilder
    {
        private readonly static  MethodInfo miTL = typeof(String).GetMethod("ToLower", Type.EmptyTypes);
        private readonly static MethodInfo containsMethod = typeof(string).GetMethod("Contains", new Type[] { typeof(string) });
        private readonly static MethodInfo startsWithMethod =
        typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
        private readonly static MethodInfo endsWithMethod =
        typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });
        public static Expression<Func<T, bool>> GetExpression<T>(IList<Filter> filters)
        {
            if (filters.Count == 0)
                return null;
            ParameterExpression param = Expression.Parameter(typeof(T), "t");
            Expression exp = null;
            if (filters.Count == 1)
                exp = GetExpression<T>(param, filters[0]);
            else if (filters.Count == 2)
                exp = GetExpression<T>(param, filters[0], filters[1]);
            else
            {
                while (filters.Count > 0)
                {
                    var f1 = filters[0];
                    var f2 = filters[1];
                    if (exp == null)
                        exp = GetExpression<T>(param, filters[0], filters[1]);
                    else
                        exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0], filters[1]));
                    filters.Remove(f1);
                    filters.Remove(f2);
                    if (filters.Count == 1)
                    {
                        exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0]));
                        filters.RemoveAt(0);
                    }
                }
            }
            return Expression.Lambda<Func<T, bool>>(exp, param);
        }
        private static Expression GetExpression<T>(ParameterExpression param, Filter filter)
        {
            try
            {
                MemberExpression member = Expression.Property(param, filter.PropertyName);
                Type type = typeof(Filter);
                PropertyInfo propertyInfo = type.GetProperty("Value");
                propertyInfo.SetValue(filter, Convert.ChangeType(filter.Value, member.Type), null);
                ConstantExpression constant = Expression.Constant(filter.Value);
                var memberExpression = member;
                switch (filter.Operation)
                {
                    case CommonEnum.Op.Equals:
                        return Expression.Equal(member, constant);
                    case CommonEnum.Op.GreaterThan:
                        return Expression.GreaterThan(member, constant);
                    case CommonEnum.Op.GreaterThanOrEqual:
                        return Expression.GreaterThanOrEqual(member, constant);
                    case CommonEnum.Op.LessThan:
                        return Expression.LessThan(member, constant);
                    case CommonEnum.Op.LessThanOrEqual:
                        return Expression.LessThanOrEqual(member, constant);
                    case CommonEnum.Op.Contains:
                        var dynamicExpression = Expression.Call(memberExpression, miTL);
                        constant = Expression.Constant(filter.Value.ToString().ToLower());
                        return Expression.Call(dynamicExpression, containsMethod, constant);
                    case CommonEnum.Op.StartsWith:
                        return Expression.Call(member, startsWithMethod, constant);
                    case CommonEnum.Op.EndsWith:
                        return Expression.Call(member, endsWithMethod, constant);
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private static BinaryExpression GetExpression<T>
        (ParameterExpression param, Filter filter1, Filter filter2)
        {
            Expression bin1 = GetExpression<T>(param, filter1);
            Expression bin2 = GetExpression<T>(param, filter2);
            return Expression.AndAlso(bin1, bin2);
        }
        private static Expression GetConvertedSource(ParameterExpression sourceParameter, PropertyInfo sourceProperty, TypeCode typeCode)
        {
            var sourceExpressionProperty = Expression.Property(sourceParameter, sourceProperty);
            var changeTypeCall = Expression.Call(typeof(Convert).GetMethod("ChangeType", new[] { typeof(object), typeof(TypeCode) }), sourceExpressionProperty, Expression.Constant(typeCode));
            Expression convert = Expression.Convert(changeTypeCall, Type.GetType("System." + typeCode));
            var convertExpr = Expression.Condition(Expression.Equal(sourceExpressionProperty, Expression.Constant(null, sourceProperty.PropertyType)), Expression.Default(Type.GetType("System." + typeCode)), convert);
            return convertExpr;
        }
    }
    [Serializable]
    public class Filter
    {
        public string PropertyName { get; set; }
        public CommonEnum.Op Operation { get; set; }
        public object Value { get; set; }
    }
}