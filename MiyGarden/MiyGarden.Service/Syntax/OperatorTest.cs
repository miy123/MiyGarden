using System;

namespace MiyGarden.Service.Syntax
{
    public class OperatorTest
    {
        public class TestEqual
        {
            public string Name { set; get; }
            public static bool operator ==(TestEqual a, TestEqual b) => a.Name == b.Name;
            public static bool operator !=(TestEqual a, TestEqual b) => a.Name != b.Name;

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(this, obj))
                {
                    return true;
                }

                if (ReferenceEquals(obj, null))
                {
                    return false;
                }

                throw new NotImplementedException();
            }

            public override int GetHashCode()
            {
                throw new NotImplementedException();
            }
        }
    }
}
