**The following documents are translated by google translate to english :)**

## Apple Juice ##
Pure .NET 2.0 code Implementation of the ActionScript3 compiler and runtime. 

## What And Why ##
You can write Unity3D projects using ActionScript3. You can use ActionScript 3 to call the Unity API, and you can download AS3 bytecodes from the network and execute your code.

### How? ###
We implemented all the features of ActionScript 3 with .net 2.0, except for "namespace" and "E4X XML." It is a pure .net 2.0 implementation and can therefore be executed in an environment where Unity can run.
We provide compilers and SDKs that compile ActionScript 3 code into bytecode and support the FlashDevelop IDE.

### How To Use ###
You need to have the following environment:
- [FlashDevelop](http://www.flashdevelop.org/ "FlashDevelop") Recommended ActionScript3 IDE.
- [JRE](https://www.java.com) FlashDevelop requires a JRE.
- Windows. Currently, the compilation tool needs to run on Windows
- Unity 4.X and above recommend 5.X or later. The demo scene in the plugin is built on Unity 5.6, but the Apple Juice ActionScript 3 engine does work on Unity 4.7.


> JAVA and FlashDevelop are not required. If you do not install the IDE, you need to use any other text editor to write the code and manually execute a compiled batch command to compile and publish. However, FlashDevelop can provide convenient features such as IntelliSense, AutoComplete, Error Prompt, Code Format, and more. For convenience, it is recommended to install JRE and FlashDevelop.

### Create Unity Project and ActionScript 3 Script Projects ###
Download the latest release package and unzip it. The contents of the decompressed directory are as follows:
> .
> ├─── SDK[X.X.X]   
> └─── UnityPackage  
>       └───AS3RuntimeForUnity.unitypackage


#### SDK[X.X.X]
>It is a custom AIRSDK. It can be recognized and loaded by FlashDevelop and used to compile and publish the code.

#### UnityPackage
>Unity plugin package "AS3RuntimeForUnity.unitypackage".

Steps to create a project:

1. Create a Unity project first. Then import the Unity plugin package inside UnityPackage.
2. Click on the menu "ASRuntime/Create ActionScript3 FlashDevelop HotFixProj".
3. Follow the prompts to locate the SDK location.
4. Then follow the prompts to create an AS3 hot update project. After the project is created, Unity's API is generated based on the default configuration.
>ActionScript 3 projects require that they be created in a blank folder.

test:

1. In the Unity project, open the test scenario "Assets/HotFixDemoScene1.scene".
2. In the ActionScript 3 project directory just generated, open the <protname>.as3proj project file with FlashDevelop.
3. Modify code in AS3 project
4. Click the FlashDevelop compile button to compile
5. Go back to the Unity project and click Play to see the result of the change.


**The following is a gif picture, if not shown, please wait for a while**
![gif](doc_cn/images/as3_unity_demo4.gif) 
