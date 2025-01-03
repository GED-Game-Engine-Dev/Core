cmake_minimum_required(VERSION 3.10)
enable_testing()
option(BUILD_SHARED_LIBS "" ON)
option(ae2f_IS_SHARED "" ON)
option(ae2f_mov_IS_64 "" OFF)

include(cmake/Core.cmake)

set(ae2f_float float CACHE STRING "Float type for the template.")
option(ae2f_DOC " " OFF)
option(ae2f_TEST "" OFF)

project(GED_CORE VERSION 1.9.3)
ae2f_CoreLibFetch(GED-Game-Engine-Dev RCore Rel-v2.7.12)

if(ae2f_mov_IS_64)
    set(ae2f_mov_IS_64_INTTYPE_t long)
    message(FATAL_ERROR "64-bit for mov is not implemented yet.")
else()
    set(ae2f_mov_IS_64_INTTYPE_t int)
endif()

configure_file("${PROJECT_SOURCE_DIR}/GED_CORE_HIN/Cam/RectPrm.cs" "${PROJECT_SOURCE_DIR}/GED_CORE/Cam/RectPrm.cs")
configure_file("${PROJECT_SOURCE_DIR}/GED_CORE_HIN/Ctrl/MousePoint.cs" "${PROJECT_SOURCE_DIR}/GED_CORE/Ctrl/MousePoint.cs")
configure_file("${PROJECT_SOURCE_DIR}/GED_CORE_HIN/DisplayWizard/iWinPtr.cs" "${PROJECT_SOURCE_DIR}/GED_CORE/DisplayWizard/iWinPtr.cs")
configure_file("${PROJECT_SOURCE_DIR}/GED_CORE_HIN/SanityCheck/Dim2.cs" "${PROJECT_SOURCE_DIR}/GED_CORE/SanityCheck/Dim2.cs")


add_custom_target("${PROJECT_NAME}_Config" ALL)

if(WIN32)
    set(lib_ext ".dll")
elseif(APPLE)
    set(lib_ext ".dylib")
else()
    set(lib_ext ".so")
endif()

foreach(tar RCore Bmp Ds err RCore CL-Bmp)
    add_custom_command(TARGET ${PROJECT_NAME}_Config POST_BUILD
        COMMAND ${CMAKE_COMMAND} -E copy
        $<TARGET_FILE:${tar}>
        ${PROJECT_SOURCE_DIR}/GED_CORE/lib${tar}${lib_ext}
    )
endforeach()
