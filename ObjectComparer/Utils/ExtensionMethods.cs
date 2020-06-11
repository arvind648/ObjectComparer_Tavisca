using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ObjectComparer.Utils
{
    public static class ExtensionMethods
    {
        #region MemberInfo
        public static Type GetMemberType(this MemberInfo memberInfo)
        {
            var propertyInfo = memberInfo as PropertyInfo;
            if (propertyInfo != null)
            {
                return propertyInfo.PropertyType;
            }

            var fieldInfo = memberInfo as FieldInfo;
            if (fieldInfo != null)
            {
                return fieldInfo.FieldType;
            }

            throw new Exception("Unsupported Type");
        }

        public static object GetMemberValue(this MemberInfo memberInfo, object obj)
        {
            var propertyInfo = memberInfo as PropertyInfo;
            if (propertyInfo != null)
            {
                try
                {
                    return propertyInfo.GetValue(obj);
                }
                catch
                {
                    return $"Unable to get value of property {memberInfo.Name} of type {memberInfo.DeclaringType}";
                }
            }

            var fieldInfo = memberInfo as FieldInfo;
            if (fieldInfo != null)
            {
                return fieldInfo.GetValue(obj);
            }

            throw new Exception("Unsupported Type");
        }

        public static string GetMethodName<T>(Expression<Action<T>> expression)
        {
            return ((MethodCallExpression)expression.Body).Method.Name;
        }
        #endregion

        #region Type
        public static bool InheritsFrom(this Type t1, Type t2)
        {
            if (null == t1 || null == t2)
            {
                return false;
            }

            if (t1 == t2)
            {
                return true;
            }

            if (t1.GetTypeInfo().IsGenericType && t1.GetTypeInfo().GetGenericTypeDefinition() == t2)
            {
                return true;
            }

            if (t1.GetTypeInfo().GetInterfaces().Any(i => i.GetTypeInfo().IsGenericType && i.GetGenericTypeDefinition() == t2 || i == t2))
            {
                return true;
            }

            return t1.GetTypeInfo().BaseType != null &&
                   InheritsFrom(t1.GetTypeInfo().BaseType, t2);
        }

        public static bool IsComparable(this Type type)
        {
            return type.GetTypeInfo().IsPrimitive ||
                   type.GetTypeInfo().IsEnum ||
                   type.InheritsFrom(typeof(IComparable)) ||
                   type.InheritsFrom(typeof(IComparable<>));
        }

        public static object GetDefaultValue(this Type t)
        {
            if (t.GetTypeInfo().IsValueType && Nullable.GetUnderlyingType(t) == null)
            {
                return Activator.CreateInstance(t);
            }

            return null;
        }

        #endregion

        #region Helper
        public static MemberInfo GetMemberInfo<T>(Expression<Func<T>> memberLambda)
        {
            MemberExpression exp;

            switch (memberLambda.Body)
            {
                case UnaryExpression body:
                    var unExp = body;
                    if (unExp.Operand is MemberExpression operand)
                    {
                        exp = operand;
                    }
                    else
                    {
                        throw new ArgumentException();
                    }

                    break;
                case MemberExpression _:
                    exp = (MemberExpression)memberLambda.Body;
                    break;
                default:
                    throw new ArgumentException();
            }

            return exp.Member;
        }
        #endregion

        #region Generic
        public static bool IsNullOrDefault<T>(this Nullable<T> value) where T : struct
        {
            return default(T).Equals(value.GetValueOrDefault());
        }
        public static bool IsNullOrDefault<T>(this T value)
        {
            return object.Equals(value, default(T));
        }
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            if (source == null) return true;
            if (source.Any()) return false;
            return true;
        }


        #endregion
    }
}
