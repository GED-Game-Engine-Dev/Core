#include <GED/Core/Ctrl/MousePoint.h>

ae2f_SHAREDEXPORT double
GED_Core_Ctrl_MousePoint_X,
GED_Core_Ctrl_MousePoint_Y;

ae2f_SHAREDEXPORT double* GED_Core_Ctrl_MousePoint_ptrX() {
	return &GED_Core_Ctrl_MousePoint_X;
}
ae2f_SHAREDEXPORT double* GED_Core_Ctrl_MousePoint_ptrY() {
	return &GED_Core_Ctrl_MousePoint_Y;
}