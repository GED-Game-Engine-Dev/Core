#include <GED/Core/Ctrl/Event.h>
#include <ae2f/DataStructure/Array.h>

ae2f_extern __declspec(dllexport) size_t GED_Core_Ctrl_Ev_TypeSize() {
	return sizeof(GED_Core_Ctrl_Ev_t);
}
ae2f_extern __declspec(dllexport) ae2f_errint_t GED_Core_Ctrl_Ev_Resize(GED_Core_Ctrl_Ev_t* mgr, size_t size) {
	if (!mgr) return ae2f_errGlobal_PTR_IS_NULL;
	return ae2f_static_cast(ae2f::DataStructure::cArray<ae2f::DataStructure::Allocator::cOwner>*, mgr)->Resize(size);
}
ae2f_extern __declspec(dllexport) void GED_Core_Ctrl_Ev_Kill(GED_Core_Ctrl_Ev_t* mgr) {
	if (!mgr) return;
	ae2f_static_cast(ae2f::DataStructure::cArray<ae2f::DataStructure::Allocator::cOwner>*, mgr)->~imp_cArray();
}
ae2f_extern __declspec(dllexport) ae2f_errint_t GED_Core_Ctrl_Ev_Sort(GED_Core_Ctrl_Ev_t* mgr, const ae2f_ds_Arr_fpElCmp_t fpcmp) {
	static ae2f_ds_Arr_fpElCmp_t GED_Core_Ctrl_Ev_Sort_CMP;
	GED_Core_Ctrl_Ev_Sort_CMP = fpcmp;
	if (!(GED_Core_Ctrl_Ev_Sort_CMP && mgr))
		ae2f_errGlobal_PTR_IS_NULL;

	auto 
		fp = [](const void* a, const void* b)
		{
			return GED_Core_Ctrl_Ev_Sort_CMP(
				ae2f_reinterpret_cast(const ae2f::DataStructure::Allocator::rRefer*, a)->data, 
				ae2f_reinterpret_cast(const ae2f::DataStructure::Allocator::rRefer*, b)->data
			);
		};

	return ae2f_ds_Arr_QSort(ae2f_union_cast(GED_Core_Ctrl_Ev_t*, ae2f::DataStructure::Allocator::rRefer*, mgr), sizeof(ae2f::DataStructure::Allocator::cOwner), fp);
}
ae2f_extern __declspec(dllexport) ae2f_errint_t GED_Core_Ctrl_Ev_GetRange(const GED_Core_Ctrl_Ev_t* mgr, const GED_Core_Ctrl_Ev_fpCond_t fpcond, size_t* Min, size_t* Max) {
	if (!(mgr && fpcond && Min && Max)) return ae2f_errGlobal_PTR_IS_NULL;

	ae2f_errint_t errcode = 0;
	size_t size = ae2f_static_cast(const ae2f::DataStructure::cArray<ae2f::DataStructure::Allocator::cOwner>*, mgr)->Count(&errcode);
	if (errcode != ae2f_errGlobal_OK) return errcode;

	Min[0] = Max[0] = 0;

	
	for (; Min[0] < size; Min[0]++) {
		auto el = ae2f_static_cast(const ae2f::DataStructure::cArray<ae2f::DataStructure::Allocator::cOwner>*, mgr)->Read(Min[0], &errcode);
		if (errcode != ae2f_errGlobal_OK) return errcode;
		if (fpcond(el.obj.data)) {
			Max[0] = Min[0];
			Min = Max;
		}
		else if (Min == Max) return ae2f_errGlobal_OK;
	}

	if (Min != Max) return GED_Core_Ctrl_Ev_GetRange_COND_MET_NONE;

	return ae2f_errGlobal_OK;
}
ae2f_extern __declspec(dllexport) ae2f_errint_t GED_Core_Ctrl_Ev_Element(const GED_Core_Ctrl_Ev_t* mgr, size_t i, size_t* elSize, void** lpEl) {
	if (!(mgr && elSize && lpEl)) return ae2f_errGlobal_PTR_IS_NULL;
	ae2f_errint_t ss = 0;
	auto d = ae2f_static_cast(const ae2f::DataStructure::cArray<ae2f::DataStructure::Allocator::cOwner>*, mgr)->Read(i, &ss);
	if (ss != ae2f_errGlobal_OK) return ss;

	lpEl[0] = d.obj.data + sizeof(size_t);
	return d.obj.Length(elSize);
}