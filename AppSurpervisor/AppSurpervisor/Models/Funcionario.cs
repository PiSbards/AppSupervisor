using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace AppSurpervisor.Models
{
    [Table("Funcionarios")]
    public class Funcionario
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public String Nome { get; set; }
        [NotNull]
        public String Setor { get; set; }
        [NotNull]
        public String Turno { get; set; }

        public Funcionario()
        {
            this.Id = 0;
            this.Nome = "";
            this.Setor = "";
            this.Turno = "";
        }
    }
}
