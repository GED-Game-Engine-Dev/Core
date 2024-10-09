link_libraries(RCore)

add_executable(
    GED_RCore_test0
    test0x0/a.c
)

add_test(NAME GED_RCore_test0 COMMAND GED_RCore_test0)