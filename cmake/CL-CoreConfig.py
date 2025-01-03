import os
import sys
import pathlib
import subprocess


HERE = os.path.dirname(os.path.abspath(__file__))
GIVEN_PATH = HERE if len(sys.argv) != 4 else (sys.argv[1])
GIVEN_CL_PATH = HERE if len(sys.argv) != 4 else (sys.argv[2])
GIVEN_COMPILER = "gcc" if len(sys.argv) != 4 else (sys.argv[3])

PRM_INCLUDE = f"{HERE}/../clinclude/"

print("Hello World! BmpCLConfig is running...")
print(f"Current Position: {HERE}")
print(f"Given Path: {GIVEN_PATH}")
print(f"Given Compiler: {GIVEN_COMPILER}")


# Preprocess 0: Get all source and throw it to ...cl
for file in (f for f in pathlib.Path(f'{GIVEN_PATH}/').rglob('*.cl.c') if f.is_file()):
    OUT_NAME = str(file.absolute())[:-2:]
    OUT_NAME_TMP = OUT_NAME + '.tmp.c'
    OUT_NAME_H = OUT_NAME + 'h'

    TMP_SRC : list[str]

    IN_CTN : list[str]
    with open(file, 'r') as F:
        IN_CTN = F.readlines()
        F.close()

    with open(OUT_NAME_TMP, 'w') as F:
        F.writelines([
            "#define ae2fCL_LocAsCL\n"
        ])

        F.writelines(IN_CTN)
        F.close()

    result = subprocess.run([
        GIVEN_COMPILER, '-Wno-error', 
        '-E', '-P', 
        '-I' + PRM_INCLUDE, '-I' + GIVEN_CL_PATH, 
        OUT_NAME_TMP, '-o', OUT_NAME
    ])
    os.unlink(OUT_NAME_TMP)

    
    with open(OUT_NAME_H, 'w') as F:
        with open(OUT_NAME) as F_I:
            l = "a"
            while l != "":
                l = F_I.readline()
                F.write('"' + l[:-1:]
                    .replace('\\', '\\\\')
                    .replace('"', '\\"')
                    + '\\n"\n'
                )
        pass
    
    print(result.stdout)
