#if !defined(GED_Core_Ctrl_Event_h)
#define GED_Core_Ctrl_Event_h
#include <ae2f/DataStructure/Array.h>
#include <ae2f/Macro/Cast.h>

// inside is Allocator::cOwner. (aka ae2f_ds_Alloc_Owner)
typedef ae2f_struct ae2f_ds_Alloc_Owner GED_Core_Ctrl_Ev_t;
typedef bool(*GED_Core_Ctrl_Ev_fpCond_t)(void*);

ae2f_extern __declspec(dllimport) void GED_Core_Ctrl_Ev_Kill(GED_Core_Ctrl_Ev_t* mgr);
ae2f_extern __declspec(dllimport) ae2f_errint_t GED_Core_Ctrl_Ev_Resize(GED_Core_Ctrl_Ev_t* mgr, size_t size);
ae2f_extern __declspec(dllimport) ae2f_errint_t GED_Core_Ctrl_Ev_Sort(GED_Core_Ctrl_Ev_t* mgr, const ae2f_ds_Arr_fpElCmp_t fpcmp);

// No Element has met the condition.
#define GED_Core_Ctrl_Ev_GetRange_COND_MET_NONE ae2f_errGlobal_LMT

ae2f_extern __declspec(dllimport) ae2f_errint_t GED_Core_Ctrl_Ev_GetRange(const GED_Core_Ctrl_Ev_t* mgr, const GED_Core_Ctrl_Ev_fpCond_t fpcond, size_t* Min, size_t* Max);
ae2f_extern __declspec(dllimport) ae2f_errint_t GED_Core_Ctrl_Ev_Element(const GED_Core_Ctrl_Ev_t* mgr, size_t i, size_t* elSize, void** lpEl);
#endif // GED_Core_Ctrl_Custom_h