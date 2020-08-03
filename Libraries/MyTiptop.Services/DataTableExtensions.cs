using System;
using System.Web;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Collections;
using MyTiptop.Core;



namespace MyTiptop.Services
{
    public   static class DataTableExtensions
    {
        /// <summary>
        /// datatable 转换为 json
        /// </summary>
        /// <param name="count"></param>
        /// <param name="page"></param> 
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DataTableToJson(this DataTable dt)
        {
            StringBuilder Json = new StringBuilder();
            Json.Append("[");
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Json.Append("{");
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            Json.Append("\"" + dt.Columns[j].ColumnName.ToString() +
                             "\":\"" + GetDeleteBR(dt.Rows[i][j].ToString()) + "\"");
                            if (j < dt.Columns.Count - 1)
                            {
                                Json.Append(",");
                                Json.Append("\r\n");
                            }
                        }
                        Json.Append("}");
                        if (i < dt.Rows.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                }
            }
            Json.Append("]");
            return Json.ToString();
        }


        /// <summary>
        /// 转化为柱状图用json
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DataTableToJsonForEcharts(this DataTable dt)
        {
            //var dimension = ['iMonth', '2017', '2018'];     //维度定义。
            //var barStr = [{ type: 'bar'},{ type: 'bar'}];    //一个标签包含多少 项目
            //var ds = [  { iMonth: '1', '2018': 43.3},         //数据
            //            { iMonth: '2', '2018': 83.1},
            //            { iMonth: '3', '2018': 86.4,},
            //            { iMonth: '4', '2018': 72.4},
            //            { iMonth: '5', '2018': 72.4},
            //            { iMonth: '6', '2018': 62.4},
            //            { iMonth: '7', '2018': 72.4},
            //            { iMonth: '8', '2018': 66.4},
            //            { iMonth: '9', '2018': 72.4},
            //            { iMonth: '10', '2018': 79.4},
            //            { iMonth: '1', '2017': 53.3},
            //            { iMonth: '2', '2017': 53.6}
            //          ];
            //年份     1   2   3   4   5   6   7   8   9   10  11  12  合计
            //2018    55  50  50  50  50  50  50  50  50  50  50  50  467
            //年份 、合计  不要

            StringBuilder Json = new StringBuilder();
            Json.Append("[");
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 1; j < dt.Columns.Count-1; j++)   //首尾两列不要
                        {

                            Json.Append("{");
                            Json.Append("\"iMonth\":\"" + GetDeleteBR(dt.Columns[j].ColumnName.ToString()) + "\"");

                            if (j < dt.Columns.Count - 1)
                            {
                                Json.Append(",");
                                Json.Append("\r\n");
                            }
                            Json.Append("\"" + dt.Rows[i][0].ToString() + "\":" + GetBR(dt.Rows[i][j].ToString()) + "");
                            Json.Append("}");

                            if (j < dt.Columns.Count - 2)
                            {
                                Json.Append(",");
                                Json.Append("\r\n");
                            }
                        }
                        
                        if (i < dt.Rows.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                }
            }
            Json.Append("]");
            return Json.ToString();
        }

        /// <summary>
        /// 转化为柱状图用json
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DataTableToJsonForBar  (this DataTable dt)
        {
            //var barStr = [{ type: 'bar'},{ type: 'bar'}];    //一个标签包含多少 项目

           // var dimension = ['iMonth', '2017', '2018'];     //维度定义。

            StringBuilder Json = new StringBuilder();
            Json.Append("[");
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Json.Append("{ type: 'bar'}");
                        //Json.Append("\"" + GetDeleteBR(dt.Rows[i][0].ToString()) + "\"");
                        if (i < dt.Rows.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                }
            }
            Json.Append("]");
            return Json.ToString();
        }

        /// <summary>
        /// 转化为柱状图用json
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DataTableToJsonForDimension(this DataTable dt)
        {
            //var dimension = ['iMonth', '2017', '2018'];     //维度定义。
            StringBuilder Json = new StringBuilder();
            Json.Append("[");
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    Json.Append("\"iMonth\",");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Json.Append("\"" + GetDeleteBR(dt.Rows[i][0].ToString()) + "\"");

                        if (i < dt.Rows.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                }
            }
            Json.Append("]");
            return Json.ToString();
        }



        /// <summary>
        /// 转化为饼状图用json
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DataTableToJsonForEchartsPie(this DataTable dt)
        {
            //var ds = [                                       //数据
            //    {value:335, name:'直接访问'},
            //    {value:310, name:'邮件营销'},
            //    {value:234, name:'联盟广告'},
            //    {value:135, name:'视频广告'},
            //    {value:1548, name:'搜索引擎'}
            //]


            StringBuilder Json = new StringBuilder();
            Json.Append("[");
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 1; j < dt.Columns.Count - 1; j++)   //首尾两列不要
                        {

                            Json.Append("{");
                            Json.Append("value:" + GetBR(dt.Rows[i][j].ToString()) + "");

                            if (j < dt.Columns.Count - 1)
                            {
                                Json.Append(",");
                                Json.Append("\r\n");
                            }
                            Json.Append("name:" + "'" + GetDeleteBR(dt.Columns[j].ColumnName.ToString()) + "'" + "");
                            Json.Append("}");

                            if (j < dt.Columns.Count - 2)
                            {
                                Json.Append(",");
                                Json.Append("\r\n");
                            }
                        }

                        if (i < dt.Rows.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                }
            }
            Json.Append("]");
            return Json.ToString();
        }



        /// <summary>
        /// List转化为Json格式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ListToJson<T>(this IEnumerable<T> list)
        {
            try
            {
                StringBuilder Json = new StringBuilder();
                Json.Append("[");
                if (list != null)
                {
                    //创建属性的集合
                    List<PropertyInfo> pList = new List<PropertyInfo>();
                    //获得反射的入口
                    Type type = typeof(T);
                    //获取字段信息
                    foreach (var pr in type.GetProperties())
                    {
                        //数据类型 System.Int32  ,虚拟字段包含Core   排除虚拟字段
                        if (pr.PropertyType.FullName.IndexOf("Core") < 0)
                        {
                            pList.Add(pr);
                            //dt.Columns.Add(pr.Name, pr.PropertyType);
                        }
                    }
                    //获取值信息
                    foreach (var item in list)
                    {
                        Json.Append("{");
                        //给row赋值
                        foreach (var pl in pList)
                        {
                            string value = "";
                            if (pl.GetValue(item, null) != null && pl.GetValue(item, null).ToString().Length > 0)
                            {
                                value = pl.GetValue(item, null).ToString();
                            }
                            Json.Append("\"" + pl.Name.ToString() + "\":\"" + GetDeleteBR(value) + "\"");

                            Json.Append(",");
                            Json.Append("\r\n");
                        }
                        //去除最后一个逗号 ","
                        Json = Json.Remove(Json.Length - 3, 3);
                        //
                        Json.Append("},");
                    }
                    //去除最后一个逗号 ","
                    if (Json.Length > 1)
                        Json = Json.Remove(Json.Length - 1, 1);
                }
                Json.Append("]");
                return Json.ToString();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// 转化一个DataTableIEnumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> list)
        {

            try
            {
                //创建属性的集合
                List<PropertyInfo> pList = new List<PropertyInfo>();
                //获得反射的入口
                Type type = typeof(T);
                DataTable dt = new DataTable();
                //把所有的public属性加入到集合 并添加DataTable的列(改写到以下 排除EF生成的无关字段)
                //Array.ForEach<PropertyInfo>(type.GetProperties(), p => { pList.Add(p); dt.Columns.Add(p.Name, p.PropertyType); });
                foreach (var pr in type.GetProperties())
                {
                    //数据类型 System.Int32  ,虚拟字段包含Core   排除虚拟字段
                    if (pr.PropertyType.FullName.IndexOf("Core") < 0)
                    {
                        pList.Add(pr);
                        dt.Columns.Add(pr.Name, pr.PropertyType);
                    }
                }
                foreach (var item in list)
                {
                    //创建一个DataRow实例
                    DataRow row = dt.NewRow();
                    //给row赋值
                    pList.ForEach(m => row[m.Name] = m.GetValue(item, null));
                    //foreach (var pl in pList)
                    //{
                    //    row[pl.Name] = pl.GetValue(item, null);
                    //}
                    //加入到DataTable
                    dt.Rows.Add(row);
                }
                return dt;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }





        /// <summary>
        /// DataTable 转换为List 集合
        /// </summary>
        /// <typeparam name="TResult">类型</typeparam>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable dt) where T : class, new()
        {
            //创建一个属性的列表
            List<PropertyInfo> prlist = new List<PropertyInfo>();
            //获取TResult的类型实例  反射的入口
            Type t = typeof(T);
            //获得TResult 的所有的Public 属性 并找出TResult属性和DataTable的列名称相同的属性(PropertyInfo) 并加入到属性列表 
            Array.ForEach<PropertyInfo>(t.GetProperties(), p => { if (dt.Columns.IndexOf(p.Name) != -1) prlist.Add(p); });
            //创建返回的集合
            List<T> oblist = new List<T>();

            foreach (DataRow row in dt.Rows)
            {
                //创建TResult的实例
                T ob = new T();
                //找到对应的数据  并赋值
                prlist.ForEach(p => { if (row[p.Name] != DBNull.Value) p.SetValue(ob, row[p.Name], null); });
                //放入到返回的集合中.
                oblist.Add(ob);
            }
            return oblist;
        }

        /// <summary>
        /// 将集合类转换成DataTable
        /// </summary>
        /// <param name="list">集合</param>
        /// <returns></returns>
        public static DataTable ToDataTableTow(IList list)
        {
            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    result.Columns.Add(pi.Name, pi.PropertyType);
                }

                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(list[i], null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }


        public static ArrayList DataTable2ArrayList(this DataTable dt)
        {
            ArrayList array = new ArrayList();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];

                Hashtable record = new Hashtable();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    object cellValue = row[j];
                    if (cellValue.GetType() == typeof(DBNull))
                    {
                        cellValue = null;
                    }
                    record[dt.Columns[j].ColumnName] = cellValue;
                }
                array.Add(record);
            }
            return array;
        }

        /// <summary>
        /// 去掉换行符
        /// </summary>
        /// <param name="str"></param>    
        /// <returns></returns>
        public static string GetDeleteBR(string strinput)
        {

            string p = "\\n|\r\n"; //数据库的的换行是\n
            string returnstr = System.Text.RegularExpressions.Regex.Replace(strinput, p, " ");
            return returnstr;


        }

        /// <summary>
        /// 去掉换行符,替换掉 ，号
        /// </summary>
        /// <param name="str"></param>    
        /// <returns></returns>
        public static string GetBR(string strinput)
        {

            string p = "\\n|\r\n"; //数据库的的换行是\n
            string returnstr = System.Text.RegularExpressions.Regex.Replace(strinput, p, " ");
            returnstr = System.Text.RegularExpressions.Regex.Replace(returnstr, ",", "");
            return returnstr;


        }

    }



}
