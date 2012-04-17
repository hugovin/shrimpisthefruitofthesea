using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Collections;

namespace JS.Helpers
{
    public enum MessageSeverity
    {
        COOL, CALM, CAUTION, WARNING, NONE
    }

    public class MessagePackage
    {
        public string Message;
        public MessageSeverity Severity;
        public string SeverityString;

        public MessagePackage(string message, MessageSeverity severity)
        {
            this.Message = message;
            this.Severity = severity;
        }

        public MessagePackage(string message, string severity)
        {
            this.Message = message;
            this.SeverityString = severity;
        }
    }

    public class Helper
    {
        public Helper()
        {
        }

        /// <summary>
        /// Validates a credit card number using the standard Luhn/mod10
        /// validation algorithm.
        /// </summary>
        /// <param name="cardNumber">Card number, with or without
        ///        punctuation</param>
        /// <returns>True if card number appears valid, false if not
        /// </returns>
        public static bool IsCreditCardValid(string cardNumber)
        {
            const string allowed = "0123456789";
            int i;

            StringBuilder cleanNumber = new StringBuilder();
            for (i = 0; i < cardNumber.Length; i++)
            {
                if (allowed.IndexOf(cardNumber.Substring(i, 1)) >= 0)
                    cleanNumber.Append(cardNumber.Substring(i, 1));
            }
            if (cleanNumber.Length < 13 || cleanNumber.Length > 16)
                return false;

            for (i = cleanNumber.Length + 1; i <= 16; i++)
                cleanNumber.Insert(0, "0");

            int multiplier, digit, sum, total = 0;
            string number = cleanNumber.ToString();

            for (i = 1; i <= 16; i++)
            {
                multiplier = 1 + (i % 2);
                digit = int.Parse(number.Substring(i - 1, 1));
                sum = digit * multiplier;
                if (sum > 9)
                    sum -= 9;
                total += sum;
            }
            return (total % 10 == 0);
        }

        public static bool IsValidEmail(string email)
        {
            string inputEmail = Helper.IsString(email, "");
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }


        public static string GenerateScriptToAccessControls(ControlCollection controls)
        {
            StringBuilder output = new StringBuilder();

            foreach (Control c in controls)
            {
                if (Helper.StringExists(c.ID))
                {
                    output.AppendFormat("var {0} = $(\"{1}\");{2}", c.ID, c.ClientID, Environment.NewLine);
                }

                if (c.Controls.Count > 0)
                {
                    output.Append(GenerateScriptToAccessControls(c.Controls));
                }
            }

            return output.ToString();
        }


        public static string Dollar(decimal amount)
        {
            return amount.ToString("C2", System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
        }
        public static string Dollar(double amount)
        {
            return amount.ToString("C2", System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
        }

        public static string Image(string src) { return Image(src, ""); }
        public static string Image(string src, string alt, params string[] attrs)
        {
            StringBuilder image_attrs = new StringBuilder();
            foreach(string attr in attrs)
            {
                image_attrs.AppendFormat("{0} ", attr);
            }
            return String.Format("<img src='{0}' alt='{1}' {2}/>", src, alt, image_attrs);
        }

        public static string URLToLink(string url, string text)
        {
            if (Helper.StringExists(url) == false) return String.Empty;
            
            text = Helper.IsString(text, url);

            string format = "";
            if (url.StartsWith("http://") || url.StartsWith("https://"))
            {
                format = "<a href=\"{0}\">{1}</a>";
            }
            else
            {
                format = "<a href=\"http://{0}\">{1}</a>";
            }
            
            return String.Format(format, url, text);
        }

        public static string Urlize(string name)
        {
            string iChars = "!@#$%^&*()=[]\\\';,./{}|\":<>?";
            string value = name.ToLower();
            value = value.Replace(' ', '_');
            
            for (int i = 0; i < iChars.Length; i++)
            {
                if (value.IndexOf(iChars[i]) != -1)
                {
                    value = value.Replace(iChars[i].ToString(), "");
                }
            }
            
            return value;
        }


        /// <summary>
        /// Convert a list of strings into a CSV string (i.e., one,two,three,,five)
        /// </summary>
        public static string ListToCSV(string[] list)
        {
            StringBuilder csv = new StringBuilder();
            foreach (string item in list)
            {
                csv.AppendFormat("{0},", item);
            }
            return csv.ToString();
        }

        public static string LevelIndicator(string indicator, int level)
        {
            StringBuilder indic_string = new StringBuilder();
            for(int i=0;i<level;i++)
            {
                indic_string.Append(indicator);
            }
            return indic_string.ToString();
        }

        #region Session
        public static object GetSessionValue(System.Web.SessionState.HttpSessionState session, string key)
        {
            return GetSessionValue(session, key, typeof(object), "");
        }

        public static object GetSessionValue(System.Web.SessionState.HttpSessionState session, string key, object defaultObject)
        {
            return GetSessionValue(session, key, typeof(object), defaultObject);
        }

        public static object GetSessionValue(System.Web.SessionState.HttpSessionState session, string key, Type forcedType, object defaultObject)
        {
            try
            {
                object value = session[key];
                if (session[key] == null)
                {
                    return defaultObject;
                }
                if (forcedType != typeof(object))
                {
                    if (value.GetType() != forcedType)
                    {
                        return defaultObject;
                    }
                }
                return value;
            }
            catch
            {
                return defaultObject;
            }
        }
        #endregion

        #region CMS Urls
        public static string CMSUrl(string action, string page)
        {
            return CMSUrl(action, page, "", "");
        }
        public static string CMSUrl(string action, string page, int id)
        {
            return CMSUrl(action, page, id.ToString(), "");
        }
        public static string CMSUrl(string action, string page, long id)
        {
            return CMSUrl(action, page, id.ToString(), "");
        }
        public static string CMSUrl(string action, string page, string id)
        {
            return CMSUrl(action, page, id, "");
        }
        public static string CMSUrl(string action, string page, int id, int section_id, params string[] args)
        {
            return CMSUrl(action, page, id.ToString(), section_id.ToString(), args);
        }
        public static string CMSUrl(string action, string page, long id, long section_id, params string[] args)
        {
            return CMSUrl(action, page, id.ToString(), section_id.ToString(), args);
        }
        public static string CMSUrl(string action, string page, string id, string section_id, params string[] args)
        {
            string a = "", p = "", i = "", si = "";
            a = StringExists(action) ? String.Format("a={0}", action) : "";
            p = StringExists(page) ? String.Format("&p={0}", page) : "";
            i = StringExists(id) ? String.Format("&id={0}", id) : "";
            si = StringExists(section_id) ? String.Format("&sid={0}", section_id) : "";

            StringBuilder arg_string = new StringBuilder();
            int counter = 1;
            foreach (string s in args)
            {
                arg_string.AppendFormat("&arg{0}={1}", counter, s);
                counter++;
            }
            return String.Format("?{0}{1}{2}{3}", a, p, i, si, arg_string.ToString());
        }
        #endregion

        #region Messages
        public static string ShowMessage(string message, MessageSeverity severity)
        {
            return ShowMessage(message, GetMessageHighlight(severity));
        }
        
        public static string ShowMessage(MessagePackage package)
        {
            return ShowMessage(package.Message, Helper.StringExists(package.SeverityString) ? package.SeverityString : GetMessageHighlight(package.Severity));
        }

        public static string ShowMessage(string message, string highlight)
        {
            return String.Format("<div class=\"message {0}\">{1}</div>", highlight, message);
        }

        private static string GetMessageHighlight(MessageSeverity severity)
        {
            string highlight = "";
            if (severity == MessageSeverity.CALM) { highlight = "highlightCalm"; }
            else if (severity == MessageSeverity.COOL) { highlight = "highlightCool"; }
            else if (severity == MessageSeverity.CAUTION) { highlight = "highlightCaution"; }
            else if (severity == MessageSeverity.WARNING) { highlight = "highlightWarning"; }
            return highlight;
        }
        #endregion 


        public static void MakeGridViewAccessible(ref GridView grid)
        {
            if (grid.Rows.Count > 0)
            {
                //This replaces <td> with <th> and adds the scope attribute
                grid.UseAccessibleHeader = true;

                //This will add the <thead> and <tbody> elements
                grid.HeaderRow.TableSection = TableRowSection.TableHeader;

                //This adds the <tfoot> element. Remove if you don't have a footer row
                grid.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }


        /// <summary>
        /// Check to see if the object passed is numberic.
        /// </summary>
        /// <param name="obj">Object to Check</param>
        /// <returns>Boolean Value; True if numeric; False otherwise;</returns>
        public static bool IsNumeric(object obj)
        {
            try
            {
                int temp = Convert.ToInt16(obj);
                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// Checks to see if the object passed can be converted to a string, is not null, and has a length greater than zero chars.
        /// </summary>
        /// <param name="obj">Object to Check</param>
        /// <returns>Boolean Value; True if is a string; False otherwise;</returns>
        public static bool StringExists(object obj)
        {
            try
            {
                string temp = Convert.ToString(obj);
                if (temp.Length > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Checks to see if the object passed can be converted to a string, is not null, and has a length greater than zero chars.
        /// </summary>
        /// <param name="ObjectVar">Object to Check</param>
        /// <returns>String Value; The converted string if conversion succeeds, else it returns your default string</returns>
        public static string IsString(object obj, string defaultString)
        {
            try
            {
                
                string temp = Convert.ToString(obj);
                if (temp != null && temp.Length > 0)
                {
                    return temp;
                }

                throw new Exception();
            }
            catch
            {
                return defaultString;
            }
        }

        public static string IsString(object obj)
        {
            return IsString(obj, String.Empty);
        }

        public static bool IsNotNull(object obj)
        {
            return obj != null;
        }

        public static bool IsType(object obj, Type obj_type)
        {
            if (obj == null) return false;
            return obj.GetType() == obj_type;
        }

        public static string VarDump(object ObjectVar)
        {
            return VarDump(ObjectVar, 1);
        }
        /// <summary>
        /// Dump the contents of an object
        /// </summary>
        /// <param name="ObjectVar">Object to Dump</param>
        /// <returns>Returns a preformated string</returns>
        public static string VarDump(object ObjectVar, int Indent)
        {
            string tabIndent = "";
            for (int i = 0; i < Indent; i++)
            {
                tabIndent += "\t";
            }
            string prevTabIndent = "";
            for (int i = 0; i < Indent - 1; i++)
            {
                prevTabIndent += "\t";
            }
            StringBuilder Dump = new StringBuilder();
            Dump.Append("<pre>");

            if (ObjectVar == null)
            {
                Dump.AppendLine("Object is NULL");
                Dump.Append("</pre>");
                return Dump.ToString();
            }

            Type ObjectType = ObjectVar.GetType();
            Dump.AppendLine(prevTabIndent + "Type: " + ObjectType.Name);

            Dump.AppendLine(prevTabIndent + "Data:");
            if (ObjectType.IsArray)
            {
                Array ar = (Array)ObjectVar;
                int count = ar.Length;
                Dump.AppendLine(tabIndent + "[Index] -> [Value]");
                for (int i = 0; i < count; i++)
                {
                    /*object temp = ar.GetValue(i);
                    if (temp.GetType().IsArray)
                    {
                        Dump.AppendLine(tabIndent + "[" + i + "] -> " + VarDump(ar.GetValue(i), Indent + 1));
                    }
                    else
                    {*/
                    Dump.AppendLine(tabIndent + "[" + i + "] -> " + ar.GetValue(i).ToString());
                    //}
                }
            }
            else
            {
                /*switch (ObjectType.Name.ToLower())

                {
                    case "arraylist":
                        ArrayList al = (ArrayList)ObjectVar;
                        int count = al.Count;
                        Dump.AppendLine(tabIndent + "[Index] -> [Value]");
                        for (int i = 0; i < count; i++)
                        {
                            //Dump.AppendLine("\t[" + i + "] -> " + al[i].ToString());
                            Dump.AppendLine(tabIndent + "[" + i + "] -> " + VarDump(al,Indent+2));
                        }
                        break;
                    case "hashtable":
                        Hashtable ht = (Hashtable)ObjectVar;
                        Dump.AppendLine(tabIndent + "[Key] -> [Value]");
                        foreach (DictionaryEntry de in ht)
                        {
                            Dump.AppendLine(tabIndent + "[" + de.Key + "] -> " + de.Value.ToString());
                        }
                        break;
                    default:
                        Dump.AppendLine(tabIndent + ObjectVar.ToString());
                        break;
                }*/
                if (ObjectType == typeof(ArrayList))
                {
                    ArrayList al = (ArrayList)ObjectVar;
                    int count = al.Count;
                    Dump.AppendLine(tabIndent + "[Index] -> [Value]");
                    for (int i = 0; i < count; i++)
                    {
                        //Dump.AppendLine("\t[" + i + "] -> " + al[i].ToString());
                        Dump.AppendLine(tabIndent + "[" + i + "] -> " + VarDump(al, Indent + 2));
                    }
                }
                else if (ObjectType == typeof(Hashtable))
                {
                    Hashtable ht = (Hashtable)ObjectVar;
                    Dump.AppendLine(tabIndent + "[Key] -> [Value]");
                    foreach (DictionaryEntry de in ht)
                    {
                        Dump.AppendLine(tabIndent + "[" + de.Key + "] -> (" + de.Value.GetType().ToString() + ") " + de.Value.ToString());
                    }
                }
                else
                {
                    Dump.AppendLine(tabIndent + ObjectVar.ToString());
                }

            }
            PropertyInfo[] Properties = ObjectType.GetProperties();
            Dump.AppendLine(prevTabIndent + "Properties:");
            foreach (PropertyInfo Property in Properties)
            {
                string name = Property.Name;
                object value = null;
                try
                {
                    value = Property.GetValue(ObjectVar, null);
                }
                catch
                {
                    value = "N/A";
                }
                //string value = "";
                if (Property.PropertyType == typeof(ArrayList) || Property.PropertyType == typeof(Hashtable))
                {
                    Dump.AppendLine(tabIndent + "[" + name + "] -> ("+value.GetType().ToString()+")" + VarDump(value, Indent + 2));
                }
                else
                {
                    if (value == null)
                    {
                        string temp = tabIndent + "[" + name + "] -> (NULL) ";
                        Dump.AppendLine(temp);
                    }
                    else
                    {
                        string temp = tabIndent + "[" + name + "] -> (" + value.GetType().ToString() + ") ";
                        temp += (StringExists(value)) ? value.ToString() : "NULL";
                        Dump.AppendLine(temp);
                    }
                }
            }

            Dump.Append("</pre>");
            return Dump.ToString();
        }

        public static int ClosestPositionToTheRight(string stringToSearch, string stringToSearchFor, int startingPosition)
        {
            int len = stringToSearch.Length;
            if (startingPosition <= len)
            {
                if (startingPosition >= 0)
                {
                    string sub = stringToSearch.Substring(startingPosition);
                    int charPos = sub.IndexOf(stringToSearchFor);
                    return charPos + startingPosition;
                }
                else
                {
                    //return -1;
                    return stringToSearch.Length - 1;
                }
            }
            else
            {
                return len - 1;
            }
        }

        public static int ClosestPositionToTheLeft(string stringToSearch, string stringToSearchFor, int startingPosition)
        {
            int len = stringToSearch.Length;
            int firstPos = 0;
            if (startingPosition <= len)
            {
                if (startingPosition > 0)
                {
                    for (int i = startingPosition; i >= 0; i--)
                    {
                        if (stringToSearch[i].ToString() == stringToSearchFor)
                        {
                            firstPos = i;
                            break;
                        }
                    }
                    return firstPos;
                }
                else
                {
                    //return -1;
                    return 0;
                }
            }
            else
            {
                return len - 1;
            }
        }

        public static string TitleCase(string TextToFormat)
        {
            return new System.Globalization.CultureInfo("en").TextInfo.ToTitleCase(TextToFormat.ToLower());
        }

        public static string StripHTML(string StringToStrip)
        {
            Regex r = new Regex(@"(\<(/?[^\>]+)\>)");
            return r.Replace(StringToStrip, "");
        }

        /// <summary>
        /// This Method is to load UserControls that have constructors
        /// </summary>
        /// <param name="UserControlPath">This is the relative path to the usercontrol</param>
        /// <param name="constructorParameters">These are the parameters to the user control contstructor</param>
        /// <returns>A UserControl object</returns>
        public static UserControl LoadUserControl(Page page, string UserControlPath, params object[] constructorParameters)
        {
            List<Type> constParamTypes = new List<Type>();
            foreach (object constParam in constructorParameters)
            {
                constParamTypes.Add(constParam.GetType());
            }

            UserControl ctl = page.LoadControl(UserControlPath) as UserControl;

            // Find the relevant constructor
            ConstructorInfo constructor = ctl.GetType().BaseType.GetConstructor(constParamTypes.ToArray());

            //And then call the relevant constructor
            if (constructor == null)
            {
                throw new MemberAccessException("The requested constructor was not found on : " + ctl.GetType().BaseType.ToString());
            }
            else
            {
                constructor.Invoke(ctl, constructorParameters);
            }

            // Finally return the fully initialized UC
            return ctl;
        }


        public static Hashtable BuildStatesHashtable()
        {
            Hashtable states = new Hashtable();
            states.Add("ALABAMA", "AL");
            states.Add("ALASKA", "AK");
            states.Add("AMERICAN SAMOA", "AS");
            states.Add("ARIZONA", "AZ");
            states.Add("ARKANSAS", "AR");
            states.Add("CALIFORNIA", "CA");
            states.Add("COLORADO", "CO");
            states.Add("CONNECTICUT", "CT");
            states.Add("DELAWARE", "DE");
            states.Add("DISTRICT OF COLUMBIA", "DC");
            states.Add("FEDERATED STATES OF MICRONESIA", "FM");
            states.Add("FLORIDA", "FL");
            states.Add("GEORGIA", "GA");
            states.Add("GUAM", "GU");
            states.Add("HAWAII", "HI");
            states.Add("IDAHO", "ID");
            states.Add("ILLINOIS", "IL");
            states.Add("INDIANA", "IN");
            states.Add("IOWA", "IA");
            states.Add("KANSAS", "KS");
            states.Add("KENTUCKY", "KY");
            states.Add("LOUISIANA", "LA");
            states.Add("MAINE", "ME");
            states.Add("MARSHALL ISLANDS", "MH");
            states.Add("MARYLAND", "MD");
            states.Add("MASSACHUSETTS", "MA");
            states.Add("MICHIGAN", "MI");
            states.Add("MINNESOTA", "MN");
            states.Add("MISSISSIPPI", "MS");
            states.Add("MISSOURI", "MO");
            states.Add("MONTANA", "MT");
            states.Add("NEBRASKA", "NE");
            states.Add("NEVADA", "NV");
            states.Add("NEW HAMPSHIRE", "NH");
            states.Add("NEW JERSEY", "NJ");
            states.Add("NEW MEXICO", "NM");
            states.Add("NEW YORK", "NY");
            states.Add("NORTH CAROLINA", "NC");
            states.Add("NORTH DAKOTA", "ND");
            states.Add("NORTHERN MARIANA ISLANDS", "MP");
            states.Add("OHIO", "OH");
            states.Add("OKLAHOMA", "OK");
            states.Add("OREGON", "OR");
            states.Add("PALAU", "PW");
            states.Add("PENNSYLVANIA", "PA");
            states.Add("PUERTO RICO", "PR");
            states.Add("RHODE ISLAND", "RI");
            states.Add("SOUTH CAROLINA", "SC");
            states.Add("SOUTH DAKOTA", "SD");
            states.Add("TENNESSEE", "TN");
            states.Add("TEXAS", "TX");
            states.Add("UTAH", "UT");
            states.Add("VERMONT", "VT");
            states.Add("VIRGIN ISLANDS", "VI");
            states.Add("VIRGINIA", "VA");
            states.Add("WASHINGTON", "WA");
            states.Add("WEST VIRGINIA", "WV");
            states.Add("WISCONSIN", "WI");
            states.Add("WYOMING", "WY");
            return states;
        }
    }
}

public class Helper : JS.Helpers.Helper
{ }

