#if !defined(GED_Core_Clock_h)
#define GED_Core_Clock_h
#include <ae2f/Macro/Cast.h>
#include <ae2f/Macro/Call.h>
#include <ctime>


ae2f_extern ae2f_SHAREDCALL time_t GED_Core_Clock_Now(time_t NewCastDuration = 0);
#endif // !defined(GED_Core_Clock_h)