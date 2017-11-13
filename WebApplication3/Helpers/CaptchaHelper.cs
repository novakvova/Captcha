using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace WebApplication3.Helpers
{
    public static class CaptchaHelper
    {
        internal const string SessionKeyPrefix = "__Captcha";
        private const string ImgFormat = "<img src=\"{0}\"/>";
        public static string Captcha(this HtmlHelper html, string name)
        {
            //Выбор GUID для представления этого вызова
            string challengeGuid = Guid.NewGuid().ToString();
            //Генерируем и сохраняем произвольный текст решения
            var session = html.ViewContext.HttpContext.Session;
            session[SessionKeyPrefix + challengeGuid] = MakeRandomSolution();
            //Визуализировать дескриптор <img> с искаженым текстом,
            //плюс скрытое поле, содержащее GUID вызова
            var urlHelper = new UrlHelper(html.ViewContext.RequestContext);
            string url = urlHelper.Action("Render", "CaptchaImage", new { challengeGuid });
            return string.Format(ImgFormat, url) + html.Hidden(name, challengeGuid);
        }
        private static string MakeRandomSolution()
        {
            Random rng = new Random();
            int length = rng.Next(5, 7);
            char[] buf = new char[length];
            for (int i = 0; i < length; i++)
                buf[i] = (char)('a' + rng.Next(26));
            return new string(buf);
        }
        //Проверка
        public static bool VerifyAndExpireSolution(HttpContextBase context, string challengeGuid, string attenptedSolution)
        {
            //немедлено удаляем решение Session[] для предотвращения атак повторением
            string solution = (string)context.Session[SessionKeyPrefix + challengeGuid];
            context.Session.Remove(SessionKeyPrefix + challengeGuid);
            return ((solution != null) && (attenptedSolution == solution));
        }
    }
}