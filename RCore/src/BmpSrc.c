#include <ae2f/Bmp/Src.h>

ae2f_SHAREDEXPORT size_t
GED_Core_BmpSrc_size() {
    return sizeof(struct ae2f_Bmp_cSrc);
}

ae2f_SHAREDEXPORT ae2f_errint_t
GED_Core_BmpSrc_init(
    struct ae2f_Bmp_cSrc* _this,
    size_t Width,
    size_t Height,
    uint8_t elsize,
    uint8_t* Addr
) {
    switch(elsize) {
        case 24:
        case 32: break;

        default: return ae2f_errGlob_IMP_NOT_FOUND;
    }

    if(!(Addr && _this)) return ae2f_errGlob_PTR_IS_NULL;

    _this->Addr = Addr;
    _this->ElSize = elsize;

    _this->rIdxer.Count = _this->rIdxer.Width = _this->rIdxer.IdxXJump = Width;
    _this->rIdxer.CurrX = 0;
    _this->rIdxer.Count *= Height;

    return ae2f_errGlob_OK;
}