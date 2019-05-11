using NUnit.Framework;

namespace OSPSuite.BDDHelper
{
   /// <summary>
   /// Defines a static context specification used for static class or extensions methods
   /// </summary>
   [TestFixture]
   public abstract class StaticContextSpecification
   {
      /// <summary>
      /// The Global context of the test (e.g. TestFixtureSetUp). This function will be called only once
      /// </summary>
      [OneTimeSetUp]
      public virtual void GlobalContext()
      {
      }

      /// <summary>
      /// calls the context and the because at once
      /// </summary>
      [SetUp]
      public void Setup()
      {
         Context();
         Because();
      }

      /// <summary>
      /// The actual action tested in the specification
      /// </summary>
      protected virtual void Because()
      {

      }

      /// <summary>
      /// The context of the test (e.g. SetUp). This function will be call for each observation
      /// </summary>
      protected virtual void Context()
      {

      }

      /// <summary>
      /// Cleanup function to release objects if necessary (e.g. TearDown). This function is called after each observation
      /// </summary>
      [TearDown]
      public virtual void Cleanup()
      {
      }

      /// <summary>
      /// Global Cleanup function to release objects if necessary (e.g. TestFixtureTearDown). This function is called once after all observations were tested
      /// </summary>
      [OneTimeTearDown]
      public virtual void GlobalCleanup()
      {
      }

   }
}