
using GED.SanityCheck;

namespace GED.Core.Manager
{
    public static class Bitmap
    {
        static internal List<byte[]> list;

        static Bitmap()
        {
            list = new List<byte[]>();
        }

        static public int Length {
            get { return list.Count; }
        }

        static public int Emplace(int index, ref byte[] raw)
        {
            if (index >= list.Count)
                return (int)FuckedNumbers.WRONG_OPERATION;

            byte[] hey = new byte[raw.Length];

            if (hey == null)
                return (int)FuckedNumbers.PTR_IS_NULL;

            try
            {
                Array.Copy(raw, hey, raw.Length);
                list[index] = hey;
            } catch
            {
                return (int)FuckedNumbers.WRONG_OPERATION;
            }

            return (int)FuckedNumbers.OK;
        }

        static public int EmplaceBack(byte[] raw)
        {
            byte[] hey = new byte[raw.Length];

            if (hey == null)
                return (int)FuckedNumbers.PTR_IS_NULL;

            try
            {
                Array.Copy(raw, hey, raw.Length);
                list.Add(hey);
            }
            catch
            {
                return (int)FuckedNumbers.WRONG_OPERATION;
            }

            return (int)FuckedNumbers.OK;
        }

        static public int GetSource(int index, out BmpSource retval)
        {
            int err;
            if (index >= list.Count)
            {
                retval = new BmpSource(out err);
                return (int)FuckedNumbers.WRONG_OPERATION;
            }

            retval = new BmpSource(out err, list[index]);
            return (int)FuckedNumbers.OK;
        }
    }
}
