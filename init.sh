git submodule init && git submodule update
cd mod/GED/RCore
sh init.sh
cd ../../../

rm -rf ./build

if [[ "$OSTYPE" == "linux-gnu"* ]]; then
    cmake -DCMAKE_BUILD_TYPE:STRING=Release -DCMAKE_EXPORT_COMPILE_COMMANDS:BOOL=TRUE -DCMAKE_C_COMPILER:STRING=gcc -DCMAKE_CXX_COMPILER:STRING=g++ -S./ -B./build
elif [[ "$OSTYPE" == "cygwin" ]]; then
    cmake -DCMAKE_BUILD_TYPE:STRING=Release -DCMAKE_EXPORT_COMPILE_COMMANDS:BOOL=TRUE -DCMAKE_C_COMPILER:STRING=gcc -DCMAKE_CXX_COMPILER:STRING=g++ -S./ -B./build -G "MinGW Makefiles"
elif [[ "$OSTYPE" == "msys" ]]; then
    cmake -DCMAKE_BUILD_TYPE:STRING=Release -DCMAKE_EXPORT_COMPILE_COMMANDS:BOOL=TRUE -DCMAKE_C_COMPILER:STRING=gcc -DCMAKE_CXX_COMPILER:STRING=g++ -S./ -B./build -G "MinGW Makefiles"
else 
    echo "We can't give that $OSTYPE"
fi

cmake --build ./build --config Release