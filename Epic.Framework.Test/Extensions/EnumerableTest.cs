using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epic.Extensions;

namespace Epic.Framework.Test.Extensions
{
/*
    [TestClass]
    public class EnumerableTest
    {
        internal class User
        {
            public string Name { get; set; }
            public int GroupID { get; set; }
        }

        internal class Group
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        internal IEnumerable<User> Users()
        {
            return new[]
            {
                new User(){ Name="张1", GroupID=1 },
                new User(){ Name="张2", GroupID=2 },
                new User(){ Name="张33", GroupID=33 }
            };
        }

        internal IEnumerable<Group> Groups()
        {
            return new[]
            {
                new Group(){ ID=1, Name ="A" },
                new Group(){ ID=2, Name ="B" },
                new Group(){ ID=3, Name ="C" }
            };
        }

        [TestMethod]
        public void LeftJoin()
        {
            var users = this.Users();
            var groups = this.Groups();

            var expected = new[]
            {
                new { Group = groups.ElementAt(0), User = users.ElementAt(0) },
                new { Group = groups.ElementAt(1), User = users.ElementAt(1) },
                new { Group = groups.ElementAt(2), User = default(User) }
            };
            var actual = groups.LeftJoin(users, e => e.ID, e => e.GroupID, (g, u) => new { Group = g, User = u });

            CollectionAssert.AreEqual(actual.ToList(), expected.ToList());
        }

        [TestMethod]
        public void RightJoin()
        {
            var users = this.Users();
            var groups = this.Groups();

            var expected = new[]
            {
                new { Group = groups.ElementAt(0), User = users.ElementAt(0) },
                new { Group = groups.ElementAt(1), User = users.ElementAt(1) },
                new { Group = default(Group), User = users.ElementAt(2) }
            };

            var actual = groups.RightJoin(users, e => e.ID, e => e.GroupID, (g, u) => new { Group = g, User = u });
            CollectionAssert.AreEqual(actual.ToList(), expected.ToList());
        }



        [TestMethod]
        public void UnionAll()
        {
            var users = this.Users();
            var groups = this.Groups();

            var expected = new[]
            {
                new { Group = groups.ElementAt(0), User = users.ElementAt(0) },
                new { Group = groups.ElementAt(1), User = users.ElementAt(1) },
                new { Group = groups.ElementAt(2), User = default(User) },
                new { Group = default(Group), User = users.ElementAt(2) }
            };

            var actual = groups.UnionAll(users, e => e.ID, e => e.GroupID, (g, u) => new { Group = g, User = u });
            CollectionAssert.AreEqual(actual.ToList(), expected.ToList());
        }

        [TestMethod]
        public void CrossJoin()
        {
            var users = this.Users();
            var groups = this.Groups();
            var expected = new[]
{
                new { Group = groups.ElementAt(0), User = users.ElementAt(0) },
                new { Group = groups.ElementAt(0), User = users.ElementAt(1) },
                new { Group = groups.ElementAt(0), User = users.ElementAt(2) },
                new { Group = groups.ElementAt(1), User = users.ElementAt(0) },
                new { Group = groups.ElementAt(1), User = users.ElementAt(1) },
                new { Group = groups.ElementAt(1), User = users.ElementAt(2) },
                new { Group = groups.ElementAt(2), User = users.ElementAt(0) },
                new { Group = groups.ElementAt(2), User = users.ElementAt(1) },
                new { Group = groups.ElementAt(2), User = users.ElementAt(2) }
            };
            var actual = groups.CrossJoin(users, (g, u) => new { Group = g, User = u });
            CollectionAssert.AreEqual(actual.ToList(), expected.ToList());
        }
    }
    */
}
