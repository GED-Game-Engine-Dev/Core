enable_testing()
cmake_minimum_required(VERSION 3.10)
option(BUILD_SHARED_LIBS "" ON)
option(ae2f_IS_SHARED "" ON)
set(ae2f_float float CACHE STRING "Float type for the template.")
option(ae2f_DOC " " OFF)
option(ae2f_mov_IS_64 "" ON)
option(ae2f_TEST "" ON)

if(ae2f_mov_IS_64)
    set(ae2f_mov_IS_64_INTTYPE_t long)
else()
    set(ae2f_mov_IS_64_INTTYPE_t int)
endif()


configure_file("${PROJECT_SOURCE_DIR}/hin/Cam/RectPrm.cs" "${PROJECT_SOURCE_DIR}/GED_CORE/Cam/RectPrm.cs")
configure_file("${PROJECT_SOURCE_DIR}/hin/Ctrl/MousePoint.cs" "${PROJECT_SOURCE_DIR}/GED_CORE/Ctrl/MousePoint.cs")
configure_file("${PROJECT_SOURCE_DIR}/hin/DisplayWizard/MinCtrlWin.cs" "${PROJECT_SOURCE_DIR}/GED_CORE/DisplayWizard/MinCtrlWin.cs")
add_subdirectory("mod/GED/RCore")

add_custom_target(GED_CORE_Config ALL)

if(WIN32)
    set(lib_ext ".dll")
elseif(APPLE)
    set(lib_ext ".dylib")
else()
    set(lib_ext ".so")
endif()

foreach(tar RCore Bmp Ds err RCore CL-Bmp)
    add_custom_command(TARGET GED_CORE_Config POST_BUILD
        COMMAND ${CMAKE_COMMAND} -E copy
        $<TARGET_FILE:${tar}>
        ${CMAKE_CURRENT_SOURCE_DIR}/GED_CORE/lib${tar}${lib_ext}
    )
endforeach()