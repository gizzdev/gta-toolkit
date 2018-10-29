using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
	public class MCShaderVariableComponent : MetaStructureWrapper<CShaderVariableComponent>
	{
		public static MetaName _MetaName = MetaName.CShaderVariableComponent;
		public MetaFile Meta;
		public uint PedcompID;
		public uint MaskID;
		public uint ShaderVariableHashString;
		public Array_byte Tracks;
		public Array_ushort Ids;
		public Array_byte Components;

		public MCShaderVariableComponent()
		{
			this.MetaName = MetaName.CShaderVariableComponent;
			this.MetaStructure = new CShaderVariableComponent();
		}

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MCShaderVariableComponent._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MCShaderVariableComponent._MetaName);
		}


		public override void Parse(MetaFile meta, CShaderVariableComponent CShaderVariableComponent)
		{
			this.Meta = meta;
			this.MetaStructure = CShaderVariableComponent;

			this.PedcompID = CShaderVariableComponent.pedcompID;
			this.MaskID = CShaderVariableComponent.maskID;
			this.ShaderVariableHashString = CShaderVariableComponent.shaderVariableHashString;
			// this.Tracks = CShaderVariableComponent.tracks;
			// this.Ids = CShaderVariableComponent.ids;
			// this.Components = CShaderVariableComponent.components;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.pedcompID = this.PedcompID;
			this.MetaStructure.maskID = this.MaskID;
			this.MetaStructure.shaderVariableHashString = this.ShaderVariableHashString;
			// this.MetaStructure.tracks = this.Tracks;
			// this.MetaStructure.ids = this.Ids;
			// this.MetaStructure.components = this.Components;

 			MCShaderVariableComponent.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}
	}
}
