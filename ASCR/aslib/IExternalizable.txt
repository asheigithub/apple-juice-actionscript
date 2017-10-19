//
// C:\Users\Manju-pc\AppData\Local\FlashDevelop\Apps\ascsdk\27.0.0\frameworks\libs\air\airglobal.swc\flash\utils\IExternalizable
//
package flash.utils
{
	import flash.utils.IDataOutput;
	import flash.utils.IDataInput;

	public interface IExternalizable
	{
		function readExternal (input:IDataInput) : void;

		function writeExternal (output:IDataOutput) : void;
	}
}
