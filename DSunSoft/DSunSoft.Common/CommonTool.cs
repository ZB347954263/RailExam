using System;
using System.Text.RegularExpressions;

namespace DSunSoft.Common
{
    public class CommonTool
    {
        /// <summary>
        /// 将秒数转换为时间表示，如60 = 00:01:00
        /// </summary>
        /// <param name="totalSeconds">秒数，如60</param>
        /// <returns>时间表示，如00:01:00</returns>
        public static string ConvertSecondsToTimeString(int totalSeconds)
        {
            string res = string.Empty;
			//if (totalSeconds < 0)
			//{
			//    throw new Exception("秒数不能为负值！");
			//}

			if(totalSeconds <0)
			{
				totalSeconds = -totalSeconds;
			}

            int h = totalSeconds/3600;
            totalSeconds = totalSeconds - 3600*h;
            int m = totalSeconds/60;
            totalSeconds = totalSeconds - 60*m;

            res = h.ToString("00") + ":" + m.ToString("00") + ":" + totalSeconds.ToString("00");

            return res;
        }

        /// <summary>
        /// 将时间表示转换为秒数，如00:01:00 = 60
        /// </summary>
        /// <param name="timeString">时间表示，如00:01:00</param>
        /// <returns>秒数，如60</returns>
        public static int ConvertTimeStringToSeconds(string timeString)
        {
            int res = 0;
            Regex regex = new Regex(@"^\d{2}:\d{2}:\d{2}$");
            if (!regex.IsMatch(timeString))
            {
                throw new Exception("不正确的时间格式！");
            }

            string[] strings = timeString.Split(':');
            res += int.Parse(strings[0]) + int.Parse(strings[1])*60 + int.Parse(strings[2])*3600;

            return res;
        }

        /// <summary>
        /// 将数字(0-20)转换为中文数字

        /// </summary>
        /// <param name="number">如0</param>
        /// <returns>如零</returns>
        public static string GetChineseNumber(int number)
        {
            string strReturn = "";
            switch (number)
            {
                case 0:
                    strReturn = "零";
                    break;
                case 1:
                    strReturn = "一";
                    break;
                case 2:
                    strReturn = "二";
                    break;
                case 3:
                    strReturn = "三";
                    break;
                case 4:
                    strReturn = "四";
                    break;
                case 5:
                    strReturn = "五";
                    break;
                case 6:
                    strReturn = "六";
                    break;
                case 7:
                    strReturn = "七";
                    break;
                case 8:
                    strReturn = "八";
                    break;
                case 9:
                    strReturn = "九";
                    break;
                case 10:
                    strReturn = "十";
                    break;
                case 11:
                    strReturn = "十一";
                    break;
                case 12:
                    strReturn = "十二";
                    break;
                case 13:
                    strReturn = "十三";
                    break;
                case 14:
                    strReturn = "十四";
                    break;
                case 15:
                    strReturn = "十五";
                    break;
                case 16:
                    strReturn = "十六";
                    break;
                case 17:
                    strReturn = "十七";
                    break;
                case 18:
                    strReturn = "十八";
                    break;
                case 19:
                    strReturn = "十九";
                    break;
                case 20:
                    strReturn = "二十";
                    break;
            }
            return strReturn;
        }

        /// <summary>
        /// 获取选项字母，如1 = A, 26 = Z
        /// </summary>
        /// <param name="index">如1</param>
        /// <returns>如A</returns>
        public static char GetSelectLetter(int index)
        {
            if (0<index && index < 27)
            {
                return Convert.ToChar(index + 64);
            }
            //else
            //{
            //    return Convert.ToChar((index - 1) / 26 + 64).ToString()
            //           + Convert.ToChar((index - 1) % 26 + 64 + 1).ToString();
            //}
            throw new Exception("Out of range!");
        }
    }
}