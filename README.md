# MovieRecommendationService
This service is created to give movie recommendations based on a users previous movie reviews.

The dataset which is used is the ["MovieLens 100K Dataset"](https://grouplens.org/datasets/movielens/100k/)

The data is stored in a Neo4j database 

The recommnedations are created with Cypher (can be seen in Recommendation_Handler.cs)
<br>It uses a version of Jaccard, inorder to give relevant recommendations.



