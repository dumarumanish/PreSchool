namespace PreSchool.Application.Services.Payments.Models.Dtos
{
    public class PaymentModeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public string Image { get; set; }

        public bool IsActive { get; set; }
    }
}
