package {
	[Doc]
	public class FuncTest{
		public function FuncTest() {
		}
	}
}
/*
类继承 -- 是主要的继承机制，并支持固定属性的继承。固定属性是声明为类定义一部分的变量、常量或方法。现在，可通过存储相关类信息的特殊类对象表示每个类定义。 

原型继承 -- 每种类都有一个关联的原型对象，而原型对象的属性由该类的所有实例共享。
在创建一个类实例时，它具有对其类的原型对象的引用，这将作为实例及与其关联的类原型对象间的链接。
运行时，如果在类实例中找不到某属性，
则会检查委托（该类的原型对象）中是否有该属性。
如果原型对象不包含这种属性，
此过程会继续在层次结构中连续的更高级别上对原型对象进行委托检查，直到找到该属性为止。
*/
//类继承和原型继承可同时存在，如下例所示：
class A {
     var x = 1
	 public function A()
	 {
     A.prototype.px = 2
	 }
 }
 dynamic class B extends A {
     var y = 3
	 public function B()
	 {
     B.prototype.py = 4
	 }
 }
  
 var b = new B()
 trace(b.x) // 1 via class inheritance
  trace(b.px) // 2 via prototype inheritance from A.prototype
  trace(b.y) // 3
  trace(b.py) // 4 via prototype inheritance from B.prototype
  
 B.prototype.px = 5
  trace(b.px) // now 5 because B.prototype hides A.prototype
  
 b.px = 6
  trace(b.px) // now 6 because b hides B.prototype
  
  var b2=new B()
  trace(b2.px) // ==5
  
  
  

  
  