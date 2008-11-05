using System;

namespace IUDICO.DataModel.Common
{
    public sealed class UniqueId
    {
        public static int Generate()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt32(buffer, 0);
        }
    }
}