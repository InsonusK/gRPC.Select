using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NUnit.Framework;

namespace gRPC.Select.Test.TestTools
{
    public class DataModel:IEquatable<DataModel>
    {
        [Key]
        public int Id { get; set; }

        public string StringValue { get; set; }
        public double DoubleValue { get; set; }
        public float FloatValue { get; set; }
        public bool BoolValue { get; set; }

        public bool Equals(DataModel other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && StringValue == other.StringValue && DoubleValue.Equals(other.DoubleValue) && FloatValue.Equals(other.FloatValue) && BoolValue == other.BoolValue;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DataModel) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, StringValue, DoubleValue, FloatValue, BoolValue);
        }

        public static bool CompareArr(IEnumerable<DataModel> expected, IEnumerable<DataModel> asserted)
        {
            var _dataModels = asserted as DataModel[] ?? asserted.ToArray();
            Assert.AreEqual(expected.Count(), _dataModels.Count());
            foreach (DataModel _dataModel in expected)
            {
                Assert.IsTrue(_dataModels.Contains(_dataModel));
            }
            return true;
        }
    }
}