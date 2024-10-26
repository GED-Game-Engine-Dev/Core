#include <GED/Core/Camera.h>
#include <stdlib.h>
#include <string.h>

#undef GED_Core_Camera_Read
#undef GED_Core_Camera_Write
#undef GED_Core_Camera_Make
#undef GED_Core_Camera_Free
#undef GED_Core_Camera_Resize

#include <stdio.h>

ae2f_SHAREDEXPORT
ae2f_errint_t GED_Core_Camera_Buff_All(GED_Core_Camera_t* _this, ae2f_struct ae2f_Bmp_cSrc* dest, uint32_t background_asRGB) {
    size_t a; ae2f_errint_t code = ae2f_errGlobal_OK;
    if((code = ae2f_ds_Alloc_vOwner_getSize(_this, &a)) != ae2f_errGlobal_OK)
    goto DONE;

    switch (background_asRGB)
    {
    default: {
        if((code = ae2f_Bmp_cSrc_Fill(dest, background_asRGB)) != ae2f_errGlobal_OK)
        goto DONE;
    } break;
    case (uint32_t)-1:
        break;
    }

    for(size_t i = 0; i < a; i+=sizeof(struct GED_Core_Camera_El)) {
        ae2f_struct GED_Core_Camera_El _element;
        if((code = ae2f_ds_Alloc_vOwner_Read(
            _this, 
            i, 
            &_element, 
            sizeof(struct GED_Core_Camera_El))) != ae2f_errGlobal_OK
        ) goto DONE;

        if(!_element.Source.Addr) continue;

        if((code = ae2f_Bmp_cSrc_Copy(dest, &_element.Source, &_element.SourceLinked)) != ae2f_errGlobal_OK) // Real
        goto DONE;

        if((code = ae2f_ds_Alloc_vOwner_Write(_this, i, &_element, sizeof(struct GED_Core_Camera_El))) != ae2f_errGlobal_OK)
        goto DONE;
    }

    DONE:
    return code;
}

ae2f_SHAREDEXPORT
ae2f_errint_t GED_Core_Camera_Resize(GED_Core_Camera_t* _this, size_t count) {
    return ae2f_ds_Alloc_vOwner_reSize(_this, count * sizeof(struct GED_Core_Camera_El));
}

ae2f_SHAREDEXPORT
ae2f_errint_t GED_Core_Camera_Free(GED_Core_Camera_t* _this) {
    return ae2f_ds_Alloc_vOwner_Del(_this);
}

ae2f_SHAREDEXPORT
ae2f_errint_t GED_Core_Camera_Make(GED_Core_Camera_t* _this) {
    return ae2f_ds_Alloc_vOwner_InitAuto(_this);
}

ae2f_SHAREDEXPORT
ae2f_errint_t GED_Core_Camera_Read(const GED_Core_Camera_t* _this, struct GED_Core_Camera_El* buff, size_t i) {
    return ae2f_ds_Alloc_vOwner_Read(_this, i * sizeof(struct GED_Core_Camera_El), buff, sizeof(struct GED_Core_Camera_El));
}

ae2f_SHAREDEXPORT
ae2f_errint_t GED_Core_Camera_Write(GED_Core_Camera_t* _this, struct GED_Core_Camera_El* buff, size_t i) {
    return ae2f_ds_Alloc_vOwner_Write(_this, i * sizeof(struct GED_Core_Camera_El), buff, sizeof(struct GED_Core_Camera_El));
}

ae2f_SHAREDEXPORT size_t
GED_Core_Camera_Size() {
    return sizeof(GED_Core_Camera_t);
}

ae2f_SHAREDEXPORT size_t
GED_Core_Camera_El_Size() {
    return sizeof(struct GED_Core_Camera_El);
}


ae2f_SHAREDEXPORT ae2f_errint_t
GED_Core_Camera_El_Init(
    struct GED_Core_Camera_El* _this,

    uint8_t Alpha,
    uint8_t ReverseIdx,
    uint32_t WidthAsResized,
    uint32_t HeightAsResized,
    uint32_t AddrXForDest,
    uint32_t AddrYForDest,
    uint32_t DataToIgnore,

    const struct ae2f_Bmp_cSrc* bitmapsource
) {
    if(!(_this && bitmapsource)) 
    return ae2f_errGlobal_PTR_IS_NULL;

    _this->SourceLinked = (struct ae2f_Bmp_cSrc_Copy_Global) {
        .AddrXForDest = AddrXForDest,
        .AddrYForDest = AddrYForDest,
        .DataToIgnore = DataToIgnore,
        .Alpha = Alpha,
        .WidthAsResized = WidthAsResized,
        .HeightAsResized = HeightAsResized,

        .ReverseIdx = ReverseIdx,

        .ReverseIdx = 0
    };

    _this->Source = bitmapsource[0];
    return ae2f_errGlobal_OK;
}

ae2f_SHAREDEXPORT ae2f_errint_t
GED_Core_Camera_El_getParam(struct GED_Core_Camera_El* _this, struct ae2f_Bmp_cSrc_Copy_Global** param) {
    if(!(_this && param)) return ae2f_errGlobal_PTR_IS_NULL;
    param[0] = &_this->SourceLinked;
    return ae2f_errGlobal_OK;
}