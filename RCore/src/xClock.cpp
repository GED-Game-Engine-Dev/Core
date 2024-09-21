#include <GED/Core/Clock.h>
#include <chrono>

using Clock_t = std::chrono::steady_clock;
static time_t castDuration;
static const auto start = Clock_t::now();
ae2f_extern time_t GED_Core_Clock_Now(time_t NewCastDuration) {
	if (NewCastDuration) castDuration = NewCastDuration;
	return std::chrono::duration_cast<std::chrono::nanoseconds>(Clock_t::now() - start).count() / castDuration;
}