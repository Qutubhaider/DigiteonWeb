using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PolarCastleWeb.Common
{
    public static class AlertMessage
    {
        public const string RecordAdded = "{0} saved successfully.";
        public const string RecordUpdated = "{0} updated successfully.";
        public const string RecordDeleted = "{0} deleted successfully.";
        public const string OperationalError = "Operational error in {0}.";

        #region SaveAlert

        public const string SaveData = "Saved successfully.";
        public const string DeleteData = "Deleted successfully.";

        #endregion
        #region Login
        public const string CredentialMisMatch = "Password does not match";
        public const string UserNotFound = "User does not exist.";
        public const string UserDeleted = "User deleted.";
        public const string UserInactive = "User inactive.";
        #endregion
    }

    public class SessionConstant
    {
        public const string Id = "Id";
        public const string unUserId = "unUserId";
        public const string stUserName = "stUserName";
        public const string stEmail = "stEmail";
        public const string RoleId = "RoleId";
        public const string ZoneId = "ZoneId";
        public const string DivisionId = "DivisionId";
        public const string DesignationId = "DesignationId";
        public const string DeskId = "DeskId";
        public const string StoreId = "StoreId";
        public const string DepartmentId = "DepartmentId";
        public const string DepartmentName = "DepartmentName";
    }

    public class CommonFunctions
    {
        public enum ActionResponse
        {
            [StringValue("RecordAdded")]
            Add = 101,
            [StringValue("RecordUpdated")]
            Update = 102,
            [StringValue("RecordDeleted")]
            Delete = 103,
            [StringValue("RecordExists")]
            Exists = 104,
            [StringValue("Error")]
            Error = 0,
            [StringValue("Warning")]
            Warning = 1
        }

        public enum UserType
        {
            Admin = 1
        }
        public enum UserStatus
        {
            Active = 1,
            InActive = 0
        }

        #region retrive enum string
        public static class StringEnum
        {
            public static string getStringValue(Enum foValue)
            {
                string lsOutPut = null;
                Type lsType = foValue.GetType();
                FieldInfo loFieldInfo = lsType.GetField(foValue.ToString());
                if (loFieldInfo != null)
                {
                    StringValue[] loAryAttibutes = loFieldInfo.GetCustomAttributes(typeof(StringValue), false) as StringValue[];
                    if (loAryAttibutes.Length > 0)
                        lsOutPut = loAryAttibutes[0].Value;
                }
                return lsOutPut;
            }
            public static Dictionary<int, string> getListOfDescription<T>()
            {
                List<string> loEnumList = new List<string>();
                Dictionary<int, string> loEnumInfo = new Dictionary<int, string>();
                var loType = typeof(T);
                if (!loType.IsEnum)
                    throw new ArgumentException();
                string[] laEnumNames = Enum.GetNames(loType);
                foreach (string lsEnum in laEnumNames)
                {
                    var loMemInfo = loType.GetMember(lsEnum);
                    var loAttributes = loMemInfo[0].GetCustomAttributes(typeof(StringValue), false);
                    int liKey = Convert.ToInt32(((T)Enum.Parse(loType, lsEnum)));
                    string lsValue = loAttributes.Length > 0 ? (((StringValue)(loAttributes[0])).Value) : liKey.ToString();
                    loEnumInfo.Add(liKey, lsValue);
                }
                return loEnumInfo;
            }
        }
        #endregion retrive enum string

        #region Enum Classes & function
        public class StringValue : System.Attribute
        {
            private string _value;

            public StringValue(string value)
            {
                _value = value;
            }

            public string Value
            {
                get { return _value; }
            }
        }
        #endregion
    }
}
