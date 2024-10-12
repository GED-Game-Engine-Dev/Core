using Avalonia.Platform;
using GED.Core.CXX;
using GED.SanityCheck;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GED.Core
{
    internal static partial class fCamera {
        [LibraryImport(DllNames.RCore, EntryPoint = "GED_Core_Camera_Buff_All")]
        public static partial int BuffAll(nint _this, nint dest, uint background_asRGB);

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_Core_Camera_Resize")]
        public static partial int Resize(nint _this, nuint count);

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_Core_Camera_Free")]
        public static partial int Free(nint _this);

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_Core_Camera_Make")]
        public static partial int Make(nint _this);

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_Core_Camera_Read")]
        public static partial int Read(nint _this, nint Element, nuint index);

        [LibraryImport(DllNames.RCore, EntryPoint = "GED_Core_Camera_Write")]
        public static partial int Write(nint _this, nint Element, nuint index);
    }

    internal struct CameraField {
        public nint A;
        public nint B;
        public nint C;
    }

    public class Camera
    {
        internal XClassMem<CameraField> memory;
        public Camera(out int _err) {
            memory = new XClassMem<CameraField>(out _err);
            if(_err != (int)FuckedNumbers.OK)
            return;
            _err = fCamera.Make(memory.bytes);
        }

        public int Resize(nuint count) => fCamera.Resize(memory.bytes, count);
        ~Camera() => fCamera.Free(memory.bytes);

        public int BuffAll(ref BmpSource src, uint Colour_Background) {
            int code = 0;
            if(code != 0) {
                code = fCamera.BuffAll(memory.bytes, src.memory.bytes, Colour_Background);
            }
            return code;
        }
        internal struct CopyParameterField {
            public byte a;
            public uint b, c, d, e, f;
        }

        internal struct ElementField {
            public BmpSourceField a;
            public CopyParameterField b;
        }

        public class Element
        {
            internal XClassMem<ElementField> memory;
            internal Element(out int state) {
                memory = new(out state);
            }
            public Element(
                out int state, 
                byte Alpha,
                uint WidthAsResized,
                uint HeightAsResized,
                uint AddrXForDest,
                uint AddrYForDest,
                uint DataToIgnore,
                in BmpSource source
            ) : this(out state) {
                ref ElementField a = ref memory.Instance();
                a.b.a = Alpha;
                a.b.b = WidthAsResized;
                a.b.c = HeightAsResized;
                a.b.d = AddrXForDest;
                a.b.e = AddrYForDest;
                a.b.f = DataToIgnore;

                ref BmpSourceField b = ref source.memory.Instance();

                a.a.a = b.a;
                a.a.b = b.b;
                a.a.c = b.c;
            }
        }

        public int Read(
            nuint index,
            out Element buffer
        ) {
            int code;
            buffer = new Element(out code);
            if(code != (int)FuckedNumbers.OK)
            return code;
            return fCamera.Read(memory.bytes, buffer.memory.bytes, index);
        }

        public int Write(
            nuint index,
            in Element buffer
        ) => fCamera.Write(memory.bytes, buffer.memory.bytes, index);
    }
}