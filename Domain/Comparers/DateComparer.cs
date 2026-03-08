using System.Collections;
using lab_1_double_s.Domain.Tasks;

namespace lab_2_double_s_Feature_team9.Domain.Comparers
{
    public class DateComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            var t1 = x as TodoTask;
            var t2 = y as TodoTask;
            if (t1 == null || t2 == null) return 0;
            return DateTime.Compare(t1.Date, t2.Date);
        }
    }
}