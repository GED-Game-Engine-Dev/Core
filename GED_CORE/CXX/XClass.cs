namespace GED.Core.CXX
{
    public unsafe class iXClass
    {
        private byte[] _Object;

        public iXClass(UIntPtr Length)
        { 
            
            _Object = new byte[Length]; 
        }

        public byte[] Raw {  get => _Object; }
    }
}