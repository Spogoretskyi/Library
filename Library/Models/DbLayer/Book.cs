namespace Library.Models.DbLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Book")]
    public partial class Book
    {
        [Key]
        public int Book_Id { get; set; }

        [Required(ErrorMessage = "Author is a required field")]
        [StringLength(50)]
        public string Author { get; set; }

        [Required(ErrorMessage = "Book is a required field")]
        [StringLength(100)]
        public string Book_Name { get; set; }

        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Digits only" )]
        [StringLength(13, MinimumLength = 10, ErrorMessage = "The ISBN field must be a string with a minimum length of 10 and a maximum length of 13")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "Year is a required field")]
        [RegularExpression(@"^(19|20)\d{2}$")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Punlisher is a required field")]
        [StringLength(100)]
        public string Publisher { get; set; }

        [Required]
        [StringLength(100)]
        public string Country { get; set; }
    }
}
