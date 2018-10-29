using System;
using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RageLib.Resources.GTA5.PC.Meta;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Structures
{
    public class MUnk_376833625 : MetaStructureWrapper<Unk_376833625>
    {
        public static MetaName _MetaName = (MetaName)(376833625);
        public MetaFile Meta;
        public byte Unk_1235281004;
        public byte Unk_4086467184;
        public byte Unk_911147899;
        public byte Unk_315291935;

        public ArrayOfBytes12 Unk_2996560424
        {
            get
            {
                ArrayOfBytes12 enabledComponents = new ArrayOfBytes12();

                Unk_884254308[] values = (Unk_884254308[]) Enum.GetValues(typeof(Unk_884254308));

                int count = 0;
                byte i = 0;

                foreach (var value in values)
                {
                    if (value == Unk_884254308.PV_COMP_INVALID || value == Unk_884254308.PV_COMP_MAX)
                        continue;

                    if (Components[value] == null)
                        enabledComponents.SetByte(count, 255);
                    else
                        enabledComponents.SetByte(count, i++);

                    count++;
                }

                return enabledComponents;
            }
        }

        public List<MUnk_3538495220> Unk_3796409423
        {
            get
            {
                var components = new List<MUnk_3538495220>();

                foreach(var entry in Components)
                    if (entry.Value != null)
                        components.Add(entry.Value);

                return components; 
            }
        }

        public List<MUnk_253191135> Unk_2131007641 = new List<MUnk_253191135>();
        public List<MCComponentInfo> CompInfos = new List<MCComponentInfo>();
        public MUnk_2858946626 PropInfo = new MUnk_2858946626();
        public uint DlcName;

        public Dictionary<Unk_884254308, MUnk_3538495220> Components = new Dictionary<Unk_884254308, MUnk_3538495220>()
        {
            { Unk_884254308.PV_COMP_HEAD, null },
            { Unk_884254308.PV_COMP_BERD, null },
            { Unk_884254308.PV_COMP_HAIR, null },
            { Unk_884254308.PV_COMP_UPPR, null },
            { Unk_884254308.PV_COMP_LOWR, null },
            { Unk_884254308.PV_COMP_HAND, null },
            { Unk_884254308.PV_COMP_FEET, null },
            { Unk_884254308.PV_COMP_TEEF, null },
            { Unk_884254308.PV_COMP_ACCS, null },
            { Unk_884254308.PV_COMP_TASK, null },
            { Unk_884254308.PV_COMP_DECL, null },
            { Unk_884254308.PV_COMP_JBIB, null },
        };


        public MUnk_376833625()
		{
			this.MetaName = (MetaName) (376833625);
			this.MetaStructure = new Unk_376833625();
        }

		public static void AddEnumAndStructureInfo(MetaBuilder mb)
		{
			var enumInfos = MetaInfo.GetStructureEnumInfo(MUnk_376833625._MetaName);

			for (int i = 0; i < enumInfos.Length; i++)
				mb.AddEnumInfo((MetaName) enumInfos[i].EnumNameHash);

			mb.AddStructureInfo(MUnk_376833625._MetaName);
			mb.AddStructureInfo((MetaName) (-756472076));
			mb.AddStructureInfo((MetaName) (253191135));
			mb.AddStructureInfo(MetaName.CComponentInfo);
			mb.AddStructureInfo((MetaName) (-1436020670));
		}


		public override void Parse(MetaFile meta, Unk_376833625 Unk_376833625)
		{
			this.Meta = meta;
			this.MetaStructure = Unk_376833625;

			this.Unk_1235281004 = Unk_376833625.Unk_1235281004;
			this.Unk_4086467184 = Unk_376833625.Unk_4086467184;
			this.Unk_911147899 = Unk_376833625.Unk_911147899;
			this.Unk_315291935 = Unk_376833625.Unk_315291935;

			//this.Unk_2996560424 = Unk_376833625.Unk_2996560424;

			var Unk_3796409423 = MetaUtils.ConvertDataArray<Unk_3538495220>(meta, Unk_376833625.Unk_3796409423);

            int componentCount = 0;

            if (Unk_376833625.Unk_2996560424.b00 != 255)
            {
                Components[Unk_884254308.PV_COMP_HEAD] = new MUnk_3538495220();
                Components[Unk_884254308.PV_COMP_HEAD].Parse(meta, Unk_3796409423[componentCount++]);
            }

            if (Unk_376833625.Unk_2996560424.b01 != 255)
            {
                Components[Unk_884254308.PV_COMP_BERD] = new MUnk_3538495220();
                Components[Unk_884254308.PV_COMP_BERD].Parse(meta, Unk_3796409423[componentCount++]);
            }

            if (Unk_376833625.Unk_2996560424.b02 != 255)
            {
                Components[Unk_884254308.PV_COMP_HAIR] = new MUnk_3538495220();
                Components[Unk_884254308.PV_COMP_HAIR].Parse(meta, Unk_3796409423[componentCount++]);
            }

            if (Unk_376833625.Unk_2996560424.b03 != 255)
            {
                Components[Unk_884254308.PV_COMP_UPPR] = new MUnk_3538495220();
                Components[Unk_884254308.PV_COMP_UPPR].Parse(meta, Unk_3796409423[componentCount++]);
            }

            if (Unk_376833625.Unk_2996560424.b04 != 255)
            {
                Components[Unk_884254308.PV_COMP_LOWR] = new MUnk_3538495220();
                Components[Unk_884254308.PV_COMP_LOWR].Parse(meta, Unk_3796409423[componentCount++]);
            }

            if (Unk_376833625.Unk_2996560424.b05 != 255)
            {
                Components[Unk_884254308.PV_COMP_HAND] = new MUnk_3538495220();
                Components[Unk_884254308.PV_COMP_HAND].Parse(meta, Unk_3796409423[componentCount++]);
            }

            if (Unk_376833625.Unk_2996560424.b06 != 255)
            {
                Components[Unk_884254308.PV_COMP_FEET] = new MUnk_3538495220();
                Components[Unk_884254308.PV_COMP_FEET].Parse(meta, Unk_3796409423[componentCount++]);
            }

            if (Unk_376833625.Unk_2996560424.b07 != 255)
            {
                Components[Unk_884254308.PV_COMP_TEEF] = new MUnk_3538495220();
                Components[Unk_884254308.PV_COMP_TEEF].Parse(meta, Unk_3796409423[componentCount++]);
            }

            if (Unk_376833625.Unk_2996560424.b08 != 255)
            {
                Components[Unk_884254308.PV_COMP_ACCS] = new MUnk_3538495220();
                Components[Unk_884254308.PV_COMP_ACCS].Parse(meta, Unk_3796409423[componentCount++]);
            }

            if (Unk_376833625.Unk_2996560424.b09 != 255)
            {
                Components[Unk_884254308.PV_COMP_TASK] = new MUnk_3538495220();
                Components[Unk_884254308.PV_COMP_TASK].Parse(meta, Unk_3796409423[componentCount++]);
            }

            if (Unk_376833625.Unk_2996560424.b10 != 255)
            {
                Components[Unk_884254308.PV_COMP_DECL] = new MUnk_3538495220();
                Components[Unk_884254308.PV_COMP_DECL].Parse(meta, Unk_3796409423[componentCount++]);
            }

            if (Unk_376833625.Unk_2996560424.b11 != 255)
            {
                Components[Unk_884254308.PV_COMP_JBIB] = new MUnk_3538495220();
                Components[Unk_884254308.PV_COMP_JBIB].Parse(meta, Unk_3796409423[componentCount++]);
            }

            //this.Unk_3796409423 = Unk_3796409423?.Select(e => { var msw = new MUnk_3538495220(); msw.Parse(meta, e); return msw; }).ToList();

            var Unk_2131007641 = MetaUtils.ConvertDataArray<Unk_253191135>(meta, Unk_376833625.Unk_2131007641);
			this.Unk_2131007641 = Unk_2131007641?.Select(e => { var msw = new MUnk_253191135(); msw.Parse(meta, e); return msw; }).ToList();

			var compInfos = MetaUtils.ConvertDataArray<CComponentInfo>(meta, Unk_376833625.compInfos);
			this.CompInfos = compInfos?.Select(e => { var msw = new MCComponentInfo(); msw.Parse(meta, e); return msw; }).ToList();

			this.PropInfo = new MUnk_2858946626();
			this.PropInfo.Parse(meta, Unk_376833625.propInfo);
			this.DlcName = Unk_376833625.dlcName;
		}

		public override void Build(MetaBuilder mb, bool isRoot = false)
		{
			this.MetaStructure.Unk_1235281004 = this.Unk_1235281004;
			this.MetaStructure.Unk_4086467184 = this.Unk_4086467184;
			this.MetaStructure.Unk_911147899 = this.Unk_911147899;
			this.MetaStructure.Unk_315291935 = this.Unk_315291935;
			this.MetaStructure.Unk_2996560424 = this.Unk_2996560424;

            if (this.Unk_3796409423 != null)
				this.MetaStructure.Unk_3796409423 = mb.AddItemArrayPtr((MetaName) (-756472076), this.Unk_3796409423.Select(e => {e.Build(mb); return e.MetaStructure; }).ToArray());
 			MUnk_3538495220.AddEnumAndStructureInfo(mb);          

			if(this.Unk_2131007641 != null)
				this.MetaStructure.Unk_2131007641 = mb.AddItemArrayPtr((MetaName) (253191135), this.Unk_2131007641.Select(e => { e.Build(mb); return e.MetaStructure; }).ToArray());
 			MUnk_253191135.AddEnumAndStructureInfo(mb);          

			if(this.CompInfos != null)
				this.MetaStructure.compInfos = mb.AddItemArrayPtr(MetaName.CComponentInfo, this.CompInfos.Select(e => { e.Build(mb); return e.MetaStructure; }).ToArray());
 			MCComponentInfo.AddEnumAndStructureInfo(mb);                    

			this.PropInfo.Build(mb);
			this.MetaStructure.propInfo = this.PropInfo.MetaStructure;
 			MUnk_2858946626.AddEnumAndStructureInfo(mb);          

			this.MetaStructure.dlcName = this.DlcName;

 			MUnk_376833625.AddEnumAndStructureInfo(mb);          

			if(isRoot)
			{
				mb.AddItem(this.MetaName, this.MetaStructure);

				this.Meta = mb.GetMeta();
			}
		}

    }
}
