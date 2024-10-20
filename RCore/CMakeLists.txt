project("RCore")

add_library(
    ${PROJECT_NAME} SHARED
    "include/GED/Core/Clock.h"
    "include/GED/Core/Ctrl/MousePoint.h" 
    "include/GED/Core/Ctrl/Event.h" 

    "src/Ctrl/MousePoint.c"
    "src/Ctrl/Event.c"
    
    "src/xClock.cpp"
    "src/Camera.c"
    "src/BmpSrc.c"
)


if(WIN32)
    message(STATUS "Building with MinGW")
    set(DLL_OUTPUT_PREFIX "lib")
    set(DLL_OUTPUT_SUFFIX ".dll")
    set(ae2f_IS_WIN ON)
    set(ae2f_IS_LINUX OFF)
elseif(UNIX)
    message(STATUS "Not using MinGW")
    set(DLL_OUTPUT_PREFIX "")
    set(DLL_OUTPUT_SUFFIX ".so")
    set(ae2f_IS_LINUX ON)
    set(ae2f_IS_WIN OFF)
else()
    message(FATAL_ERROR "No imp") # 구현 없음 ㅺ
endif()



include(FetchContent)
find_package(Bmp)
if(NOT Bmp_FOUND)
    message("ae2f::Bmp not found")

    FetchContent_Declare(
        Bmp
        GIT_REPOSITORY https://github.com/yuisanae2f/ae2f_Bmp.git
        GIT_TAG "Rel-v2.2.0"
        SOURCE_DIR ${CMAKE_CURRENT_BINARY_DIR}/external/ae2f/Bmp
    )
    
    FetchContent_MakeAvailable(Bmp)

    target_link_libraries(${PROJECT_NAME} Bmp)
else()
    target_link_libraries(${PROJECT_NAME} ae2f::Bmp)
endif()


include_directories(include)

# Header
target_include_directories(${PROJECT_NAME}
    INTERFACE
        $<BUILD_INTERFACE:${CMAKE_CURRENT_SOURCE_DIR}/include>
        $<INSTALL_INTERFACE:${CMAKE_INSTALL_INCLUDEDIR}>
)

# ��ġ ����
install(TARGETS ${PROJECT_NAME}
    EXPORT ${PROJECT_NAME}Targets
    LIBRARY DESTINATION ${CMAKE_INSTALL_LIBDIR}
    INCLUDES DESTINATION ${CMAKE_INSTALL_INCLUDEDIR}
)

install(DIRECTORY include/
    DESTINATION ${CMAKE_INSTALL_INCLUDEDIR}/
)

# ��Ű�� ����
install(EXPORT ${PROJECT_NAME}Targets
    FILE ${PROJECT_NAME}Targets.cmake
    NAMESPACE GED::
    DESTINATION ${CMAKE_INSTALL_LIBDIR}/cmake
)

# ��Ű�� ���� ���� ����
include(CMakePackageConfigHelpers)
configure_package_config_file(
    "${CMAKE_CURRENT_SOURCE_DIR}/Config.cmake.in"
    "${CMAKE_CURRENT_BINARY_DIR}/${PROJECT_NAME}Config.cmake"
    INSTALL_DESTINATION ${CMAKE_INSTALL_LIBDIR}/cmake/
)

install(FILES
    "${CMAKE_CURRENT_BINARY_DIR}/${PROJECT_NAME}Config.cmake"
    DESTINATION ${CMAKE_INSTALL_LIBDIR}/cmake
)


set(DLL_OUTPUT_DIR "${CMAKE_CURRENT_SOURCE_DIR}/../GED_CORE")

add_custom_command(
    TARGET ${PROJECT_NAME}
    POST_BUILD
    COMMAND ${CMAKE_COMMAND} -E copy $<TARGET_FILE:${PROJECT_NAME}> ${DLL_OUTPUT_DIR}/${DLL_OUTPUT_PREFIX}RCore${DLL_OUTPUT_SUFFIX}
    COMMENT "Copying ${PROJECT_NAME} to ${DLL_OUTPUT_DIR}/${DLL_OUTPUT_PREFIX}RCore${DLL_OUTPUT_SUFFIX}"
)

add_custom_command(
    TARGET ${PROJECT_NAME}
    POST_BUILD
    COMMAND ${CMAKE_COMMAND} -E copy $<TARGET_FILE:Bmp> ${DLL_OUTPUT_DIR}/${DLL_OUTPUT_PREFIX}Bmp${DLL_OUTPUT_SUFFIX}
    COMMENT "Copying Bmp to ${DLL_OUTPUT_DIR}/${DLL_OUTPUT_PREFIX}Bmp${DLL_OUTPUT_SUFFIX}"
)

add_custom_command(
    TARGET ${PROJECT_NAME}
    POST_BUILD
    COMMAND ${CMAKE_COMMAND} -E copy $<TARGET_FILE:Core> ${DLL_OUTPUT_DIR}/${DLL_OUTPUT_PREFIX}Core${DLL_OUTPUT_SUFFIX}
    COMMENT "Copying Core to ${DLL_OUTPUT_DIR}/${DLL_OUTPUT_PREFIX}Core${DLL_OUTPUT_SUFFIX}"
)

add_subdirectory(test)