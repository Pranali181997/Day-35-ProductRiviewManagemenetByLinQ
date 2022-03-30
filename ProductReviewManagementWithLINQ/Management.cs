using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ProductReviewManagementWithLINQ
{
    public class Management
    {
        //UC-2
        public readonly DataTable dataTable = new DataTable();
        public void TopRecords(List<ProductReview> listReview)
        {
            var recordedData = (from productReviews in listReview
                                orderby productReviews.Rating descending
                                select productReviews).Take(3);

            Console.WriteLine("\nTop 3 high rated products are:");

            foreach (var v in recordedData)
            {
                Console.WriteLine($"ProductID:{v.ProductID} \t UserID:{v.UserID} \t Rating:{v.Rating}\t Review:{v.Review} \t IsLike:{v.isLike}");
            }
        }
            //UC-3
            public void SelectedRecords(List<ProductReview> listReview)
            {
                var recordedData = (from productReviews in listReview
                                    where (productReviews.ProductID == 1 || productReviews.ProductID == 4 || productReviews.ProductID == 9) && productReviews.Rating > 3
                                    select productReviews);

                Console.WriteLine("\nProducts with rating greater than 3 and id=1 or 4 or 9 are:");

                foreach (var v in recordedData)
                {
                    Console.WriteLine($"ProductID:{v.ProductID}\tUserID:{v.UserID}\tRating:{v.Rating}\tReview:{v.Review}\tIsLike:{v.isLike}");
                }
            }
        //UC-4
        public void RetrieveCountOfRecords(List<ProductReview> listReview)
        {
            var recordedData = listReview.GroupBy(x => x.ProductID).Select(x => new { ProductID = x.Key, Count = x.Count() });

            Console.WriteLine("ProductId and their review count:");

            foreach (var v in recordedData)
            {
                Console.WriteLine($"ProductID:{v.ProductID},ReviewCount:{v.Count}");
            }
        }
        //UC-5
        public void RetrieveOnlyProductId(List<ProductReview> listReview)
        {
            var recordedData = from ProductReview in listReview select (ProductReview.ProductID, ProductReview.Review);

            foreach (var v in recordedData)
            {
                Console.WriteLine("Product Id : " + v.ProductID + " " + "Review are : " + v.Review);
            }
        }
        //UC-6
        public void SkipTopFiveRecords(List<ProductReview> listReview)
        {
            var recordedData = (from productReviews in listReview
                                select productReviews).Skip(5);
            foreach (var list in recordedData)
            {
                Console.WriteLine("ProductID: " + list.ProductID + " " + "UserId: " + list.UserID + " " + "Rating: " + list.Rating + " "
             + "Review: " + list.Review + " " + "IsLike: " + list.isLike);

            }
        }
    }
}