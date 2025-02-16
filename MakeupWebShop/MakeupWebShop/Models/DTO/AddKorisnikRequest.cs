﻿using MakeupWebShop.Db;
using System.ComponentModel.DataAnnotations;

namespace MakeupWebShop.Models.DTO
{
    public class AddKorisnikRequest
    {
       // public int KorisnikId { get; set; }
        [MaxLength(50, ErrorMessage = "The Ime cant be longer than 50 characters")]
        public string Ime { get; set; } = null!;
        [MaxLength(50, ErrorMessage = "The Prezime cant be longer than 50 characters")]
        public string Prezime { get; set; } = null!;
        [MaxLength(50, ErrorMessage = "The Email cant be longer than 50 characters")]
        public string Email { get; set; } = null!;
        [MaxLength(20, ErrorMessage = "The Kontakt cant be longer than 220 characters")]
        public string Kontakt { get; set; } = null!;
        [MaxLength(50, ErrorMessage = "The Username cant be longer than 50 characters")]
        public string? Username { get; set; }

        public string? Lozinka { get; set; }

        public TblAdresa Adresa { get; set; }
    }
}
