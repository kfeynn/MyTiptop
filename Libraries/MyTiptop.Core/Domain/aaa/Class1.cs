using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTiptop.Core.Domain.aaa
{

    public String ins_rvbs(String p_rvbs00, String p_rvbs01, String p_rvbs021,
                String p_rvbs04, float p_rvbs06, String p_rvbs03,
                String p_tc_breuser)
    {
        String l_success = "A";
        int l_tc_brb11 = 0;
        int l_cnt = 0;
        int l_num = 0;
        int l_sfv03 = 0;
        float l_sfv09 = 0.0F;
        float l_rvbs06 = 0.0F;
        float l_sum = 0.0F;
        String l_sfv11 = null;
        String l_sfuplant = null;
        String l_sfulegal = null;
        String l_tc_smd003 = null;
        int l_rvbs03 = 0;

        String g_date = this.df.format(new Date());
        DBConnect conn = new DBConnect();
        DBConnect conn1 = new DBConnect();

        String l_sql = ("select count(*) from rvbs_file where rvbs04='"
                + p_rvbs04 + "'");
        try
        {
            ResultSet rs = conn.executeQuery(l_sql);
            if (rs.next())
            {
                l_num = rs.getInt(1);
            }
            rs.close();
        }
        catch (SQLException e)
        {
            e.printStackTrace();
            l_success = "D";
        }

        // NO.20171208 by cjq begin 添加存储位置判断
        l_sql = "select count(*) from tc_ime_file where tc_ime03='" + p_rvbs03
                + "'";
        try
        {
            ResultSet rs = conn.executeQuery(l_sql);
            if (rs.next())
            {
                l_rvbs03 = rs.getInt(1);
            }
            rs.close();
        }
        catch (SQLException e)
        {
            e.printStackTrace();
            l_success = "E";
        }

        if (l_rvbs03 > 0)
        {
            if (l_num == 0)
            {
                l_sql = "SELECT tc_smd003 FROM tc_smd_file WHERE 1=1";
                try
                {
                    ResultSet rs = conn.executeQuery(l_sql);
                    while (rs.next())
                    {
                        l_tc_smd003 = rs.getString(1);
                    }
                    rs.close();
                }
                catch (SQLException e)
                {
                    e.printStackTrace();
                    l_success = "Z";
                }
                if (l_tc_smd003.equals("Y"))
                {
                    l_sql = ("SELECT sum(sfv09) FROM sfv_file WHERE sfv01='"
                            + p_rvbs01 + "' AND sfv04='" + p_rvbs021 + "'");
                    try
                    {
                        ResultSet rs = conn.executeQuery(l_sql);
                        if (rs.next())
                        {
                            l_sum = rs.getFloat(1);
                        }
                        rs.close();
                    }
                    catch (SQLException e)
                    {
                        e.printStackTrace();
                        l_success = "B";
                    }

                    if (l_sum < p_rvbs06)
                    {
                        l_success = "X";
                    }
                    else
                    {

                        l_sql = ("SELECT DISTINCT sfv03,sfv09,sfv11,sfuplant,sfulegal FROM sfv_file,sfu_file,tc_bre_file WHERE sfv01=sfu01 AND sfv01='"
                        + p_rvbs01
                        + "' AND sfv04='"
                        + p_rvbs021
                        + "' AND tc_bre01 ='" + p_rvbs04 + "' ORDER BY 1");
                        try
                        {
                            ResultSet rs = conn.executeQuery(l_sql);
                            while (rs.next())
                            {
                                l_sfv03 = rs.getInt(1);
                                l_sfv09 = rs.getFloat(2);
                                l_sfv11 = rs.getString(3);
                                l_sfuplant = rs.getString(4);
                                l_sfulegal = rs.getString(5);
                                if (l_sfv03 > 0)
                                {
                                    l_sql = ("select max(rvbs022) from rvbs_file where rvbs01='"
                                            + p_rvbs01
                                            + "' AND rvbs02='"
                                            + l_sfv03 + "'");
                                    try
                                    {
                                        ResultSet rs1 = conn1
                                                .executeQuery(l_sql);
                                        if (rs1.next())
                                        {
                                            l_cnt = rs1.getInt(1);
                                        }
                                        rs1.close();
                                    }
                                    catch (SQLException e)
                                    {
                                        e.printStackTrace();
                                        l_success = "D";
                                    }
                                    if (l_cnt > 0)
                                    {
                                        l_sql = ("select sum(rvbs06) from rvbs_file where rvbs01='"
                                                + p_rvbs01
                                                + "' AND rvbs02='"
                                                + l_sfv03 + "'");
                                        try
                                        {
                                            ResultSet rs1 = conn1
                                                    .executeQuery(l_sql);
                                            if (rs1.next())
                                            {
                                                l_rvbs06 = rs1.getFloat(1);
                                            }
                                            rs1.close();
                                        }
                                        catch (SQLException e)
                                        {
                                            e.printStackTrace();
                                            l_success = "E";
                                        }
                                        l_sfv09 -= l_rvbs06;
                                        l_sum -= l_rvbs06;
                                        if (l_sum < p_rvbs06)
                                        {
                                            l_success = "X";
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        l_cnt = 0;
                                    }

                                    if (l_sfv09 > 0.0F)
                                    {
                                        if (l_sfv09 >= p_rvbs06)
                                        {
                                            l_cnt++;
                                            l_sql = ("INSERT INTO rvbs_file VALUES('"
                                                    + p_rvbs00
                                                    + "','"
                                                    + p_rvbs01
                                                    + "',"
                                                    + l_sfv03
                                                    + ",'"
                                                    + p_rvbs03
                                                    + "','"
                                                    + p_rvbs04
                                                    + "',to_date('"
                                                    + g_date
                                                    + "','yyyyMMdd'),"
                                                    + p_rvbs06
                                                    + ",'2',' ','"
                                                    + p_rvbs021
                                                    + "',"
                                                    + l_cnt
                                                    + ",1,0,0,0,0,'"
                                                    + l_sfuplant
                                                    + "','"
                                                    + l_sfulegal + "')");
                                            try
                                            {
                                                conn1.executeUpdate(l_sql);
                                            }
                                            catch (SQLException e)
                                            {
                                                e.printStackTrace();
                                                l_success = "F";
                                            }
                                            break;
                                        }
                                        else
                                        {
                                            l_cnt++;

                                            l_sql = ("INSERT INTO rvbs_file VALUES('"
                                                    + p_rvbs00
                                                    + "','"
                                                    + p_rvbs01
                                                    + "',"
                                                    + l_sfv03
                                                    + ",'"
                                                    + p_rvbs03
                                                    + "','"
                                                    + p_rvbs04
                                                    + "',to_date('"
                                                    + g_date
                                                    + "','yyyyMMdd'),"
                                                    + l_sfv09
                                                    + ",'2',' ','"
                                                    + p_rvbs021
                                                    + "',"
                                                    + l_cnt
                                                    + ",1,0,0,0,0,'"
                                                    + l_sfuplant
                                                    + "','"
                                                    + l_sfulegal + "')");
                                            try
                                            {
                                                conn1.executeUpdate(l_sql);
                                            }
                                            catch (SQLException e)
                                            {
                                                e.printStackTrace();
                                                l_success = "F";
                                            }
                                            p_rvbs06 -= l_sfv09;
                                            if (p_rvbs06 == 0.0F)
                                            {
                                                break;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    l_success = "C";
                                }
                            }
                            rs.close();
                        }
                        catch (SQLException e)
                        {
                            e.printStackTrace();
                            l_success = "B";
                        }
                    }
                }
                else
                {
                    l_sql = ("SELECT SUM(sfv09) FROM sfv_file,tc_brb_file WHERE sfv01='"
                            + p_rvbs01
                            + "' AND sfv04=tc_brb22 AND tc_brb03 ='"
                            + p_rvbs04 + "'");
                    try
                    {
                        ResultSet rs = conn.executeQuery(l_sql);
                        if (rs.next())
                        {
                            l_sum = rs.getFloat(1);
                        }
                        rs.close();
                    }
                    catch (SQLException e)
                    {
                        e.printStackTrace();
                        l_success = "B";
                    }

                    if (l_sum < p_rvbs06)
                    {
                        l_success = "X";
                    }
                    else
                    {
                        l_sql = ("SELECT DISTINCT sfv03,sfv09,sfv11,sfuplant,sfulegal FROM sfv_file,sfu_file,tc_brb_file WHERE sfv01=sfu01 AND sfv01='"
                                + p_rvbs01
                                + "' AND sfv04=tc_brb22 AND tc_brb03 ='"
                                + p_rvbs04 + "' ORDER BY 1");
                        try
                        {
                            ResultSet rs = conn.executeQuery(l_sql);
                            while (rs.next())
                            {
                                l_sfv03 = rs.getInt(1);
                                l_sfv09 = rs.getFloat(2);
                                l_sfv11 = rs.getString(3);
                                l_sfuplant = rs.getString(4);
                                l_sfulegal = rs.getString(5);

                                String str7 = "select count(*) from rvbs_file where rvbs01='"
                                        + p_rvbs01
                                        + "' AND rvbs02='"
                                        + l_sfv03
                                        + "'";
                                try
                                {
                                    ResultSet rs1 = conn1.executeQuery(str7);
                                    if (rs1.next())
                                    {
                                        l_cnt = rs1.getInt(1);
                                    }
                                    rs1.close();
                                }
                                catch (SQLException e)
                                {
                                    e.printStackTrace();
                                    l_success = "D";
                                }
                                if (l_cnt > 0)
                                {
                                    String str8 = "select max(rvbs022) from rvbs_file where rvbs01='"
                                            + p_rvbs01
                                            + "' AND rvbs02='"
                                            + l_sfv03 + "'";
                                    try
                                    {
                                        ResultSet rs1 = conn1
                                                .executeQuery(str8);
                                        if (rs1.next())
                                        {
                                            l_cnt = rs1.getInt(1);
                                        }
                                        rs1.close();
                                    }
                                    catch (SQLException e)
                                    {
                                        e.printStackTrace();
                                        l_success = "D";
                                    }
                                    String str9 = "select sum(rvbs06) from rvbs_file where rvbs01='"
                                            + p_rvbs01
                                            + "' AND rvbs02='"
                                            + l_sfv03 + "'";
                                    try
                                    {
                                        ResultSet rs1 = conn1
                                                .executeQuery(str9);
                                        if (rs1.next())
                                        {
                                            l_rvbs06 = rs1.getFloat(1);
                                        }
                                        rs1.close();
                                    }
                                    catch (SQLException e)
                                    {
                                        e.printStackTrace();
                                        l_success = "E";
                                    }
                                    l_sfv09 -= l_rvbs06;
                                    l_sum -= l_rvbs06;
                                    if (l_sum < p_rvbs06)
                                    {
                                        l_success = "X";
                                        break;
                                    }
                                }
                                else
                                {
                                    l_cnt = 0;
                                }

                                if (l_sfv09 > 0.0F)
                                {
                                    if (l_sfv09 >= p_rvbs06)
                                    {
                                        l_cnt++;

                                        l_sql = ("INSERT INTO rvbs_file VALUES('"
                                                + p_rvbs00
                                                + "','"
                                                + p_rvbs01
                                                + "',"
                                                + l_sfv03
                                                + ",'"
                                                + p_rvbs03
                                                + "','"
                                                + p_rvbs04
                                                + "',to_date('"
                                                + g_date
                                                + "','yyyyMMdd'),"
                                                + p_rvbs06
                                                + ",'2',' ','"
                                                + p_rvbs021
                                                + "',"
                                                + l_cnt
                                                + ",1,0,0,0,0,'"
                                                + l_sfuplant
                                                + "','"
                                                + l_sfulegal + "')");
                                        try
                                        {
                                            conn1.executeUpdate(l_sql);
                                        }
                                        catch (SQLException e)
                                        {
                                            e.printStackTrace();
                                            l_success = "F";
                                        }
                                        break;
                                    }
                                    else
                                    {
                                        l_cnt++;

                                        l_sql = ("INSERT INTO rvbs_file VALUES('"
                                                + p_rvbs00
                                                + "','"
                                                + p_rvbs01
                                                + "',"
                                                + l_sfv03
                                                + ",'"
                                                + p_rvbs03
                                                + "','"
                                                + p_rvbs04
                                                + "',to_date('"
                                                + g_date
                                                + "','yyyyMMdd'),"
                                                + l_sfv09
                                                + ",'2',' ','"
                                                + p_rvbs021
                                                + "',"
                                                + l_cnt
                                                + ",1,0,0,0,0,'"
                                                + l_sfuplant
                                                + "','"
                                                + l_sfulegal + "')");
                                        try
                                        {
                                            conn1.executeUpdate(l_sql);
                                        }
                                        catch (SQLException e)
                                        {
                                            e.printStackTrace();
                                            l_success = "F";
                                        }
                                        p_rvbs06 -= l_sfv09;
                                        if (p_rvbs06 == 0.0F)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                            rs.close();
                        }
                        catch (SQLException e)
                        {
                            e.printStackTrace();
                            l_success = "B";
                        }
                    }
                }
            }
            else
            {
                l_success = "W";
            }
        }
        else
        {
            l_success = "E";
        }

        if (l_success.equals("A"))
        {
            String str6 = "update tc_bre_file set tc_breud05 = sysdate,tc_breuser='"
                    + p_tc_breuser + "'  where tc_bre01='" + p_rvbs04 + "'";
            try
            {
                conn1.executeUpdate(str6);
            }
            catch (SQLException e)
            {
                e.printStackTrace();
                l_success = "F";
            }
        }
        try
        {
            conn.close();
            conn1.close();
        }
        catch (Exception e)
        {
            e.printStackTrace();
        }
        return l_success;
    }


}
