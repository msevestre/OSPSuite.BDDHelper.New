using System;
using NUnit.Framework;

namespace OSPSuite.BDDHelper
{
   /// <summary>
   /// Attribute should be use on TestFixture class that should be recognized as Integration Test.
   /// </summary>
   [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
   public class IntegrationTestsAttribute : CategoryAttribute
   {
      /// <summary>
      /// Constructor setting the name to "IntegrationTests"
      /// </summary>
      public IntegrationTestsAttribute() : base("IntegrationTests")
      {
      }
   }
}