﻿using C2.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2.Core
{
    delegate bool EqualityComparison<T>(T x, T y);

    static class CollectionExtensions
    {
        public static bool IsEmpty(this IEnumerable list)
        {
            if (list == null)
                throw new NullReferenceException();

            if (list is ICollection)
                return ((ICollection)list).Count == 0;

            return !list.GetEnumerator().MoveNext();
        }

        public static bool IsEmpty<T>(this IEnumerable<T> list)
        {
            if (list == null)
                throw new NullReferenceException();

            if (list is ICollection<T>)
                return ((ICollection<T>)list).Count == 0;

            if (list is ICollection)
                return ((ICollection)list).Count == 0;

            return !list.GetEnumerator().MoveNext();
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
        {
            return list == null || IsEmpty(list);
        }

        public static bool IsNull<T>(this IEnumerable<T> list)
        {
            return list == null;
        }
        public static bool Remove(this Dictionary<string, string> list, string k1, string k2, string k3, string k4, string k5)
        {
            return list.Remove(k1) | list.Remove(k2) | list.Remove(k3) | list.Remove(k4) | list.Remove(k5);
        }
        public static bool IsNullOrEmpty(this IEnumerable list)
        {
            return list == null || IsEmpty(list);
        }

        public static bool Exists<T>(this IEnumerable<T> list, Predicate<T> match)
        {
            if (list == null)
                throw new ArgumentNullException();

            foreach (T item in list)
            {
                if (match(item))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool _Contains<T>(this T[] array, T value)
        {
            if (array == null)
                throw new ArgumentNullException();

            var comp = EqualityComparer<T>.Default;
            for (int i = 0; i < array.Length; i++)
                if (comp.Equals(array[i], value))
                    return true;

            return false;
        }

        public static bool _Contains<T>(this IEnumerable<T> list, T value)
        {
            return _Contains<T>(list, value, EqualityComparer<T>.Default);
        }

        public static bool _Contains<T>(this IEnumerable<T> list, T value, EqualityComparer<T> equalityComparer)
        {
            if (list == null)
                throw new ArgumentNullException();

            if (equalityComparer == null)
                equalityComparer = EqualityComparer<T>.Default;

            foreach (var item in list)
            {
                if (equalityComparer.Equals(value, item))
                    return true;
            }

            return false;
        }

        public static bool _Contains<T>(this IEnumerable<T> list, T value, EqualityComparison<T> comparison)
        {
            if (list == null)
                throw new ArgumentNullException();

            foreach (var item in list)
            {
                if (comparison(value, item))
                    return true;
            }

            return false;
        }

        public static T Find<T>(this IEnumerable<T> list, Predicate<T> match)
        {
            if (list == null)
                throw new ArgumentNullException();

            foreach (T item in list)
            {
                if (match(item))
                {
                    return item;
                }
            }

            return default(T);
        }

        public static List<T> FindAll<T>(this IEnumerable<T> list, Predicate<T> match)
        {
            if (list == null)
                throw new NullReferenceException();

            List<T> result = new List<T>();
            foreach (T item in list)
            {
                if (match(item))
                    result.Add(item);
            }
            return result;
        }

        //public static IEnumerable<T> Where<T>(this IEnumerable<T> list, Predicate<T> match)
        //{
        //    if (list == null)
        //        throw new NullReferenceException();

        //    foreach (T item in list)
        //    {
        //        if (match(item))
        //            yield return item;
        //    }
        //}

        //public static T[] ToArray<T>(this IEnumerable<T> source)
        //{
        //    if (source == null)
        //        throw new NullReferenceException();

        //    var list = new List<T>(source);
        //    return list.ToArray();
        //}

        public static bool CollectionEqual<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            if (first == null && second == null)
                return true;
            if (first == null || second == null)
                return false;

            if (first.Count() != second.Count())
                return false;

            foreach (var item in first)
            {
                if (!second._Contains(item))
                    return false;
            }

            return true;
        }

        public static string JoinString<T>(this IEnumerable<T> list)
        {
            return JoinString(list, item => item.ToString(), ",");
        }

        public static string JoinString<T>(this IEnumerable<T> list, Func<T, string> formatter)
        {
            return JoinString<T>(list, formatter, ",");
        }

        public static string JoinString<T>(this IEnumerable<T> list, string separator)
        {
            return JoinString<T>(list, item => item.ToString(), separator);
        }

        public static string JoinString<T>(this IEnumerable<T> list, Func<T, string> formatter, string separator)
        {
            StringBuilder sb = new StringBuilder();
            foreach (T item in list)
            {
                if (item == null)
                    continue;

                if (sb.Length > 0)
                {
                    sb.Append(separator);
                }

                sb.Append(formatter(item));
            }

            return sb.ToString();
        }

        public static string ReverseString(this String str)
        {   
            char[] cs = str.ToCharArray();
            Array.Reverse(cs);
            return new string(cs);
        }
        public static string[] Split(this string str, string separator)
        {
            return str.Split(separator, StringSplitOptions.None);
        }
        public static string[] Split(this string str, string separator, StringSplitOptions options)
        {
            return str.Split(new string[] { separator }, options);
        }
        public static string[] SplitWhitespace(this string str)
        {
            return str.Split(new string[] { "\t", OpUtil.StringBlank }, StringSplitOptions.None);
        }

        public static string[] SplitLine(this string str)
        {
            return str.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static bool In(this string str, string[] vs)
        {
            foreach (string v in vs)
                if (str.Contains(v))
                    return true;
            return false;
        }
    }
}
