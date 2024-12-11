enable_testing()
cmake_minimum_required(VERSION 3.10)
option(BUILD_SHARED_LIBS "" ON)
option(ae2f_IS_SHARED "" ON)
set(ae2f_float float CACHE STRING "Float type for the template.")
option(ae2f_DOC " " ON)
option(ae2f_mov_IS_64 "" OFF)

configure_file("${PROJECT_SOURCE_DIR}/hin/Cam/RectPrm.cs.in" "${PROJECT_SOURCE_DIR}/GED_CORE/Cam/RectPrm.cs")
configure_file("${PROJECT_SOURCE_DIR}/hin/Ctrl/MousePoint.cs.in" "${PROJECT_SOURCE_DIR}/GED_CORE/Ctrl/MousePoint.cs")
configure_file("${PROJECT_SOURCE_DIR}/hin/DisplayWizard/MainWin.cs.in" "${PROJECT_SOURCE_DIR}/GED_CORE/DisplayWizard/MainWin.cs")
add_subdirectory("mod/GED/RCore")

add_custom_target(GED_CORE_Config ALL)


foreach(tar RCore Bmp Ds err RCore CL-Bmp)
    add_custom_command(TARGET GED_CORE_Config POST_BUILD
        COMMAND ${CMAKE_COMMAND} -E copy
        $<TARGET_FILE:${tar}>
        ${CMAKE_CURRENT_SOURCE_DIR}/GED_CORE/lib${tar}
    )
endforeach()