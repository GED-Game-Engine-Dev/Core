project("RCore")

add_library(
    ${PROJECT_NAME} SHARED
    "include/GED/Core/Clock.h"
    "src/xClock.cpp"
    "include/GED/Core/Ctrl/MousePoint.h" 
    "src/Ctrl/MousePoint.c"
 "include/GED/Core/Ctrl/Event.h" "src/Ctrl/xEvent.cpp")

include(FetchContent)
find_package(Bmp)
if(NOT Bmp_FOUND)
    message("ae2f::Bmp not found")

    FetchContent_Declare(
        Bmp
        GIT_REPOSITORY https://github.com/yuisanae2f/ae2f_Bmp.git
        GIT_TAG "Rel-v2.1.0"
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
    INSTALL_DESTINATION ${CMAKE_INSTALL_LIBDIR}/cmake/GED
)

install(FILES
    "${CMAKE_CURRENT_BINARY_DIR}/${PROJECT_NAME}Config.cmake"
    DESTINATION ${CMAKE_INSTALL_LIBDIR}/cmake
)


set(DLL_OUTPUT_DIR "${CMAKE_CURRENT_SOURCE_DIR}/../GED_CORE/lib/RCore.dll")
add_custom_command(
    TARGET ${PROJECT_NAME}
    POST_BUILD
    COMMAND ${CMAKE_COMMAND} -E copy $<TARGET_FILE:${PROJECT_NAME}> ${DLL_OUTPUT_DIR}
    COMMENT "Copying ${PROJECT_NAME} to ${DLL_OUTPUT_DIR}"
)