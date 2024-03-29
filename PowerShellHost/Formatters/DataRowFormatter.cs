﻿using System.Data;
using System.Management.Automation;
using System.Text;

namespace PowerShellHost.Formatters
{
    internal class DataRowFormatter : IFormatter
    {
        private readonly object _object;

        public DataRowFormatter(object objectToFormat)
        {
            var baseObject = ((PSObject)objectToFormat).BaseObject;
            _object = baseObject;
        }

        public string Format()
        {
            var builder = new StringBuilder();

            var dataRow = (DataRow)_object;
            foreach (DataColumn dataColumn in dataRow.Table.Columns)
            {
                builder.AppendLine($"{dataColumn.ColumnName}\t{dataRow[dataColumn]}");
            }

            return builder.ToString();
        }
    }
}
