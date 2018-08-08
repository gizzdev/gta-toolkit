namespace RageLib.Resources.GTA5.PC.Meta.ExtendedTypes
{
    public class MetaStructureWrapper<T> where T : struct
    {
        public MetaName MetaName;
        public T MetaStructure;

        public MetaStructureWrapper(MetaName MetaName)
        {
            this.MetaName = MetaName;
        }
    }
}
