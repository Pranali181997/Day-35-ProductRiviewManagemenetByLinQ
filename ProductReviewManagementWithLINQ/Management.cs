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
                Console.WriteLine($"ProductID:{v.ProductID}\tUserID:{v.UserID}\tRating:{v.Rating}\tReview:{v.Review}\tIsLike:{v.isLike}");
            }
        }
    }
}