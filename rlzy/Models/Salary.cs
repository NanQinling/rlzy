using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ab;

namespace rlzy
{
    class Salary
    {

        /// <summary>
        /// 获取上一期工资年月、当前工资日期、当前工资年月
        /// </summary>
        /// <param name="上一期工资年月"></param>
        /// <param name="当前工资日期"></param>
        /// <param name="当前工资年月"></param>
        public static void GetDate(out string last_year_month, out string current_date_gz, out string current_date_jj, out string current_year_month)
        {

            //当前期间判断，要根据当前发放库获取最新的工资年月。
            string s1 = "SELECT Max(发放日期) AS 当前期间 FROM ffb WHERE ffb.工资范围='VN' AND ffb.发放类型='工资' and 是否发放= TRUE";
            Hc_db.do_nonquery(s1);
            DataTable dt_last_year_month = Hc_db.get_datatable(s1);
            last_year_month = dt_last_year_month.Rows[0][0].ToString().Substring(0, 6);
            current_date_gz = DateTime.Parse($"{last_year_month.Substring(0, 4)}-{last_year_month.Substring(4, 2)}-10").AddMonths(1).ToString("yyyyMMdd");
            current_date_jj = DateTime.Parse($"{last_year_month.Substring(0, 4)}-{last_year_month.Substring(4, 2)}-20").AddMonths(1).ToString("yyyyMMdd");
            current_year_month = current_date_gz.Substring(0, 6);
        }


        /// <summary>
        /// 读取Excel考勤汇总表，并将数据写入emp_attendance表中
        /// </summary>
        public static void ImportAttendance()
        {
            string s1 = "select emp_bas.人员编号,emp_bas.姓名 from emp_bas where emp_bas.结束日期='99991231'";  //根据最新的人员情况表导入考勤数据，没用的考勤数据就不导入了。
            DataTable dt_emp_bas = Hc_db.get_datatable(s1);

            string s2 = "select 人员编号,8116 as 考勤项,应出勤天数 as 数量,考勤年月 as 考勤日期 from [Sheet1$] where 应出勤天数 is not null union all select 人员编号,8107 as 考勤项,实际出勤天数,考勤年月 as 考勤日期 from [Sheet1$] where 实际出勤天数 is not null union all select 人员编号,8100 as 考勤项,出差天数,考勤年月 as 考勤日期 from [Sheet1$] where 出差天数 is not null union all select 人员编号,8056 as 考勤项,旷工天数,考勤年月 as 考勤日期 from [Sheet1$] where 旷工天数 is not null union all select 人员编号,8050 as 考勤项,年假天数,考勤年月 as 考勤日期 from [Sheet1$] where 年假天数 is not null union all select 人员编号,8059 as 考勤项,事假天数,考勤年月 as 考勤日期 from [Sheet1$] where 事假天数 is not null union all select 人员编号,8052 as 考勤项,病假天数,考勤年月 as 考勤日期 from [Sheet1$] where 病假天数 is not null union all select 人员编号,8063 as 考勤项,调休天数,考勤年月 as 考勤日期 from [Sheet1$] where 调休天数 is not null union all select 人员编号,8064 as 考勤项,产假天数,考勤年月 as 考勤日期 from [Sheet1$] where 产假天数 is not null union all select 人员编号,8062 as 考勤项,陪产假,考勤年月 as 考勤日期 from [Sheet1$] where 陪产假 is not null union all select 人员编号,8054 as 考勤项,婚假天数,考勤年月 as 考勤日期 from [Sheet1$] where 婚假天数 is not null union all select 人员编号,8055 as 考勤项,丧假天数,考勤年月 as 考勤日期 from [Sheet1$] where 丧假天数 is not null union all select 人员编号,8901 as 考勤项,迟到早退次数,考勤年月 as 考勤日期 from [Sheet1$] where 迟到早退次数 is not null union all select 人员编号,8902 as 考勤项,缺卡次数,考勤年月 as 考勤日期 from [Sheet1$] where 缺卡次数 is not null union all select 人员编号,8000 as 考勤项,工作日加班次数,考勤年月 as 考勤日期 from [Sheet1$] where 工作日加班次数 is not null union all select 人员编号,8010 as 考勤项,休息日加班次数,考勤年月 as 考勤日期 from [Sheet1$] where 休息日加班次数 is not null union all select 人员编号,8903 as 考勤项,打卡签到次数,考勤年月 as 考勤日期 from [Sheet1$] where 打卡签到次数 is not null";
            DataTable dt_import = Hc_db.get_datatable_ex(s2);

            dt_emp_bas.Columns.Add("是否导入考勤", typeof(bool));

            //写入access数据库
            for (int i = 0; i < dt_emp_bas.Rows.Count; i++)
            {
                for (int j = 0; j < dt_import.Rows.Count; j++)
                {
                    if (dt_emp_bas.Rows[i][0].ToString() == dt_import.Rows[j][0].ToString())
                    {
                        string id = dt_import.Rows[j]["人员编号"].ToString();
                        string kaoqinxiang = dt_import.Rows[j]["考勤项"].ToString();
                        double numb = dt_import.Rows[j]["数量"].ToString() == "" ? 0 : double.Parse(dt_import.Rows[j]["数量"].ToString());
                        string kaoqinriqi = dt_import.Rows[j]["考勤日期"].ToString() + "01";

                        string s3 = $"delete from emp_attendance where 人员编号 = '{id}' and 考勤项 = '{kaoqinxiang}' and 数量 = {numb} and 考勤日期 = '{kaoqinriqi}'";
                        Hc_db.do_nonquery(s3);
                        string s4 = $"insert into emp_attendance (人员编号,考勤项,数量,考勤日期) values ('{id}','{kaoqinxiang}',{numb},'{kaoqinriqi}')";
                        Hc_db.do_nonquery(s4);

                        dt_emp_bas.Rows[i]["是否导入考勤"] = true;        //导完后标记出没有考勤人员
                    }
                }
            }


        }


        /// <summary>
        /// 根据emp_attendance、emp_pay_basge表生成缺勤工资扣，并更新emp_attendance表工资扣字段。
        /// </summary>
        /// <param name="last_year_month"></param>
        public static void GetAttendanceGZK(string last_year_month)
        {
            string s8 = $"select emp_attendance.人员编号,emp_attendance.考勤项,emp_attendance.数量,emp_attendance.考勤日期,emp_pay_bas.金额 as 基本工资 from emp_attendance,emp_pay_bas where emp_attendance.人员编号=emp_pay_bas.人员编号 and emp_attendance.考勤日期 like '{last_year_month}%' and emp_pay_bas.结束日期= '99991231'";
            DataTable dt_emp_attendance = Hc_db.get_datatable(s8);

            dt_emp_attendance.Columns.Add("实际工作年限", typeof(double));
            dt_emp_attendance.Columns.Add("本企业工作年限", typeof(double));
            dt_emp_attendance.Columns.Add("工资扣", typeof(double));

            #region 从特殊日期表导入实际工作年限和本企业工作年限
            string s9 = "select emp_bas.人员编号,emp_speci_date.日期类型,emp_speci_date.日期 from emp_bas,emp_speci_date where emp_bas.结束日期='99991231' and emp_bas.人员编号=emp_speci_date.人员编号";
            DataTable dt_emp_speci_date = Hc_db.get_datatable(s9);
            for (int i = 0; i < dt_emp_speci_date.Rows.Count; i++)
            {
                for (int j = 0; j < dt_emp_attendance.Rows.Count; j++)
                {
                    if (dt_emp_attendance.Rows[j]["人员编号"].ToString() == dt_emp_speci_date.Rows[i]["人员编号"].ToString())
                    {
                        if (dt_emp_speci_date.Rows[i]["日期类型"].ToString() == "Z1")
                        {
                            dt_emp_attendance.Rows[j]["实际工作年限"] = DateTime.Now.Year - double.Parse(dt_emp_speci_date.Rows[i]["日期"].ToString().Substring(0, 4)) + 1;
                        }
                        if (dt_emp_speci_date.Rows[i]["日期类型"].ToString() == "Z3")
                        {
                            dt_emp_attendance.Rows[j]["本企业工作年限"] = DateTime.Now.Year - double.Parse(dt_emp_speci_date.Rows[i]["日期"].ToString().Substring(0, 4)) + 1;
                        }
                    }
                }
            }
            #endregion

            #region 病假扣工资    //12天以内按天享受病假工资，奖金部门内部考核；12天及以上按天享受病假工资，扣1个月月奖。

            for (int j = 0; j < dt_emp_attendance.Rows.Count; j++)
            {
                if (dt_emp_attendance.Rows[j]["考勤项"].ToString() == "8052")
                {
                    if (double.Parse(dt_emp_attendance.Rows[j]["实际工作年限"].ToString()) < 10)
                    {
                        if (double.Parse(dt_emp_attendance.Rows[j]["本企业工作年限"].ToString()) < 5)
                        {
                            dt_emp_attendance.Rows[j]["工资扣"] = Math.Round(double.Parse(dt_emp_attendance.Rows[j]["基本工资"].ToString()) / 21.75 * double.Parse(dt_emp_attendance.Rows[j]["数量"].ToString()) * 0.3, 2);
                        }
                        else
                        {
                            dt_emp_attendance.Rows[j]["工资扣"] = Math.Round(double.Parse(dt_emp_attendance.Rows[j]["基本工资"].ToString()) / 21.75 * double.Parse(dt_emp_attendance.Rows[j]["数量"].ToString()) * 0.25, 2);
                        }
                    }
                    else
                    {
                        if (double.Parse(dt_emp_attendance.Rows[j]["本企业工作年限"].ToString()) < 5)
                        {
                            dt_emp_attendance.Rows[j]["工资扣"] = Math.Round(double.Parse(dt_emp_attendance.Rows[j]["基本工资"].ToString()) / 21.75 * double.Parse(dt_emp_attendance.Rows[j]["数量"].ToString()) * 0.25, 2);
                        }
                        else if (double.Parse(dt_emp_attendance.Rows[j]["本企业工作年限"].ToString()) >= 5 && double.Parse(dt_emp_attendance.Rows[j]["本企业工作年限"].ToString()) < 10)
                        {
                            dt_emp_attendance.Rows[j]["工资扣"] = Math.Round(double.Parse(dt_emp_attendance.Rows[j]["基本工资"].ToString()) / 21.75 * double.Parse(dt_emp_attendance.Rows[j]["数量"].ToString()) * 0.20, 2);
                        }
                        else if (double.Parse(dt_emp_attendance.Rows[j]["本企业工作年限"].ToString()) >= 10 && double.Parse(dt_emp_attendance.Rows[j]["本企业工作年限"].ToString()) < 15)
                        {
                            dt_emp_attendance.Rows[j]["工资扣"] = Math.Round(double.Parse(dt_emp_attendance.Rows[j]["基本工资"].ToString()) / 21.75 * double.Parse(dt_emp_attendance.Rows[j]["数量"].ToString()) * 0.15, 2);
                        }
                        else if (double.Parse(dt_emp_attendance.Rows[j]["本企业工作年限"].ToString()) >= 15 && double.Parse(dt_emp_attendance.Rows[j]["本企业工作年限"].ToString()) < 20)
                        {
                            dt_emp_attendance.Rows[j]["工资扣"] = Math.Round(double.Parse(dt_emp_attendance.Rows[j]["基本工资"].ToString()) / 21.75 * double.Parse(dt_emp_attendance.Rows[j]["数量"].ToString()) * 0.10, 2);
                        }
                        else if (double.Parse(dt_emp_attendance.Rows[j]["本企业工作年限"].ToString()) >= 20 && double.Parse(dt_emp_attendance.Rows[j]["本企业工作年限"].ToString()) < 30)
                        {
                            dt_emp_attendance.Rows[j]["工资扣"] = Math.Round(double.Parse(dt_emp_attendance.Rows[j]["基本工资"].ToString()) / 21.75 * double.Parse(dt_emp_attendance.Rows[j]["数量"].ToString()) * 0.05, 2);
                        }
                        else
                        {
                            dt_emp_attendance.Rows[j]["工资扣"] = Math.Round(double.Parse(dt_emp_attendance.Rows[j]["基本工资"].ToString()) / 21.75 * double.Parse(dt_emp_attendance.Rows[j]["数量"].ToString()) * 0.00, 2);
                        }
                    }
                }
            }
            #endregion

            #region 事假扣工资    //12天以内按天扣发工资，奖金部门内部考核；当月累计12天或跨月连续12天扣1个月月奖。
            for (int j = 0; j < dt_emp_attendance.Rows.Count; j++)
            {
                if (dt_emp_attendance.Rows[j]["考勤项"].ToString() == "8059")
                {
                    dt_emp_attendance.Rows[j]["工资扣"] = Math.Round(double.Parse(dt_emp_attendance.Rows[j]["基本工资"].ToString()) / 21.75 * double.Parse(dt_emp_attendance.Rows[j]["数量"].ToString()), 2);
                }
            }

            #endregion

            #region 迟到早退扣    //按次数扣发工资，每次50元
            for (int j = 0; j < dt_emp_attendance.Rows.Count; j++)
            {
                if (dt_emp_attendance.Rows[j]["考勤项"].ToString() == "8901")
                {
                    dt_emp_attendance.Rows[j]["工资扣"] = Math.Round(double.Parse(dt_emp_attendance.Rows[j]["数量"].ToString()) * 50, 2);
                }
            }
            #endregion

            #region 缺卡扣    //按次数扣发工资，每次50元
            for (int j = 0; j < dt_emp_attendance.Rows.Count; j++)
            {
                if (dt_emp_attendance.Rows[j]["考勤项"].ToString() == "8902")
                {
                    dt_emp_attendance.Rows[j]["工资扣"] = Math.Round(double.Parse(dt_emp_attendance.Rows[j]["数量"].ToString()) * 50, 2);
                }
            }
            #endregion

            #region 更新emp_attendance表
            for (int i = 0; i < dt_emp_attendance.Rows.Count; i++)
            {
                string id = dt_emp_attendance.Rows[i]["人员编号"].ToString();
                string kaoqinxiang = dt_emp_attendance.Rows[i]["考勤项"].ToString();
                double gongzikou = dt_emp_attendance.Rows[i]["工资扣"].ToString() == "" ? 0 : double.Parse(dt_emp_attendance.Rows[i]["工资扣"].ToString());
                string kaoqingriqi = dt_emp_attendance.Rows[i]["考勤日期"].ToString();
                string s10 = $"update emp_attendance set 工资扣 = {gongzikou} where 人员编号 = '{id}' and 考勤项 = '{kaoqinxiang}' and 考勤日期 = '{kaoqingriqi}'";
                Hc_db.do_nonquery(s10);
            }
            #endregion

        }


        /// <summary>
        /// 根据emp_bas,emp_bonus_factor两张表生成月奖标准，并将数据写入emp_bonus_addi_off_cycle表
        /// </summary>
        /// <param name="current_date_jj"></param>
        public static void GenerateBonus(string current_date_jj)
        {
            string s1 = "select emp_bas.人员编号,emp_bas.姓名 from emp_bas where emp_bas.结束日期='99991231'";  //根据最新的人员情况表导入考勤数据，没用的考勤数据就不导入了。
            DataTable dt_emp_bas = Hc_db.get_datatable(s1);

            s1 = $"select emp_bonus_factor.人员编号,9020 as 工资项,金额,'{current_date_jj}' as 发放日期 from emp_bas,emp_bonus_factor where emp_bas.人员编号 = emp_bonus_factor.人员编号 and emp_bonus_factor.工资等级类型='A0' and emp_bonus_factor.结束日期= '99991231' UNION ALL select emp_bas.人员编号,4000 as 工资项,(Sum(IIf(emp_bonus_factor.工资项='J101',emp_bonus_factor.金额,0))+Sum(IIf(emp_bonus_factor.工资项='J103',emp_bonus_factor.金额,0)))*(Sum(IIf(emp_bonus_factor.工资项='J102',emp_bonus_factor.金额,0))+Sum(IIf(emp_bonus_factor.工资项='J104',emp_bonus_factor.金额,0)))*Sum(IIf(emp_bonus_factor.工资项='J105',emp_bonus_factor.金额,0))*(SELECT bas_bonus_prize.月奖基数 from bas_bonus_prize where bas_bonus_prize.结束日期= '99991231'),'{current_date_jj}' as 发放日期 from emp_bas,emp_bonus_factor where emp_bas.人员编号 = emp_bonus_factor.人员编号 and emp_bonus_factor.工资等级类型='A1' and emp_bonus_factor.结束日期= '99991231' group by emp_bas.人员编号";
            var dt_emp_bonus_factor = Hc_db.get_datatable(s1);

            dt_emp_bas.Columns.Add("是否生成月奖标准", typeof(bool));

            //写入access数据库
            for (int i = 0; i < dt_emp_bas.Rows.Count; i++)
            {
                for (int j = 0; j < dt_emp_bonus_factor.Rows.Count; j++)
                {
                    if (dt_emp_bas.Rows[i][0].ToString() == dt_emp_bonus_factor.Rows[j][0].ToString())
                    {
                        string id = dt_emp_bonus_factor.Rows[j]["人员编号"].ToString();
                        string gongzixiang = dt_emp_bonus_factor.Rows[j]["工资项"].ToString();
                        double numb = dt_emp_bonus_factor.Rows[j]["金额"].ToString() == "" ? 0 : double.Parse(dt_emp_bonus_factor.Rows[j]["金额"].ToString());
                        string fafangriqi = dt_emp_bonus_factor.Rows[j]["发放日期"].ToString();

                        s1 = $"delete from emp_bonus_addi_off_cycle where 人员编号 = '{id}' and 工资项 = '{gongzixiang}' and 金额 = {numb} and 发放日期 = '{fafangriqi}'";
                        Hc_db.do_nonquery(s1);
                        s1 = $"insert into emp_bonus_addi_off_cycle (人员编号,工资项,金额,发放日期) values ('{id}','{gongzixiang}',{numb},'{fafangriqi}')";
                        Hc_db.do_nonquery(s1);

                        dt_emp_bas.Rows[i]["是否生成月奖标准"] = true;        //导完后标记出没有考勤人员
                    }
                }
            }




        }



        /// <summary>
        /// 根据emp_attendance、emp_bonus_addi_off_cycle表生成缺勤月奖扣，并更新emp_attendance表月奖扣字段。
        /// </summary>
        /// <param name="last_year_month"></param>
        /// <param name="current_date_jj"></param>
        public static void GetAttendanceYJK(string last_year_month, string current_date_jj)
        {
            string s8 = $"select emp_attendance.人员编号,emp_attendance.考勤项,emp_attendance.数量,emp_attendance.考勤日期,emp_bonus_addi_off_cycle.金额 as 月奖标准 from emp_attendance,emp_bonus_addi_off_cycle where emp_attendance.人员编号=emp_bonus_addi_off_cycle.人员编号 and emp_attendance.考勤日期 like '{last_year_month}%' and emp_bonus_addi_off_cycle.发放日期='{current_date_jj}'";
            DataTable dt_emp_attendance = Hc_db.get_datatable(s8);

            dt_emp_attendance.Columns.Add("实际工作年限", typeof(double));
            dt_emp_attendance.Columns.Add("本企业工作年限", typeof(double));
            dt_emp_attendance.Columns.Add("月奖扣", typeof(double));

            #region 病假扣月奖    //12天以内按天享受病假工资，奖金部门内部考核；12天及以上按天享受病假工资，扣1个月月奖。
            for (int j = 0; j < dt_emp_attendance.Rows.Count; j++)
            {
                if (dt_emp_attendance.Rows[j]["考勤项"].ToString() == "8052" && double.Parse(dt_emp_attendance.Rows[j]["数量"].ToString()) >= 12)
                {
                    dt_emp_attendance.Rows[j]["月奖扣"] = dt_emp_attendance.Rows[j]["月奖标准"];
                }
            }
            #endregion

            #region 事假扣月奖    //12天以内按天扣发工资，奖金部门内部考核；当月累计12天或跨月连续12天扣1个月月奖。
            for (int j = 0; j < dt_emp_attendance.Rows.Count; j++)
            {
                if (dt_emp_attendance.Rows[j]["考勤项"].ToString() == "8059" && double.Parse(dt_emp_attendance.Rows[j]["数量"].ToString()) >= 12)
                {
                    dt_emp_attendance.Rows[j]["月奖扣"] = dt_emp_attendance.Rows[j]["月奖标准"];
                }
            }

            #endregion

            #region 婚假扣    //工资照发，不享受月奖
            for (int j = 0; j < dt_emp_attendance.Rows.Count; j++)
            {
                if (dt_emp_attendance.Rows[j]["考勤项"].ToString() == "8054")
                {
                    dt_emp_attendance.Rows[j]["月奖扣"] = Math.Round(double.Parse(dt_emp_attendance.Rows[j]["月奖标准"].ToString()) / 21.75 * double.Parse(dt_emp_attendance.Rows[j]["数量"].ToString()), 2);
                }
            }
            #endregion

            #region 探亲假扣    //工资照发，不享受月奖
            for (int j = 0; j < dt_emp_attendance.Rows.Count; j++)
            {
                if (dt_emp_attendance.Rows[j]["考勤项"].ToString() == "8051")
                {
                    dt_emp_attendance.Rows[j]["月奖扣"] = Math.Round(double.Parse(dt_emp_attendance.Rows[j]["月奖标准"].ToString()) / 21.75 * double.Parse(dt_emp_attendance.Rows[j]["数量"].ToString()), 2);
                }
            }
            #endregion

            #region 护理假扣    //工资照发，不享受月奖
            for (int j = 0; j < dt_emp_attendance.Rows.Count; j++)
            {
                if (dt_emp_attendance.Rows[j]["考勤项"].ToString() == "8062")
                {
                    dt_emp_attendance.Rows[j]["月奖扣"] = Math.Round(double.Parse(dt_emp_attendance.Rows[j]["月奖标准"].ToString()) / 21.75 * double.Parse(dt_emp_attendance.Rows[j]["数量"].ToString()), 2);
                }
            }
            #endregion

            #region 打卡签到次数    //按次数从奖金中核发，每次10元
            for (int j = 0; j < dt_emp_attendance.Rows.Count; j++)
            {
                if (dt_emp_attendance.Rows[j]["考勤项"].ToString() == "8903")
                {
                    dt_emp_attendance.Rows[j]["月奖扣"] = Math.Round(double.Parse(dt_emp_attendance.Rows[j]["数量"].ToString()) * -10, 2);
                }
            }
            #endregion

            #region 更新emp_attendance表
            for (int i = 0; i < dt_emp_attendance.Rows.Count; i++)
            {
                string id = dt_emp_attendance.Rows[i]["人员编号"].ToString();
                string kaoqinxiang = dt_emp_attendance.Rows[i]["考勤项"].ToString();
                double yuejiangkou = dt_emp_attendance.Rows[i]["月奖扣"].ToString() == "" ? 0 : double.Parse(dt_emp_attendance.Rows[i]["月奖扣"].ToString());
                string kaoqingriqi = dt_emp_attendance.Rows[i]["考勤日期"].ToString();
                string s10 = $"update emp_attendance set 月奖扣={yuejiangkou} where 人员编号 = '{id}' and 考勤项 = '{kaoqinxiang}' and 考勤日期 = '{kaoqingriqi}'";
                Hc_db.do_nonquery(s10);
            }
            #endregion
        }









        /// <summary>
        /// 工资运算
        /// </summary>
        /// <param name="工资范围"></param>
        /// <param name="工资日期"></param>
        /// <param name="工资年月"></param>
        /// <returns></returns>
        public static DataTable GetSalarySum(string salary_range, string current_date_gz, string current_year_month)
        {
            string s1 = $"select emp_bas.所属二级机构,emp_bas.所属三级机构,emp_bas.人员编号,emp_bas.姓名,emp_bas.身份证号," + $"Sum(IIf(dt_res.工资项='9010',dt_res.金额,0))+Sum(IIf(dt_res.工资项='9050',dt_res.金额,0))+" + $"Sum(IIf(dt_res.工资项='1000',dt_res.金额,0))+Sum(IIf(dt_res.工资项='1099',dt_res.金额,0)) AS 岗位工资," + $"Sum(IIf(dt_res.工资项='3000',dt_res.金额,0))+Sum(IIf(dt_res.工资项='3099',dt_res.金额,0)) AS 年功工资," + $"Sum(IIf(dt_res.工资项='2090',dt_res.金额,0)) AS 书报费," + $"Sum(IIf(dt_res.工资项='2080',dt_res.金额,0)) AS 洗理费," + $"Sum(IIf(dt_res.工资项='2120',dt_res.金额,0))+Sum(IIf(dt_res.工资项='2999',dt_res.金额,0)) AS 其它津补贴," + $"Sum(IIf(dt_res.工资项='2328',dt_res.金额,0)) AS 副食补贴, " + $"Sum(IIf(dt_res.工资项='2314',dt_res.金额,0)) AS 知识分子补助," + $"Sum(IIf(dt_res.工资项='2316',dt_res.金额,0)) AS 保健津贴," + $"Sum(IIf(dt_res.工资项='2030',dt_res.金额,0)) AS 运龄津贴," + $"Sum(IIf(dt_res.工资项='2010',dt_res.金额,0)) AS 夜班津贴," + $"Sum(IIf(dt_res.工资项='2304',dt_res.金额,0)) AS 通讯补贴," + $"Sum(IIf(dt_res.工资项='3500',dt_res.金额,0)) AS 加班工资," + $"Sum(IIf(dt_res.工资项='1040',dt_res.金额,0)) AS 缺勤扣减," + $"岗位工资+年功工资+书报费+洗理费+其它津补贴+副食补贴+知识分子补助+保健津贴+运龄津贴+夜班津贴+通讯补贴+加班工资-缺勤扣减 AS 应发合计," + $"Sum(IIf(dt_res.工资项='7180',dt_res.金额,0)) AS 基本养老," + $"Sum(IIf(dt_res.工资项='7290',dt_res.金额,0)) AS 企业年金," + $"Sum(IIf(dt_res.工资项='7220',dt_res.金额,0)) AS 医疗," + $"Sum(IIf(dt_res.工资项='7200',dt_res.金额,0)) AS 失业," + $"Sum(IIf(dt_res.工资项='7160',dt_res.金额,0)) AS 公积金," + $"Sum(IIf(dt_res.工资项='7580',dt_res.金额,0))+Sum(IIf(dt_res.工资项='7570',dt_res.金额,0)) AS 个税," + $"Sum(IIf(dt_res.工资项='7997',dt_res.金额,0))+Sum(IIf(dt_res.工资项='7998',dt_res.金额,0))+Sum(IIf(dt_res.工资项='7999',dt_res.金额,0)) AS 其它扣减," + $"基本养老+企业年金+医疗+失业+公积金+个税+其它扣减 AS 扣减合计," + $"应发合计-扣减合计 AS 实发" + $" from (select dt_sum.人员编号,dt_sum.工资项,sum(dt_sum.金额) as 金额 from (select emp_pay_bas.人员编号,emp_pay_bas.工资项,emp_pay_bas.金额 from emp_pay_bas where '{current_date_gz}' between emp_pay_bas.开始日期 and emp_pay_bas.结束日期 union all select emp_pay_recu.人员编号,emp_pay_recu.工资项,emp_pay_recu.金额 from emp_pay_recu where '{current_date_gz}' between emp_pay_recu.开始日期 and emp_pay_recu.结束日期 union all select emp_pay_addi.人员编号,emp_pay_addi.工资项,emp_pay_addi.金额 from emp_pay_addi where 发放日期 like '{current_year_month}%') as dt_sum group by dt_sum.人员编号,dt_sum.工资项) as dt_res,emp_bas where '{current_date_gz}' between emp_bas.开始日期 and emp_bas.结束日期 and emp_bas.工资范围 = '{salary_range}' and emp_bas.人员编号 = dt_res.人员编号 group by emp_bas.所属二级机构,emp_bas.所属三级机构,emp_bas.人员编号,emp_bas.姓名,emp_bas.身份证号";       //先将三个表汇总，再按照工资项进行分组求和，再通过工资项进行行转列呈现。
            DataTable dt_salary_sum = Hc_db.get_datatable(s1);
            return dt_salary_sum;
        }





        /// <summary>
        /// 对三个表结构相同的表进行数据合并
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <param name="dt3"></param>
        /// <returns>返回的新表格</returns>
        public DataTable GetAllDataTable(DataTable dt1, DataTable dt2, DataTable dt3)
        {

            DataTable newTable = dt1.Copy();
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                newTable.Rows.Add(dt2.Rows[i].ItemArray);
            }
            for (int i = 0; i < dt3.Rows.Count; i++)
            {
                newTable.Rows.Add(dt3.Rows[i].ItemArray);
            }
            return newTable;
        }











    }
}