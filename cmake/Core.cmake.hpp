#include "cmake.hpp"

namespace ___DOC_CMAKE {
    /// @brief 
    /// When activated, it would generate project with the deaders of cmake utility functions.
    /// 
    /// Default value is ON.
    option ae2f_DOC = ON;

    /// @brief
    /// Activating this flag will cause all libraries built as shared libraries.
    option ae2f_IS_SHARED = OFF;

    /// @brief When activated, it would generate test projects
    option ae2f_TEST = ON;

    /// @brief Pre-defined library prefix.
    /// @warning Note that value is auto-generated.
    CACHE STRING ae2f_LIBPREFIX = ae2f_IS_SHARED ? "SHARED" : "STATIC";

    /// @brief The root project source directory.
    CACHE STRING ae2f_ProjRoot = "${CMAKE_CURRENT_SOURCE_DIR}";

    /// @brief The root binary directory.
    CACHE STRING ae2f_BinRoot = "${CMAKE_CURRENT_BINARY_DIR}";

    /// @brief
    /// Set the structure pack for pre-defined structures from interfaces.
    ///
    /// If it is set to zero, default structure pack will be preserved.
    ///
    /// Default value is 0.
    constexpr unsigned int ae2f_packcount = 0;

    /// @brief
    /// Pre-defined global float type. \n
    /// It is set to float as default.
    using ae2f_float = float;

    /// @brief
    /// Makes a Library installable.
    /// 
    /// @param prm_TarName
    /// Library name you want.
    /// 
    /// @param prm_TarPrefix
    /// [STATIC | SHARED | INTERFACE]
    /// 
    /// @param prm_includeDir
    /// The include directory relative to the project CMakeLists.txt
    /// 
    /// @param prm_namespace
    /// Namespace (or header root directory after include)
    /// 
    /// @param ...
    /// The sources for the project.
    function ae2f_CoreLibTent(
        param prm_TarName, 
        param prm_TarPreFix, 
        param prm_includeDir, 
        param prm_namespace, 
        ...
    );

    /// @brief
    /// Iterates a directory `prm_TestSourcesDir` and 
    /// Make a test case for every source.
    /// 
    /// @param prm_LibName
    /// Base Library name
    /// 
    /// @param prm_TestSourcesDir
    /// A directory where the stand-alone test codes locate. \n
    /// Every sources under that directory must be stand-alone, which means it must not depends on another memory, function, etc.
    /// 
    /// @param ...
    /// Additional Libraries if you want
    /// 
    /// @see ___DOC_CMAKE::ae2f_TEST
    function ae2f_CoreTestTent(
        param prm_LibName, 
        param prm_TestSourcesDir, 
        ...
    );

    /// @brief
    /// Generate an interface project for document code for cmake utility functions. \n
    /// Available when ___DOC_CMAKE::ae2f_DOC is ON.
    /// 
    /// @param prm_TarName
    /// Library name you want.
    /// 
    /// @param prm_includeDir
    /// Where the documents exist
    /// The include directory relative to the project CMakeLists.txt
    /// 
    /// @param prm_namespace
    /// Namespace (or header root directory after include)
    /// 
    /// @param ...
    /// The past documents name
    /// @see ___DOC_CMAKE::ae2f_CoreLibTent
    /// @see ___DOC_CMAKE::ae2f_DOC
    function ae2f_CoreUtilityDocTent(
        param prm_TarName, 
        param prm_includeDir, 
        param prm_namespace,
        ...
    );

    /// @brief 
    /// It will try to fetch the cmake project ae2f-Core like project for Local and Github. \n
    /// @see ___DOC_CMAKE::ae2f_LibDirGlob is the given path to check. \n 
    /// 
    /// Once the project is in given directory, it will not try to fetch it from internet.
    /// @param prm_AuthorName 
    /// Author name
    /// @param prm_TarName
    /// Target name must be the repository's name.
    /// @param prm_TagName
    /// Tag name
    function ae2f_CoreLibFetch(
        param prm_AuthorName,
        param prm_TarName,
        param prm_TagName
    );
}