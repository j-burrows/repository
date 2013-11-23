using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web;
namespace Repository.Data{
    public static class Helpers{
        public static bool IsNulltype(this object nullable) {
            return nullable == null || nullable is DBNull;
        }

        public static int ToInt(this object column, int fallback = 0){
            return column.IsNulltype()?
                fallback :
                Convert.ToInt32(column);
        }
        public static string ToStr(this object column, string fallback = ""){
            return column.IsNulltype() ?
                fallback :
                column.ToString();
        }
        public static char ToChar(this object column, char fallback = '\0'){
            return column.IsNulltype() ?
                fallback :
                Convert.ToChar(column);
        }
        public static DateTime ToDateTime(this object column) {
            return column.IsNulltype() ?
                DateTime.MinValue :
                Convert.ToDateTime(column);
        }
        public static DateTime ToDateTime(this object column, DateTime fallback){
            return column.IsNulltype() ?
                fallback :
                Convert.ToDateTime(column);
        }
        public static decimal ToDec(this object column, decimal fallback=0) {
            return column.IsNulltype() ?
                fallback :
                Convert.ToDecimal(column);
        }

        public static string Format(this string changing){
            return changing == null ?
                string.Empty :
                changing;
        }
        public static SqlDbType SqlDbTypeFactory(this object CsObject) { 
            if(CsObject is int){
                return SqlDbType.Int;
            }
            else if(CsObject is string){
                return SqlDbType.VarChar;
            }
            else if(CsObject is DateTime){
                return SqlDbType.DateTime;
            }
            else if(CsObject is decimal){
                return SqlDbType.Decimal;
            }
            else if(CsObject is bool){
                return SqlDbType.Bit;
            }

            return SqlDbType.VarChar;
        }

        public static void AddParam(this SqlCommand building, string paramName, 
            object paramValue){
            if(paramValue != null){
                building.Parameters.Add("@" + paramName, paramValue.SqlDbTypeFactory())
                    .Value = paramValue;
            }
            else{
                building.Parameters.Add("@" + paramName, paramValue.SqlDbTypeFactory())
                    .Value = DBNull.Value;
            }
        }

        public static void AddParams(this SqlCommand building, params Param[] parameters) {
            foreach (Param param in parameters) {
                building.AddParam(param.key, param.value);
            }
        }

        public static string Scrub(this string encoding) {
            if (encoding != null){
                encoding = HttpUtility.HtmlEncode(encoding);
                encoding = Regex.Replace(encoding, "'", "''");
            }
            return encoding;
        }

        public static bool MatchingKeyAndType<T>(
            this Repository.Data.IDataUnit dataunit, 
            Repository.Business.IBusinessUnit comparing)
            where T : class,  Repository.Data.IDataUnit {
            //Returns true iif of matching type and keys.
            T converted;
            if((converted = comparing as T) != null){
                if(converted.key == dataunit.key){
                    return true;
                }
            }
            return false;
        }
    }

    public class Param {
        public string key { get; set; }
        public object value { get; set; }
        public Param() { }
        public Param(string key, object value) {
            this.key = key;
            this.value = value;
        }
    }

    public class Polymorphism{
        public static bool IsInHierachy(Type derivedClass, Type baseClass){
            return derivedClass == baseClass
                || derivedClass.IsSubclassOf(baseClass);
        }
    }
}
