using MovieRecommendationLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo4jClient;

namespace MovieRecommendationLibrary.Neo4jDatabaseHandler
{
    public partial class Neo4jDatabaseHandler
    {
        public List<MovieRecommendation> GetRecommendationOnMovie(long movieId)
        {
            List<MovieRecommendation> result = null;
            try
            {
                using (var graphClient = CreateClient())
                {
                    graphClient.Connect();

                    result = graphClient.Cypher
                        .Match("(m:Movie)-[:HAS_GENRE]-(t:Genre)<-[:HAS_GENRE]-(other:Movie)")
                        .Where((Movie m) => m.MovieId == movieId)
                        .With("m, other, COUNT(t) AS intersection, COLLECT(t.name) AS i")
                        .Match("(m)-[:HAS_GENRE]-(mt)")
                        .With("other, intersection,i, COLLECT(mt.name) AS genreList1")
                        .Match("(other)-[:HAS_GENRE]-(ot)")
                        .With("other,intersection,i, genreList1, COLLECT(ot.name) AS genreList2")
                        .With("other,intersection, genreList1, genreList2, ((1.0*intersection)/SIZE(genreList1+filter(x IN genreList2 WHERE NOT x IN genreList1))) AS jaccard")
                        .Where("jaccard > 0.7")
                        .With(@"{   title: other.title, 
                                    movieId: other.movieId,
                                    averageRating: other.averageRating,
                                    jaccard: jaccard 
                                } as movie")
                        .Return<MovieRecommendation>(movie => movie.As<MovieRecommendation>())
                        .OrderByDescending("movie.averageRating")
                        .Limit(10)
                        .Results.ToList();
                }
            }
            catch (Exception)
            {
            }
            return result;
        }
    }
}
