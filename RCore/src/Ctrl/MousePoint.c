#include <GED/Core/Ctrl/MousePoint.h>

double
GED_Core_Ctrl_MousePoint_X,
GED_Core_Ctrl_MousePoint_Y;

__declspec(dllexport) double* GED_Core_Ctrl_MousePoint_ptrX() {
	return &GED_Core_Ctrl_MousePoint_X;
}

__declspec(dllexport) double* GED_Core_Ctrl_MousePoint_ptrY() {
	return &GED_Core_Ctrl_MousePoint_Y;
}