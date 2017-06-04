package {
	[Doc]
	public class FuncTest{
		public function FuncTest() {
			traceParams(100, 130, "two"); // 100,130,two
			trace(average(4, 7, 13));     // 8
		}
	}
}


function traceParams(... rest) {
 	trace(rest);
 }
 
function average(... args) : Number{
	var sum:Number = 0;
	for (var i:uint = 0; i < args.length; i++) {
		sum += args[i];
	}
	return (sum / args.length);
}
