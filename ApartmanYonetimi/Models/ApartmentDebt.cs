namespace ApartmanYonetimi.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    using System;

    public partial class ApartmentDebt
    {
        public int Id { get; set; }
        public string ApartmentNo { get; set; }
        public int ApartmentId { get; set; }
        public string Description { get; set; }
        public decimal Money { get; set; }
        public DateTime Date { get; set; }
        public bool IsRender { get; set; }

    }
}