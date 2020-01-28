using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieRecommendationLibrary.Model;
using MovieRecommendationLibrary.Neo4jDatabaseHandler;
using RecommendationRESTService.Models;

namespace MovieRecommendationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationController : ControllerBase
    {
        // GET: api/Recommendation/5
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<MovieRecommendation>> Get(long id)
        {
            try
            {
                Neo4jDatabaseHandler handler = Neo4jHandler.GetHandler();
                return handler.GetRecommendationOnMovie(id);
            }
            catch (Exception)
            {
            }
            return null;
        }

    }
}
