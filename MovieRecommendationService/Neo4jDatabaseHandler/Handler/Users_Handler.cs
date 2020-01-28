using MovieRecommendationLibrary.Model;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommendationLibrary.Neo4jDatabaseHandler
{
    public partial class Neo4jDatabaseHandler
    {
        
        //Create a user, only if they don't already exist
        public void CreateUser(User newUser)
        {
            Console.WriteLine($"CreateUser:  {newUser.UserId}");
            try
            {
                using (var graphClient = CreateClient())
                {
                    graphClient.Connect();

                    graphClient.Cypher
                        .Merge($"(user:User {{ userId: {newUser.UserId} }})")
                        .ExecuteWithoutResults();
                    //.Merge("(user:User { serId: {userId} })")
                    //.OnCreate()
                    //.Set("user = {newUser}")
                    //.WithParams(new
                    //{
                    //    userId = newUser.UserId,
                    //    newUser
                    //})
                    //.ExecuteWithoutResults();



                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public User GetUser(long userId)
        {
            User result = null;
            try
            {
                using (var graphClient = CreateClient())
                {
                    graphClient.Connect();

                    result = graphClient.Cypher
                        .Match($"(user:User {{ userId: {userId} }})")
                        .Return(user => user.As<User>())
                        .Results
                        .FirstOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
        
        public long GetUserCount()
        {
            long result = 0;
            using (var graphClient = CreateClient())
            {
                graphClient.Connect();

                //graphClient.Cypher
                //    .OptionalMatch("(user:User)-[FRIENDS_WITH]-(friend:User)")
                //    .Where((User user) => user.UserId == 1234)
                //    .Return((user, friend) => new
                //    {
                //        User = user.As<User>(),
                //        NumberOfFriends = friend.Count()
                //    })
                //    .Results;
            }
            return result;
        }

        public List<User> GetAllUsers()
        {
            List<User> result = null;
            try
            {
                using (var graphClient = CreateClient())
                {
                    graphClient.Connect();

                    result = graphClient.Cypher
                        .Match("(user:User)")
                        .Return(user => user.As<User>())
                        .Results
                        .ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public void DeleteUser(long userid)
        {
            try
            {
                using (var graphClient = CreateClient())
                {
                    graphClient.Connect();

                    graphClient.Cypher
                        .Match($"(user:User {{ userId: {userid} }})")
                        .OptionalMatch("(user)-[r]->()")
                        .Delete("r, user")
                        .ExecuteWithoutResults();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteAllUsers()
        {
            try
            {
                using (var graphClient = CreateClient())
                {
                    graphClient.Connect();

                    graphClient.Cypher
                        .Match("(user:User)")
                        .OptionalMatch("(user)-[r]->()")
                        .Delete("r, user")
                        .ExecuteWithoutResults();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        



    }
}
