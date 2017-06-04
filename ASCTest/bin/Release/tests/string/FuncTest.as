package {
	[Doc]
	public class FuncTest{
		public function FuncTest()
		{
			var str1:String = "Bonjour"; 
			var str2:String = "from"; 
			var str3:String = "Paris"; 
			var str4:String = str1.concat(" ", str2, " ", str3); 

			trace(str4);
			
			var str:String = "Area = "; 
			var area:Number = Math.PI * Math.pow(3, 2); 
			str = str + area; // str == "Area = 28.274333882308138"
			
			trace(str);
			
			trace("Total: $" + 4.55 + 1.45); // output: Total: $4.551.45 
			trace("Total: $" + (4.55 + 1.45)); // output: Total: $6
			
			var myStr:String = String.fromCharCode(104,101,108,108,111,32,119,111,114,108,100,33); 
			// Sets myStr to "hello world!"
			trace(myStr);
			
			
			var str:String = "The moon, the stars, the sea, the land"; 
			trace(str.indexOf("the")); // output: 10
			
			var str:String = "The moon, the stars, the sea, the land" 
			trace(str.lastIndexOf("the")); // output: 30
			
			var str:String = "The moon, the stars, the sea, the land" 
			trace(str.lastIndexOf("the", 29)); // output: 21
			
			var str:String = "Hello from Paris, Texas!!!"; 
			trace(str.slice(11,15)); // output: Pari 
			trace(str.slice(-3,-1)); // output: !! 
			trace(str.slice(-3,26)); // output: !!! 
			trace(str.slice(-3,str.length)); // output: !!! 
			trace(str.slice(-8,-3)); // output: Texas
			
			var queryStr:String = "first=joe&last=cheng&title=manager&StartDate=3/6/65"; 
			var params:Array = queryStr.split("&", 2); // params == ["first=joe","last=cheng"]
			
			trace(params);
			
			var str:String = "Hello from Paris, Texas!!!"; 
			trace(str.substr(11,15)); // output: Paris, Texas!!! 
			trace(str.substring(11,15)); // output: Pari
			
			 var str:String = " JOS¨¦ BAR?A";
			trace(str.toLowerCase()); // jos¨¦ bar?a
			
			var str:String = "Jos¨¦ Bar?a";
			trace(str.toUpperCase()); // JOS¨¦ BAR?A
			
			var str:String = "a\tb\nc";
			trace(str);
			
			
			var companyStr:String = new String("     Company X");
            var productStr:String = "Product Z Basic     ";
            var emptyStr:String = " ";
            var strHelper:StringHelper = new StringHelper();

            var companyProductStr:String = companyStr + emptyStr + productStr;
            trace("'" + companyProductStr + "'");    // '     Company X Product Z Basic     '

            companyProductStr = strHelper.replace(companyProductStr, "Basic", "Professional");
            trace("'" + companyProductStr + "'");    // '     Company X Product Z Professional     '

            companyProductStr = strHelper.trim(companyProductStr, emptyStr);
            trace("'" + companyProductStr + "'");    // 'Company X Product Z Professional'

			
		}
	}
}
class StringHelper {
    public function StringHelper() {
    }

    public function replace(str:String, oldSubStr:String, newSubStr:String):String {
        return str.split(oldSubStr).join(newSubStr);
    }

    public function trim(str:String, char:String):String {
        return trimBack(trimFront(str, char), char);
    }

    public function trimFront(str:String, char:String):String {
        char = stringToCharacter(char);
        if (str.charAt(0) == char) {
            str = trimFront(str.substring(1), char);
        }
        return str;
    }

    public function trimBack(str:String, char:String):String {
        char = stringToCharacter(char);
        if (str.charAt(str.length - 1) == char) {
            str = trimBack(str.substring(0, str.length - 1), char);
        }
        return str;
    }

    public function stringToCharacter(str:String):String {
        if (str.length == 1) {
            return str;
        }
        return str.slice(0, 1);
    }
}

