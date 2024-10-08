#include <GED/Core/Camera.h>
#include <stdlib.h>
#include <string.h>

ae2f_SHAREDEXPORT ae2f_extern 
ae2f_errint_t GED_Core_Camera_Buff_All(GED_Core_Camera_t* _this, ae2f_struct ae2f_Bmp_cSrc* dest, uint32_t background_asRGB) {
    size_t a; ae2f_errint_t code = ae2f_errGlobal_OK;
    if((code = ae2f_ds_Alloc_vOwner_getSize(_this, &a)) != ae2f_errGlobal_OK)
    goto DONE;

    if((code = ae2f_Bmp_cSrc_Fill(dest, background_asRGB)) != ae2f_errGlobal_OK)
    goto DONE;

    for(size_t i = 0; i < a; i++) {
        ae2f_struct GED_Core_Camera_El _element;
        if((code = ae2f_ds_Alloc_vOwner_Read(
            _this, 
            i * sizeof(struct GED_Core_Camera_El), 
            &_element, 
            sizeof(struct GED_Core_Camera_El))) != ae2f_errGlobal_OK
        ) goto DONE;

        if(!_element.Source.Addr)
        continue;

        if((code = ae2f_Bmp_cSrc_Copy(dest, &_element.Source, &_element.SourceLinked)) != ae2f_errGlobal_OK)
        goto DONE;

        if((code = ae2f_ds_Alloc_vOwner_Write(_this, i * sizeof(struct GED_Core_Camera_El), &_element, sizeof(struct GED_Core_Camera_El))) != ae2f_errGlobal_OK)
        goto DONE;
    }

    DONE:
    return code;
}

ae2f_SHAREDEXPORT ae2f_extern 
ae2f_errint_t GED_Core_Camera_Resize(GED_Core_Camera_t* _this, size_t count) {
    return ae2f_ds_Alloc_vOwner_reSize(_this, count * sizeof(struct GED_Core_Camera_El));
}

ae2f_SHAREDEXPORT ae2f_extern 
ae2f_errint_t GED_Core_Camera_Free(GED_Core_Camera_t* _this) {
    return ae2f_ds_Alloc_vOwner_Del(_this);
}