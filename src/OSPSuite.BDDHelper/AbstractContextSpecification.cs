namespace OSPSuite.BDDHelper
{
   /// <summary>
   /// Defines a type dependent context specification used. The variable sut (system under test) 
   /// needs to be initialized in the context or global context.
   /// </summary>
   public abstract class AbstractContextSpecification<T> : StaticContextSpecification
   {
      /// <summary>
      /// system under test
      /// </summary>
      protected T sut;
   }
}