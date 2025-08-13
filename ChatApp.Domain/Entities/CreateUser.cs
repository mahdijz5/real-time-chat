using MongoDB.Bson;
using ChatApp.Domain.ValueObjects;

namespace ChatApp.Domain.Entities
{
    public class CreateUser
    {

        public NonEmptyString Username { get; private set; }

        public NonEmptyString Password { get; private set; }



        public static CreateUser MkUnsafe(string username, string password)
        {
            return new CreateUser(username, password);
        }

        private CreateUser(string username, string password)
        {
            Username = NonEmptyString.MkUnsafe(username);
            Password = NonEmptyString.MkUnsafe(password);
        }


    }
}
