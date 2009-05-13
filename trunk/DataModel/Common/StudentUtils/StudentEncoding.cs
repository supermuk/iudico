using System.Text;

namespace IUDICO.DataModel.Common.StudentUtils
{
    static class StudentEncoding
    {
        public static Encoding GetEncoding()
        {
            return Encoding.GetEncoding(1251);
        }
    }
}