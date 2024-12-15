namespace GED.Core.SanityCheck {
    using errint_t = byte;

    public static class States {
        
        public const errint_t
        OK = 0,
        // Failed to find the function on preprocessor which is callable for some reason
        // No operation has beed done.
        IMP_NOT_FOUND = 1,

        // Failed to refer the pointer either l-value inside the function.
        PTR_IS_NULL = 0b10,

        // Failed freeing the memory.
        FLUSH_FAILED = 0b100,

        // stdlib allocating functions (malloc, calloc, realloc) has been failed.
        ALLOC_FAILED = 0b1000,

        // Found that operation is invalid inside the function.
        // The operation may have been ceased while the middle.
        WRONG_OPERATION = 0b10000,

        // Does not mean a thing.
        // Just a Largest value of [ae2f_errGlobal] field.
        NFOUND = 0b100000,
        
        // The operation went done.
        // Note that operation may not be valid.
        DONE_HOWEV = 0b1000000;
    }
}