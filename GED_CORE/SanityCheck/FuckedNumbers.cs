namespace GED.SanityCheck {
    public enum FuckedNumbers {
        OK,
        // Failed to find the function on preprocessor which is callable for some reason
        // No operation has beed done.
        IMP_NOT_FOUND,

        // Failed to refer the pointer either l-value inside the function.
        PTR_IS_NULL,

        // Failed freeing the memory.
        FLUSH_FAILED,

        // stdlib allocating functions (malloc, calloc, realloc) has been failed.
        ALLOC_FAILED,

        // Found that operation is invalid inside the function.
        // The operation may have been ceased while the middle.
        WRONG_OPERATION,

        // Does not mean a thing.
        // Just a Largest value of [ae2f_errGlobal] field.
        LMT
    }
}