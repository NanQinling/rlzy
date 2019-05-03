using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ab;


namespace rlzy.Models
{
    class man
    {
        string UserId;
        string Name;
        public string[] pay_name;
        public float[] pay;
        public man(string x, string y)
        {
            UserId = x;
            Name = y;
        }
        public void set_kqb()
        {
            string sql = "insert into emp_remuneration(";
            for (int i = 0; i < pay_name.Length; i++)
            {
                sql += pay_name[i] + ",";
            }
            sql += ") values(";
            for (int i = 0; i < pay_name.Length; i++)
            {
                sql += pay[i].ToString() + ",";
            }
            sql += ")";
            Hc_db.do_nonquery(sql);
        }
    }
}
