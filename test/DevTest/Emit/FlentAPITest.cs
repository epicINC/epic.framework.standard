using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection.Emit;
using System.Reflection;
using System.Linq;

namespace DevTest.Emit
{

    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public static string Title { get; set; }
    }

    public struct ValueUser
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public static string Title { get; set; }
    }

    public class UserTest
    {

        public static User Convert()
        {

            User.Title = "abc";

            return new User()
            {
                ID = 1,
                Name = "test"
            };


        }

        public static User Convert(User value)
        {
            value.ID = 1;
            value.Name = "test";
            return value;
        }


   
    }

    public class ValueUserTest
    {

        public static ValueUser Convert()
        {

            ValueUser.Title = "abc";

            return new ValueUser()
            {
                ID = 1,
                Name = "test"
            };


        }

        public static ValueUser Convert(ValueUser value)
        {
            value.ID = 1;
            value.Name = "test";
            return value;
        }



    }






}
