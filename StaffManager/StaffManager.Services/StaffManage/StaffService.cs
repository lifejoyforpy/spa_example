using StaffManager.Framework.Dapper;
using StaffManager.IServices.StaffManage;
using StaffMange.Models.StaffMange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffManager.Services.StaffManage
{
    public class StaffService : IStaffService
    {
        public Response<List<string>> getLine()
        {
            Response<List<string>> rsp = new Response<List<string>>();
            try
            {

                string sql = @"select DISTINCT  line from m_auxiliary_manpower";
                rsp.data = DapperRepository.Query<string>(sql, null);
                if (rsp.data.Count == 0)
                    rsp.flag = 0;
            }
            catch (Exception ex)
            {
                rsp.flag = 0;
                rsp.msg = ex.Message.ToString();

            }

            return rsp;
        }

        public Response<List<StaffWorkInfo>> getOnlieInfo(StaffOnlieModel model)
        {
            Response<List<StaffWorkInfo>> rsp = new Response<List<StaffWorkInfo>>();
            try
            {

                model.line = getUserLine(model.user);

                string sql = @"select   a.WorkID empno, a.WorkerName empname ,a.PostName job,a.line ,a.shift_no shift , b.Status_flag 
                             from  m_auxiliary_manpower a LEFT JOIN (select * from  mfg_onlinewip where work_date=@work_date ) b on a.WorkID=b.empNo
                             where   b.empNo is NULL  and a.line=@line and  a.IsEnabled='Y' ";
                if (model.status == 1)
                {
                    sql = @"select a.empNo ,a.empName ,b.PostName job , a.Status_flag  from mfg_onlinewip a ,m_auxiliary_manpower b where a.line=@line and a.work_date=@work_date
                         and a.line=b.line and a.empNo=b.WorkID";
                }
                rsp.data = DapperRepository.Query<StaffWorkInfo>(sql, new { line = model.line, shift = model.shift, work_date = model.workdate });
            }
            catch (Exception ex)
            {
                rsp.flag = 0;
                rsp.msg = ex.Message.ToString();

            }
            return rsp;

        }

        public string getUserLine(string user)
        {
            string line = "";
            try
            {
                string sql = @"select line  from m_auxiliary_manpower where WorkerName=@WorkerName";
                line = DapperRepository.QuerySingle<string>(sql, new { WorkerName = user });
            }
            catch (Exception)
            {


            }
            return line;
        }

        public Response<bool> goWork(List<StaffWorkInfo> model)
        {
            Response<bool> rsp = new Response<bool>();
            List<StaffOnlie> entity = new List<StaffOnlie>();
            List<StaffOnlineHis> entity_his = new List<StaffOnlineHis>();
            foreach (var item in model)
            {
                StaffOnlie obj = new StaffOnlie();
                StaffOnlineHis obj_his = new StaffOnlineHis();
                obj.empNo = item.empno;
                obj.empName = item.empname;
                obj.line = item.line;
                obj.online_time = DateTime.Now;
                obj.shift_no = item.shift;
                obj.old_line = item.line;
                obj.work_date = DateTime.Now.ToString("yyyy-MM-dd");
                obj.Status_flag = "0";

                obj_his.empNo = item.empno;
                obj_his.line = item.line;
                obj_his.work_date = DateTime.Now.ToString("yyyy-MM-dd");

                entity.Add(obj);
                entity_his.Add(obj_his);
            }
            List<KeyValuePair<string, object>> cmd = new List<KeyValuePair<string, object>>();

            try
            {
                string sql = @"insert into  mfg_onlinewip
                    (work_date, empNo, empName, line, Status_flag, online_time, shift_no, old_line)
                    VALUES (@work_date, @empNo, @empName, @line, @Status_flag, @online_time, @shift_no, @old_line)";

                string sql_his = "insert into mfg_onlinehistory(work_date, empNo, empName, line, Status_flag, online_time, offline_time, offline_Mark, shift_no, ligang_time, huigang_time, ligang_mark, old_line, new_line, new_line_time)  (select work_date, empNo, empName, line, Status_flag, online_time, offline_time, offline_Mark, shift_no, ligang_time, huigang_time, ligang_mark, old_line, new_line, new_line_time from mfg_onlinewip where work_date=@work_date and line=@line and empNo=@empno ) ";
                cmd.Add(new KeyValuePair<string, object>(sql, entity));
                cmd.Add(new KeyValuePair<string, object>(sql_his, entity_his));
                rsp.data = DapperRepository.Execute(cmd);
                if (!rsp.data)
                    rsp.flag = 0;
            }
            catch (Exception ex)
            {
                rsp.flag = 0;
                rsp.msg = ex.Message.ToString();

            }
            return rsp;
        }

        public int test()
        {
            return 0;
        }

        //调岗
        public Response<bool> transWork(List<StaffWorkInfo> model)
        {

            Response<bool> rsp = new Response<bool>();
            List<StaffOnlie> entity = new List<StaffOnlie>();
            List<StaffOnlineHis> entity_his = new List<StaffOnlineHis>();
            foreach (var item in model)
            {
                StaffOnlie obj = new StaffOnlie();
                StaffOnlineHis obj_his = new StaffOnlineHis();

                obj.empNo = item.empno;
                obj.empName = item.empname;
                obj.line = item.new_line;
                obj.shift_no = item.shift;
                obj.online_time = DateTime.Now;
                obj.old_line = item.line;
                obj.work_date = DateTime.Now.ToString("yyyy-MM-dd");
                obj.Status_flag = "0";
                obj_his.empNo = item.empno;
                obj_his.line = item.new_line;
                obj_his.work_date = DateTime.Now.ToString("yyyy-MM-dd");

                entity.Add(obj);
                entity_his.Add(obj_his);

            }
            List<KeyValuePair<string, object>> cmd = new List<KeyValuePair<string, object>>();

            try
            {
                string sql = @"insert into  mfg_onlinewip
                    (work_date, empNo, empName, line, Status_flag, online_time, shift_no, old_line,new_line)
                    VALUES (@work_date, @empNo, @empName, @line, @Status_flag, @online_time, @shift_no,@old_line, @line)";
                string sql_his = "insert into mfg_onlinehistory(work_date, empNo, empName, line, Status_flag, online_time, offline_time, offline_Mark, shift_no, ligang_time, huigang_time, ligang_mark, old_line, new_line, new_line_time)  (select work_date, empNo, empName, line, Status_flag, online_time, offline_time, offline_Mark, shift_no, ligang_time, huigang_time, ligang_mark, old_line, new_line, new_line_time from mfg_onlinewip where work_date=@work_date and line=@line and empNo=@empno ) ";
                cmd.Add(new KeyValuePair<string, object>(sql, entity));
                cmd.Add(new KeyValuePair<string, object>(sql_his, entity_his));
                rsp.data = DapperRepository.Execute(cmd);
                if (!rsp.data)
                    rsp.flag = 0;
            }
            catch (Exception ex)
            {
                rsp.flag = 0;
                rsp.msg = ex.Message.ToString();

            }
            return rsp;
        }

        //离岗
        public Response<bool> offpost(List<StaffWorkInfo> model)
        {
            Response<bool> rsp = new Response<bool>();
            List<StaffOnlie> entity = new List<StaffOnlie>();
            List<StaffOnlineHis> entity_his = new List<StaffOnlineHis>();
            foreach (var item in model)
            {
                StaffOnlie obj = new StaffOnlie();
                StaffOnlineHis obj_his = new StaffOnlineHis();

                obj.empNo = item.empno;
                obj.empName = item.empname;
                obj.line = item.line;
                obj.shift_no = item.shift;
                obj.work_date = DateTime.Now.ToString("yyyy-MM-dd");
                obj.Status_flag = "1";
                obj.ligang_mark = item.ligang_mark;
                obj.ligang_time = DateTime.Now;

                obj_his.empNo = item.empno;
                obj_his.line = item.line;
                obj_his.work_date = DateTime.Now.ToString("yyyy-MM-dd");

                entity.Add(obj);
                entity_his.Add(obj_his);

            }
            List<KeyValuePair<string, object>> cmd = new List<KeyValuePair<string, object>>();

            try
            {
                string sql = @"update mfg_onlinewip set ligang_mark=@ligang_mark,ligang_time=@ligang_time ,Status_flag=@Status_flag
                            where work_date=@work_date and empNo=@empNo and line=@line";
                string sql_his = "insert into mfg_onlinehistory(work_date, empNo, empName, line, Status_flag, online_time, offline_time, offline_Mark, shift_no, ligang_time, huigang_time, ligang_mark, old_line, new_line, new_line_time)  (select work_date, empNo, empName, line, Status_flag, online_time, offline_time, offline_Mark, shift_no, ligang_time, huigang_time, ligang_mark, old_line, new_line, new_line_time from mfg_onlinewip where work_date=@work_date and line=@line and empNo=@empno ) ";
                cmd.Add(new KeyValuePair<string, object>(sql, entity));
                cmd.Add(new KeyValuePair<string, object>(sql_his, entity_his));
                rsp.data = DapperRepository.Execute(cmd);
                if (!rsp.data)
                    rsp.flag = 0;
            }
            catch (Exception ex)
            {
                rsp.flag = 0;
                rsp.msg = ex.Message.ToString();

            }
            return rsp;

        }
        //回岗
        public Response<bool> returnpost(List<StaffWorkInfo> model)
        {

            Response<bool> rsp = new Response<bool>();
            List<StaffOnlie> entity = new List<StaffOnlie>();
            List<StaffOnlineHis> entity_his = new List<StaffOnlineHis>();
            List<OffWorkDetail> offWorkDetails = new List<OffWorkDetail>();
            foreach (var item in model)
            {
                StaffOnlie obj = new StaffOnlie();
                StaffOnlineHis obj_his = new StaffOnlineHis();
                OffWorkDetail offwork = new OffWorkDetail();
                obj.empNo = item.empno;
                obj.empName = item.empname;
                obj.line = item.new_line;
                obj.shift_no = item.shift;
                obj.online_time = DateTime.Now;
                obj.huigang_time = DateTime.Now;
                obj.old_line = item.line;
                obj.work_date = DateTime.Now.ToString("yyyy-MM-dd");
                obj.Status_flag = "0";

                obj_his.empNo = item.empno;
                obj_his.line = item.new_line;
                obj_his.work_date = DateTime.Now.ToString("yyyy-MM-dd");

                offwork.emp_no = item.empno;
                offwork.work_date = DateTime.Now.ToString("yyyy-MM-dd");
                offwork.ligang_time = item.ligang_time;
                offwork.huigang_time = DateTime.Now;
                offwork.ligang_time_length = DateTime.Now.Subtract(item.ligang_time).TotalSeconds;
                offwork.shift_no = item.shift;

                entity.Add(obj);
                entity_his.Add(obj_his);
                offWorkDetails.Add(offwork);

            }
            List<KeyValuePair<string, object>> cmd = new List<KeyValuePair<string, object>>();

            try
            {
                string sql = @"update mfg_onlinewip set huigang_time=@huigang_time,Status_flag=@Status_flag
                        where work_date=@work_date and empNo=@empNo and line=@old_line ";
                string sql_his = "insert into mfg_onlinehistory(work_date, empNo, empName, line, Status_flag, online_time, offline_time, offline_Mark, shift_no, ligang_time, huigang_time, ligang_mark, old_line, new_line, new_line_time)  (select work_date, empNo, empName, line, Status_flag, online_time, offline_time, offline_Mark, shift_no, ligang_time, huigang_time, ligang_mark, old_line, new_line, new_line_time from mfg_onlinewip where work_date=@work_date and line=@line and empNo=@empno ) ";

                string sql_record = @"INSERT  into mfg_ligang_total(emp_no, work_date, ligang_ci, ligang_time, huigang_time, ligang_time_length, shift_no) VALUES
                                  (@emp_no, @work_date, @ligang_ci, @ligang_time, @huigang_time, @ligang_time_length, @shift_no)";

                cmd.Add(new KeyValuePair<string, object>(sql, entity));
                cmd.Add(new KeyValuePair<string, object>(sql_his, entity_his));
                cmd.Add(new KeyValuePair<string, object>(sql_record, offWorkDetails));
                rsp.data = DapperRepository.Execute(cmd);
                if (!rsp.data)
                    rsp.flag = 0;
            }
            catch (Exception ex)
            {
                rsp.flag = 0;
                rsp.msg = ex.Message.ToString();

            }
            return rsp;
        }
        //下班
        public Response<bool> offwork(List<StaffWorkInfo> model)
        {
            Response<bool> rsp = new Response<bool>();
            List<StaffOnlie> entity = new List<StaffOnlie>();
            List<StaffOnlineHis> entity_his = new List<StaffOnlineHis>();
            foreach (var item in model)
            {
                StaffOnlie obj = new StaffOnlie();
                StaffOnlineHis obj_his = new StaffOnlineHis();

                obj.empNo = item.empno;
                obj.empName = item.empname;
                obj.Status_flag = "2";
                obj.offline_time = DateTime.Now;
                obj.line = item.line;
                obj.work_date= DateTime.Now.ToString("yyyy-MM-dd");



                obj_his.empNo = item.empno;
                obj_his.line = item.line;
                obj_his.work_date = DateTime.Now.ToString("yyyy-MM-dd");

                entity.Add(obj);
                entity_his.Add(obj_his);

            }
            List<KeyValuePair<string, object>> cmd = new List<KeyValuePair<string, object>>();

            try
            {
                string sql = @"update mfg_onlinewip set offline_time=@offline_time,Status_flag=@Status_flag
                            where work_date=@work_date and empNo=@empNo and line=@line";
                string sql_his = "insert into mfg_onlinehistory(work_date, empNo, empName, line, Status_flag, online_time, offline_time, offline_Mark, shift_no, ligang_time, huigang_time, ligang_mark, old_line, new_line, new_line_time)  (select work_date, empNo, empName, line, Status_flag, online_time, offline_time, offline_Mark, shift_no, ligang_time, huigang_time, ligang_mark, old_line, new_line, new_line_time from mfg_onlinewip where work_date=@work_date and line=@line and empNo=@empno ) ";
                cmd.Add(new KeyValuePair<string, object>(sql, entity));
                cmd.Add(new KeyValuePair<string, object>(sql_his, entity_his));
                rsp.data = DapperRepository.Execute(cmd);
                if (!rsp.data)
                    rsp.flag = 0;
            }
            catch (Exception ex)
            {
                rsp.flag = 0;
                rsp.msg = ex.Message.ToString();

            }
            return rsp;
        }

        public Response<List<StaffWorkInfo>> getpostinfo(StaffOnlieModel model)
        {
            Response<List<StaffWorkInfo>> rsp = new Response<List<StaffWorkInfo>>();
            try
            {
                model.line = getUserLine(model.user);

                string sql = @"select a.empNo ,a.empName ,CASE when a.old_line is null then a.line else a.old_line end source_line ,a.line,a.shift_no,a.Status_flag,b.postname job
                             from mfg_onlinewip a ,m_auxiliary_manpower b
                             where a.line=b.line and a.empNo=b.WorkID
                             and a.line=@line and a.work_date=@work_date and a.Status_flag ='0' ";
                if (model.status == 1)
                {
                    sql = @"select a.empNo ,a.empName  ,a.line,a.shift_no,a.Status_flag,a.ligang_mark,a.ligang_time,b.postname job 
                         from mfg_onlinewip a ,m_auxiliary_manpower b
                          where a.line=b.line and a.empNo=b.WorkID
                        and a.line=@line and a.work_date=@work_date
                           and a.Status_flag ='1'";
                }
                rsp.data = DapperRepository.Query<StaffWorkInfo>(sql, new { line = model.line, shift = model.shift, work_date = model.workdate });
            }
            catch (Exception ex)
            {
                rsp.flag = 0;
                rsp.msg = ex.Message.ToString();

            }
            return rsp;



        }

        public Response<List<string>> getoffreasonlist()
        {
            Response<List<string>> rsp = new Response<List<string>>();
            try
            {
                string sql = @"select * from mfg_ligang_desc";
                rsp.data = DapperRepository.Query<string>(sql, null);
                if (rsp.data.Count == 0)
                    rsp.flag = 0;
            }
            catch (Exception ex)
            {
                rsp.flag = 0;
                rsp.msg = ex.Message.ToString();

            }

            return rsp;

        }

        //在线调岗位
        public Response<bool> tranpost(List<StaffWorkInfo> model)
        {

            Response<bool> rsp = new Response<bool>();
            List<StaffOnlie> entity = new List<StaffOnlie>();
            List<StaffOnlineHis> entity_his = new List<StaffOnlineHis>();
            foreach (var item in model)
            {
                StaffOnlie obj = new StaffOnlie();
                StaffOnlineHis obj_his = new StaffOnlineHis();

                obj.empNo = item.empno;
                obj.empName = item.empname;
                obj.line = item.new_line;
                obj.shift_no = item.shift;
                obj.online_time = DateTime.Now;
                obj.old_line = item.line;
                obj.work_date = DateTime.Now.ToString("yyyy-MM-dd");
                obj.Status_flag = "0";

                obj_his.empNo = item.empno;
                obj_his.line = item.new_line;
                obj_his.work_date = DateTime.Now.ToString("yyyy-MM-dd");

                entity.Add(obj);
                entity_his.Add(obj_his);

            }
            List<KeyValuePair<string, object>> cmd = new List<KeyValuePair<string, object>>();

            try
            {
                string sql = @"update mfg_onlinewip set line=@line,old_line=@old_line,new_line=@new_line,new_line_time=@new_line_time
                        where work_date=@work_date and empNo=@empNo and line=@old_line ";
                string sql_his = "insert into mfg_onlinehistory(work_date, empNo, empName, line, Status_flag, online_time, offline_time, offline_Mark, shift_no, ligang_time, huigang_time, ligang_mark, old_line, new_line, new_line_time)  (select work_date, empNo, empName, line, Status_flag, online_time, offline_time, offline_Mark, shift_no, ligang_time, huigang_time, ligang_mark, old_line, new_line, new_line_time from mfg_onlinewip where work_date=@work_date and line=@line and empNo=@empno ) ";

                cmd.Add(new KeyValuePair<string, object>(sql, entity));
                cmd.Add(new KeyValuePair<string, object>(sql_his, entity_his));
                rsp.data = DapperRepository.Execute(cmd);
                if (!rsp.data)
                    rsp.flag = 0;
            }
            catch (Exception ex)
            {
                rsp.flag = 0;
                rsp.msg = ex.Message.ToString();

            }
            return rsp;
        }

        public Response<User> login(User user)
        {
            Response<User> rsp = new Response<User>();
            try {
                string sql = @"select* from ucenter_member where  Password=@Password and UserJobNo=@UserJobNo";
                rsp.data= DapperRepository.QuerySingle<User>(sql, user, false);
                if (rsp.data==null)
                    rsp.flag = 0;
            }
            catch (Exception ex)
            {
                rsp.flag = 0;
                rsp.msg = ex.Message.ToString();
            }
            return rsp;
        }
    }
}
