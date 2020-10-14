using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLibrary.Models
{
    public class OrderModel
    {
        /*
         * [dbo].[Order](
	        [Id] [int] IDENTITY(1,1) NOT NULL,
	        [OrderName] [nvarchar](50) NOT NULL,
	        [OrderDate] [datetime2](7) NOT NULL,
	        [FoodId] [int] NOT NULL,
	        [Quantity] [int] NOT NULL,
	        [Total] [money] NOT NULL,
         */

        public int Id { get; set; }
        [Required]
        [MinLength(3,ErrorMessage ="Unesi barem tri znaka")]
        [MaxLength(20,ErrorMessage ="Max broj znakova je 20")]
        [DisplayName("Name for narudzba")]
        public string OrderName { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        [DisplayName("Klopa")]
        [Range(1, int.MaxValue, ErrorMessage ="Odaberi hranu")]
        public int FoodId { get; set; }

        public string FoodTitle { get; set; }
        [Required]
        [Range(1,10,ErrorMessage ="Mozes odabrati do 10 jela")]
        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }
}
