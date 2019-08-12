using System;

namespace NumberToWordConverter
{
    public static class ConverterUtility
    {
        public static String Tens(string number)
        {
            int _Number = Convert.ToInt32(number);
            string  returnValue = string.Empty;
            string result = Enum.GetName(typeof(TENS), _Number);
            if (!string.IsNullOrEmpty(result))
            {
                returnValue = result;
            }
            else
            {
                if (_Number > 0)
                {
                    returnValue = Tens(number.Substring(0, 1) + "0") + " " + Ones(number.Substring(1));
                }
            }
            return returnValue;
        }

        public static string Ones(string number)
        {
            if (Enum.TryParse<ONES>(number, out ONES result))
            {
                return result.ToString();
            }
            else
            {
                return "";
            }
        }

        public static string ConvertToWords(string Number)
        {
            string word = "";
            bool isDone = false;
            Number = Number.Trim();
            double dblAmt = (Convert.ToDouble(Number));

            if (dblAmt > 0)
            {
                int numDigits = Number.Length;
                int pos = 0;//store digit grouping    
                String place = "";//digit grouping name:hundres,thousand,etc...   
                switch (numDigits)
                {
                    case 1://ones' range
                        word = Ones(Number);
                        isDone = true;
                        break;
                    case 2://tens' range
                        word = Tens(Number);
                        isDone = true;
                        break;
                    case 3://hundreds' range    
                        pos = (numDigits % 3) + 1;
                        place = " HUNDRED";
                        break;
                    case 4://thousands' range    
                    case 5:
                        pos = (numDigits % 4) + 1;
                        place = " THOUSAND";
                        break;
                    case 6: //Lakhs range
                        pos = (numDigits % 6) + 1;
                        place = " LAKH";
                        break;
                    case 7://millions' range    
                    case 8:
                    case 9:
                        pos = (numDigits % 7) + 1;
                        place = " MILLION";
                        break;
                    case 10://Billions's range    
                    case 11:
                    case 12:

                        pos = (numDigits % 10) + 1;
                        place = " BILLION";
                        break;
                        
                    default:
                        isDone = true;
                        break;
                }

                if (!isDone)
                {//if conversion is not done, continue...(Recursion comes in now!!)    
                    if (Number.Substring(0, pos) != "0" && Number.Substring(pos) != "0")
                    {
                        try
                        {
                            word = ConvertToWords(Number.Substring(0, pos)) +" " + place +" "+ ConvertToWords(Number.Substring(pos));
                        }
                        catch { }
                    }
                    else
                    {
                        word = ConvertToWords(Number.Substring(0, pos)) + ConvertToWords(Number.Substring(pos));
                    }
                }
            }
            return word;
        }
    }
}
