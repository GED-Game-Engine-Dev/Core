#include "cmake.hpp"

namespace ___DOC_CMAKE {
    /// @brief 
    /// Copies all files under wanted include path to /pyinclude
    ///
    /// @param ...
    /// Absolute path where the files will be collected.
    ///
    /// @warning
    /// Notice that this must be absolute path
    function ae2fCL_CoreAppendInclude(...);

    /// @brief 
    /// Make a configuration target for a ae2fCL Projects
    ///
    /// @param prm_ProjName 
    /// Your project name
    ///
    /// @param prm_SrcScanTar 
    /// The directory where your project's cl code exists.
    ///
    /// @warning
    /// Notice that this must be absolute.
    function ae2fCL_CoreAddConfProjTarDep(
        param prm_ProjName, 
        param prm_SrcScanTar
    );
}