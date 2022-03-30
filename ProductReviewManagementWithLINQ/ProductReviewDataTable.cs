using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ProductReviewManagementWithLINQ
{
    public class ProductReviewDataTable
    {
        public static DataTable productDataTable = new DataTable();
        public static void AddDataIntoDataTable()
        {
            //UC-8
            //Adding Fields into the datatable
            productDataTable.Columns.Add("ProductId", typeof(Int32));
            productDataTable.Columns.Add("UserId", typeof(Int32));
            productDataTable.Columns.Add("Rating", typeof(double));
            productDataTable.Columns.Add("Review", typeof(string));
            productDataTable.Columns.Add("IsLike", typeof(bool));

            //Creating rows and Adding data into rows
            productDataTable.Rows.Add(1, 1, 5, "Excellent", true);
            productDataTable.Rows.Add(5, 10, 1, "Poor     ", false);
            productDataTable.Rows.Add(3, 3, 3, "Average  ", true);
            productDataTable.Rows.Add(3, 6, 5, "Excellent", true);
            productDataTable.Rows.Add(2, 2, 4, "Nice     ", true);
            productDataTable.Rows.Add(4, 7, 4, "Nice     ", true);
            productDataTable.Rows.Add(2, 8, 3, "Average  ", true);
            productDataTable.Rows.Add(4, 4, 2, "Satisfactory", false);
            productDataTable.Rows.Add(1, 9, 2, "Satisfactory", false);
            productDataTable.Rows.Add(9, 5, 1, "Poor     ", false);

            Console.WriteLine("\nDataTable contents:");
            foreach (var v in productDataTable.AsEnumerable())
            {
                Console.WriteLine($"ProductID:{v.Field<int>("ProductId")}\tUserID:{v.Field<int>("UserId")}\tRating:{v.Field<double>("Rating")}\tReview:{v.Field<string>("Review")}\tIsLike:{v.Field<bool>("IsLike")}");
            }
        }
        //UC-9
        public static void RetrieveAllRecordsWhoseIsLikeIsTrue()
        {
            var retrievedData = from records in productDataTable.AsEnumerable()
                                where (records.Field<bool>("IsLike") == true)
                                select records;
            Console.WriteLine("\nRecords in table whose IsLike value is true:");
            foreach (var v in retrievedData)
            {
                Console.WriteLine($"ProductID:{v.Field<int>("ProductId")}\tUserID:{v.Field<int>("UserId")}\tRating:{v.Field<double>("Rating")}\tReview:{v.Field<string>("Review")}\tIsLike:{v.Field<bool>("IsLike")}");
            }
        }
        //UC-10
        public static void GetAverageOfEachProductId()
        {
            var avgTable = productDataTable.AsEnumerable().GroupBy(r => r.Field<int>("ProductId")).Select(x => new { ProductId = x.Key, Average = x.Average(r => r.Field<double>("Rating")) });
            Console.WriteLine("\n ProductID and Average Rating are");
            foreach (var v in avgTable)
            {
                Console.WriteLine($"ProductId :{v.ProductId} ,AverageRating: {v.Average}");
            }
        }
        //UC-11
        public static void RetrieveWhosMesgNice(List<ProductReview> productReviewList)
        {
            //var MesgNice = from Review in productDataTable.AsEnumerable()
            //where Review.Field<string>("Review").Equals("Nice") select Review;  
            //Console.WriteLine("\n ProductID and Review are");

            //    Console.WriteLine($"ProductID:{v.Field<int>("ProductId")}\tUserID:{v.Field<int>("UserId")}\tRating:{v.Field<double>("Rating")}\tReview:{v.Field<string>("Review")}\tIsLike:{v.Field<bool>("IsLike")}");

            //}
            foreach (var list in (from Review in productDataTable.AsEnumerable()
                               where Review.Field<string>("Review").Equals("Nice")
                             select Review))
            {
                Console.WriteLine($"ProductID:{list.Field<int>("ProductId")}\tUserID:{list.Field<int>("UserId")}\tRating:{list.Field<double>("Rating")}\tReview:{list.Field<string>("Review")}\tIsLike:{list.Field<bool>("IsLike")}");
            }
        }
        //UC-12
        public static void AddAndGetUserIdOnly10()
        {
            IList<ProductReview> productReviews = new List<ProductReview>();
            List<string> reviewList = new List<string> { "good", "bad", "nice" };
            Random random = new Random();
           for(int i = 1; i < 25; i++)
            {
                ProductReview reviews = new ProductReview();
                reviews.ProductID = random.Next(1, 10);
                reviews.UserID = 10;
                reviews.Rating = random.Next(1, 6);
                reviews.Review = reviewList[random.Next(reviewList.Count)];
                reviews.isLike = Convert.ToBoolean(random.Next(2));
                productReviews.Add(reviews);
            }
           foreach(var reviews in productReviews)
            {
                productDataTable.Rows.Add(reviews.ProductID,reviews.UserID,reviews.Rating,reviews.Review,reviews.isLike);
            }
            var reviewTable = (from reviews in productDataTable.AsEnumerable()
                               where reviews.Field<int>("UserId").Equals(10)
                               select reviews).OrderBy(x => x.Field< double > ("Rating"));
            Console.WriteLine($"\nProductId \t UserId \t Rating \t Review \t isLike");
            foreach(var review in reviewTable)
            {
                Console.WriteLine($"ProductID:{review.Field<int>("ProductId")}\tUserID:{review.Field<int>("UserId")}\tRating:{review.Field<double>("Rating")}\tReview:{review.Field<string>("Review")}\tIsLike:{review.Field<bool>("IsLike")}");
            }
        }
    }
}