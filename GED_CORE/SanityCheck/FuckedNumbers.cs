namespace GED.Core.SanityCheck {
    internal static class FuckedNumbers {
        
        public const int 
        OK = 0,
        // Failed to find the function on preprocessor which is callable for some reason
        // No operation has beed done.
        IMP_NOT_FOUND = 1,

        // Failed to refer the pointer either l-value inside the function.
        PTR_IS_NULL = 2,

        // Failed freeing the memory.
        FLUSH_FAILED = 3,

        // stdlib allocating functions (malloc, calloc, realloc) has been failed.
        ALLOC_FAILED = 4,

        // Found that operation is invalid inside the function.
        // The operation may have been ceased while the middle.
        WRONG_OPERATION = 5,

        // Does not mean a thing.
        // Just a Largest value of [ae2f_errGlobal] field.
        LMT = 6;
    }
}