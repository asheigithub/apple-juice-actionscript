
@set path=%path%;C:\Program Files (x86)\Java\jdk1.8.0_131\bin

javac -source 1.4 -target 1.4 -s src src\com\adobe\flash\compiler\clients\MXMLC.java -d classes
jar cvfm mxmlc-cli.jar MANIFEST.MF -C classes com\adobe\flash\compiler\clients\MXMLC.class

pause