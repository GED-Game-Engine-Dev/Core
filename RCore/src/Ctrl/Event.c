#include <GED/Core/Ctrl/Event.h>

ae2f_SHAREDEXPORT ae2f_errint_t GED_Core_Ctrl_Ev_Kill(GED_Core_Ctrl_Ev_t* mgr) {
    ae2f_errint_t code = ae2f_ds_Alloc_vOwner_Del(&mgr->list);
    mgr->wel = 0;
    return code;
}
ae2f_SHAREDEXPORT ae2f_errint_t GED_Core_Ctrl_Ev_Resize(GED_Core_Ctrl_Ev_t* mgr, size_t size) {
    return ae2f_ds_Alloc_vOwner_reSize(&mgr->list, ((mgr->wel)) * size);
}
ae2f_SHAREDEXPORT ae2f_errint_t GED_Core_Ctrl_Ev_Sort(GED_Core_Ctrl_Ev_t* mgr, const ae2f_ds_Arr_fpElCmp_t fpcmp) {
    if(!mgr->wel) return ae2f_errGlobal_WRONG_OPERATION;
    return ae2f_ds_Arr_QSort(ae2f_reinterpret_cast(struct ae2f_ds_Alloc_Refer*, &mgr->list), mgr->wel, fpcmp);
}
ae2f_SHAREDEXPORT ae2f_errint_t GED_Core_Ctrl_Ev_GetRange(const GED_Core_Ctrl_Ev_t* mgr, const GED_Core_Ctrl_Ev_fpCond_t fpcond, size_t* Min, size_t* Max) {
    size_t lenbuff; ae2f_errint_t code;
    if(!mgr->wel) return ae2f_errGlobal_WRONG_OPERATION;

    if((code = ae2f_ds_Alloc_vOwner_getSize(&mgr->list, &lenbuff)) != ae2f_errGlobal_OK)
    return code;

    unsigned found = 0;
    Min[0] = 0;

    char Buff[255];

    for(size_t* i = Min; i[0] < lenbuff / mgr->wel; i[0]++) {

        if((code = ae2f_ds_Alloc_vOwner_Read(&mgr->list, i[0] * mgr->wel, Buff, mgr->wel)) != ae2f_errGlobal_OK)
        return code;

        switch (found += fpcond(Buff))
        {
            case 1: {
                Max[0] = i[0];
                i = Max;
            }
            case 2: return ae2f_errGlobal_OK;
        }
    }

    switch(found) {
        case 0: return GED_Core_Ctrl_Ev_GetRange_COND_MET_NONE;
        case 1: return GED_Core_Ctrl_Ev_GetRange_COND_MET_ONLY_ONE;
        case 2: return ae2f_errGlobal_OK;
    }
}
ae2f_SHAREDEXPORT ae2f_errint_t GED_Core_Ctrl_Ev_Element(const GED_Core_Ctrl_Ev_t* mgr, size_t i, void* lpEl) {
    return ae2f_ds_Alloc_vOwner_Read(&mgr->list, mgr->wel * i, lpEl, mgr->wel);
}
ae2f_SHAREDEXPORT ae2f_errint_t GED_Core_Ctrl_Ev_Element_Set(const GED_Core_Ctrl_Ev_t* mgr, size_t i, const void* lpEl) {
    return ae2f_ds_Alloc_vOwner_Write(&mgr->list, mgr->wel * i, lpEl, mgr->wel);
}
ae2f_SHAREDEXPORT size_t GED_Core_Ctrl_Ev_TypeSize() {
    return sizeof(struct GED_Core_Ctrl_Ev);
}
ae2f_SHAREDEXPORT ae2f_errint_t GED_Core_Ctrl_Ev_Make(GED_Core_Ctrl_Ev_t* mgr, uint8_t elsize) {
    if(!elsize) return ae2f_errGlobal_WRONG_OPERATION;
    mgr->wel = elsize;
    return ae2f_ds_Alloc_vOwner_InitAuto(&mgr->list);
}