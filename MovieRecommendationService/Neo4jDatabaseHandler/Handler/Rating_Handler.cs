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

        // Needs testing
        public void SetUserMovieRating(long userid, long movieId, int rating)
        {

            Console.WriteLine($"SetUserMovieRating:  {userid}, {movieId}, {rating}");

            try
            {
                using (var graphClient = CreateClient())
                {
                    graphClient.Connect();

                    graphClient.Cypher
                        .Match("(user:User { userId: " + userid + "})", "(movie:Movie { movieId: " + movieId + "})")
                        .Create("(user)-[:RATED { rating: " + rating + "}]->(movie)")
                        .ExecuteWithoutResults();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Needs testing
        public List<UserRating> GetUserMovieRatings(long userid)
        {
            List<UserRating> result = null;
            try
            {
                using (var graphClient = CreateClient())
                {
                    graphClient.Connect();
                    

                    result = graphClient.Cypher
                        .Match($"(user:User {{ userId: { userid } }})-[r:RATED]->(movie:Movie)")
                        .With(@"{   userId: user.userId, 
                                    rating: r.rating,
                                    movieId: movie.movieId
                                } as userRating")
                        .Return<UserRating>(userRating => userRating.As<UserRating>())
                        .Results
                        .ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex; 
            }
            return result;
        }

        // Needs testing
        public void DeleteUserRatingMovie(long userid, long movieId)
        {
            using (var graphClient = CreateClient())
            {
                graphClient.Connect();

                graphClient.Cypher
                    .OptionalMatch($"(user:User {{ userId: {userid} }})-[r]->(movie:Movie {{ movieId: { movieId } }})")
                    .Delete("r")
                    .ExecuteWithoutResults();
            }
        }
    }
}
