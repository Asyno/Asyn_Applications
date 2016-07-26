
using System;

namespace Serialization_Single
{
    [Serializable]
    class Person
    {
        private int _Alter;
        public string Name { get; set; }

        public Person(int alter,string name)
        {
            Name = name;
            _Alter = alter;
        }

        public int Alter { get { return _Alter; } }
    }
}
