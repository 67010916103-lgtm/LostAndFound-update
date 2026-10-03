using System.ComponentModel.DataAnnotations;

namespace LostAndFound.Models
{
    public enum ItemType
    {
        [Display(Name = "ของหาย")] Lost,
        [Display(Name = "พบของ")] Found
    }

    public enum ItemStatus
    {
        [Display(Name = "รอดำเนินการ")] Pending,
        [Display(Name = "ตรวจสอบแล้ว")] Verified,
        [Display(Name = "มีผู้รับแล้ว")] Claimed,
        [Display(Name = "ส่งคืนแล้ว")] Returned
    }

    public enum ReporterType
    {
        [Display(Name = "นิสิต / นักศึกษา")] Student,
        [Display(Name = "อาจารย์ / บุคลากร")] Staff,
        [Display(Name = "บุคคลภายนอก / พ่อค้าแม่ค้า")] Guest
    }

    public class Item
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "กรุณากรอกชื่อสิ่งของ")]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "กรุณาระบุรายละเอียดเพิ่มเติม")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "กรุณาระบุสถานที่พบหรือทำหาย")]
        public string Location { get; set; } = string.Empty;

        [Required]
        public ItemType Type { get; set; } = ItemType.Found;

        public ItemStatus Status { get; set; } = ItemStatus.Pending;

        public string? ImageUrl { get; set; }

        public DateTime EventDate { get; set; } = DateTime.Now;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "กรุณาเลือกหมวดหมู่")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        [Required(ErrorMessage = "กรุณาเลือกสถานะผู้แจ้ง")]
        public ReporterType ReporterType { get; set; } = ReporterType.Student;

        public string? StudentId { get; set; }

        [Required(ErrorMessage = "กรุณากรอกเบอร์โทรศัพท์หรือช่องทางติดต่อ")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}