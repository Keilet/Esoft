namespace Esoftr.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Task")]
    public partial class Task
    {
        public int ID { get; set; }

        public int ExecutorID { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        public DateTime CreateDateTime { get; set; }

        [Column(TypeName = "date")]
        public DateTime Deadline { get; set; }

        public double Difficulty { get; set; }

        public int Time { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; }

        [Required]
        [StringLength(45)]
        public string WorkType { get; set; }

        public DateTime? CompletedDateTime { get; set; }

        public bool IsDeleted { get; set; }

        public virtual Executor Executor { get; set; }
    }
}
