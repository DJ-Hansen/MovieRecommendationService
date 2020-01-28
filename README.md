# MovieRecommendationService
This service is created to give movie recommendations based on a users previous movie reviews.

The dataset which is used is the ["MovieLens 100K Dataset"](https://grouplens.org/datasets/movielens/100k/)

The data is stored in a Neo4j database 

The recommnedations are created with Cypher (can be seen in [Recommendation_Handler.cs](https://github.com/DJ-Hansen/MovieRecommendationService/blob/master/MovieRecommendationService/Neo4jDatabaseHandler/Handler/Recommendation_Handler.cs))
<br>It uses a version of [Jaccard index](https://deepai.org/machine-learning-glossary-and-terms/jaccard-index), inorder to give good recommendations.



