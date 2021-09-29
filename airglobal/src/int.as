package 
{
	use namespace AS3;
   
  
   public final class int
   {
      public static const MIN_VALUE:int = -2147483648;
      
      public static const MAX_VALUE:int = 2147483647;
      
      public static const length:int = 1;
      
      public function int(value:* = 0)
      {
      }
      
      AS3 function toString(radix:* = 10) : String
      {
         return Number(this).toString(radix);
      }
      
      AS3 function valueOf() : int
      {
         return this;
      }
      
      AS3 function toExponential(p:* = 0) : String
      {
         return Number(this).toExponential(p);
      }
      
      AS3 function toPrecision(p:* = 0) : String
      {
         return Number(this).toPrecision(p);
      }
      
      AS3 function toFixed(p:* = 0) : String
      {
         return Number(this).toFixed(p);
      }
   }

}