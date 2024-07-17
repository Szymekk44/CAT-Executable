namespace CatRunner.Cat
{
	public enum Commands : byte
	{
		Console = 1,
		Variable = 2,
		Create = 3,
		Destroy = 4,
		Set = 5,
		Add = 6,
		Subtract = 7,
		Multiply = 8,
		Divide = 9,
		RestFromDivision = 10,
		Bool = 11,
		Byte = 12,
		Short = 13,
		UShort = 14,
		Int = 15,
		UInt = 16,
		Long = 17,
		ULong = 18,
		Float = 19,
		Double = 20,
		String = 21,
		VarMemory = 22,
		VarLocal = 23,
		TwoOperations = 24,
		Jump = 25,
		LessThan = 26,
		GreaterThan = 27,
		StartParanthesis = 28,
		EndParanthesis = 29,
		Continue = 30,
		Stop = 31,

		End = 32
	}
}
