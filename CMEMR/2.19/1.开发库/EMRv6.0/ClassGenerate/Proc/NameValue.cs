using System;
using System.Collections.Generic;
using System.Text;

namespace ClassGenerate.Proc
{
    public class NameValue
    {
        public NameValue() { }
        public NameValue(string name, string fieldName, object value) 
        {
            Name = name;
            FieldName = fieldName;
            Value = value;
        }
        public string Name;
        public string FieldName;//NAME
        public string MemberName;
        public object Value;//name_
        public string FieldType;
        public string ValueIsNotEmpty;
        public string ValueIsNotNull;//!String.IsNullOrEmpty(this.NAME)
        public string FieldNameIsNotNull;//!String.IsNullOrEmpty(new_jhpix_pat_master_index_.NAME)
    }
}
