project(GED_Core)
# build with csproj
include(ExternalProject)
set(PROJECT_PATH ${CMAKE_CURRENT_SOURCE_DIR}/GED_CORE.csproj)

ExternalProject_Add(
    ${PROJECT_NAME}
    SOURCE_DIR ${CMAKE_CURRENT_SOURCE_DIR}
    CONFIGURE_COMMAND ""
    BUILD_COMMAND dotnet build ${PROJECT_PATH} --configuration Release
    INSTALL_COMMAND ""
)

add_dependencies(${PROJECT_NAME} RCore)