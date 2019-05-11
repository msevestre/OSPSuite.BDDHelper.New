using System.Collections;
using System.Collections.Generic;

namespace OSPSuite.BDDHelper
{
   public class Enumerable<T> : IEnumerable<T>
   {
      private readonly List<T> _list = new List<T>();

      IEnumerator IEnumerable.GetEnumerator()
      {
         return GetEnumerator();
      }

      public IEnumerator<T> GetEnumerator()
      {
         return _list.GetEnumerator();
      }

      public void Add(T item)
      {
         _list.Add(item);
      }
   }
}