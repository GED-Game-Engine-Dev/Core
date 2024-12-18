namespace GED.Core {
    /// <summary>
    /// 
    /// </summary>
    public struct MovCol_t {
        public byte raw;

        const byte 
        C = 0b0000,
        L = 0b0001,
        R = 0b0010,
        U = 0b0100,
        D = 0b1000;
    };
}