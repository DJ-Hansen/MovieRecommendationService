using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieRecommendationLibrary.Neo4jDatabaseHandler;
using MovieRecommendationLibrary.Model;
using RecommendationRESTService.Models;

namespace MovieRecommendationService.Controllers
{
    [Route("api/movie")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        // GET: api/Movie
        [HttpGet]
        public ActionResult<IEnumerable<Movie>> Get()
        {
            try
            {
                Neo4jDatabaseHandler handler = Neo4jHandler.GetHandler();
                return handler.GetAllMovies();
            }
            catch (Exception)
            {
            }
            return null;
        }

        // GET: api/Movie/5
        [HttpGet("{id}")]
        public ActionResult<Movie> Get(long id)
        {
            Movie result = null;
            try
            {
                Neo4jDatabaseHandler handler = Neo4jHandler.GetHandler();
                result = handler.GetMovie(id);
            }
            catch (Exception)
            {
            }

            return result;
        }

        // POST: api/Movie
        [HttpPost]
        public void Post([FromBody] Movie value)
        {
            try
            {
                Neo4jDatabaseHandler handler = Neo4jHandler.GetHandler();
                handler.CreateMovie(value);
            }
            catch (Exception)
            {
            }
        }

        //// PUT: api/Movie/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            try
            {
                Neo4jDatabaseHandler handler = Neo4jHandler.GetHandler();
                handler.DeleteMovie(id);
            }
            catch (Exception)
            {
            }
        }
    }
}
