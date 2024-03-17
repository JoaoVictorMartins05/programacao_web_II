﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Projeto_WEB_2.Models;

[Table("tbPaciente")]
[Index("IdCidade", Name = "IX_tbPaciente_IdCidade")]
public partial class TbPaciente
{
    [Key]
    public int IdPaciente { get; set; }

    [Required]
    [StringLength(100)]
    [Unicode(false)]
    public string Nome { get; set; }

    [Required]
    [Column("RG")]
    [StringLength(15)]
    [Unicode(false)]
    public string Rg { get; set; }

    [Required]
    [Column("CPF")]
    [StringLength(15)]
    [Unicode(false)]
    public string Cpf { get; set; }

    public DateOnly DataNascimento { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string NomeResponsavel { get; set; }

    [Required]
    [StringLength(1)]
    [Unicode(false)]
    public string Sexo { get; set; }

    public int Etnia { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Endereco { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Bairro { get; set; }

    public int? IdCidade { get; set; }

    [StringLength(25)]
    [Unicode(false)]
    public string TelResidencial { get; set; }

    [StringLength(25)]
    [Unicode(false)]
    public string TelComercial { get; set; }

    [StringLength(25)]
    [Unicode(false)]
    public string TelCelular { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string Profissao { get; set; }

    public bool? FlgAtleta { get; set; }

    public bool? FlgGestante { get; set; }

    [ForeignKey("IdCidade")]
    [InverseProperty("TbPaciente")]
    public virtual TbCidade IdCidadeNavigation { get; set; }

    [InverseProperty("IdPacienteNavigation")]
    public virtual ICollection<TbAntropometria> TbAntropometria { get; set; } = new List<TbAntropometria>();

    [InverseProperty("IdPacienteNavigation")]
    public virtual ICollection<TbEscalaBristolPacienteConsulta> TbEscalaBristolPacienteConsulta { get; set; } = new List<TbEscalaBristolPacienteConsulta>();

    [InverseProperty("IdPacienteNavigation")]
    public virtual ICollection<TbExameXPacientes> TbExameXPacientes { get; set; } = new List<TbExameXPacientes>();

    [InverseProperty("IdPacienteNavigation")]
    public virtual ICollection<TbHistoriaPatologica> TbHistoriaPatologica { get; set; } = new List<TbHistoriaPatologica>();

    [InverseProperty("IdPacienteNavigation")]
    public virtual ICollection<TbHistoricoAlimentarNutricional> TbHistoricoAlimentarNutricional { get; set; } = new List<TbHistoricoAlimentarNutricional>();

    [InverseProperty("IdPacienteNavigation")]
    public virtual ICollection<TbHistoricoDoencaAtual> TbHistoricoDoencaAtual { get; set; } = new List<TbHistoricoDoencaAtual>();

    [InverseProperty("IdPacienteNavigation")]
    public virtual ICollection<TbHistoricoSocialAlimentar> TbHistoricoSocialAlimentar { get; set; } = new List<TbHistoricoSocialAlimentar>();

    [InverseProperty("IdPacienteNavigation")]
    public virtual ICollection<TbHoraPacienteProfissional> TbHoraPacienteProfissional { get; set; } = new List<TbHoraPacienteProfissional>();

    [InverseProperty("IdPacienteNavigation")]
    public virtual ICollection<TbMedicoPaciente> TbMedicoPaciente { get; set; } = new List<TbMedicoPaciente>();
}