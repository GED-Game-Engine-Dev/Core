#if !defined(GED_Core_Camera_h)
#define GED_Core_Camera_h

#include <ae2f/Bmp/Src.hpp>
#include <ae2f/DataStructure/Array.h>

struct GED_Core_Camera_El {
    ae2f_struct ae2f_Bmp_cSrc Source;
    ae2f_struct ae2f_Bmp_cSrc_Copy_Global SourceLinked;
    // ae2f_struct ae2f_Bmp_cSrc_Copy_Global ChangeQuery;
};

typedef ae2f_struct ae2f_ds_Alloc_Owner GED_Core_Camera_t;


/// @param This:ae2f_ds_Alloc_Owner 
#define GED_Core_Camera_Init(This) ae2f_ds_Alloc_vOwner_InitAuto(This)

ae2f_SHAREDCALL ae2f_extern ae2f_errint_t GED_Core_Camera_Buff_All(GED_Core_Camera_t* _this, ae2f_struct ae2f_Bmp_cSrc* dest, uint32_t background);
ae2f_SHAREDCALL ae2f_extern ae2f_errint_t GED_Core_Camera_Resize(GED_Core_Camera_t* _this, size_t count);
ae2f_SHAREDCALL ae2f_extern ae2f_errint_t GED_Core_Camera_Free(GED_Core_Camera_t* _this);

ae2f_SHAREDCALL ae2f_extern ae2f_errint_t GED_Core_Camera_Append(
    GED_Core_Camera_t* _this, 
    const ae2f_struct GED_Core_Camera_El* element
);

#endif