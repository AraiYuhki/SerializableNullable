using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Xeon.Utility.Test
{
    public class SerializableNullableTest
    {
        [Serializable]
        private class SerializeData
        {
            [SerializeField]
            public List<SerializableNullable<int>> data = new();
        }
        [Test]
        public void Test1()
        {
            var intValueA = new SerializableNullable<int>(10);
            var intValueB = new SerializableNullable<int>(null);
            Assert.That(intValueA != intValueB);
            Assert.That(intValueA.HasValue);
            Assert.That(!intValueB.HasValue);
            Assert.That(intValueA.ToString() == "10");
            Assert.That(intValueB.ToString() == "null");
        }

        [Test]
        public void JsonTest()
        {
            var data = new SerializeData();
            for (var count = 0; count < 10; count++)
            {
                if (count % 3 == 0)
                    data.data.Add(new SerializableNullable<int>(null));
                else
                    data.data.Add(new SerializableNullable<int>(count));
            }
            var json = JsonUtility.ToJson(data, true);

            var actual = JsonUtility.FromJson<SerializeData>(json);
            foreach (var (row, index) in data.data.Select((row, index) => (row, index)))
            {
                int? except = index % 3 == 0 ? null : index;
                Assert.That(except == row);
            }

            Assert.That(true);
        }
    }
}
