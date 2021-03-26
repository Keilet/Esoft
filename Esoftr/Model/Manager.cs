namespace Esoftr.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Manager")]
    public partial class Manager
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Manager()
        {
            Executor = new HashSet<Executor>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int JuniorMinimum { get; set; }

        public int MiddleMinimum { get; set; }

        public int SeniorMinimum { get; set; }

        public double AnalysisCoefficient { get; set; }

        public double InstallationCoefficient { get; set; }

        public double SupportCoefficient { get; set; }

        public double TimeCoefficient { get; set; }

        public double DifficultyCoefficient { get; set; }

        public double ToMoneyCoefficient { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Executor> Executor { get; set; }

        public virtual User User { get; set; }
    }
}
