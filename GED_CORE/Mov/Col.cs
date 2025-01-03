namespace GED.Core {
    /// <summary>
    /// Stands for direction vector. <br/>
    /// Could represent multiple directions or so.
    /// </summary>
    public struct MovCol_t {
        /// <summary>
        /// It contains raw value.
        /// </summary>
        public byte raw;

        public const byte 
        C = 0b0000,
        L = 0b0001,
        R = 0b0010,
        U = 0b0100,
        D = 0b1000;
    };
}