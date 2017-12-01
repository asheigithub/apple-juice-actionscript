using ASBinCode;
using ASBinCode.rtti;
using ASRuntime;
using ASRuntime.nativefuncs;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ASCTest.regNativeFunctions
{
	class system_ConsoleKey_buildin
	{
		public static void regNativeFunctions(CSWC bin)
		{
			bin.regNativeFunction(LinkSystem_Buildin.getCreator("system_ConsoleKey_creator", default(System.ConsoleKey)));
			bin.regNativeFunction(new system_ConsoleKey_ctor());
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_Backspace_getter",()=>{ return System.ConsoleKey.Backspace;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_Tab_getter",()=>{ return System.ConsoleKey.Tab;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_Clear_getter",()=>{ return System.ConsoleKey.Clear;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_Enter_getter",()=>{ return System.ConsoleKey.Enter;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_Pause_getter",()=>{ return System.ConsoleKey.Pause;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_Escape_getter",()=>{ return System.ConsoleKey.Escape;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_Spacebar_getter",()=>{ return System.ConsoleKey.Spacebar;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_PageUp_getter",()=>{ return System.ConsoleKey.PageUp;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_PageDown_getter",()=>{ return System.ConsoleKey.PageDown;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_End_getter",()=>{ return System.ConsoleKey.End;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_Home_getter",()=>{ return System.ConsoleKey.Home;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_LeftArrow_getter",()=>{ return System.ConsoleKey.LeftArrow;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_UpArrow_getter",()=>{ return System.ConsoleKey.UpArrow;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_RightArrow_getter",()=>{ return System.ConsoleKey.RightArrow;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_DownArrow_getter",()=>{ return System.ConsoleKey.DownArrow;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_Select_getter",()=>{ return System.ConsoleKey.Select;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_Print_getter",()=>{ return System.ConsoleKey.Print;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_Execute_getter",()=>{ return System.ConsoleKey.Execute;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_PrintScreen_getter",()=>{ return System.ConsoleKey.PrintScreen;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_Insert_getter",()=>{ return System.ConsoleKey.Insert;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_Delete_getter",()=>{ return System.ConsoleKey.Delete;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_Help_getter",()=>{ return System.ConsoleKey.Help;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_D0_getter",()=>{ return System.ConsoleKey.D0;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_D1_getter",()=>{ return System.ConsoleKey.D1;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_D2_getter",()=>{ return System.ConsoleKey.D2;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_D3_getter",()=>{ return System.ConsoleKey.D3;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_D4_getter",()=>{ return System.ConsoleKey.D4;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_D5_getter",()=>{ return System.ConsoleKey.D5;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_D6_getter",()=>{ return System.ConsoleKey.D6;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_D7_getter",()=>{ return System.ConsoleKey.D7;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_D8_getter",()=>{ return System.ConsoleKey.D8;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_D9_getter",()=>{ return System.ConsoleKey.D9;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_A_getter",()=>{ return System.ConsoleKey.A;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_B_getter",()=>{ return System.ConsoleKey.B;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_C_getter",()=>{ return System.ConsoleKey.C;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_D_getter",()=>{ return System.ConsoleKey.D;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_E_getter",()=>{ return System.ConsoleKey.E;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_F_getter",()=>{ return System.ConsoleKey.F;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_G_getter",()=>{ return System.ConsoleKey.G;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_H_getter",()=>{ return System.ConsoleKey.H;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_I_getter",()=>{ return System.ConsoleKey.I;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_J_getter",()=>{ return System.ConsoleKey.J;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_K_getter",()=>{ return System.ConsoleKey.K;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_L_getter",()=>{ return System.ConsoleKey.L;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_M_getter",()=>{ return System.ConsoleKey.M;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_N_getter",()=>{ return System.ConsoleKey.N;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_O_getter",()=>{ return System.ConsoleKey.O;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_P_getter",()=>{ return System.ConsoleKey.P;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_Q_getter",()=>{ return System.ConsoleKey.Q;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_R_getter",()=>{ return System.ConsoleKey.R;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_S_getter",()=>{ return System.ConsoleKey.S;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_T_getter",()=>{ return System.ConsoleKey.T;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_U_getter",()=>{ return System.ConsoleKey.U;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_V_getter",()=>{ return System.ConsoleKey.V;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_W_getter",()=>{ return System.ConsoleKey.W;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_X_getter",()=>{ return System.ConsoleKey.X;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_Y_getter",()=>{ return System.ConsoleKey.Y;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_Z_getter",()=>{ return System.ConsoleKey.Z;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_LeftWindows_getter",()=>{ return System.ConsoleKey.LeftWindows;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_RightWindows_getter",()=>{ return System.ConsoleKey.RightWindows;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_Applications_getter",()=>{ return System.ConsoleKey.Applications;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_Sleep_getter",()=>{ return System.ConsoleKey.Sleep;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_NumPad0_getter",()=>{ return System.ConsoleKey.NumPad0;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_NumPad1_getter",()=>{ return System.ConsoleKey.NumPad1;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_NumPad2_getter",()=>{ return System.ConsoleKey.NumPad2;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_NumPad3_getter",()=>{ return System.ConsoleKey.NumPad3;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_NumPad4_getter",()=>{ return System.ConsoleKey.NumPad4;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_NumPad5_getter",()=>{ return System.ConsoleKey.NumPad5;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_NumPad6_getter",()=>{ return System.ConsoleKey.NumPad6;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_NumPad7_getter",()=>{ return System.ConsoleKey.NumPad7;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_NumPad8_getter",()=>{ return System.ConsoleKey.NumPad8;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_NumPad9_getter",()=>{ return System.ConsoleKey.NumPad9;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_Multiply_getter",()=>{ return System.ConsoleKey.Multiply;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_Add_getter",()=>{ return System.ConsoleKey.Add;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_Separator_getter",()=>{ return System.ConsoleKey.Separator;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_Subtract_getter",()=>{ return System.ConsoleKey.Subtract;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_Decimal_getter",()=>{ return System.ConsoleKey.Decimal;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_Divide_getter",()=>{ return System.ConsoleKey.Divide;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_F1_getter",()=>{ return System.ConsoleKey.F1;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_F2_getter",()=>{ return System.ConsoleKey.F2;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_F3_getter",()=>{ return System.ConsoleKey.F3;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_F4_getter",()=>{ return System.ConsoleKey.F4;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_F5_getter",()=>{ return System.ConsoleKey.F5;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_F6_getter",()=>{ return System.ConsoleKey.F6;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_F7_getter",()=>{ return System.ConsoleKey.F7;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_F8_getter",()=>{ return System.ConsoleKey.F8;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_F9_getter",()=>{ return System.ConsoleKey.F9;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_F10_getter",()=>{ return System.ConsoleKey.F10;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_F11_getter",()=>{ return System.ConsoleKey.F11;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_F12_getter",()=>{ return System.ConsoleKey.F12;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_F13_getter",()=>{ return System.ConsoleKey.F13;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_F14_getter",()=>{ return System.ConsoleKey.F14;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_F15_getter",()=>{ return System.ConsoleKey.F15;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_F16_getter",()=>{ return System.ConsoleKey.F16;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_F17_getter",()=>{ return System.ConsoleKey.F17;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_F18_getter",()=>{ return System.ConsoleKey.F18;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_F19_getter",()=>{ return System.ConsoleKey.F19;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_F20_getter",()=>{ return System.ConsoleKey.F20;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_F21_getter",()=>{ return System.ConsoleKey.F21;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_F22_getter",()=>{ return System.ConsoleKey.F22;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_F23_getter",()=>{ return System.ConsoleKey.F23;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_F24_getter",()=>{ return System.ConsoleKey.F24;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_BrowserBack_getter",()=>{ return System.ConsoleKey.BrowserBack;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_BrowserForward_getter",()=>{ return System.ConsoleKey.BrowserForward;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_BrowserRefresh_getter",()=>{ return System.ConsoleKey.BrowserRefresh;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_BrowserStop_getter",()=>{ return System.ConsoleKey.BrowserStop;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_BrowserSearch_getter",()=>{ return System.ConsoleKey.BrowserSearch;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_BrowserFavorites_getter",()=>{ return System.ConsoleKey.BrowserFavorites;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_BrowserHome_getter",()=>{ return System.ConsoleKey.BrowserHome;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_VolumeMute_getter",()=>{ return System.ConsoleKey.VolumeMute;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_VolumeDown_getter",()=>{ return System.ConsoleKey.VolumeDown;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_VolumeUp_getter",()=>{ return System.ConsoleKey.VolumeUp;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_MediaNext_getter",()=>{ return System.ConsoleKey.MediaNext;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_MediaPrevious_getter",()=>{ return System.ConsoleKey.MediaPrevious;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_MediaStop_getter",()=>{ return System.ConsoleKey.MediaStop;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_MediaPlay_getter",()=>{ return System.ConsoleKey.MediaPlay;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_LaunchMail_getter",()=>{ return System.ConsoleKey.LaunchMail;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_LaunchMediaSelect_getter",()=>{ return System.ConsoleKey.LaunchMediaSelect;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_LaunchApp1_getter",()=>{ return System.ConsoleKey.LaunchApp1;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_LaunchApp2_getter",()=>{ return System.ConsoleKey.LaunchApp2;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_Oem1_getter",()=>{ return System.ConsoleKey.Oem1;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_OemPlus_getter",()=>{ return System.ConsoleKey.OemPlus;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_OemComma_getter",()=>{ return System.ConsoleKey.OemComma;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_OemMinus_getter",()=>{ return System.ConsoleKey.OemMinus;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_OemPeriod_getter",()=>{ return System.ConsoleKey.OemPeriod;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_Oem2_getter",()=>{ return System.ConsoleKey.Oem2;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_Oem3_getter",()=>{ return System.ConsoleKey.Oem3;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_Oem4_getter",()=>{ return System.ConsoleKey.Oem4;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_Oem5_getter",()=>{ return System.ConsoleKey.Oem5;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_Oem6_getter",()=>{ return System.ConsoleKey.Oem6;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_Oem7_getter",()=>{ return System.ConsoleKey.Oem7;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_Oem8_getter",()=>{ return System.ConsoleKey.Oem8;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_Oem102_getter",()=>{ return System.ConsoleKey.Oem102;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_Process_getter",()=>{ return System.ConsoleKey.Process;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_Packet_getter",()=>{ return System.ConsoleKey.Packet;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_Attention_getter",()=>{ return System.ConsoleKey.Attention;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_CrSel_getter",()=>{ return System.ConsoleKey.CrSel;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_ExSel_getter",()=>{ return System.ConsoleKey.ExSel;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_EraseEndOfFile_getter",()=>{ return System.ConsoleKey.EraseEndOfFile;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_Play_getter",()=>{ return System.ConsoleKey.Play;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_Zoom_getter",()=>{ return System.ConsoleKey.Zoom;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_NoName_getter",()=>{ return System.ConsoleKey.NoName;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_Pa1_getter",()=>{ return System.ConsoleKey.Pa1;}));
			bin.regNativeFunction(LinkSystem_Buildin.getStruct_static_field_getter("system_ConsoleKey_OemClear_getter",()=>{ return System.ConsoleKey.OemClear;}));
			bin.regNativeFunction(new system_ConsoleKey_operator_bitOr());
		}

		class system_ConsoleKey_ctor : NativeFunctionBase
		{
			public system_ConsoleKey_ctor()
			{
				para = new List<RunTimeDataType>();
			}

			public override bool isMethod
			{
				get
				{
					return true;
				}
			}

			public override string name
			{
				get
				{
					return "system_ConsoleKey_ctor";
				}
			}

			List<RunTimeDataType> para;
			public override List<RunTimeDataType> parameters
			{
				get
				{
					return para;
				}
			}

			public override RunTimeDataType returnType
			{
				get
				{
					return RunTimeDataType.rt_void;
				}
			}

			public override RunTimeValueBase execute(RunTimeValueBase thisObj, SLOT[] argements, object stackframe, out string errormessage, out int errorno)
			{
				errormessage = null; errorno = 0;
				return ASBinCode.rtData.rtUndefined.undefined;

			}
		}

		class system_ConsoleKey_operator_bitOr : ASRuntime.nativefuncs.NativeConstParameterFunction
		{
			public system_ConsoleKey_operator_bitOr() : base(2)
			{
				para = new List<RunTimeDataType>();
				para.Add(RunTimeDataType.rt_void);
				para.Add(RunTimeDataType.rt_void);
			}

			public override bool isMethod
			{
				get
				{
					return true;
				}
			}

			public override string name
			{
				get
				{
					return "system_ConsoleKey_operator_bitOr";
				}
			}

			List<RunTimeDataType> para;
			public override List<RunTimeDataType> parameters
			{
				get
				{
					return para;
				}
			}

			public override RunTimeDataType returnType
			{
				get
				{
					return RunTimeDataType.rt_void;
				}
			}

			public override void execute3(RunTimeValueBase thisObj, FunctionDefine functionDefine, SLOT returnSlot, SourceToken token, StackFrame stackframe, out bool success)
			{
				System.ConsoleKey ts1;

				if (argements[0].rtType == RunTimeDataType.rt_null)
				{
					ts1 = default(System.ConsoleKey);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[0]).value;
					ts1 = (System.ConsoleKey)argObj.value;
				}

				System.ConsoleKey ts2;

				if (argements[1].rtType == RunTimeDataType.rt_null)
				{
					ts2 = default(System.ConsoleKey);
				}
				else
				{
					LinkObj<object> argObj = (LinkObj<object>)((ASBinCode.rtData.rtObjectBase)argements[1]).value;
					ts2 = (System.ConsoleKey)argObj.value;
				}

				((StackSlot)returnSlot).setLinkObjectValue(
					bin.getClassByRunTimeDataType(functionDefine.signature.returnType), stackframe.player, ts1 | ts2);

				success = true;
			}
		}

	}
}
