enable_testing()
cmake_minimum_required(VERSION 3.10)
set(BUILD_SHARED_LIBS ON)
set(ae2f_IS_SHARED ON)
set(ae2f_float float CACHE STRING "Float type for the template.")

set(CMAKE_LIBRARY_OUTPUT_DIRECTORY ${CMAKE_CURRENT_SOURCE_DIR}/GED_CORE)
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${CMAKE_CURRENT_SOURCE_DIR}/GED_CORE) # Window copying troubleshoot

configure_file("${PROJECT_SOURCE_DIR}/hin/SanityCheck/Float.cs.in" "${PROJECT_SOURCE_DIR}/GED_CORE/SanityCheck/Float.cs")
add_subdirectory("mod/GED/RCore")
