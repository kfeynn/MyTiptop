using System;
using System.Text;
using System.Drawing;
using System.Web.Mvc;
using System.Collections.Generic;

using MyTiptop.Core;
using MyTiptop.Services;
using MyTiptop.Web.Framework;


namespace MyTiptop.Web.Controllers
{
    public partial class ToolController : Controller
    {
        /// <summary>
        /// 验证图片
        /// </summary>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片高度</param>
        /// <returns></returns>
        public ImageResult VerifyImage(int width = 56, int height = 20)
        {
            //获得用户唯一标示符sid
            string sid = MallUtils.GetSidCookie();
            //当sid为空时
            if (sid == null)
            {
                //生成sid
                sid = Sessions.GenerateSid();
                //将sid保存到cookie中
                MallUtils.SetSidCookie(sid);
            }

            //生成验证值
            string verifyValue = Randoms.CreateRandomValue(4, false).ToLower();
            //生成验证图片
            RandomImage verifyImage = Randoms.CreateRandomImage(verifyValue, width, height, Color.White, Color.Blue, Color.DarkRed);
            //将验证值保存到session中
            Sessions.SetItem(sid, "verifyCode", verifyValue);

            //输出验证图片
            return new ImageResult(verifyImage.Image, verifyImage.ContentType);
        }

    }
}