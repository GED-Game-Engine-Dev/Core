#if !defined(GED_Core_Camera_h)
#define GED_Core_Camera_h

#include <ae2f/Bmp/Src.h>
#include <ae2f/Ds/Arr.h>

struct GED_Core_Camera_El {
    ae2f_struct ae2f_Bmp_cSrc Source;
    ae2f_struct ae2f_Bmp_cSrc_Copy_Global SourceLinked;
};

typedef ae2f_struct ae2f_ds_Alloc_cOwn GED_Core_Camera_t;


/// @param This:ae2f_ds_Alloc_Owner 
#define GED_Core_Camera_Init(This) ae2f_ds_Alloc_vOwner_InitAuto(This)

ae2f_SHAREDCALL ae2f_extern ae2f_errint_t GED_Core_Camera_Buff_All(GED_Core_Camera_t* _this, ae2f_struct ae2f_Bmp_cSrc* dest, uint32_t background);

#define GED_Core_Camera_Read(_this, buff, i) ae2f_ds_Alloc_vOwner_Read((_this), (i) * sizeof(struct GED_Core_Camera_El), (buff), sizeof(struct GED_Core_Camera_El))
#define GED_Core_Camera_Write(_this, buff, i) ae2f_ds_Alloc_vOwner_Write((_this), (i) * sizeof(struct GED_Core_Camera_El), (buff), sizeof(struct GED_Core_Camera_El))
#define GED_Core_Camera_Make ae2f_ds_Alloc_vOwner_InitAuto
#define GED_Core_Camera_Free ae2f_ds_Alloc_vOwner_Del
#define GED_Core_Camera_Resize(_this, count) ae2f_ds_Alloc_vOwner_reSize((_this), (count) * sizeof(struct GED_Core_Camera_El))

#endif