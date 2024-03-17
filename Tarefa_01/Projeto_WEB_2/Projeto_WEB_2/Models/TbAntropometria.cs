﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Projeto_WEB_2.Models;

[Table("tbAntropometria")]
[Index("IdHoraPacienteProfissional", Name = "IX_tbAntropometria_IdHoraPaciente_Profissional")]
[Index("IdPaciente", Name = "IX_tbAntropometria_IdPaciente")]
public partial class TbAntropometria
{
    [Key]
    public int IdAntropometria { get; set; }

    [Column("IdHoraPaciente_Profissional")]
    public int IdHoraPacienteProfissional { get; set; }

    public int IdPaciente { get; set; }

    public int? Estatura { get; set; }

    public int? AlturaJoelho { get; set; }

    public int? PesoAtual { get; set; }

    public int? PesoUsual { get; set; }

    public int? TipoProtocolo { get; set; }

    public int? Tricipal { get; set; }

    public int? Subescap { get; set; }

    public int? AuxiliarMed { get; set; }

    public int? SupraIliaca { get; set; }

    public int? Abdomen { get; set; }

    public int? Peitoral { get; set; }

    public int? Coxa { get; set; }

    public int? Biceps { get; set; }

    public int? Panturrilha { get; set; }

    public int? PercGordura { get; set; }

    public int? CircunfBraco { get; set; }

    public int? CircunfAbdomen { get; set; }

    public int? CircunfCintura { get; set; }

    public int? CircunfQuadril { get; set; }

    [ForeignKey("IdHoraPacienteProfissional")]
    [InverseProperty("TbAntropometria")]
    public virtual TbHoraPacienteProfissional IdHoraPacienteProfissionalNavigation { get; set; }

    [ForeignKey("IdPaciente")]
    [InverseProperty("TbAntropometria")]
    public virtual TbPaciente IdPacienteNavigation { get; set; }
}