using Inventor.Infrastructure.Utils;

namespace CourtJustice.Web.Utils
{
    public static class DateTimeConverter
    {
        public static DateTime ConvertDateTime(string Date)
        {
            DateTime date = new DateTime();
            try
            {
                string CurrentPattern = Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
                string[] Split = new string[] { "-", "/", @"\", "." };
                string[] Patternvalue = CurrentPattern.Split(Split, StringSplitOptions.None);
                string[] DateSplit = Date.Split(Split, StringSplitOptions.None);
                string NewDate = "";
                //if (Patternvalue[0].ToLower().Contains("d") == true && Patternvalue[1].ToLower().Contains("m") == true && Patternvalue[2].ToLower().Contains("y") == true)
                //{
                //    NewDate = DateSplit[1] + "/" + DateSplit[0] + "/" + DateSplit[2];
                //}
                //else if (Patternvalue[0].ToLower().Contains("m") == true && Patternvalue[1].ToLower().Contains("d") == true && Patternvalue[2].ToLower().Contains("y") == true)
                //{
                //    NewDate = DateSplit[0] + "/" + DateSplit[1] + "/" + DateSplit[2];
                //}
                //else if (Patternvalue[0].ToLower().Contains("y") == true && Patternvalue[1].ToLower().Contains("m") == true && Patternvalue[2].ToLower().Contains("d") == true)
                //{
                //    NewDate = DateSplit[2] + "/" + DateSplit[0] + "/" + DateSplit[1];
                //}
                //else if (Patternvalue[0].ToLower().Contains("y") == true && Patternvalue[1].ToLower().Contains("d") == true && Patternvalue[2].ToLower().Contains("m") == true)
                //{
                //    NewDate = DateSplit[2] + "/" + DateSplit[1] + "/" + DateSplit[0];
                //}
                date = new DateTime(DateSplit[2].ToInt16(), DateSplit[1].ToInt16(), DateSplit[0].ToInt16()); ;
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }

            return date;

        }
    }
}
