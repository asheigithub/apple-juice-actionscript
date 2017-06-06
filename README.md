# ASTool
### .net2.0实现的AS3解释器
脚本系统的程序核心功能已全部完成。
包括 类继承，接口，原型链,结构化异常处理 try catch ,语句块标签label。
未实现且不准备实现的语言要素为 namespace和with。
实现了完整的编译期类型检查。错误提示和Adobe Air尽量一致。
可使用flash develop作为ide进行as3脚本编辑器


### 扩展语法
新增语言特性 yield。和C#的yield语法完全一致，可以自动生成一个枚举器对象.
```actionscript3
var yieldtest=function(a):*
{
        for (var i:int = 0; i < 100; i++ )
        {
          if(i>=a)
          {
            trace("exit yield");
            //**输出调用堆栈。
            trace(new Error().getStackTrace());
            yield break;
          }
          trace("current output:",i);
          yield return i;
        }
      }

      for (var k in yieldtest(4))
      {


        trace("receive:",k);
        //**输出调用堆栈。
        trace(new Error().getStackTrace());
      }
```
### 与.net类库方便集成。
as3本身是完整的面向对象支持语言，因此可以将.net类库大部分保持原风格的集成进来。同时又可以使用原型链进行扩展。(虽然并不推荐使用原型链)
见如下，集成了.net的System.DateTime,同时又使用原型链扩展

    var d3 = DateTime.constructor_______(Int64(123456789),j);
    trace(d3);
    trace(DateTime.MinValue.add(TimeSpan.fromDays(10)).addDays(15).addHours(120).addYears(1000));
    DateTime.prototype.kk = function()
    {
        trace(this.year);
    };
    d3.kk.apply(d3);

### 当前进度
链接.net类库到脚本系统中。由于系统本身使用.net2.0 开发的，所以链接系统类库比创建代理类的性能要高。
考虑到为了避免使用反射（IOS系统你懂的）因此需要制作代码生成工具，来自动生成链接的代码。
目前正在人肉进行基础类的链接，待链接代码基本可以生成模板后，即可制作代码生成器。

### 编译和执行
系统会先将代码编译后再执行，目前系统的测试代码每次均为先编译一遍后再执行，还需要制作一个将编译结果序列化与反序列号的功能。
下一步将进行这个工作

### 如何尝鲜
编译项目后，在 ASCTest\bin\Release 下有一个tests目录。该目录下每个文件夹都是一个测试项目，可自由修改测试。
