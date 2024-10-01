#include <GED/Core/Clock.h>
#include <chrono>
#include <stdio.h>

using Clock_t = std::chrono::steady_clock;
static time_t castDuration;
static const auto start = Clock_t::now();
ae2f_extern ae2f_SHAREDEXPORT time_t GED_Core_Clock_Now(time_t NewCastDuration) {
	if (NewCastDuration) castDuration = NewCastDuration;
	return std::chrono::duration_cast<std::chrono::nanoseconds>(Clock_t::now() - start).count() / castDuration;
}