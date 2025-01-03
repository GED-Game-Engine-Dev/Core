include(cmake/Core.cmake)

if(NOT ae2f_clincludecleaned)
    set(ae2f_clincludecleaned ON CACHE STRING "")
    file(REMOVE_RECURSE ${ae2f_ProjRoot}/clinclude)
endif()

# @brief 
# Copies all files under wanted include path to /clinclude
# 
# @param ... 
# Absolute path where the files will be collected.
#
# @warning
# Notice that this must be absolute path
function(ae2fCL_CoreAppendInclude)
    foreach(prm_IncludeDir ${ARGN})
        file(GLOB_RECURSE INCLUDE_TAR "${prm_IncludeDir}/**")
        
        foreach(tar IN LISTS INCLUDE_TAR)
            file(RELATIVE_PATH tar_rel "${prm_IncludeDir}" "${tar}")
            configure_file("${prm_IncludeDir}/${tar_rel}" "${ae2f_ProjRoot}/clinclude/${tar_rel}")
        endforeach()

        unset (INCLUDE_TAR)
        unset (tar_rel)
    endforeach()
endfunction()

# @brief 
# Make a configuration target for a ae2fCL Projects
#
# @param prm_ProjName 
# Your project name
#
# @param prm_SrcScanTar 
# The directory where your project's cl code exists.
#
# @warning
# Notice that this must be absolute.
function(ae2fCL_CoreAddConfProjTarDep prm_ProjName prm_SrcScanTar)
    if(NOT OpenCL_FOUND)
        find_package(OpenCL REQUIRED)
        link_libraries(OpenCL::OpenCL)
    endif()
    if(NOT TARGET "${prm_ProjName}-CLConfig")
        target_link_libraries(${prm_ProjName} PUBLIC OpenCL::OpenCL)

        add_custom_target(
            "${prm_ProjName}-CLConfig" COMMAND python
            ${ae2f_ProjRoot}/cmake/CL-CoreConfig.py 
            ${prm_SrcScanTar} ${OpenCL_INCLUDE_DIR} 
            ${CMAKE_C_COMPILER}
        )

        add_dependencies("${prm_ProjName}" "${prm_ProjName}-CLConfig")
        add_dependencies("${prm_ProjName}" OpenCL::OpenCL)
    endif()
endfunction()