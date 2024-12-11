enable_testing()
cmake_minimum_required(VERSION 3.10)
option(BUILD_SHARED_LIBS "" ON)
option(ae2f_IS_SHARED "" ON)
set(ae2f_float float CACHE STRING "Float type for the template.")
option(ae2f_DOC " " ON)
option(ae2f_mov_IS_64 "" OFF)

set(CMAKE_LIBRARY_OUTPUT_DIRECTORY ${CMAKE_CURRENT_SOURCE_DIR}/GED_CORE)
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${CMAKE_CURRENT_SOURCE_DIR}/GED_CORE) # Window copying troubleshoot

configure_file("${PROJECT_SOURCE_DIR}/hin/Cam/RectPrm.cs.in" "${PROJECT_SOURCE_DIR}/GED_CORE/Cam/RectPrm.cs")
configure_file("${PROJECT_SOURCE_DIR}/hin/Ctrl/MousePoint.cs.in" "${PROJECT_SOURCE_DIR}/GED_CORE/Ctrl/MousePoint.cs")
configure_file("${PROJECT_SOURCE_DIR}/hin/DisplayWizard/MainWin.cs.in" "${PROJECT_SOURCE_DIR}/GED_CORE/DisplayWizard/MainWin.cs")
add_subdirectory("mod/GED/RCore")