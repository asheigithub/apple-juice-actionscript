package
{
	import flash.display.Sprite;
	import system._Object_;
	import unityengine.Animation;
	import unityengine.Camera;
	import unityengine.GameObject;
	import unityengine.MonoBehaviour;
	import unityengine.Quaternion;
	import unityengine.Random;
	import unityengine.TextMesh;
	import unityengine.Time;
	import unityengine.UObject;
	import unityengine.Vector3;
	import unityengine.ai.NavMesh;
	import unityengine.ui.Button;
	[Doc]
	/**
	 * ...
	 * @author 
	 */
	public class Main extends Sprite 
	{
		
		var cubes:Vector.<GameObject> = new Vector.<GameObject>();		
		var mvs:Vector.<Vector3> = new Vector.<Vector3>();
		
		public function Main() 
		{
			var camera:Camera = Camera(GameObject.find("Main Camera").getComponent(Camera));			
			trace("maincamera:", camera, camera.transform.position);
			
			trace( "camera is main:", _Object_.equals(camera, Camera.main) );
			
			
			var mono:MonoBehaviour = GameObject.find("AS3Player").getComponent(MonoBehaviour) as MonoBehaviour;
			trace(mono.name);
			mono.startCoroutine( Iterator(  
				(
				function()
				{
					trace("a",Time.frameCount);
					
					yield return 1;
					
					trace("b",Time.frameCount);
					
					yield return 2;
					
					trace("c",Time.frameCount);
					yield return 3;
				}
				)()
			));
			
			
			
			var cube:UObject = GameObject.find("Cube");
			for (var i:int = 0; i < 100; i++) 
			{
				var c2:GameObject = UObject.instantiate__(cube) as GameObject;
				
				c2.transform.position = new Vector3( Random.range(-5,5),Random.range(0,5),Random.range(-5,5) );
				
				cubes.push(c2);
				
				mvs.push( new Vector3(Random.range( -5, 5), Random.range(-5, 5), Random.range( -5, 5)) );
				mvs[mvs.length - 1].normalize();
				
				if (i > 10)
				{
					c2.setActive(false);
					
				}
				
			}	
			
			
			var btn:Button = Button( GameObject.find("Button").getComponent(Button));			
			btn.onClick.addListener(			
				onclick			
			);
			
			
		}
		
		private function onclick()
		{
			isstop = !isstop;
			trace("isstop?" , isstop);
			
		}
		
		private var isstop:Boolean = false;
		public function update():void
		{
			if (isstop)
				return;
			
			for (var i:int = 0; i < 100; i++) 
			{
				var cube:GameObject = cubes[i];
				var v:Vector3 = mvs[i];
				
				cube.transform.localPosition += v * Time.deltaTime;
				
				var p:Vector3 = cube.transform.localPosition;
				if (p.x <-5 || p.y < -5 || p.z < -5 || p.x > 5 || p.y > 5 || p.z > 5)
				{
					mvs[i] =-mvs[i];
				}
				
				//var k:NavMesh;
				//k = cube;
			}
			
		}
		
	}
	
}
