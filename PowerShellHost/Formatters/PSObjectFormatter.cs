using System.Collections.Generic;
using System.Data;
using System.Management.Automation;

namespace PowerShellHost.Formatters
{
    public class PSObjectFormatter
    {
        internal IFormatter CreateFormatter(PSObject obj)
        {
            var baseObject = obj.BaseObject;

            if (baseObject is string)
            {
                return new StringFormatter(obj);
            }

            if (baseObject is DataRow)
            {
                return new DataRowFormatter(obj);
            }

            return new GenericObjectFormatter(obj);
        }

        //public string Format(PSObject obj)
        //{ 
        //    var baseObject = obj.BaseObject;

        //    if (baseObject.GetType() == typeof(string))
        //    {
        //        return baseObject.ToString();
        //    }

        //    if (baseObject.GetType() == typeof(DataRow))
        //    {
        //        var dataRow = (DataRow) baseObject;
        //        foreach (DataColumn dataColumn in dataRow.Table.Columns)
        //        {
        //            return $"{dataColumn.ColumnName}\t{dataRow[dataColumn]}";
        //        }
        //    }
        //    else
        //    {
        //        foreach (var prop in obj.Properties)
        //        {
        //            try
        //            {
        //                return $"{prop.Name}\t{prop.Value}";

        //            }
        //            catch (Exception e)
        //            {
        //                return e.Message;

        //                //AppendLine(e.Message);
        //            }
        //        }
        //    }

        //    return String.Empty;
        //}


        public string Format(PSObject obj)
        {
            var formatter = CreateFormatter(obj);
            return formatter.Format();
        }

        public IEnumerable<string> Format(ICollection<dynamic> data)
        {
            var list = new List<string>();

            foreach (PSObject obj in data)
            {
                var formatter = CreateFormatter(obj);
                list.Add(formatter.Format());
            }

            return list;
        }
    }
}
