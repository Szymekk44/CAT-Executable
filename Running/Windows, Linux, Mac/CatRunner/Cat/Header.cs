using CatRunner.Cat.Variables;
using System.IO;
using System;
using System.Collections.Generic;

namespace CatRunner.Cat
{
	public class Header
	{
		public Header(Executor executor)
		{
			myExecutor = executor;
		}

		public int CatVersion;
		public byte PermissionLevel;
		public string? AppName;
		public string? AppDescription;
		public Executor myExecutor;

		public void Read(BinaryReader reader)
		{
			if (!CheckMagic(reader))
				throw new Exception("This is not a valid CAT Executable file.");
			CatVersion = reader.ReadInt32();
			PermissionLevel = reader.ReadByte();
			AppName = reader.ReadString();
			AppDescription = reader.ReadString();
			ReadGlobalVariables(reader);

		}

		public void ReadGlobalVariables(BinaryReader reader)
		{
			long count = reader.ReadInt64();
			Console.WriteLine($"Reading {count} global variables!");
			myExecutor.GlobalVariables = new Dictionary<string, Variables.Variable>();
			for (int i = 0; i < count; i++)
			{
				ReadVariable.CreateVariable(reader, myExecutor, true, true);
			}
		}

		public bool CheckMagic(BinaryReader reader)
		{
			byte[] magic = reader.ReadBytes(4);
			return (magic[0] == 127 && magic[1] == 67 && magic[2] == 65 && magic[3] == 84);
		}
	}
}
