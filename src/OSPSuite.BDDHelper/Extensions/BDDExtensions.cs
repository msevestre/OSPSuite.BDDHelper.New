using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace OSPSuite.BDDHelper.Extensions
{
   /// <summary>
   ///    Useful class to assert that an exception is raised
   /// </summary>
   public static class The
   {
      /// <summary>
      ///    Returns the action given as parameter to enable a syntax such as The.Action(xx).ShouldTrowAn...
      /// </summary>
      public static Action Action(Action action)
      {
         return action;
      }

      /// <summary>
      ///    Returns the action given as parameter to enable a syntax such as The.Action(xx).ShouldTrowAn...
      /// </summary>
      public static Func<Task> Action(Func<Task> action)
      {
         return action;
      }
   }

   /// <summary>
   ///    Extensions to encapsulate Unit test framework behaviour
   /// </summary>
   public static class BDDExtensions
   {
      /// <summary>
      ///    Asserts that <paramref name="item" /> is greater than <paramref name="expected" />.
      /// </summary>
      public static void ShouldBeGreaterThan<T>(this T item, T expected) where T : IComparable<T>
      {
         (item.CompareTo(expected) > 0).ShouldBeTrue();
      }

      /// <summary>
      ///    Asserts that <paramref name="actual" /> is greater or equal to <paramref name="expected" />.
      /// </summary>
      public static void ShouldBeGreaterThanOrEqualTo<T>(this T actual, T expected) where T : IComparable<T>
      {
         (actual.CompareTo(expected) >= 0).ShouldBeTrue();
      }

      /// <summary>
      ///    Asserts that <paramref name="item" /> is smaller than <paramref name="expected" />.
      /// </summary>
      public static void ShouldBeSmallerThan<T>(this T item, T expected) where T : IComparable<T>
      {
         (item.CompareTo(expected) < 0).ShouldBeTrue();
      }

      /// <summary>
      ///    Asserts that <paramref name="item" /> is smaller or equal to <paramref name="expected" />.
      /// </summary>
      public static void ShouldBeSmallerThanOrEqualTo<T>(this T item, T expected) where T : IComparable<T>
      {
         (item.CompareTo(expected) <= 0).ShouldBeTrue();
      }

      /// <summary>
      ///    Asserts that <paramref name="items" /> only contains the element found in  <paramref name="itemsToFind" />.
      /// </summary>
      public static void ShouldOnlyContain<T>(this IEnumerable<T> items, IEnumerable<T> itemsToFind)
      {
         ShouldOnlyContain(items, itemsToFind.ToArray());
      }

      /// <summary>
      ///    Asserts that <paramref name="items" /> only contains the element found in  <paramref name="itemsToFind" />.
      /// </summary>
      public static void ShouldOnlyContain<T>(this IEnumerable<T> items, params T[] itemsToFind)
      {
         var results = new List<T>(items);
         results.Count.ShouldBeEqualTo(itemsToFind.Length);
         foreach (var itemToFind in itemsToFind)
         {
            results.Contains(itemToFind).ShouldBeTrue();
         }
      }

      /// <summary>
      ///    Asserts that <paramref name="items" /> contains the element found in  <paramref name="itemsToFind" />.
      ///    The items enumerable may contain more elements than <paramref name="itemsToFind" />
      /// </summary>
      public static void ShouldContain<T>(this IEnumerable<T> items, params T[] itemsToFind)
      {
         var results = new List<T>(items);
         foreach (var itemToFind in itemsToFind)
         {
            results.Contains(itemToFind).ShouldBeTrue();
         }
      }

      /// <summary>
      ///    Asserts that <paramref name="items" /> does not contain any element found in <paramref name="itemsToFind" />
      /// </summary>
      public static void ShouldNotContain<T>(this IEnumerable<T> items, params T[] itemsToFind)
      {
         var results = new List<T>(items);
         foreach (var itemToFind in itemsToFind)
         {
            results.Contains(itemToFind).ShouldBeFalse();
         }
      }

      /// <summary>
      ///    Asserts that <paramref name="items" /> has no element.
      /// </summary>
      public static void ShouldBeEmpty<T>(this IEnumerable<T> items)
      {
         items.Any().ShouldBeFalse();
      }

      /// <summary>
      ///    Asserts that <paramref name="items" /> has at least one element.
      /// </summary>
      public static void ShouldNotBeEmpty<T>(this IEnumerable<T> items)
      {
         items.Any().ShouldBeTrue();
      }

      /// <summary>
      ///    Asserts that <paramref name="items" /> only contains the element found in  <paramref name="itemsToFind" /> and in
      ///    the
      ///    same order.
      /// </summary>
      public static void ShouldOnlyContainInOrder<T>(this IEnumerable<T> items, IEnumerable<T> itemsToFind)
      {
         ShouldOnlyContainInOrder(items, itemsToFind.ToArray());
      }

      /// <summary>
      ///    Asserts that <paramref name="items" /> only contains the element found in  <paramref name="itemsToFind" /> and in
      ///    the
      ///    same order.
      /// </summary>
      public static void ShouldOnlyContainInOrder<T>(this IEnumerable<T> items, params T[] itemsToFind)
      {
         var results = new List<T>(items);
         results.ShouldHaveSameLengthAs(itemsToFind);
         for (var i = 0; i < itemsToFind.Count(); i++)
         {
            results[i].ShouldBeEqualTo(itemsToFind[i]);
         }
      }

      /// <summary>
      ///    Asserts that <paramref name="item" /> is true and logs the <paramref name="message" /> otherwise if defined
      /// </summary>
      public static void ShouldBeTrue(this bool item, string message = null)
      {
         item.ShouldBeEqualTo(true, message);
      }

      /// <summary>
      ///    Asserts that <paramref name="item" /> is false and logs the <paramref name="message" /> otherwise if defined
      /// </summary>
      public static void ShouldBeFalse(this bool item, string message = null)
      {
         item.ShouldBeEqualTo(false, message);
      }

      /// <summary>
      ///    Asserts that <paramref name="actual" /> is equal to <paramref name="expected" /> using the object.Equals comparer.
      ///    Logs the <paramref name="message" /> if not equal and if defined
      /// </summary>
      public static void ShouldBeEqualTo<T>(this T actual, T expected, string message = null)
      {
         if (shouldCheckReferences<T>())
            Assert.AreSame(expected, actual, message);
         else
            Assert.AreEqual(expected, actual, message);
      }

      /// <summary>
      ///    Asserts that the double array <paramref name="actual" /> is equal to the double array <paramref name="expected" />
      ///    within the
      ///    given relative tolerance.
      /// </summary>
      public static void ShouldBeEqualTo(this double[] actual, double[] expected, double relTol, string message = null)
      {
         actual.ShouldHaveSameLengthAs(expected);
         for (var i = 0; i < expected.Count(); i++)
         {
            actual[i].ShouldBeEqualTo(expected[i], relTol, message);
         }
      }

      /// <summary>
      ///    Asserts that the double <paramref name="actual" /> is equal to the double <paramref name="expected" /> within the
      ///    given relative tolerance.
      /// </summary>
      public static void ShouldBeEqualTo(this double actual, double expected, double relTol, string message = null)
      {
         string error = $"{actual} and {expected} are not equal within relative tolerance {relTol}.";
         if (!string.IsNullOrEmpty(message))
            error = $"{message}. {error}";

         relativeDeviationWithinTolerance(actual, expected, relTol).ShouldBeTrue(error);
      }

      /// <summary>
      ///    Asserts that the float array <paramref name="actual" /> is equal to the float array <paramref name="expected" />
      ///    within the
      ///    given relative tolerance.
      /// </summary>
      public static void ShouldBeEqualTo(this float[] actual, float[] expected, double relTol, string message = null)
      {
         actual.ShouldHaveSameLengthAs(expected);
         for (var i = 0; i < expected.Count(); i++)
         {
            actual[i].ShouldBeEqualTo(expected[i], relTol, message);
         }
      }

      /// <summary>
      ///    Asserts that the float <paramref name="actual" /> is equal to the float <paramref name="expected" /> within the
      ///    given relative tolerance.
      /// </summary>
      public static void ShouldBeEqualTo(this float actual, float expected, double relTol, string message = null)
      {
         string error = $"{actual} and {expected} are not equal within relative tolerance {relTol}.";
         if (!string.IsNullOrEmpty(message))
            error = $"{message}. {error}";

         relativeDeviationWithinTolerance(actual, expected, relTol).ShouldBeTrue(error);
      }

      /// <summary>
      ///    Asserts that the string <paramref name="actual" /> is the null or empty string
      /// </summary>
      public static void ShouldBeNullOrEmpty(this string actual, string message = null)
      {
         string.IsNullOrEmpty(actual).ShouldBeTrue(message);
      }

      /// <summary>
      ///    Asserts that the <paramref name="actual" /> and <paramref name="expected"/> list have the same length
      /// </summary>
      public static void ShouldHaveSameLengthAs<T>(this IReadOnlyList<T> actual, IReadOnlyList<T> expected)
      {
         actual.Count.ShouldBeEqualTo(expected.Count, $"Length are not equal ({actual.Count} vs {expected.Count})");
      }

      private static bool shouldCheckReferences<T>()
      {
         var type = typeof(T);

         if (type.IsValueType)
            return false;

         if (type == typeof(string))
            return false;

         if (type.IsArray)
            return false;

         if (!typeof(IEnumerable).IsAssignableFrom(type))
            return false;

         return !isPrimitiveEnumerableType(type);
      }

      private static bool isPrimitiveEnumerableType(Type enumerableType)
      {
         if (!enumerableType.IsGenericType)
            return false;

         var genericType = enumerableType.GetGenericTypeDefinition();
         return
            typeof(IEnumerable<>) == genericType ||
            typeof(List<>).IsAssignableFrom(genericType) ||
            typeof(IList<>).IsAssignableFrom(genericType) ||
            typeof(IReadOnlyList<>).IsAssignableFrom(genericType);
      }

      /// <summary>
      ///    Asserts that <paramref name="actual" /> is not equal to <paramref name="expected" /> using the object.Equals
      ///    comparer.
      ///    Logs the <paramref name="message" /> if not and if defined
      /// </summary>
      public static void ShouldNotBeEqualTo<T>(this T actual, T expected, string message = null)
      {
         if (shouldCheckReferences<T>())
            Assert.AreNotSame(expected, actual, message);
         else
            Assert.AreNotEqual(expected, actual, message);
      }

      /// <summary>
      ///    Asserts that <paramref name="workToPerform" /> throws an exception when performed.
      /// </summary>
      public static void ShouldThrowAn<TException>(this Action workToPerform) where TException : Exception
      {
         var resultingException = getExceptionFromPerforming(workToPerform);
         resultingException.ShouldNotBeNull();
         resultingException.ShouldBeAnInstanceOf<TException>();
      }

      /// <summary>
      ///    Asserts that <paramref name="workToPerform" /> throws an exception when performed.
      /// </summary>
      public static void ShouldThrowAn<TException>(this Func<Task> workToPerform) where TException : Exception
      {
         try
         {
            workToPerform().Wait();
            true.ShouldBeFalse("Exception not thrown as expected");
         }
         catch (TException)
         {
         }
         catch (AggregateException ex)
         {
            ex.InnerExceptions.FirstOrDefault(e => e is TException).ShouldNotBeEqualTo(null, $"Exception of type {typeof(TException)} not found");
         }
      }

      /// <summary>
      ///    Asserts that <paramref name="item" /> is an instance of <typeparamref name="T" />.
      /// </summary>
      public static void ShouldBeAnInstanceOf<T>(this object item)
      {
         ShouldBeAnInstanceOf(item, typeof(T));
      }

      /// <summary>
      ///    Asserts that <paramref name="item" /> is an instance of <paramref name="type" />
      /// </summary>
      public static void ShouldBeAnInstanceOf(this object item, Type type)
      {
         Assert.IsInstanceOf(type, item);
      }

      /// <summary>
      ///    Asserts that <paramref name="item" /> is not null.
      /// </summary>
      public static void ShouldNotBeNull(this object item)
      {
         Assert.IsNotNull(item);
      }

      /// <summary>
      ///    Asserts that <paramref name="item" /> is null.
      /// </summary>
      public static void ShouldBeNull(this object item)
      {
         Assert.IsNull(item);
      }

      private static Exception getExceptionFromPerforming(Action work)
      {
         try
         {
            work();
            return null;
         }
         catch (Exception e)
         {
            return e;
         }
      }

      private static bool relativeDeviationWithinTolerance(double value1, double value2, double relTol)
      {
         if (double.IsNaN(value1) && double.IsNaN(value2))
            return true;

         if (double.IsPositiveInfinity(value1) && double.IsPositiveInfinity(value2))
            return true;

         if (double.IsNegativeInfinity(value1) && double.IsNegativeInfinity(value2))
            return true;

         double absMinValue = Math.Abs(value1) <= Math.Abs(value2) ? Math.Abs(value1) : Math.Abs(value2);
         if (absMinValue == 0)
            return value1 == value2;

         double deviation = Math.Abs(value1 - value2) / absMinValue;
         return deviation <= relTol;
      }
   }
}