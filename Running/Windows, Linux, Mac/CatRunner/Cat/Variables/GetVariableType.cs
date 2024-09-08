using System;

namespace CatRunner.Cat.Variables
{
	public static class GetVariableType
	{
		public static VarType Get(byte fromByte)
		{
			switch (fromByte)
			{
				case 11:
					return VarType.Bool;
				case 12:
					return VarType.Byte;
				case 13:
					return VarType.Short;
				case 14:
					return VarType.UShort;
				case 15:
					return VarType.Int;
				case 16:
					return VarType.UInt;
				case 17:
					return VarType.Long;
				case 18:
					return VarType.ULong;
				case 19:
					return VarType.Float;
				case 20:
					return VarType.Double;
				case 21:
					return VarType.String;
				default:
					throw new Exception("Unknown variable type: " + fromByte);
			}
		}
	}
}
