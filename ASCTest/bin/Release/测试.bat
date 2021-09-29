@echo 脚本系统的程序核心功能已全部完成。包括 类继承，接口，原型链,结构化异常处理 try catch ,语句块标签label。未实现且不准备实现的语言要素为 namespace和with。
@echo 新增语言特性 yield。和C#的yield语法完全一致，可以自动生成一个枚举器对象，参见test yield
@echo=
@echo=
 
@echo tests下的每一个子文件夹都是一个测试项目
@echo 可用ASCTest	.\tests\XXX	单独运行一个测试
@echo 本批处理会循环执行每一个测试。
@echo 项目中的as3编码为ANSI.因为批处理中如果不是ANSI编码将无法正确显示汉字
@echo 实际上as3文件的编码应该是UTF-8。

@echo=
@echo ----
@echo=

@set /p var=按任意键开始测试


@set n=0

::@cd .\tests

@for /R .\tests  %%i in (.) do  @call:process %%~ni

pause
exit

:process
::@echo %1
::@goto subend

@set /a n+=1
@if %n%==1 (goto subend)

@cls

@echo show src code  %1
@echo=

@if not exist .\tests\%1\FuncTest.as (goto subend)

@type .\tests\%1\FuncTest.as



@set /p var=按任意键开始编译并执行
@echo ---build and run---
@ASCTest	.\tests\%1


@set /p var=按任意键继续下一个测试


:subend
