package {
	[Doc]
	public class FuncTest
	{
	}
}
//下例定义 Team 类。Team 类包括用于在该类内检索和设置属性的 getter 和 setter 方法： 
class Team { 
		var teamName:String; 
		var teamCode:String; 
		var teamPlayers:Array = new Array(); 
		public function Team(param_name:String, param_code:String) { 
			teamName = param_name; 
			teamCode = param_code; 
		} 
		public function get name():String { 
			return teamName; 
		} 
		public function set name(param_name:String):void { 
			teamName = param_name; 
		}
	} 
	//在脚本中输入下面的代码： 
	
var giants:Team = new Team("San Fran", "SFO"); 
trace(giants.name); 
giants.name = "San Francisco"; 
trace(giants.name); 
/*
San Fran San Francisco */
