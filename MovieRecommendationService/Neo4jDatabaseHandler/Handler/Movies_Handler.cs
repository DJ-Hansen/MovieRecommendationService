using MovieRecommendationLibrary.Model;
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
        public void CreateMovie(Movie newMovie)
        {
            Console.WriteLine($"CreateMovie: {newMovie.MovieId}");
            try
            {
                using (var graphClient = CreateClient())
                {
                    graphClient.Connect();
                
                    graphClient.Cypher
                        .Create("(user:Movie {newMovie})")
                        .WithParam("newMovie", newMovie)
                        .ExecuteWithoutResults();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Movie GetMovie(long movieId)
        {
            Movie result = null;
            
            try
            {
                using (var graphClient = CreateClient())
                {
                    graphClient.Connect();

                    result = graphClient.Cypher
                        .Match($"(movie:Movie {{ movieId:  {movieId}  }})")
                        .Return(movie => movie.As<Movie>())
                        .Results
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
        
        public List<Movie> GetAllMovies()
        {
            List<Movie> result = null;

            try
            {
                using (var graphClient = CreateClient())
                {
                    graphClient.Connect();

                    result = graphClient.Cypher
                        .Match("(movie:Movie)")
                        .Return(movie => movie.As<Movie>())
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

        public void DeleteMovie(long movieId)
        {
            using (var graphClient = CreateClient())
            {
                graphClient.Connect();

                graphClient.Cypher
                    .Match($"(movie:Movie {{ movieId: {movieId} }})")
                    .OptionalMatch("()-[r]->(movie)")
                    .Delete("r, movie")
                    .ExecuteWithoutResults();
            }
        }

        public void DeleteAllMovies()
        {
            try
            {
                using (var graphClient = CreateClient())
                {
                    graphClient.Connect();

                    graphClient.Cypher
                        .Match("(movie:Movie)")
                        .OptionalMatch("()-[r]->(movie)-[g]->()")
                        .Delete("g, r, movie")
                        .ExecuteWithoutResults();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public dynamic DeleteAllMoviesResult()
        {
            dynamic result = null;
            try
            {
                using (var graphClient = CreateClient())
                {
                    graphClient.Connect();

                    result = graphClient.Cypher
                        .Match("(movie:Movie)")
                        .OptionalMatch("()-[r]->(movie)-[g]->()")
                        .Delete("g, r, movie")
                        .Return(movie => movie.As<dynamic>());
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return result;
        }


        /// <summary>
        /// Genres
        /// </summary>
        /// 
        public List<Genre> GetGenres()
        {
            List<Genre> result = null;
            try
            {
                using (var graphClient = CreateClient())
                {
                    graphClient.Connect();

                    result = graphClient.Cypher
                        .Match("(genre:Genre)")
                        .Return(genre => genre.As<Genre>())
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


        public List<Movie> GetMoviesThatHaveGenre(string genreString)
        {
            List<Movie> result = null;
            try
            {
                using (var graphClient = CreateClient())
                {
                    graphClient.Connect();

                    result = graphClient.Cypher
                        .Match($"(genre:Genre {{ genreName: {genreString} }})<-(movie:Movie)")
                        .Return(movie => movie.As<Movie>())
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


        

        public void SetMovieGenre(long movieId, List<Genre> genreList)
        {
            try
            {
                using (var graphClient = CreateClient())
                {
                    graphClient.Connect();
                    
                    foreach (Genre currentGenre in genreList)
                    {
                        var test =
                        graphClient.Cypher
                            .Merge($"(genre:Genre {{ genreName: '{currentGenre.GenreName}' }})")
                            .With("genre")
                            .Match($"(movie:Movie {{ movieId: { movieId } }} )")
                            .CreateUnique($"(movie)-[:HAS_GENRE]->(genre)");

                        graphClient.Cypher
                            .Merge($"(genre:Genre {{ genreName: '{currentGenre.GenreName}' }})")
                            .With("genre")
                            .Match($"(movie:Movie {{ movieId: { movieId } }} )")
                            .CreateUnique($"(movie)-[:HAS_GENRE]->(genre)")
                            .ExecuteWithoutResults();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public Movie GetMovieWithGenres(long movieId)
        {
            Movie result = null;

            try
            {
                using (var graphClient = CreateClient())
                {
                    graphClient.Connect();

                    result = graphClient.Cypher
                        .Match($"(movie:Movie  {{ movieId: { movieId } }} )")
                        .Return(movie => movie.As<Movie>())
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

        public List<Movie> GetAllMoviesWithGenres()
        {
            List<Movie> result = null;

            try
            {
                using (var graphClient = CreateClient())
                {
                    graphClient.Connect();

                    result = graphClient.Cypher
                        .Match("(movie:Movie)")
                        .Return(movie => movie.As<Movie>())
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


        public void GenreMovie(string name)
        {
            using (var graphClient = CreateClient())
            {
                graphClient.Connect();

                graphClient.Cypher
                    .Match($"(genre:Genre {{ genreName: {name} }})")
                    .OptionalMatch("()-[r]->(genre)")
                    .Delete("r, genre")
                    .ExecuteWithoutResults();
            }
        }

        public void DeleteAllGenres()
        {
            try
            {
                using (var graphClient = CreateClient())
                {
                    graphClient.Connect();

                    graphClient.Cypher
                        .Match("(genre:Genre)")
                        .OptionalMatch("()-[r]->(genre)")
                        .Delete("r, genre")
                        .ExecuteWithoutResults();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
