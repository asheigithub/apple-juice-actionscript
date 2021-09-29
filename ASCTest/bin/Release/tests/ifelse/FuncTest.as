
package {
	[Doc]
	public class FuncTest{};
}
//以下示例使用 if 和 else 语句的组合来对 score_txt 和特定的值进行比较： 
var score_txt={"text":70};

if (score_txt.text>90) { 
	trace("A"); 
} 
else if (score_txt.text>75) { 
	trace("B"); 
} 
else if (score_txt.text>60) { 
	trace("C"); 
} 
else { 
	trace("F"); 
}